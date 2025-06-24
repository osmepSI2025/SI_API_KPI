using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Repository;
using SME_API_KPI.Services;
using System.Text.Json;

namespace SME_API_KPI.Service
{
    public class MMeasureService
    {
        private readonly MMeasureRepository _repository;
        private readonly ICallAPIService _serviceApi;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly string _FlagDev;
        public MMeasureService(MMeasureRepository repository, IConfiguration configuration, ICallAPIService serviceApi, IApiInformationRepository repositoryApi)
        {
            _repository = repository;
             _serviceApi = serviceApi;
            _repositoryApi = repositoryApi;
            _FlagDev = configuration["Devlopment:FlagDev"] ?? throw new ArgumentNullException("FlagDev is missing in appsettings.json");

        }

        public async Task<IEnumerable<MMeasure>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch
            {
                return Enumerable.Empty<MMeasure>();
            }
        }

        public async Task<MMeasure?> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MMeasure entity)
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

        public async Task<bool> UpdateAsync(MMeasure entity)
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
        public async Task BatchEndOfDay_MMeasure()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var MMeasureApirespone = new MMeasureApirespone();
     
            var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "Getmeasure" });
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

            var apiResponse = await _serviceApi.GetDataApiAsync(apiParam, null);
            var result = JsonSerializer.Deserialize<MMeasureApirespone>(apiResponse, options);

            MMeasureApirespone = result ?? new MMeasureApirespone();
            if (MMeasureApirespone.data != null)
            {
                foreach (var item in MMeasureApirespone.data)
                {
                    try
                    {
                        var existing = await _repository.GetByIdAsync(item.Masterid);

                        if (existing == null)
                        {
                            // Create new record
                            var newData = new MMeasure
                            {
                                Description = item.Description,
                                Masterid = item.Masterid,
                            };

                            await _repository.AddAsync(newData);
                            Console.WriteLine($"[INFO] Created new MMeasure with ID {newData.Masterid}");
                        }
                        else
                        {
                            // Update existing record


                            existing.Description = item.Description;
                            existing.Masterid = item.Masterid;

                            await _repository.UpdateAsync(existing);
                            Console.WriteLine($"[INFO] Updated MMeasure with ID {existing.Masterid}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] Failed to process MMeasure ID {item.Masterid}: {ex.Message}");
                    }
                }
            }



        }
        public async Task<MMeasureApirespone> GetAllAsyncSearch_MMeasure()
        {
            try
            {
                // Get data from repository
                var Ldata = await _repository.GetAllAsync();

                if (Ldata == null || !Ldata.Any())
                {
                    await BatchEndOfDay_MMeasure();

                    var Ldata2 = await _repository.GetAllAsync();
                    if (Ldata2 == null || !Ldata2.Any())
                    {
                        return new MMeasureApirespone
                        {
                            ResponseCode = "202",
                            ResponseMsg = "No data found",
                            Timestamp = DateTime.UtcNow,
                            data = new List<MMeasureModels>()
                        };
                    }
                    else
                    {
                        var models2 = Ldata2.Select(r => new MMeasureModels
                        {
                            Masterid = r.Masterid,
                            Description = r.Description
                        }).ToList();

                        return new MMeasureApirespone
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
                    var models = Ldata.Select(r => new MMeasureModels
                    {
                        Masterid = r.Masterid,
                        Description = r.Description
                    }).ToList();

                    return new MMeasureApirespone
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
                Console.WriteLine($"[ERROR] Failed to search MMeasure: {ex.Message}");
                return new MMeasureApirespone
                {
                    ResponseCode = "500",
                    ResponseMsg = "Internal Server Error",
                    Timestamp = DateTime.UtcNow,
                    data = new List<MMeasureModels>()
                };
            }
        }


    }
}
