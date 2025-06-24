using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Repository;
using SME_API_KPI.Services;
using System.Text.Json;

namespace SME_API_KPI.Service
{
    public class MPlanKpiListService
    {
        private readonly MPlanKpiListRepository _repository;
        private readonly ICallAPIService _serviceApi;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly string _FlagDev;
        public MPlanKpiListService(MPlanKpiListRepository repository, IConfiguration configuration, ICallAPIService serviceApi, IApiInformationRepository repositoryApi)
        {
            _repository = repository;
            _serviceApi = serviceApi;
            _repositoryApi = repositoryApi;
            _FlagDev = configuration["Devlopment:FlagDev"] ?? throw new ArgumentNullException("FlagDev is missing in appsettings.json");

        }

        public async Task<IEnumerable<MPlanKpiList>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch
            {
                return Enumerable.Empty<MPlanKpiList>();
            }
        }

       
        public async Task<bool> AddAsync(MPlanKpiList entity)
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

        public async Task<bool> UpdateAsync(MPlanKpiList entity)
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
        public async Task BatchEndOfDay_MPlanKpiList(searchMPlanKpiListModels xmodel)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var MPlanKpiListApirespone = new MPlanKpiListApirespone();
            var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "Getlist" });
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
            var result = JsonSerializer.Deserialize<MPlanKpiListApirespone>(apiResponse, options);

            MPlanKpiListApirespone = result ?? new MPlanKpiListApirespone();

            if (MPlanKpiListApirespone.data != null)
            {
                foreach (var item in MPlanKpiListApirespone.data)
                {
                    try
                    {
                        var existing = await _repository.GetByIdAsync(item.Planid,item.Planyear,item.Plantitle, item.PlanTypeid);

                        if (existing == null)
                        {
                            // Create new record
                            var newData = new MPlanKpiList
                            {
                                Planid = item.Planid,
                              PlanTypeid = item.PlanTypeid,
                               Planyear = item.Planyear,                       
                                Plantitle = item.Plantitle,                         
                                Planremark = item.Planremark,
                                Effectivedate = item.Effectivedate,
                                Enddate = item.Enddate,

                            };

                            await _repository.AddAsync(newData);
                            Console.WriteLine($"[INFO] Created new MPlanKpiList with ID {newData.Planid}");
                        }
                        else
                        {
                            // Update existing record

                            existing.Planid = item.Planid;
                            existing.PlanTypeid = item.PlanTypeid;
                            existing.Planyear = item.Planyear;
                            existing.Plantitle = item.Plantitle;
                            existing.Planremark = item.Planremark;
                            existing.Effectivedate = item.Effectivedate;
                            existing.Enddate = item.Enddate;



                            await _repository.UpdateAsync(existing);
                            Console.WriteLine($"[INFO] Updated MPlanKpiList with ID {existing.Planid}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] Failed to process MPlanKpiList ID {item.Planid}: {ex.Message}");
                    }
                }
            }



        }
        public async Task<MPlanKpiListApirespone> GetAllAsyncSearch_MPlanKpiList(searchMPlanKpiListModels xmodel)
        {
            try
            {
                // Get data from repository
                var Ldata = await _repository.GetAllAsyncSearch_MPlanKpiList(xmodel);

                if (Ldata == null || !Ldata.Any())
                {
                    await BatchEndOfDay_MPlanKpiList(xmodel);

                    var Ldata2 = await _repository.GetAllAsyncSearch_MPlanKpiList(xmodel);
                    if (Ldata2 == null || !Ldata2.Any())
                    {
                        return new MPlanKpiListApirespone
                        {
                            ResponseCode = "200",
                            ResponseMsg = "No data found",
                            Timestamp = DateTime.UtcNow,
                            data = new List<MPlanKpiListModels>()
                        };
                    }
                    else
                    {
                        var models2 = Ldata2.Select(r => new MPlanKpiListModels
                        {
                            Planid = r.Planid,
                            Plantitle = r.Plantitle,
                            PlanTypeid = r.PlanTypeid,
                            Planyear = r.Planyear,
                            Planremark = r.Planremark,
                            Effectivedate = r.Effectivedate,
                            Enddate = r.Enddate,
                        }).ToList();

                        return new MPlanKpiListApirespone
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
                    var models = Ldata.Select(r => new MPlanKpiListModels
                    {
                        Planid = r.Planid,
                        Plantitle = r.Plantitle,
                        PlanTypeid = r.PlanTypeid,
                        Planyear = r.Planyear,
                        Planremark = r.Planremark,
                        Effectivedate = r.Effectivedate,
                        Enddate = r.Enddate,
                    }).ToList();

                    return new MPlanKpiListApirespone
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
                Console.WriteLine($"[ERROR] Failed to search MPlanKpiList: {ex.Message}");
                return new MPlanKpiListApirespone
                {
                    ResponseCode = "500",
                    ResponseMsg = "Internal Server Error",
                    Timestamp = DateTime.UtcNow,
                    data = new List<MPlanKpiListModels>()
                };
            }
        }


    }
}
