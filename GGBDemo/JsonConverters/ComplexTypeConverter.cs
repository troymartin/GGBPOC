using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebGrease.Css.Extensions;

namespace GGBDemo.JsonConverters
{
    /// <summary>
    /// Complext type converter
    /// This class will convert attributes to json properties
    /// </summary>
    public class ComplexTypeConverter : JsonConverter
    {
       
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var rootObject = Activator.CreateInstance(objectType);
            var objJson = JToken.ReadFrom(reader);

            foreach (var token in objJson)
            {
                var propInfo = rootObject.GetType().GetProperty(token.Path, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (!propInfo.CanWrite) continue;
                var tk = token as JProperty;
                if (tk?.Value is JObject)
                {
                    var val = tk.Value.SelectToken("value") as JValue;
                    if (val != null)
                        propInfo.SetValue(rootObject, Convert.ChangeType(val.Value, propInfo.PropertyType.UnderlyingSystemType), null);
                }
                else
                {
                    if (tk != null)
                        propInfo.SetValue(rootObject, Convert.ChangeType(tk.Value, propInfo.PropertyType.UnderlyingSystemType), null);
                }
            }
            return rootObject;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var jo = new JObject();
            var type = value.GetType();
            foreach (var propInfo in type.GetProperties())
            {
                if (!propInfo.CanRead) continue;
                var propVal = propInfo.GetValue(value, null);

                jo.Add(propInfo.Name, JToken.FromObject(propVal ?? string.Empty, serializer));
                var cutomAttributes = propInfo.GetCustomAttributes();
                cutomAttributes.ForEach(customAttribute =>
                {
                    if (customAttribute == null) return;
                    if (customAttribute is RequiredAttribute)
                    {
                        jo.Add(propInfo.Name, JToken.FromObject(new {required = true}, serializer));
                    }
                    else if (customAttribute is DataTypeAttribute)
                    {
                        jo.Add(propInfo.Name,
                            JToken.FromObject(
                                new {dataType = ((DataTypeAttribute) customAttribute).DataType.ToString()},
                                serializer));
                    }
                    else if (customAttribute is DisplayAttribute)
                    {
                        jo.Add(propInfo.Name,
                            JToken.FromObject(new {name = ((DisplayAttribute) customAttribute).Name}, serializer));
                    }
                });
            }
            jo.WriteTo(writer);
        }
    }
}