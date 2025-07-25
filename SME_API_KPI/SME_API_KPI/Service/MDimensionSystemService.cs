using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Repository;
using SME_API_KPI.Services;
using System.Text.Json;

namespace SME_API_KPI.Service
{
    public class MDimensionSystemService
    {
        private readonly MDimensionSystemRepository _repository;
        private readonly ICallAPIService _serviceApi;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly string _FlagDev;
        public MDimensionSystemService(MDimensionSystemRepository repository, IConfiguration configuration, ICallAPIService serviceApi, IApiInformationRepository repositoryApi)
        {
            _repository = repository;
            _serviceApi = serviceApi;
            _repositoryApi = repositoryApi;
            _FlagDev = configuration["Devlopment:FlagDev"] ?? throw new ArgumentNullException("FlagDev is missing in appsettings.json");

        }

        public async Task<IEnumerable<MDimensionSystem>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch
            {
                return Enumerable.Empty<MDimensionSystem>();
            }
        }

        public async Task<MDimensionSystem?> GetByIdAsync(int id)
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

        public async Task<bool> AddAsync(MDimensionSystem entity)
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

        public async Task<bool> UpdateAsync(MDimensionSystem entity)
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
        public async Task BatchEndOfDay_MDimensionSystem()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var MDimensionSystemApirespone = new MDimensionSystemApirespone();
            var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "getdimenson" });
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
            var result = JsonSerializer.Deserialize<MDimensionSystemApirespone>(apiResponse, options);

            MDimensionSystemApirespone = result ?? new MDimensionSystemApirespone();

            if (MDimensionSystemApirespone.data != null)
            {
                foreach (var item in MDimensionSystemApirespone.data)
                {
                    try
                    {
                        var existing = await _repository.GetByIdAsync(item.Dimensionid);

                        if (existing == null)
                        {
                            // Create new record
                            var newData = new MDimensionSystem
                            {
                                Dimensionname = item.Dimensionname,
                                Dimensionid = item.Dimensionid,
                                 Plantypeid = item.Plantypeid,
                            };

                            await _repository.AddAsync(newData);
                            Console.WriteLine($"[INFO] Created new MDimensionSystem with ID {newData.Dimensionid}");
                        }
                        else
                        {
                            // Update existing record


                            existing.Dimensionname = item.Dimensionname;
                            existing.Dimensionid = item.Dimensionid;
                            existing.Plantypeid = item.Plantypeid;

                            await _repository.UpdateAsync(existing);
                            Console.WriteLine($"[INFO] Updated MDimensionSystem with ID {existing.Dimensionid}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] Failed to process MDimensionSystem ID {item.Dimensionid}: {ex.Message}");
                    }
                }
            }



        }
        public async Task<MDimensionSystemApirespone> GetAllAsyncSearch_MDimensionSystem()
        {
            try
            {
                // Get data from repository
                var Ldata = await _repository.GetAllAsync();

                if (Ldata == null || !Ldata.Any())
                {

                    await BatchEndOfDay_MDimensionSystem();

                    var Ldata2 = await _repository.GetAllAsync();
                    if (Ldata2 == null || !Ldata2.Any())
                    {
                        return new MDimensionSystemApirespone
                        {
                            ResponseCode = "200",
                            ResponseMsg = "No Data Found",
                            Timestamp = DateTime.UtcNow,
                            data = new List<MDimensionSystemModels>()
                        };
                    }
                    else
                    {

                        // Replace this block in the GetAllAsyncSearch_MDimensionSystem method
                        var models2 = Ldata2.Select(r => new MDimensionSystemModels
                        {
                            Dimensionid = r.Dimensionid,
                            Dimensionname = r.Dimensionname,
                             Plantypeid = r.Plantypeid


                        }).ToList();

                        // Wrap in a single response object
                        var response = new MDimensionSystemApirespone
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
                    // Replace this block in the GetAllAsyncSearch_MDimensionSystem method
                    var models = Ldata.Select(r => new MDimensionSystemModels
                    {
                        Dimensionid = r.Dimensionid,
                        Dimensionname = r.Dimensionname
                        , Plantypeid = r.Plantypeid
                    }).ToList();

                    // Wrap in a single response object
                    var response = new MDimensionSystemApirespone
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
                Console.WriteLine($"[ERROR] Failed to search MDivision: {ex.Message}");
                return new MDimensionSystemApirespone
                {
                    ResponseCode = "500",
                    ResponseMsg = "Internal Server Error",
                    Timestamp = DateTime.UtcNow,
                    data = new List<MDimensionSystemModels>()
                };
            }
        }


    }
}
