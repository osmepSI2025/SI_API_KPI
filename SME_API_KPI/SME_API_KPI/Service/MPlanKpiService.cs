using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Repository;
using SME_API_KPI.Services;
using System.Text.Json;

namespace SME_API_KPI.Service
{
    public class MPlanKpiService
    {
        private readonly MPlanKpiRepository _repository;
        private readonly ICallAPIService _serviceApi;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly string _FlagDev;

        public MPlanKpiService(MPlanKpiRepository repository, IConfiguration configuration, ICallAPIService serviceApi, IApiInformationRepository repositoryApi)
        {
            _repository = repository;
            _serviceApi = serviceApi;
            _repositoryApi = repositoryApi;
            _FlagDev = configuration["Devlopment:FlagDev"] ?? throw new ArgumentNullException("FlagDev is missing in appsettings.json");

        }

        public async Task<IEnumerable<MPlanKpi>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch
            {
                return Enumerable.Empty<MPlanKpi>();
            }
        }

        //public async Task<MPlanKpi?> GetByIdAsync(int id)
        //{
        //    try
        //    {
        //        return await _repository.GetByIdAsync(id);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public async Task<bool> AddAsync(MPlanKpi entity)
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

        public async Task<bool> UpdateAsync(MPlanKpi entity)
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
        public async Task BatchEndOfDay_MPlanKpi(searchMPlanKpiModels xmodel)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var MPlanKpiApirespone = new MPlanKpiApirespone();
            var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "GetKPI" });
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
            var result = JsonSerializer.Deserialize<MPlanKpiApirespone>(apiResponse, options);

            MPlanKpiApirespone = result ?? new MPlanKpiApirespone();

            if (MPlanKpiApirespone.data != null)
            {
                foreach (var item in MPlanKpiApirespone.data)
                {
                    try
                    {
                        var existing = await _repository.GetByIdAsync(item.Planid, item.Kpiid);

                        // Map API response kpilist to TPlanKpilist
                        var tPlanKpilists = item.Kpilist?.Select(k => new TPlanKpilist
                        {
                            PlanId = item.Planid,
                            Kpiid = k.Kpiid,
                            Kpiindex = k.Index,
                            Kpiname = k.Kpiname,
                            Weight = (decimal?)k.Weight,
                            Target = k.Target,
                            TPlanKpidivisions = k.Divisionname?.Select(d => new TPlanKpidivision
                            {
                                DivisionName = d.Divisionname
                            }).ToList() ?? new List<TPlanKpidivision>()
                        }).ToList();

                        if (existing == null)
                        {
                            // Create new record
                            var newData = new MPlanKpi
                            {
                                Kpiid = item.Kpiid,
                                PlanId = item.Planid,
                                TPlanKpilists = tPlanKpilists ?? new List<TPlanKpilist>()
                            };

                            await _repository.AddAsync(newData);
                            Console.WriteLine($"[INFO] Created new MPlanKpi with ID {newData.PlanId}");
                        }
                        else
                        {
                            // Update existing record
                            existing.Kpiid = item.Kpiid;
                            existing.PlanId = item.Planid;
                            existing.TPlanKpilists = tPlanKpilists ?? new List<TPlanKpilist>();

                            await _repository.UpdateAsync(existing);
                            Console.WriteLine($"[INFO] Updated MPlanKpi with ID {existing.PlanId}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] Failed to process MPlanKpi ID {item.Planid}: {ex.Message}");
                    }
                }
            }


        }
        public async Task<MPlanKpiApirespone> GetAllAsyncSearch_MPlanKpi(searchMPlanKpiModels xmodel)
        {
            try
            {
                // Get data from repository
                var Ldata = await _repository.GetAllAsyncSearch_MPlanKpi(xmodel);

                if (Ldata == null || !Ldata.Any())
                {
                    await BatchEndOfDay_MPlanKpi(xmodel);

                    var Ldata2 = await _repository.GetAllAsyncSearch_MPlanKpi(xmodel);
                    if (Ldata2 == null || !Ldata2.Any())
                    {
                        return new MPlanKpiApirespone
                        {
                             ResponseCode = "200",
                            ResponseMsg = "No data found",
                            Timestamp = DateTime.UtcNow,
                            data = new List<MPlanGetKpiData>()
                        };
                    }
                    else
                    {
                        return BuildApiResponse(Ldata2);
                    }
                }
                else
                {
                    return BuildApiResponse(Ldata);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to search MPlanKpi: {ex.Message}");
                return new MPlanKpiApirespone
                {
                    ResponseCode = "500",
                    ResponseMsg = "Internal Server Error",
                    Timestamp = DateTime.UtcNow,
                    data = new List<MPlanGetKpiData>()
                };
            }
        }

        private MPlanKpiApirespone BuildApiResponse(IEnumerable<MPlanKpi> data)
        {
            return new MPlanKpiApirespone
            {
                ResponseCode = "200",
                ResponseMsg = "OK",
                Timestamp = DateTime.UtcNow,
                data = data.Select(d => new MPlanGetKpiData
                {
                    Planid = d.PlanId,
                    Kpiid = d.Kpiid,
                    Kpilist = d.TPlanKpilists?.Select(k => new MPlanGetKpiList
                    {
                        Index = k.Kpiindex ?? 0,
                        Kpiid = k.Kpiid,
                        Kpiname = k.Kpiname,
                        Weight = (double)(k.Weight ?? 0),
                        Target = k.Target,
                        Divisionname = k.TPlanKpidivisions?.Select(div => new MPlanGetKpiDivisionName
                        {
                            Divisionname = div.DivisionName
                        }).ToList() ?? new List<MPlanGetKpiDivisionName>()
                    }).ToList() ?? new List<MPlanGetKpiList>()
                }).ToList()
            };
        }


    }
}
