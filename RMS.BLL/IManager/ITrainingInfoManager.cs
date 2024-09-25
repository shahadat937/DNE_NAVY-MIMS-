using RMS.Model;
using RMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.IManager
{
    public interface ITrainingInfoManager
    {
        List<TrainingInfo> GetAll();
        //TrainingInfo GetAlls();
        TrainingInfo GetTrainingInfoById(string id);
        TrainingInfo GetCourseById(long id);

        TrainingInfo GetTrainingInfoByRankAndCourse(long? course, long? rank);
        int Delete(string id);
        ResponseModel Saving(TrainingInfo trainingInfo);

        List<TrainingInfo> GetTrainingInfoAll();


        ResponseModel SaveTrainingInfo(TrainingInfo trainingInfo);

        List<TrainingInfo> FindOne(long id);
    }
}
