using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Repository;
using SME_API_KPI.Services;
using System.Text.Json;

namespace SME_API_KPI.Service
{
    public class MKpiSystemKpiTargetService
    {
        private readonly MKpiSystemKpiTargetRepository _repository;
        private readonly ICallAPIService _serviceApi;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly string _FlagDev;

        public MKpiSystemKpiTargetService(MKpiSystemKpiTargetRepository repository, IConfiguration configuration, ICallAPIService serviceApi, IApiInformationRepository repositoryApi)
        {
            _repository = repository;
            _serviceApi = serviceApi;
            _repositoryApi = repositoryApi;
            _FlagDev = configuration["Devlopment:FlagDev"] ?? throw new ArgumentNullException("FlagDev is missing in appsettings.json");
        }

        public async Task<IEnumerable<MKpiSystemKpiTarget>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MKpiSystemKpiTarget?> GetByIdAsync(string  kpiid,string planid)
        {
            return await _repository.GetByIdAsync(kpiid,planid);
        }

        public async Task<bool> AddAsync(MKpiSystemKpiTarget entity)
        {
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(MKpiSystemKpiTarget entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
        public async Task BatchEndOfDay_MPlanKpiTarget(searchMPlanKpiTargetModels xmodel)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var MKpiSystemKpiTargetApiRespone = new MKpiSystemKpiTargetApiRespone();
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

            try
            {
                var apiResponse = await _serviceApi.GetDataApiAsync(apiParam, xmodel);

                var result = JsonSerializer.Deserialize<MKpiSystemKpiTargetApiRespone>(apiResponse, options);

                MKpiSystemKpiTargetApiRespone = result ?? new MKpiSystemKpiTargetApiRespone();

                // Replace the foreach loop to match MKpiSystemKpiTargetApiRespone structure
                if (MKpiSystemKpiTargetApiRespone.data != null)
                {
                    var item = MKpiSystemKpiTargetApiRespone.data;
                    try
                    {
                        var existing = await _repository.GetByIdAsync(item.kpiid, xmodel.Planid);

                        // Map API response Target to TKpiSystemKpiTargets
                        var tKpiSystemKpiTargets = item.target?.Select(t => new TKpiSystemKpiTarget
                        {
                            KpiId = item.kpiid,
                            PeriodId = t.periodId,
                            IsSkip = t.isSkip,
                            Sequence = t.sequence,
                            Weight= t.weight,
                            TKpiSystemKpiTargetLevels = t.labelstr?.Select(l => new TKpiSystemKpiTargetLevel
                            {
                                LevelDesc = l.levlDesc,
                                LabelStr = l.labelstr
                            }).ToList() ?? new List<TKpiSystemKpiTargetLevel>()
                        }).ToList();

                        if (existing == null)
                        {
                            // Create new record
                            var newData = new MKpiSystemKpiTarget
                            {
                                PlanId = xmodel.Planid,
                                KpiId = item.kpiid,
                                KpiName = item.kpiname,
                                TKpiSystemKpiTargets = tKpiSystemKpiTargets ?? new List<TKpiSystemKpiTarget>()
                            };

                            await _repository.AddAsync(newData);
                            Console.WriteLine($"[INFO] Created new MKpiSystemKpiTarget with KPI ID {newData.KpiId}");
                        }
                        else
                        {
                            // Update existing record
                            existing.PlanId = xmodel.Planid;
                            existing.KpiId = item.kpiid;
                            existing.KpiName = item.kpiname;
                            existing.TKpiSystemKpiTargets = tKpiSystemKpiTargets ?? new List<TKpiSystemKpiTarget>();

                            await _repository.UpdateAsync(existing);
                            Console.WriteLine($"[INFO] Updated MKpiSystemKpiTarget with KPI ID {existing.KpiId}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] Failed to process MKpiSystemKpiTarget KPI ID {item.kpiid}: {ex.Message}");
                    }
                }


            }
            catch (Exception ex)
            {
                //var errorLog = new ErrorLogModels
                //{
                //    Message = "Function " + apiModels.ServiceNameTh + " " + ex.Message,
                //    StackTrace = ex.StackTrace,
                //    Source = ex.Source,
                //    TargetSite = ex.TargetSite?.ToString(),
                //    ErrorDate = DateTime.Now,
                //    UserName = apiModels.Username, // ดึงจาก context หรือ session
                //    Path = apiModels.Urlproduction,
                //    HttpMethod = apiModels.MethodType,
                //    RequestData = requestJson, // serialize เป็น JSON
                //    InnerException = ex.InnerException?.ToString(),
                //    SystemCode = Api_SysCode,
                //    CreatedBy = "system"
                //       ,
                //    HttpCode = "500",
                //};
                //await RecErrorLogApiAsync(apiModels, errorLog);
            }




        }
        public async Task<MKpiSystemKpiTargetApiRespone> GetAllAsyncSearch_MPlanKpiTarget(searchMPlanKpiTargetModels xmodel)
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
                        return new MKpiSystemKpiTargetApiRespone();
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

        private MKpiSystemKpiTargetApiRespone BuildApiResponse(IEnumerable<MKpiSystemKpiTarget> data)
        {
            var first = data.FirstOrDefault();
            if (first == null)
            {
                return new MKpiSystemKpiTargetApiRespone
                {
                    status = 200,
                    message = "OK",
                    data = null
                };
            }

            return new MKpiSystemKpiTargetApiRespone
            {
                status = 200,
                message = "OK",
                data = new MKpiSystemKpiTargetData
                {
                    kpiid = first.KpiId,
                    kpiname = first.KpiName,
                    target = first.TKpiSystemKpiTargets?.Select(t => new MKpiSystemKpiTargetDetail
                    {
                        periodId = t.PeriodId,
                        sequence = t.Sequence,
                        isSkip = t.IsSkip,
                        weight = t.Weight, // Set as needed
                        labelstr = t.TKpiSystemKpiTargetLevels?.Select(l => new MKpiSystemKpiTargetLabel
                        {
                            levlDesc = l.LevelDesc,
                            labelstr = l.LabelStr
                        }).ToList() ?? new List<MKpiSystemKpiTargetLabel>()
                    }).ToList() ?? new List<MKpiSystemKpiTargetDetail>()
                }
            };
        }
    }
}