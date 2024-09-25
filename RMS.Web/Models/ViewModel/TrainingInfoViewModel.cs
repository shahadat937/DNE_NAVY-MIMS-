using RMS.Model;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RMS.Web.Models.ViewModel
{
    public class TrainingInfoViewModel
    {
        public TrainingInfo TrainingInfo { get; set; }
        public List<TrainingInfo> TrainingInfos { get; set; }
        public List<CommonCode> Ranks { get; set; }
        public List<CommonCode> NameOfCourses { get; set; }
        public List<CommonCode> CountryName { get; set; }
        public List<CommonCode> TrainingCategory { get; set; } 
        public TrainingInfoViewModel()
        {
            TrainingInfo = new TrainingInfo();
            TrainingInfos = new List<TrainingInfo>();
            Ranks = new List<CommonCode>();
            NameOfCourses = new List<CommonCode>();
            CountryName =new List<CommonCode>();
            TrainingCategory =new List<CommonCode>();
            
            
        }
        public IEnumerable<SelectListItem> TrainingCategorySelectListItems
        {
            get { return new SelectList(TrainingCategory, "CommonCodeID", "TypeValue"); }

        }
        public IEnumerable<SelectListItem> CountryNameSelectListItems
        {
            get { return new SelectList(CountryName, "CommonCodeID", "TypeValue"); }

        }
        public IEnumerable<SelectListItem> RanksListItems
        {
            get { return new SelectList(Ranks, "CommonCodeID", "TypeValue"); }

        }
        public IEnumerable<SelectListItem> NameOfCoursesListItems
        {
            get { return new SelectList(NameOfCourses, "CommonCodeID", "TypeValue"); }

        }
        public string SearchKey { get; set; }
        public long S { get; set; }
        public long R { get; set; }
        public string CommonCode1 { get; set; }
    }
}