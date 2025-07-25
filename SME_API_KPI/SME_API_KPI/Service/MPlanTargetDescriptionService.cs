using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Repository;
using SME_API_KPI.Services;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SME_API_KPI.Service
{
    public class MPlanTargetDescriptionService
    {
        private readonly MPlanTargetDescriptionRepository _repository;
        private readonly ICallAPIService _serviceApi;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly string _FlagDev;
        public MPlanTargetDescriptionService(MPlanTargetDescriptionRepository repository, IConfiguration configuration, ICallAPIService serviceApi, IApiInformationRepository repositoryApi)
        {
            _repository = repository;
            _serviceApi = serviceApi;
            _repositoryApi = repositoryApi;
            _FlagDev = configuration["Devlopment:FlagDev"] ?? throw new ArgumentNullException("FlagDev is missing in appsettings.json");

        }

        public async Task<IEnumerable<MPlanTargetDescription>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch
            {
                return Enumerable.Empty<MPlanTargetDescription>();
            }
        }

        public async Task<MPlanTargetDescription?> GetByIdAsync(string  planId,string kpiid)
        {
            try
            {
                return await _repository.GetByIdAsync(planId,kpiid);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MPlanTargetDescription entity)
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

        public async Task<bool> UpdateAsync(MPlanTargetDescription entity)
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
        public async Task BatchEndOfDay_MPlanTargetDescription(searchMPlanTargetDescriptionModels xmodel)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var MPlanTargetDescriptionApirespone = new MPlanTargetDescriptionApirespone();
            var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "gettargetdescription" });
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
            try {
                var apiResponse = await _serviceApi.GetDataTargetAndKpiDesApiAsync(apiParam, xmodel.Planid,xmodel.Kpiid);
                var result = JsonSerializer.Deserialize<MPlanTargetDescriptionApirespone>(apiResponse, options);

                MPlanTargetDescriptionApirespone = result ?? new MPlanTargetDescriptionApirespone();

                if (MPlanTargetDescriptionApirespone.data != null)
                {
                    try
                    {
                        var existing = await _repository.GetByIdAsync(MPlanTargetDescriptionApirespone.data.Planid, MPlanTargetDescriptionApirespone.data.Kpiid);

                        if (existing == null)
                        {
                            // Create new record
                            var newData = new MPlanTargetDescription
                            {
                                Kpiid = MPlanTargetDescriptionApirespone.data.Kpiid,
                                Planid = MPlanTargetDescriptionApirespone.data.Planid,
                                Target = MPlanTargetDescriptionApirespone.data.Target,
                            };

                            await _repository.AddAsync(newData);
                            Console.WriteLine($"[INFO] Created new MPlanTargetDescription with ID {newData.Planid}");
                        }
                        else
                        {
                            // Update existing record


                            existing.Kpiid = MPlanTargetDescriptionApirespone.data.Kpiid;
                            existing.Planid = MPlanTargetDescriptionApirespone.data.Planid;
                            existing.Target = MPlanTargetDescriptionApirespone.data.Target;

                            await _repository.UpdateAsync(existing);
                            Console.WriteLine($"[INFO] Updated MPlanTargetDescription with ID {existing.Planid}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] Failed to process MPlanTargetDescription ID {MPlanTargetDescriptionApirespone.data.Planid}: {ex.Message}");
                    }
                 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to retrieve API information: {ex.Message}");
                return;
            }
      



        }
        public async Task<MPlanTargetDescriptionApirespone> GetAllAsyncSearch_MPlanTargetDescription(searchMPlanTargetDescriptionModels xmodel)
        {
            try
            {
                // Get data from repository
                var Ldata = await _repository.GetAllAsyncSearch_MPlanTargetDescription(xmodel);

                if (Ldata == null || !Ldata.Any())
                {
                    await BatchEndOfDay_MPlanTargetDescription(xmodel);

                    var Ldata2 = await _repository.GetAllAsyncSearch_MPlanTargetDescription(xmodel);
                    if (Ldata2 == null || !Ldata2.Any())
                    {
                        return new MPlanTargetDescriptionApirespone
                        {
                             status = 200,
                            message = "No data found",
                           // Timestamp = DateTime.UtcNow,
                            data = new MPlanTargetDescriptionModels()
                        };
                    }
                    else
                    {
                        var models2 = new MPlanTargetDescriptionModels
                        {
                            Planid = Ldata2.ToList()[0].Planid,
                            Target = Ldata2.ToList()[0].Target,
                            Kpiid = Ldata2.ToList()[0].Kpiid,
                        };

                        return new MPlanTargetDescriptionApirespone
                        {
                            status =200,
                            message = "OK",
                          //  Timestamp = DateTime.UtcNow,
                            data = models2
                        };
                    }
                }
                else
                {
               
                    var models = new MPlanTargetDescriptionModels
                    {
                        Planid = Ldata.ToList()[0].Planid,
                        Target = Ldata.ToList()[0].Target,
                        Kpiid = Ldata.ToList()[0].Kpiid,
                    };
                    return new MPlanTargetDescriptionApirespone
                    {
                        status = 200,
                        message = "OK",
                      //  Timestamp = DateTime.UtcNow,
                        data = models
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to search MPlanTargetDescription: {ex.Message}");
                return new MPlanTargetDescriptionApirespone
                {
                    status = 500,
                    message = "Internal Server Error",
                 //   Timestamp = DateTime.UtcNow,
                    data = new MPlanTargetDescriptionModels()
                };
            }
        }


    }
}
