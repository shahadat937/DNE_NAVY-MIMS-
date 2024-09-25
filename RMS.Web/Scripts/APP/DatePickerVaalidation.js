//$(function () {
//    $("#Text1").datepicker({
//        numberOfMonths: 1,
//        onSelect: function (selected) {
//            var dt = new Date(selected);
//            dt.setDate(dt.getDate() + 1);
//            $("#Text2").datepicker("option", "minDate", dt);
//        }
//    });
//    $("#Text2").datepicker({
//        numberOfMonths: 1,
//        onSelect: function (selected) {
//            var dt = new Date(selected);
//            dt.setDate(dt.getDate() - 1);
//            $("#Text1").datepicker("option", "maxDate", dt);
//        }
//    });
//});



$(function () {
    // -----------1st element -------------
    
    var year = new Date();
    $(".datefrom").datepicker({
        dateFormat: 'dd MM yy',
        yearRange: '1920:' + (year.getFullYear() + 50),
        changeMonth: true,
        changeYear: true,
        onSelect: function (selected) {
            var dt = new Date(selected);
            dt.setDate(dt.getDate() + 1);
            $(".dateto").datepicker("option", "minDate", dt);

        }
    });
    var years = new Date();
    $(".dateto").datepicker({
        dateFormat: 'dd MM yy',
        yearRange: '1920:' + (years.getFullYear() + 50),
        changeMonth: true,
        changeYear: true,
        onSelect: function (selected) {
            var dt = new Date(selected);
            dt.setDate(dt.getDate() - 1);
            $(".datefrom").datepicker("option", "maxDate", dt);
        }
    });
    
    // -----------2nd element -------------
    $(".datefrom1").datepicker({
        dateFormat: 'dd MM yy',
        yearRange: '1920:' + (year.getFullYear() + 50),
        changeMonth: true,
        changeYear: true,
        onSelect: function (selected) {
            var dt = new Date(selected);
            dt.setDate(dt.getDate() + 1);
            $(".dateto1").datepicker("option", "minDate", dt);

        }
    });

    $(".dateto1").datepicker({
        dateFormat: 'dd MM yy',
        yearRange: '1920:' + (year.getFullYear() + 50),
        changeMonth: true,
        changeYear: true,
        onSelect: function (selected) {
            var dt = new Date(selected);
            dt.setDate(dt.getDate() - 1);
            $(".datefrom1").datepicker("option", "maxDate", dt);
        }
    });
    // -----------3rd element -------------
    $(".datefrom2").datepicker({
        dateFormat: 'dd MM yy',
        yearRange: '1920:' + (year.getFullYear() + 50),
        changeMonth: true,
        changeYear: true,
        onSelect: function (selected) {
            var dt = new Date(selected);
            dt.setDate(dt.getDate() + 1);
            $(".dateto2").datepicker("option", "minDate", dt);

        }
    });

    $(".dateto2").datepicker({
        dateFormat: 'dd MM yy',
        yearRange: '1920:' + (year.getFullYear() + 50),
        changeMonth: true,
        changeYear: true,
        onSelect: function (selected) {
            var dt = new Date(selected);
            dt.setDate(dt.getDate() - 1);
            $(".datefrom2").datepicker("option", "maxDate", dt);
        }
    });
    // -----------4th element -------------
    $(".datefrom3").datepicker({
        dateFormat: 'dd MM yy',
        yearRange: '1920:' + (year.getFullYear() + 50),
        changeMonth: true,
        changeYear: true,
        onSelect: function (selected) {
            var dt = new Date(selected);
            dt.setDate(dt.getDate() + 1);
            $(".dateto3").datepicker("option", "minDate", dt);

        }
    });

    $(".dateto3").datepicker({
        dateFormat: 'dd MM yy',
        yearRange: '1920:' + (year.getFullYear() + 50),
        changeMonth: true,
        changeYear: true,
        onSelect: function (selected) {
            var dt = new Date(selected);
            dt.setDate(dt.getDate() - 1);
            $(".datefrom3").datepicker("option", "maxDate", dt);
        }
    });
    // -----------5th element -------------
    $(".datefrom4").datepicker({
        dateFormat: 'dd MM yy',
        yearRange: '1920:' + (year.getFullYear() + 50),
        changeMonth: true,
        changeYear: true,
        onSelect: function (selected) {
            var dt = new Date(selected);
            dt.setDate(dt.getDate() + 1);
            $(".dateto4").datepicker("option", "minDate", dt);

        }
    });

    $(".dateto4").datepicker({
        dateFormat: 'dd MM yy',
        yearRange: '1920:' + (year.getFullYear() + 50),
        changeMonth: true,
        changeYear: true,
        onSelect: function (selected) {
            var dt = new Date(selected);
            dt.setDate(dt.getDate() - 1);
            $(".datefrom4").datepicker("option", "maxDate", dt);
        }
    });
});

