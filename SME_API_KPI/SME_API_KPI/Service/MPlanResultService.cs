using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Repository;
using SME_API_KPI.Services;
using System.Text.Json;

namespace SME_API_KPI.Service
{
    public class MPlanResultService
    {
        private readonly MPlanResultRepository _repository;
        private readonly ICallAPIService _serviceApi;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly string _FlagDev;
        public MPlanResultService(MPlanResultRepository repository, IConfiguration configuration, ICallAPIService serviceApi, IApiInformationRepository repositoryApi)
        {
            _repository = repository;
            _serviceApi = serviceApi;
            _repositoryApi = repositoryApi;
            _FlagDev = configuration["Devlopment:FlagDev"] ?? throw new ArgumentNullException("FlagDev is missing in appsettings.json");

        }

        public async Task<IEnumerable<MPlanResult>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch
            {
                return Enumerable.Empty<MPlanResult>();
            }
        }

        //public async Task<MPlanResult?> GetByIdAsync(int id)
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

        public async Task<bool> AddAsync(MPlanResult entity)
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

        public async Task<bool> UpdateAsync(MPlanResult entity)
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
        public async Task BatchEndOfDay_MPlanResult(searchMPlanResultModels xmodel)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var MPlanResultApirespone = new MPlanResultApirespone();
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
            var result = JsonSerializer.Deserialize<MPlanResultApirespone>(apiResponse, options);

            MPlanResultApirespone = result ?? new MPlanResultApirespone();

            if (MPlanResultApirespone.data != null)
            {
                foreach (var item in MPlanResultApirespone.data)
                {
                    try
                    {
                        var existing = await _repository.GetByIdAsync(xmodel.Planid,xmodel.Kpiid,xmodel.Periodid,xmodel.Assignid,xmodel.Point,xmodel.Result);

                        if (existing == null)
                        {
                            // Create new record
                            var newData = new MPlanResult
                            {
                                Kpiid = xmodel.Kpiid,
                                Planid = xmodel.Planid,
                                Assignid = xmodel.Assignid,
                                Point = xmodel.Point,
                                Result = xmodel.Result,
                                Status = item.Status,
                            };

                            await _repository.AddAsync(newData);
                            Console.WriteLine($"[INFO] Created new MPlanResult with ID {newData.Planid}");
                        }
                        else
                        {
                            // Update existing record


                            existing.Kpiid = xmodel.Kpiid;
                            existing.Planid = xmodel.Planid;
                            existing.Assignid = xmodel.Assignid;
                            existing.Point = xmodel.Point;
                            existing.Result = xmodel.Result;
                            existing.Status = item.Status;


                            await _repository.UpdateAsync(existing);
                            Console.WriteLine($"[INFO] Updated MPlanResult with ID {existing.Planid}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] Failed to process MPlanResult ID {item.Status}: {ex.Message}");
                    }
                }
            }



        }
        public async Task<IEnumerable<MPlanResultApirespone>> GetAllAsyncSearch_MPlanResult(searchMPlanResultModels xmodel)
        {
            try
            {
                // Get data from repository
                var Ldata = await _repository.GetAllAsyncSearch_MPlanResult(xmodel);

                if (Ldata == null || !Ldata.Any())
                {

                    await BatchEndOfDay_MPlanResult(xmodel);

                    var Ldata2 = await _repository.GetAllAsyncSearch_MPlanResult(xmodel);
                    if (Ldata2 == null || !Ldata2.Any())
                    {
                        return Enumerable.Empty<MPlanResultApirespone>();
                    }
                    else
                    {

                        // Replace this block in the GetAllAsyncSearch_MPlanResult method
                        var models2 = Ldata2.Select(r => new MPlanResultModels
                        {
                            Status = r.Status,
                        }).ToList();

                        // Wrap in a single response object
                        var response = new MPlanResultApirespone
                        {
                            ResponseCode = "200",
                            ResponseMsg = "OK",
                            Timestamp = DateTime.UtcNow,
                            data = models2
                        };

                        return new List<MPlanResultApirespone> { response };
                    }

                }
                else
                {
                    // Replace this block in the GetAllAsyncSearch_MPlanResult method
                    var models = Ldata.Select(r => new MPlanResultModels
                        {
                        Status = r.Status,
                    }).ToList();

                    // Wrap in a single response object
                    var response = new MPlanResultApirespone
                    {
                        ResponseCode = "200",
                        ResponseMsg = "OK",
                        Timestamp = DateTime.UtcNow,
                        data = models
                    };

                    return new List<MPlanResultApirespone> { response };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to search MPlanResult: {ex.Message}");
                //    return Enumerable.Empty<MPlanResultApirespone>();
                return null;
            }
        }


    }
}
