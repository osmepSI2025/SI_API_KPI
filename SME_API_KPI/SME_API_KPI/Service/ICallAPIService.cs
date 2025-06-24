using SME_API_KPI.Models;

namespace SME_API_KPI.Services
{
    public interface ICallAPIService
    {
        Task<string> GetDataApiAsync(MapiInformationModels apiModels, object xdata);

    }
}
