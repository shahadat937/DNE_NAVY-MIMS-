function checkvalue() {
    var name = $(".callsign").val(); //Value entered in the text box
    var status = $("#divStatus");
    var user = $.trim(name);
    $.post("/ShipInfo/CallSignCheck", { sign: name },
                function (result) {
                    if (result == 0) {
                        $(".callsign").css('background-color', '#ff8693');
                        status.html("<font color=red>" + name + " Exists in System  </font>");

                    } else {
                        $(".callsign").css('background-color', '#FFFFFF');
                        status.html("");
                    }
                });
}





