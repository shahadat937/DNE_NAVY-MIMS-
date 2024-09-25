using RMS.Model;

namespace RMS.DAL.IRepository
{
    public interface IControlShipInfoRepository : IRepository<ControlShipInfo>
    {
        int GetMaxControlCode(decimal parentCode);
    }
}
