jQuery.validator.addMethod("sprintnumber", function(value, element) {
    if (value === '') return false;
    var sprintNum = Number(value);
    return (sprintNum > 0);
});

jQuery.validator.unobtrusive.adapters.addBool("sprintnumber");
