var FormViewModel = function() {
    var self = this;
    self.ActionName = ko.observable("");
    self.Method = ko.observable("");
    self.AccessToken = ko.observable("");
    self.FormElements = ko.observableArray([]);

    //self.mappingDefinitions = {
    //    FormElements: {
    //        key: function(item) {
    //            return ko.utils.unwrapObservable(item.Id);
    //        },
    //        create: function(options) {
    //            var formElement = ko.mapping.fromJS(options.data, self.mappingDefinitions);
    //            return formElement;
    //        }
    //    }
    //}
}