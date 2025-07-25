using Microsoft.AspNetCore.Mvc.ApiExplorer;
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

        public async Task<MPlanKpiDescription?> GetByIdAsync(string id,string kpiid)
        {
            try
            {
                return await _repository.GetByIdAsync(id, kpiid);
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

            var apiResponse = await _serviceApi.GetDataTargetAndKpiDesApiAsync(apiParam, xmodel.Planid,xmodel.Kpiid);
            var result = JsonSerializer.Deserialize<MPlanKpiDescriptionApirespone>(apiResponse, options);

            MPlanKpiDescriptionApirespone = result ?? new MPlanKpiDescriptionApirespone();

            if (MPlanKpiDescriptionApirespone.data != null)
            {
                try
                {
                    var existing = await _repository.GetByIdAsync(MPlanKpiDescriptionApirespone.data.Planid, MPlanKpiDescriptionApirespone.data.Kpiid);

                    if (existing == null)
                    {
                        // Create new record
                        var newData = new MPlanKpiDescription
                        {
                            Kpiid = MPlanKpiDescriptionApirespone.data.Kpiid,
                            Planid = MPlanKpiDescriptionApirespone.data.Planid,
                            Kpidescription = MPlanKpiDescriptionApirespone.data.Kpidescription,
                        };

                        await _repository.AddAsync(newData);
                        Console.WriteLine($"[INFO] Created new MPlanKpiDescription with ID {newData.Planid}");
                    }
                    else
                    {
                        // Update existing record


                        existing.Kpiid = MPlanKpiDescriptionApirespone.data.Kpiid;
                        existing.Planid = MPlanKpiDescriptionApirespone.data.Planid;
                        existing.Kpidescription = MPlanKpiDescriptionApirespone.data.Kpidescription;

                        await _repository.UpdateAsync(existing);
                        Console.WriteLine($"[INFO] Updated MPlanKpiDescription with ID {existing.Planid}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Failed to process MPlanKpiDescription ID {MPlanKpiDescriptionApirespone.data.Planid}: {ex.Message}");
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
                             status =200,
                            message = "No data found",
                           
                            data = new MPlanKpiDescriptionModels()
                        };
                    }
                    else
                    {
                        var models2 = new MPlanKpiDescriptionModels
                        {
                            Planid = Ldata2.ToList()[0].Planid,
                            Kpiid = Ldata2.ToList()[0].Kpiid,
                            Kpidescription = Ldata2.ToList()[0].Kpidescription,
                        };

                        return new MPlanKpiDescriptionApirespone
                        {
                            status = 200,
                            message = "OK",
                         
                            data = models2
                        };
                    }
                }
                else
                {
                    var models = new MPlanKpiDescriptionModels
                    {
                        Planid = Ldata.ToList()[0].Planid,
                        Kpiid = Ldata.ToList()[0].Kpiid,
                        Kpidescription = Ldata.ToList()[0].Kpidescription
                    };

                    return new MPlanKpiDescriptionApirespone
                    {
                        status = 200,
                        message = "OK",
                     
                        data = models
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to search MPlanKpiDescription: {ex.Message}");
                return new MPlanKpiDescriptionApirespone
                {
                    status = 500,
                    message = "Internal Server Error",
                   
                    data = new MPlanKpiDescriptionModels()
                };
            }
        }


    }
}
