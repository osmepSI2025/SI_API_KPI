using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Repository;
using SME_API_KPI.Services;
using System.Text.Json;

namespace SME_API_KPI.Service
{
    public class MKpiTypeService 
    {
        private readonly MKpiTypeRepository _repository;
        private readonly ICallAPIService _serviceApi;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly string _FlagDev;

        public MKpiTypeService(MKpiTypeRepository repository, IConfiguration configuration, ICallAPIService serviceApi, IApiInformationRepository repositoryApi)

        {
            _repository = repository;
            _serviceApi = serviceApi;
            _repositoryApi = repositoryApi;
            _FlagDev = configuration["Devlopment:FlagDev"] ?? throw new ArgumentNullException("FlagDev is missing in appsettings.json");

        }

        public Task<IEnumerable<MKpiType>> GetAllAsync() => _repository.GetAllAsync();

        public Task<MKpiType?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task<bool> AddAsync(MKpiType entity) => _repository.AddAsync(entity);

        public Task<bool> UpdateAsync(MKpiType entity) => _repository.UpdateAsync(entity);

        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);

        public async Task BatchEndOfDay_MKpiType()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var MKpiStatusApirespone = new MKpiStatusApirespone();
            var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "GetkpiType" });
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

            var apiResponse = await _serviceApi.GetDataApiAsync(apiParam, null);
            var result = JsonSerializer.Deserialize<MKpiStatusApirespone>(apiResponse, options);

            MKpiStatusApirespone = result ?? new MKpiStatusApirespone();

            if ( MKpiStatusApirespone.data != null)
            {
                foreach (var item in MKpiStatusApirespone.data)
                {
                    try
                    {
                        var existing = await _repository.GetByIdAsync(item.Masterid);

                        if (existing == null)
                        {
                            // Create new record
                            var newData = new MKpiType
                            {
                                Description = item.Description,
                                Masterid = item.Masterid,
                            };

                            await _repository.AddAsync(newData);
                            Console.WriteLine($"[INFO] Created new MKpiType with ID {newData.Masterid}");
                        }
                        else
                        {
                            // Update existing record
                            
                             
                            existing.Description = item.Description;
                            existing.Masterid = item.Masterid;

                            await _repository.UpdateAsync(existing);
                            Console.WriteLine($"[INFO] Updated MKpiType with ID {existing.Masterid}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] Failed to process MKpiType ID {item.Masterid}: {ex.Message}");
                    }
                }
            }



        }
        public async Task<MKpiStatusApirespone> GetAllAsyncSearch_MKpiType()
        {
            try
            {
                // Get data from repository
                var Ldata = await _repository.GetAllAsync();

                if (Ldata == null || !Ldata.Any())
                {
                    await BatchEndOfDay_MKpiType();

                    var Ldata2 = await _repository.GetAllAsync();
                    if (Ldata2 == null || !Ldata2.Any())
                    {
                        return new MKpiStatusApirespone
                        {
                            ResponseCode = "200",
                            ResponseMsg = "No data found",
                            Timestamp = DateTime.UtcNow,
                            data = new List<MStatusModels>()
                        };
                    }
                    else
                    {
                        var models2 = Ldata2.Select(r => new MStatusModels
                        {
                            Masterid = r.Masterid,
                            Description = r.Description
                        }).ToList();

                        return new MKpiStatusApirespone
                        {
                            ResponseCode = "200",
                            ResponseMsg = "OK",
                            Timestamp = DateTime.UtcNow,
                            data = models2
                        };
                    }
                }
                else
                {
                    var models = Ldata.Select(r => new MStatusModels
                    {
                        Masterid = r.Masterid,
                        Description = r.Description
                    }).ToList();

                    return new MKpiStatusApirespone
                    {
                        ResponseCode = "200",
                        ResponseMsg = "OK",
                        Timestamp = DateTime.UtcNow,
                        data = models
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to search MKpiType: {ex.Message}");
                return new MKpiStatusApirespone
                {
                    ResponseCode = "500",
                    ResponseMsg = "Internal Server Error",
                    Timestamp = DateTime.UtcNow,
                    data = new List<MStatusModels>()
                };
            }
        }


    }
}
