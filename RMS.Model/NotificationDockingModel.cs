namespace RMS.Model
{
    public class NotificationDockingModel
    {
        public long ShipId { get; set; }
        public string ShipName { get; set; }
        public System.DateTime NextRefitDateFrom { get; set; }
        public System.DateTime NextDokingDateFrom { get; set; }
        public System.DateTime NextRefitDateTo { get; set; }
        public System.DateTime NextDokingDateTo { get; set; }
        public int NextRefitDayFrom { get; set; }
        public int NextDokingDayFrom { get; set; }
        public int NextRefitDayTo { get; set; }
        public int NextDockingDayTo { get; set; }
        public string Message { get; set; }
    }
}
