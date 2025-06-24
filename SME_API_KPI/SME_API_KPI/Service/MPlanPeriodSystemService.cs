using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Repository;
using SME_API_KPI.Services;
using System.Numerics;
using System.Text.Json;

namespace SME_API_KPI.Service
{
    public class MPlanPeriodService
    {
        private readonly MPlanPeriodRepository _repository;
        private readonly ICallAPIService _serviceApi;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly string _FlagDev;
        public MPlanPeriodService(MPlanPeriodRepository repository, IConfiguration configuration, ICallAPIService serviceApi, IApiInformationRepository repositoryApi)
        {
            _repository = repository;
            _serviceApi = serviceApi;
            _repositoryApi = repositoryApi;
            _FlagDev = configuration["Devlopment:FlagDev"] ?? throw new ArgumentNullException("FlagDev is missing in appsettings.json");

        }

        public async Task<IEnumerable<MPlanPeriod>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch
            {
                return Enumerable.Empty<MPlanPeriod>();
            }
        }

        //public async Task<MPlanPeriod?> GetByIdAsync(int id)
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

        public async Task<bool> AddAsync(MPlanPeriod entity)
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

        public async Task<bool> UpdateAsync(MPlanPeriod entity)
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
        public async Task BatchEndOfDay_MPlanPeriod(searchMPlanPeriodModels xmodels)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var MPlanPeriodApirespone = new MPlanPeriodApirespone();
            var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "getPeriod" });
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

            var apiResponse = await _serviceApi.GetDataApiAsync(apiParam, xmodels);
            var result = JsonSerializer.Deserialize<MPlanPeriodApirespone>(apiResponse, options);

            MPlanPeriodApirespone = result ?? new MPlanPeriodApirespone();
            if (MPlanPeriodApirespone.data != null)
            {
                foreach (var item in MPlanPeriodApirespone.data)
                {
                    try
                    {
                        var existing = await _repository.GetByIdAsync(item.Year,item.Planid,item.Userid);

                        if (existing == null)
                        {
                            // Create new record
                            var newData = new MPlanPeriod
                            {
                                PeriodId = xmodels.PeriodId,
                                PlanYear = item.Year,
                                PlanTypeId = xmodels.PlanTypeId,
                                Planid = item.Planid,
                                Startdate = item.Startdate,
                                Enddate = item.Enddate,
                                Userid = item.Userid,
                                Period = item.Period,

                            };

                            await _repository.AddAsync(newData);
                            Console.WriteLine($"[INFO] Created new MPlanPeriod with ID {newData.Planid}");
                        }
                        else
                        {
                            // Update existing record

                            existing.PeriodId = xmodels.PeriodId;
                            existing.PlanYear = item.Year;
                            existing.PlanTypeId = xmodels.PlanTypeId;
                            existing.Planid = item.Planid;
                            existing.Startdate = item.Startdate;
                            existing.Enddate = item.Enddate;
                            existing.Userid = item.Userid;
                            existing.Period = item.Period;
                            await _repository.UpdateAsync(existing);
                            Console.WriteLine($"[INFO] Updated MPlanPeriod with ID {existing.Planid}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] Failed to process MPlanPeriod ID {item.Planid}: {ex.Message}");
                    }
                }
            }



        }
        public async Task<MPlanPeriodApirespone> GetAllAsyncSearch_MPlanPeriod(searchMPlanPeriodModels xmodels)
        {
            try
            {
                // Get data from repository
                var Ldata = await _repository.GetAllAsyncSearch_MPlanPeriod(xmodels);

                if (Ldata == null || !Ldata.Any())
                {
                    await BatchEndOfDay_MPlanPeriod(xmodels);

                    var Ldata2 = await _repository.GetAllAsyncSearch_MPlanPeriod(xmodels);
                    if (Ldata2 == null || !Ldata2.Any())
                    {
                        return new MPlanPeriodApirespone
                        {
                             ResponseCode = "200",
                            ResponseMsg = "No data found",
                            Timestamp = DateTime.UtcNow,
                            data = new List<MPlanPeriodModels>()
                        };
                    }
                    else
                    {
                        var models2 = Ldata2.Select(r => new MPlanPeriodModels
                        {
                            Fullname = r.Fullname,
                            Planid = r.Planid,
                            PlanTypeId = r.PlanTypeId,
                            Startdate = r.Startdate,
                            Enddate = r.Enddate,
                            Userid = r.Userid,
                            Period = r.Period,
                            Year = r.PlanYear,
                        }).ToList();

                        var response = new MPlanPeriodApirespone
                        {
                            ResponseCode = "200",
                            ResponseMsg = "OK",
                            Timestamp = DateTime.UtcNow,
                            data = models2
                        };

                        return response;
                    }
                }
                else
                {
                    var models = Ldata.Select(r => new MPlanPeriodModels
                    {
                        Fullname = r.Fullname,
                        Planid = r.Planid,
                        PlanTypeId = r.PlanTypeId,
                        Startdate = r.Startdate,
                        Enddate = r.Enddate,
                        Userid = r.Userid,
                        Period = r.Period,
                        Year = r.PlanYear,
                    }).ToList();

                    var response = new MPlanPeriodApirespone
                    {
                        ResponseCode = "200",
                        ResponseMsg = "OK",
                        Timestamp = DateTime.UtcNow,
                        data = models
                    };

                    return response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to search MPlanPeriod: {ex.Message}");
                return new MPlanPeriodApirespone
                {
                    ResponseCode = "500",
                    ResponseMsg = "Internal Server Error",
                    Timestamp = DateTime.UtcNow,
                    data = new List<MPlanPeriodModels>()
                };
            }
        }


    }
}
