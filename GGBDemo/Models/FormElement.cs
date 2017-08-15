
namespace GGBDemo.Models
{
    public class FormElement
    {

        public int Id { get; set; }
        public bool IsCheckBox { get; internal set; }
        public bool IsText { get; set; }
        public string Name { get; set; }
        public bool Required { get; set; }
        public string Title { get; internal set; }
        public string Value { get; internal set; }
    }
}