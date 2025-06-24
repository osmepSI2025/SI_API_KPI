using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Repository;
using SME_API_KPI.Services;
using System.Text.Json;

namespace SME_API_KPI.Service
{
    public class MDivisionService
    {
        private readonly MDivisionRepository _repository;
        private readonly ICallAPIService _serviceApi;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly string _FlagDev;
        public MDivisionService(MDivisionRepository repository, IConfiguration configuration, ICallAPIService serviceApi, IApiInformationRepository repositoryApi)
        {
            _repository = repository;
            _serviceApi = serviceApi;
            _repositoryApi = repositoryApi;
            _FlagDev = configuration["Devlopment:FlagDev"] ?? throw new ArgumentNullException("FlagDev is missing in appsettings.json");

        }

        public async Task<IEnumerable<MDivision>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch
            {
                return Enumerable.Empty<MDivision>();
            }
        }

        public async Task<MDivision?> GetByIdAsync(int id)
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

        public async Task<bool> AddAsync(MDivision entity)
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

        public async Task<bool> UpdateAsync(MDivision entity)
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
        public async Task BatchEndOfDay_MDivision()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var MDivisionApirespone = new MDivisionApirespone();
            var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "getdivision" });
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
            var result = JsonSerializer.Deserialize<MDivisionApirespone>(apiResponse, options);

            MDivisionApirespone = result ?? new MDivisionApirespone();

            if (MDivisionApirespone.data != null)
            {
                foreach (var item in MDivisionApirespone.data)
                {
                    try
                    {
                        var existing = await _repository.GetByIdAsync(item.Divisionid);

                        if (existing == null)
                        {
                            // Create new record
                            var newData = new MDivision
                            {
                                 Divisionname = item.Divisionname,
                                Divisioncode = item.Divisioncode,
                                Divisionid = item.Divisionid,
                            };

                            await _repository.AddAsync(newData);
                            Console.WriteLine($"[INFO] Created new MDivision with ID {newData.Divisionid}");
                        }
                        else
                        {
                            // Update existing record


                            existing.Divisionname = item.Divisionname;
                            existing.Divisioncode = item.Divisioncode;
                            existing.Divisionid = item.Divisionid;

                            await _repository.UpdateAsync(existing);
                            Console.WriteLine($"[INFO] Updated MDivision with ID {existing.Divisionid}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] Failed to process MDivision ID {item.Divisionid}: {ex.Message}");
                    }
                }
            }



        }
        public async Task<MDivisionApirespone> GetAllAsyncSearch_MDivision()
        {
            try
            {
                // Get data from repository
                var Ldata = await _repository.GetAllAsync();

                if (Ldata == null || !Ldata.Any())
                {
                    await BatchEndOfDay_MDivision();

                    var Ldata2 = await _repository.GetAllAsync();
                    if (Ldata2 == null || !Ldata2.Any())
                    {
                        return new MDivisionApirespone
                        {
                            ResponseCode = "200",
                            ResponseMsg = "No data found",
                            Timestamp = DateTime.UtcNow,
                            data = new List<MDivisionModels>()
                        };
                    }
                    else
                    {
                        var models2 = Ldata2.Select(r => new MDivisionModels
                        {
                            Divisionid = r.Divisionid,
                            Divisioncode = r.Divisioncode,
                            Divisionname = r.Divisionname,
                        }).ToList();

                        return new MDivisionApirespone
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
                    var models = Ldata.Select(r => new MDivisionModels
                    {
                        Divisionid = r.Divisionid,
                        Divisioncode = r.Divisioncode,
                        Divisionname = r.Divisionname,
                    }).ToList();

                    return new MDivisionApirespone
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
                Console.WriteLine($"[ERROR] Failed to search MDivision: {ex.Message}");
                return new MDivisionApirespone
                {
                    ResponseCode = "500",
                    ResponseMsg = "Internal Server Error",
                    Timestamp = DateTime.UtcNow,
                    data = new List<MDivisionModels>()
                };
            }
        }


    }
}
