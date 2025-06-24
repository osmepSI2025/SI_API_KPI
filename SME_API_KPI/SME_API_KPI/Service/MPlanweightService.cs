using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Repository;
using SME_API_KPI.Services;
using System.Text.Json;

namespace SME_API_KPI.Service
{
    public class MPlanweightService
    {
        private readonly MPlanweightRepository _repository;
        private readonly ICallAPIService _serviceApi;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly string _FlagDev;
        public MPlanweightService(MPlanweightRepository repository, IConfiguration configuration, ICallAPIService serviceApi, IApiInformationRepository repositoryApi)
        {
            _repository = repository;
            _serviceApi = serviceApi;
            _repositoryApi = repositoryApi;
            _FlagDev = configuration["Devlopment:FlagDev"] ?? throw new ArgumentNullException("FlagDev is missing in appsettings.json");

        }

        public async Task<IEnumerable<MPlanweight>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch
            {
                return Enumerable.Empty<MPlanweight>();
            }
        }

        public async Task<MPlanweight?> GetByIdAsync(int id)
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

        public async Task<bool> AddAsync(MPlanweight entity)
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

        public async Task<bool> UpdateAsync(MPlanweight entity)
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
        public async Task BatchEndOfDay_MPlanweight(searchMPlanweightModels xmodel)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var MPlanweightApirespone = new MPlanweightApirespone();
           
            var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "Getweight" });
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
            var result = JsonSerializer.Deserialize<MPlanweightApirespone>(apiResponse, options);

            MPlanweightApirespone = result ?? new MPlanweightApirespone();
            if (MPlanweightApirespone.data != null)
            {
                foreach (var item in MPlanweightApirespone.data)
                {
                    try
                    {
                        var existing = await _repository.GetByIdAsync(item.Planid);

                        if (existing == null)
                        {
                            // Create new record
                            var newData = new MPlanweight
                            {
                                Kpiid = item.Kpiid,
                                Planid = item.Planid,
                                Weight = item.Weight,
                            };

                            await _repository.AddAsync(newData);
                            Console.WriteLine($"[INFO] Created new MPlanweight with ID {newData.Planid}");
                        }
                        else
                        {
                            // Update existing record


                            existing.Kpiid = item.Kpiid;
                            existing.Planid = item.Planid;
                           existing.Weight = item.Weight;


                            await _repository.UpdateAsync(existing);
                            Console.WriteLine($"[INFO] Updated MPlanweight with ID {existing.Planid}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] Failed to process MPlanweight ID {item.Planid}: {ex.Message}");
                    }
                }
            }



        }
        public async Task<MPlanweightApirespone> GetAllAsyncSearch_MPlanweight(searchMPlanweightModels xmodel)
        {
            try
            {
                // Get data from repository
                var Ldata = await _repository.GetAllAsyncSearch_MPlanweight(xmodel);

                if (Ldata == null || !Ldata.Any())
                {
                    await BatchEndOfDay_MPlanweight(xmodel);

                    var Ldata2 = await _repository.GetAllAsyncSearch_MPlanweight(xmodel);
                    if (Ldata2 == null || !Ldata2.Any())
                    {
                        return new MPlanweightApirespone
                        {
                             ResponseCode = "200",
                            ResponseMsg = "No data found",
                            Timestamp = DateTime.UtcNow,
                            data = new List<MPlanweightModels>()
                        };
                    }
                    else
                    {
                        var models2 = Ldata2.Select(r => new MPlanweightModels
                        {
                            Planid = r.Planid,
                            Weight = r.Weight,
                            Kpiid = r.Kpiid,
                        }).ToList();

                        return new MPlanweightApirespone
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
                    var models = Ldata.Select(r => new MPlanweightModels
                    {
                        Planid = r.Planid,
                        Weight = r.Weight,
                        Kpiid = r.Kpiid,
                    }).ToList();

                    return new MPlanweightApirespone
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
                Console.WriteLine($"[ERROR] Failed to search MPlanweight: {ex.Message}");
                return new MPlanweightApirespone
                {
                    ResponseCode = "500",
                    ResponseMsg = "Internal Server Error",
                    Timestamp = DateTime.UtcNow,
                    data = new List<MPlanweightModels>()
                };
            }
        }


    }
}
