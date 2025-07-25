using SME_API_KPI.Models;

namespace SME_API_KPI.Services
{
    public interface ICallAPIService
    {
        Task<string> GetDataApiAsync(MapiInformationModels apiModels, object xdata);
        Task<string> GetDataTargetAndKpiDesApiAsync(MapiInformationModels apiModels, string planid,string kpiid);

        Task<string> GetDataPlanPeroidApiAsync(MapiInformationModels apiModels,int planyear ,string planTypeid, int? peroidid);

        Task<string> PutDataApiAsync(MapiInformationModels apiModels, object xdata);
    }
}
