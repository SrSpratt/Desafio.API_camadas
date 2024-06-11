//Valores de compra e venda
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

//Quantidade no estoque
jQuery.validator.addMethod("validatequantity", function (value, element, params) {
    var val = parseInt(value);
    var rule = parseInt(params.rule);
    return !isNaN(val) && (val > rule);
}, function (params, element) {
    return "Value must be higher than " + params.rule + "!";
});

jQuery.validator.unobtrusive.adapters.add("validatequantity", ["rule", "message"], function (options) {
    options.rules["validatequantity"] = {
        rule: options.params.rule
    }
    options.messages["validatequantity"] = options.params.message;
});

//Categoria
jQuery.validator.addMethod("validatecategory", function (value, element, params) {
    var invalidCategory = params.word;
    return value.toLowerCase() !== invalidCategory.toLowerCase();
}, 'Select a valid category!');
jQuery.validator.unobtrusive.adapters.add("validatecategory", ["word", "message"], function (options) {
    options.rules["validatecategory"] = {
        word: options.params.word
    }
    options.messages["validatecategory"] = options.params.message;
});

//Data de validade
jQuery.validator.addMethod("validatedate", function (value, element, params) {
    var invalidDate = new Date(params.date);
    var value = new Date(value);
    return (value > invalidDate);
}, 'Select a valid date!');
jQuery.validator.unobtrusive.adapters.add("validatedate", ["date", "message"], function (options) {
    options.rules["validatedate"] = {
        date: options.params.date
    }
    options.messages["validatedate"] = options.params.message;
});