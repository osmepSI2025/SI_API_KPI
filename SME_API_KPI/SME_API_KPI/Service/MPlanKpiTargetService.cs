using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Repository;
using SME_API_KPI.Services;
using System.Text.Json;

namespace SME_API_KPI.Service
{
    public class MPlanKpiTargetService
    {
        private readonly MPlanKpiTargetRepository _repository;
        private readonly ICallAPIService _serviceApi;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly string _FlagDev;

        public MPlanKpiTargetService(MPlanKpiTargetRepository repository
            , IConfiguration configuration, ICallAPIService serviceApi, IApiInformationRepository repositoryApi)
        {
            _repository = repository;
            _serviceApi = serviceApi;
            _repositoryApi = repositoryApi;
            _FlagDev = configuration["Devlopment:FlagDev"] ?? throw new ArgumentNullException("FlagDev is missing in appsettings.json");

        }

        public async Task<IEnumerable<MPlanKpiTarget>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch
            {
                return Enumerable.Empty<MPlanKpiTarget>();
            }
        }

        //public async Task<MPlanKpiTarget?> GetByIdAsync(int id)
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

        public async Task<bool> AddAsync(MPlanKpiTarget entity)
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

        public async Task<bool> UpdateAsync(MPlanKpiTarget entity)
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
        public async Task BatchEndOfDay_MPlanKpiTarget(searchMPlanKpiTargetModels xmodel)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var MPlanKpiTargetApirespone = new MPlanKpiTargetApirespone();
            var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "GetKpiTarget" });
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
            var result = JsonSerializer.Deserialize<MPlanKpiTargetApirespone>(apiResponse, options);

            MPlanKpiTargetApirespone = result ?? new MPlanKpiTargetApirespone();

            if (MPlanKpiTargetApirespone.data != null)
            {
                foreach (var item in MPlanKpiTargetApirespone.data)
                {
                    try
                    {
                        var existing = await _repository.GetByIdAsync(item.Planid,item.Kpiid);

                        // Map API response Target to TKpiTargets
                        var tKpiTargets = item.Target?.Select(t => new TKpiTarget
                        {
                            PlanId = item.Planid,
                            Kpiid = item.Kpiid,
                            PeriodId = t.PeriodID,
                            PeriodDetail = t.PeriodDetail,
                            Sequence = t.Sequence,
                            IsSkip = t.IsSkip,
                            LevelId = t.Levelid,
                            LevelDesc = t.Leveldesc,
                            LabelStr = t.Labelstr
                        }).ToList();

                        if (existing == null)
                        {
                            // Create new record
                            var newData = new MPlanKpiTarget
                            {
                                Kpiid = item.Kpiid,
                                PlanId = item.Planid,
                                TKpiTargets = tKpiTargets ?? new List<TKpiTarget>()
                            };

                            await _repository.AddAsync(newData);
                            Console.WriteLine($"[INFO] Created new MPlanKpiTarget with ID {newData.PlanId}");
                        }
                        else
                        {
                            // Update existing record
                            existing.Kpiid = item.Kpiid;
                            existing.PlanId = item.Planid;
                            existing.TKpiTargets = tKpiTargets ?? new List<TKpiTarget>();

                            await _repository.UpdateAsync(existing);
                            Console.WriteLine($"[INFO] Updated MPlanKpiTarget with ID {existing.PlanId}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] Failed to process MPlanKpiTarget ID {item.Planid}: {ex.Message}");
                    }
                }
            }




        }
        public async Task<MPlanKpiTargetApirespone> GetAllAsyncSearch_MPlanKpiTarget(searchMPlanKpiTargetModels xmodel)
        {
            try
            {
                // Get data from repository
                var Ldata = await _repository.GetAllAsyncSearch_MPlanKpiTarget(xmodel);

                if (Ldata == null || !Ldata.Any())
                {
                    await BatchEndOfDay_MPlanKpiTarget(xmodel);

                    var Ldata2 = await _repository.GetAllAsyncSearch_MPlanKpiTarget(xmodel);
                    if (Ldata2 == null || !Ldata2.Any())
                    {
                        return new MPlanKpiTargetApirespone();
                    }
                    else
                    {
                        var response = BuildApiResponse(Ldata2);
                        return response;
                    }
                }
                else
                {
                    var response = BuildApiResponse(Ldata);
                    return response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to search MPlanKpiTarget: {ex.Message}");
                return null;
            }
        }

        private MPlanKpiTargetApirespone BuildApiResponse(IEnumerable<MPlanKpiTarget> data)
        {
            return new MPlanKpiTargetApirespone
            {
                ResponseCode = "200",
                ResponseMsg = "OK",
                Timestamp = DateTime.UtcNow,
                data = data.Select(d => new MPlanKpiTargetData
                {
                    Planid = d.PlanId,
                    Kpiid = d.Kpiid,
                    Target = d.TKpiTargets?.Select(t => new MPlanKpiTargetDetail
                    {
                        PeriodID = t.PeriodId,
                        PeriodDetail = t.PeriodDetail,
                        Sequence = t.Sequence ?? 0,
                        IsSkip = t.IsSkip,
                        Levelid = t.LevelId ?? 0,
                        Leveldesc = t.LevelDesc,
                        Labelstr = t.LabelStr
                    }).ToList() ?? new List<MPlanKpiTargetDetail>()
                }).ToList()
            };
        }

    }
}
