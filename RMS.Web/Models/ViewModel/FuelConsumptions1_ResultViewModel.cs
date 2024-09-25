using System;
using System.Collections.Generic;
using System.Web.Mvc;
using RMS.Model;



namespace RMS.Web.Models.ViewModel
{
    public class FuelConsumptions1_ResultViewModel
    {
        public FuelConsumptions1_Result FuelConsumptions1 { get; set; }
        public List<FuelConsumptions1_Result> FuelConsumptions1_Results { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<LubOilConsumption> LubOilConsumptions { get; set; }
        public List<MachineryInfo> MachineryInfos { get; set; }

        public FuelConsumptions1_ResultViewModel()
        {
           FuelConsumptions1= new FuelConsumptions1_Result(); 
            FuelConsumptions1_Results= new List<FuelConsumptions1_Result>();
            ShipInfos= new List<ShipInfo>();
            LubOilConsumptions=new List<LubOilConsumption>();
            MachineryInfos= new List<MachineryInfo>();


        }
        public string SearchKey { get; set; }

        public IEnumerable<SelectListItem> ShipinSelectListItems
        {
            get { return new SelectList(ShipInfos, "ShipInfoIdentity", "ShipName"); }
        }
    }
}