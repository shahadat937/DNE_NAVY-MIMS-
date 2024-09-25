
$(function () {
    var year = new Date();
    $(".datepicker").datepicker({
        dateFormat: 'dd MM yy',
        yearRange: '1920:' + (year.getFullYear() + 50),
        changeMonth: true,
        changeYear: true,
    });
});


   