using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Repository;
using SME_API_KPI.Services;
using System.Text.Json;

namespace SME_API_KPI.Service
{
    public class MKpiSystemWeightService
    {
        private readonly MKpiSystemWeightRepository _repository;
        private readonly ICallAPIService _serviceApi;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly string _FlagDev;
        public MKpiSystemWeightService(MKpiSystemWeightRepository repository, IConfiguration configuration, ICallAPIService serviceApi, IApiInformationRepository repositoryApi)
        {
            _repository = repository;
            _serviceApi = serviceApi;
            _repositoryApi = repositoryApi;
            _FlagDev = configuration["Devlopment:FlagDev"] ?? throw new ArgumentNullException("FlagDev is missing in appsettings.json");
        }

        public async Task<IEnumerable<MKpiSystemWeight>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception)
            {
                return Enumerable.Empty<MKpiSystemWeight>();
            }
        }

        public async Task<MKpiSystemWeight?> GetByIdAsync(string planid,string kpiid)
        {
            try
            {
                return await _repository.GetByIdAsync(planid, kpiid);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MKpiSystemWeight entity)
        {
            try
            {
                return await _repository.AddAsync(entity);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MKpiSystemWeight entity)
        {
            try
            {
                return await _repository.UpdateAsync(entity);
            }
            catch (Exception)
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
            catch (Exception)
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

            }).FirstOrDefault();

            var apiResponse = await _serviceApi.GetDataApiAsync(apiParam, xmodel);
            var result = JsonSerializer.Deserialize<MPlanweightApirespone>(apiResponse, options);

            MPlanweightApirespone = result ?? new MPlanweightApirespone();
            if (MPlanweightApirespone.data != null)
            {
                // For new JSON structure, data is a single object, not a list
                var item = MPlanweightApirespone.data;
                try
                {
                    var existing = await _repository.GetByIdAsync(item.kpiid, xmodel.Planid);

                    if (existing == null)
                    {
                        // Create new record
                        var newData = new MKpiSystemWeight
                        {
                            KpiId = item.kpiid,
                            Planid = xmodel.Planid,
                            KpiName = item.kpiname,
                            TKpiSystemWeights = item.target?.Select(t => new TKpiSystemWeight
                            {
                                KpiId = item.kpiid,
                                PeriodId = t.periodId,
                                Weight = t.weight
                            }).ToList() ?? new List<TKpiSystemWeight>()
                        };

                        await _repository.AddAsync(newData);
                        Console.WriteLine($"[INFO] Created new MKpiSystemWeight with KPI ID {newData.KpiId}");
                    }
                    else
                    {
                        // Update existing record
                        existing.KpiId = item.kpiid;
                        existing.Planid = xmodel.Planid;
                        existing.KpiName = item.kpiname;
                        existing.TKpiSystemWeights = item.target?.Select(t => new TKpiSystemWeight
                        {
                            KpiId = item.kpiid,
                            PeriodId = t.periodId,
                            Weight = t.weight
                        }).ToList() ?? new List<TKpiSystemWeight>();

                        await _repository.UpdateAsync(existing);
                        Console.WriteLine($"[INFO] Updated MKpiSystemWeight with KPI ID {existing.KpiId}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Failed to process MKpiSystemWeight KPI ID {item.kpiid}: {ex.Message}");
                }
            }



        }
        public async Task<MPlanweightApirespone> GetAllAsyncSearch_MPlanweight(searchMPlanweightModels xmodel)
        {
            try
            {
                var Ldata = await _repository.GetAllAsyncSearch_MPlanweight(xmodel);

                if (Ldata == null || !Ldata.Any())
                {
                    await BatchEndOfDay_MPlanweight(xmodel);

                    var Ldata2 = await _repository.GetAllAsyncSearch_MPlanweight(xmodel);
                    if (Ldata2 == null || !Ldata2.Any())
                    {
                        return new MPlanweightApirespone
                        {
                            status = 200,
                            message = "No data found",
                            data = null
                        };
                    }
                    else
                    {
                        var first = Ldata2.FirstOrDefault();
                        return new MPlanweightApirespone
                        {
                            status = 200,
                            message = "OK",
                            data = first == null ? null : new MPlanweightData
                            {
                                kpiid = first.KpiId,
                                kpiname = first.KpiName,
                                target = first.TKpiSystemWeights?.Select(t => new MPlanweightTarget
                                {
                                    periodId = t.PeriodId,
                                    weight = t.Weight
                                }).ToList() ?? new List<MPlanweightTarget>()
                            }
                        };
                    }
                }
                else
                {
                    var first = Ldata.FirstOrDefault();
                    return new MPlanweightApirespone
                    {
                        status = 200,
                        message = "OK",
                        data = first == null ? null : new MPlanweightData
                        {
                            kpiid = first.KpiId,
                            kpiname = first.KpiName,
                            target = first.TKpiSystemWeights?.Select(t => new MPlanweightTarget
                            {
                                periodId = t.PeriodId,
                                weight = t.Weight
                            }).ToList() ?? new List<MPlanweightTarget>()
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to search MPlanweight: {ex.Message}");
                return new MPlanweightApirespone
                {
                    status = 500,
                    message = "Internal Server Error",
                    data = null
                };
            }
        }
    }
}