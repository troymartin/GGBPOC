/// <reference path="knockout-3.4.2.debug.js" />
/// <reference path="~/Scripts/knockout/FormViewModel.js" />
/// <reference path="~/Scripts/knockout.mapping.js" />
/// <reference path="~/Scripts/jquery-1.10.2.intellisense.js" />
var viewModels;
$(function() {
    viewModels = {};
    if (typeof FormViewModel != 'undefined')
        viewModels.FormViewModel = new FormViewModel();

    
    ko.applyBindings(viewModels);

    $('#load').on("click", function() {
        $(".jsonForm").each(function () {
            var model = $(this).attr("data-model");
            $.ajax({
                url: "Forms/GetModel?modelName=" + model,
                type: "GET",
                success: function (data) {
                    console.log("got the model");
                    viewModels.FormViewModel.ActionName(data.ActionName);
                    viewModels.FormViewModel.Method(data.Method);
                    viewModels.FormViewModel.FormElements(data.FormElements);
                    viewModels.FormViewModel.AccessToken(data.AccessToken);
                },
                error: function (data) {

                }
            });
        });
        $(".jsonForm").submit(function (e) {
            $.ajax({
                url: "Forms/PostModel",
                data:$(this).serialize(),
                type: "POST",
                success: function (data) {
                    alert('form submitted!');
                },
                error: function (data) {

                }
            });
            e.stopPropagation();
            return false;
        });
    });
});



