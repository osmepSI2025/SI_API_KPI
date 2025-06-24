using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Repository;
using SME_API_KPI.Services;
using System.Text.Json;

namespace SME_API_KPI.Service
{
    public class MInputFormateService
    {
        private readonly MInputFormateRepository _repository;
        private readonly ICallAPIService _serviceApi;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly string _FlagDev;
        public MInputFormateService(MInputFormateRepository repository, IConfiguration configuration, ICallAPIService serviceApi, IApiInformationRepository repositoryApi)
        {
            _repository = repository;
             _serviceApi = serviceApi;
            _repositoryApi = repositoryApi;
            _FlagDev = configuration["Devlopment:FlagDev"] ?? throw new ArgumentNullException("FlagDev is missing in appsettings.json");

        }

        public async Task<IEnumerable<MInputFormate>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch
            {
                return Enumerable.Empty<MInputFormate>();
            }
        }

        public async Task<MInputFormate?> GetByIdAsync(int id)
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

        public async Task<bool> AddAsync(MInputFormate entity)
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

        public async Task<bool> UpdateAsync(MInputFormate entity)
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
        public async Task BatchEndOfDay_MInputFormate()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var MInputFormateApirespone = new MInputFormateApirespone();
            var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "GetStatus" });
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
            var result = JsonSerializer.Deserialize<MInputFormateApirespone>(apiResponse, options);

            MInputFormateApirespone = result ?? new MInputFormateApirespone();

            if (MInputFormateApirespone.data != null)
            {
                foreach (var item in MInputFormateApirespone.data)
                {
                    try
                    {
                        var existing = await _repository.GetByIdAsync(item.Masterid);

                        if (existing == null)
                        {
                            // Create new record
                            var newData = new MInputFormate
                            {
                                Description = item.Description,
                                Masterid = item.Masterid,
                            };

                            await _repository.AddAsync(newData);
                            Console.WriteLine($"[INFO] Created new MInputFormate with ID {newData.Masterid}");
                        }
                        else
                        {
                            // Update existing record


                            existing.Description = item.Description;
                            existing.Masterid = item.Masterid;

                            await _repository.UpdateAsync(existing);
                            Console.WriteLine($"[INFO] Updated MInputFormate with ID {existing.Masterid}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] Failed to process MInputFormate ID {item.Masterid}: {ex.Message}");
                    }
                }
            }



        }
        public async Task<MInputFormateApirespone> GetAllAsyncSearch_MInputFormate()
        {
            try
            {
                // Get data from repository
                var Ldata = await _repository.GetAllAsync();

                if (Ldata == null || !Ldata.Any())
                {
                    await BatchEndOfDay_MInputFormate();

                    var Ldata2 = await _repository.GetAllAsync();
                    if (Ldata2 == null || !Ldata2.Any())
                    {
                        return new MInputFormateApirespone
                        {
                             ResponseCode = "200",
                            ResponseMsg = "No data found",
                            Timestamp = DateTime.UtcNow,
                            data = new List<MInputFormateModels>()
                        };
                    }
                    else
                    {
                        var models2 = Ldata2.Select(r => new MInputFormateModels
                        {
                            Masterid = r.Masterid,
                            Description = r.Description
                        }).ToList();

                        return new MInputFormateApirespone
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
                    var models = Ldata.Select(r => new MInputFormateModels
                    {
                        Masterid = r.Masterid,
                        Description = r.Description
                    }).ToList();

                    return new MInputFormateApirespone
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
                Console.WriteLine($"[ERROR] Failed to search MInputFormate: {ex.Message}");
                return new MInputFormateApirespone
                {
                    ResponseCode = "500",
                    ResponseMsg = "Internal Server Error",
                    Timestamp = DateTime.UtcNow,
                    data = new List<MInputFormateModels>()
                };
            }
        }


    }
}
