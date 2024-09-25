

var TimeOut;
function StartTime() {
    //TimeOut = window.setTimeout('window.location = "My_Page_To_Refresh.aspx";', 598000);
    TimeOut = window.setTimeout('window.location = "/Account/LogIn";', 3598000);
}

function StopAndStartTime() {
    window.clearTimeOut(TimeOut);
    StartTime();
}

window.onload = StartTime();

