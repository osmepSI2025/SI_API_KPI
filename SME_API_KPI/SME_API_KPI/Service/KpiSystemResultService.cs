using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Repository;
using SME_API_KPI.Services;
using System.Text.Json;

namespace SME_API_KPI.Service
{
    public class KpiSystemResultService
    {
        private readonly MStatusRepository _repository;
        private readonly ICallAPIService _serviceApi;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly string _FlagDev;

        public KpiSystemResultService(MStatusRepository repository, IConfiguration configuration, ICallAPIService serviceApi, IApiInformationRepository repositoryApi)

        {
            _repository = repository;
            _serviceApi = serviceApi;
            _repositoryApi = repositoryApi;
            _FlagDev = configuration["Devlopment:FlagDev"] ?? throw new ArgumentNullException("FlagDev is missing in appsettings.json");

        }

        public Task<IEnumerable<MStatus>> GetAllAsync() => _repository.GetAllAsync();

        public Task<MStatus?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task<bool> AddAsync(MStatus entity) => _repository.AddAsync(entity);

        public Task<bool> UpdateAsync(MStatus entity) => _repository.UpdateAsync(entity);

        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);

        public async Task<KpiSystemResultResponeModels> KpiSystem_PutResult(KpiSystemResultModels models)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var MKpiStatusApirespone = new KpiSystemResultResponeModels();
            var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "KPI-RESULT" });
            var apiParam = LApi.Select(x => new MapiInformationModels
            {
                ServiceNameCode = x.ServiceNameCode,
                ApiKey = x.ApiKey,
                AuthorizationType = x.AuthorizationType,
                ContentType = x.ContentType,
                CreateDate = x.CreateDate,
                Id = x.Id,
                MethodType = x.MethodType,
                ServiceNameTh = x.ServiceNameTh,
                Urldevelopment = x.Urldevelopment,
                Urlproduction = x.Urlproduction,
                Username = x.Username,
                Password = x.Password,
                UpdateDate = x.UpdateDate,
                Bearer = x.Bearer,
                AccessToken = x.AccessToken

            }).FirstOrDefault(); // Use FirstOrDefault to handle empty lists

            var apiResponse = await _serviceApi.PutDataApiAsync(apiParam, models);
            var result = JsonSerializer.Deserialize<KpiSystemResultResponeModels>(apiResponse, options);


            return result;

        }
    }
}
