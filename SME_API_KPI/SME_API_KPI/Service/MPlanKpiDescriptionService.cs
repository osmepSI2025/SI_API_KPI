using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Repository;
using SME_API_KPI.Services;
using System.Text.Json;

namespace SME_API_KPI.Service
{
    public class MPlanKpiDescriptionService
    {
        private readonly MPlanKpiDescriptionRepository _repository;
        private readonly ICallAPIService _serviceApi;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly string _FlagDev;
        public MPlanKpiDescriptionService(MPlanKpiDescriptionRepository repository, IConfiguration configuration, ICallAPIService serviceApi, IApiInformationRepository repositoryApi)
        {
            _repository = repository;
            _serviceApi = serviceApi;
            _repositoryApi = repositoryApi;
            _FlagDev = configuration["Devlopment:FlagDev"] ?? throw new ArgumentNullException("FlagDev is missing in appsettings.json");

        }

        public async Task<IEnumerable<MPlanKpiDescription>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch
            {
                return Enumerable.Empty<MPlanKpiDescription>();
            }
        }

        public async Task<MPlanKpiDescription?> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MPlanKpiDescription entity)
        {
            try
            {
                return await _repository.AddAsync(entity);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MPlanKpiDescription entity)
        {
            try
            {
                return await _repository.UpdateAsync(entity);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                return await _repository.DeleteAsync(id);
            }
            catch
            {
                return false;
            }
        }
        public async Task BatchEndOfDay_MPlanKpiDescription(searchMPlanKpiDescriptionModels xmodel)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var MPlanKpiDescriptionApirespone = new MPlanKpiDescriptionApirespone();
            var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "getkpidescription" });
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

            var apiResponse = await _serviceApi.GetDataApiAsync(apiParam, xmodel);
            var result = JsonSerializer.Deserialize<MPlanKpiDescriptionApirespone>(apiResponse, options);

            MPlanKpiDescriptionApirespone = result ?? new MPlanKpiDescriptionApirespone();

            if (MPlanKpiDescriptionApirespone.data != null)
            {
                foreach (var item in MPlanKpiDescriptionApirespone.data)
                {
                    try
                    {
                        var existing = await _repository.GetByIdAsync(item.Planid);

                        if (existing == null)
                        {
                            // Create new record
                            var newData = new MPlanKpiDescription
                            {
                                Kpiid = item.Kpiid,
                                Planid = item.Planid,
                                                };

                            await _repository.AddAsync(newData);
                            Console.WriteLine($"[INFO] Created new MPlanKpiDescription with ID {newData.Planid}");
                        }
                        else
                        {
                            // Update existing record


                            existing.Kpiid = item.Kpiid;
                            existing.Planid = item.Planid;
                        

                            await _repository.UpdateAsync(existing);
                            Console.WriteLine($"[INFO] Updated MPlanKpiDescription with ID {existing.Planid}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] Failed to process MPlanKpiDescription ID {item.Planid}: {ex.Message}");
                    }
                }
            }



        }
        public async Task<MPlanKpiDescriptionApirespone> GetAllAsyncSearch_MPlanKpiDescription(searchMPlanKpiDescriptionModels xmodel)
        {
            try
            {
                // Get data from repository
                var Ldata = await _repository.GetAllAsyncSearch_MPlanKpiDescription(xmodel);

                if (Ldata == null || !Ldata.Any())
                {
                    await BatchEndOfDay_MPlanKpiDescription(xmodel);

                    var Ldata2 = await _repository.GetAllAsyncSearch_MPlanKpiDescription(xmodel);
                    if (Ldata2 == null || !Ldata2.Any())
                    {
                        return new MPlanKpiDescriptionApirespone
                        {
                             ResponseCode = "200",
                            ResponseMsg = "No data found",
                            Timestamp = DateTime.UtcNow,
                            data = new List<MPlanKpiDescriptionModels>()
                        };
                    }
                    else
                    {
                        var models2 = Ldata2.Select(r => new MPlanKpiDescriptionModels
                        {
                            Planid = r.Planid,
                            Kpiid = r.Kpiid,
                        }).ToList();

                        return new MPlanKpiDescriptionApirespone
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
                    var models = Ldata.Select(r => new MPlanKpiDescriptionModels
                    {
                        Planid = r.Planid,
                        Kpiid = r.Kpiid,
                    }).ToList();

                    return new MPlanKpiDescriptionApirespone
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
                Console.WriteLine($"[ERROR] Failed to search MPlanKpiDescription: {ex.Message}");
                return new MPlanKpiDescriptionApirespone
                {
                    ResponseCode = "500",
                    ResponseMsg = "Internal Server Error",
                    Timestamp = DateTime.UtcNow,
                    data = new List<MPlanKpiDescriptionModels>()
                };
            }
        }


    }
}
