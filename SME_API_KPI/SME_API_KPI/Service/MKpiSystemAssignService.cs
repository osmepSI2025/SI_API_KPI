using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Repository;
using SME_API_KPI.Services;
using System.Text.Json;

namespace SME_API_KPI.Service
{
    public class MKpiSystemAssignService
    {
        private readonly MKpiSystemAssignRepository _repository;
        private readonly ICallAPIService _serviceApi;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly string _FlagDev;
        public MKpiSystemAssignService(MKpiSystemAssignRepository repository, IConfiguration configuration, ICallAPIService serviceApi, IApiInformationRepository repositoryApi)
        {
            _repository = repository;
            _serviceApi = serviceApi;
            _repositoryApi = repositoryApi;
            _FlagDev = configuration["Devlopment:FlagDev"] ?? throw new ArgumentNullException("FlagDev is missing in appsettings.json");
        }

        public async Task<IEnumerable<MKpiSystemAssign>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception)
            {
                return Enumerable.Empty<MKpiSystemAssign>();
            }
        }

        public async Task<MKpiSystemAssign?> GetByIdAsync(string kpiid,string planid)
        {
            try
            {
                return await _repository.GetByIdAsync(kpiid,planid);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MKpiSystemAssign entity)
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

        public async Task<bool> UpdateAsync(MKpiSystemAssign entity)
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
        public async Task BatchEndOfDay_MPlanKpiAssign(searchMPlanKpiAssignModels xmodel)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var MPlanKpiAssignApirespone = new MPlanKpiAssignApirespone();
            var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "GetKpiAssign" });
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
            var result = JsonSerializer.Deserialize<MPlanKpiAssignApirespone>(apiResponse, options);

            MPlanKpiAssignApirespone = result ?? new MPlanKpiAssignApirespone();

            if (MPlanKpiAssignApirespone.data != null)
            {
                // For new JSON structure, data is a single object, not a list
                var item = MPlanKpiAssignApirespone.data;
                try
                {
                    var existing = await _repository.GetByIdAsync(item.kpiid, xmodel.planid);

                    if (existing == null)
                    {
                        // Create new record
                        var newData = new MKpiSystemAssign
                        {
                            KpiId = item.kpiid,
                            KpiName = item.kpiname,
                            PlanId = xmodel.planid,
                            Weight = item.weight,
                            TKpiSystemAssignDivisions = item.divisionname?.Select(d => new TKpiSystemAssignDivision
                            {
                                KpiId = item.kpiid,
                                DivisionName = d.divisionname
                            }).ToList() ?? new List<TKpiSystemAssignDivision>()
                        };

                        await _repository.AddAsync(newData);
                        Console.WriteLine($"[INFO] Created new MKpiSystemAssign with KPI ID {newData.KpiId}");
                    }
                    else
                    {
                        // Update existing record
                        existing.KpiId = item.kpiid;
                        existing.KpiName = item.kpiname;
                        existing.PlanId = xmodel.planid;
                        existing.Weight = item.weight;
                        existing.TKpiSystemAssignDivisions = item.divisionname?.Select(d => new TKpiSystemAssignDivision
                        {
                            KpiId = item.kpiid,
                            DivisionName = d.divisionname
                        }).ToList() ?? new List<TKpiSystemAssignDivision>();

                        await _repository.UpdateAsync(existing);
                        Console.WriteLine($"[INFO] Updated MKpiSystemAssign with KPI ID {existing.KpiId}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Failed to process MKpiSystemAssign KPI ID {item.kpiid}: {ex.Message}");
                }
            }



        }
        public async Task<MPlanKpiAssignApirespone> GetAllAsyncSearch_MPlanKpiAssign(searchMPlanKpiAssignModels xmodel)
        {
            try
            {
                var Ldata = await _repository.GetAllAsyncSearch_MPlanKpiAssign(xmodel);

                if (Ldata == null || !Ldata.Any())
                {
                    await BatchEndOfDay_MPlanKpiAssign(xmodel);

                    var Ldata2 = await _repository.GetAllAsyncSearch_MPlanKpiAssign(xmodel);
                    if (Ldata2 == null || !Ldata2.Any())
                    {
                        return new MPlanKpiAssignApirespone
                        {
                            status = 200,
                            message = "No data found",
                            data = null
                        };
                    }
                    else
                    {
                        var first = Ldata2.FirstOrDefault();
                        return new MPlanKpiAssignApirespone
                        {
                            status = 200,
                            message = "OK",
                            data = first == null ? null : new MPlanKpiAssignData
                            {
                                kpiid = first.KpiId,
                                kpiname = first.KpiName,
                                weight = first.Weight,
                                divisionname = first.TKpiSystemAssignDivisions?.Select(d => new MPlanKpiAssignDivision
                                {
                                    divisionname = d.DivisionName
                                }).ToList() ?? new List<MPlanKpiAssignDivision>()
                            }
                        };
                    }
                }
                else
                {
                    var first = Ldata.FirstOrDefault();
                    return new MPlanKpiAssignApirespone
                    {
                        status = 200,
                        message = "OK",
                        data = first == null ? null : new MPlanKpiAssignData
                        {
                            kpiid = first.KpiId,
                            kpiname = first.KpiName,
                            weight = first.Weight,
                            divisionname = first.TKpiSystemAssignDivisions?.Select(d => new MPlanKpiAssignDivision
                            {
                                divisionname = d.DivisionName
                            }).ToList() ?? new List<MPlanKpiAssignDivision>()
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to search MPlanKpiAssign: {ex.Message}");
                return new MPlanKpiAssignApirespone
                {
                    status = 500,
                    message = "Internal Server Error",
                    data = new MPlanKpiAssignData()
                };
            }
        }
    }
}