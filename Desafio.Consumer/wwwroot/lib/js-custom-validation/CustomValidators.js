jQuery.validator.addMethod("validatevalues", function (value, element, params) {
    var min = parseFloat(params.min);
    var max = parseFloat(params.max);
    var val = parseFloat(value);
    return !isNaN(val) && (val >= min && val <= max);
}, function (params, element) {
    return "Value must between " + params.min + " and " + params.max + "!"
});
jQuery.validator.unobtrusive.adapters.add("validatevalues", ["min", "max", "message"], function (options) {
    options.rules["validatevalues"] = {
        min: options.params.min,
        max: options.params.max
    };
    options.messages["validatevalues"] = options.params.message;
});