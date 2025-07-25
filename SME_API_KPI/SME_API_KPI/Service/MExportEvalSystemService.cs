using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Repository;
using SME_API_KPI.Services;
using System.Text.Json;

namespace SME_API_KPI.Service
{
    public class MExportEvalSystemService
    {
        private readonly MExportEvalSystemRepository _repository;
        private readonly ICallAPIService _serviceApi;
        private readonly IApiInformationRepository _repositoryApi;
        private readonly string _FlagDev;
        public MExportEvalSystemService(MExportEvalSystemRepository repository, IConfiguration configuration, ICallAPIService serviceApi, IApiInformationRepository repositoryApi)
        {
            _repository = repository;
            _serviceApi = serviceApi;
            _repositoryApi = repositoryApi;
            _FlagDev = configuration["Devlopment:FlagDev"] ?? throw new ArgumentNullException("FlagDev is missing in appsettings.json");

        }

        public async Task<IEnumerable<MExportEval>> GetAllAsync(searchMExportEvalModels models)
        {
            try
            {
                return await _repository.GetAllAsync(models);
            }
            catch
            {
                return Enumerable.Empty<MExportEval>();
            }
        }

        public async Task<MExportEval?> GetByIdAsync(string planid,int seq)
        {
            try
            {
                return await _repository.GetByIdAsync(planid,seq);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MExportEval entity)
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

        public async Task<bool> UpdateAsync(MExportEval entity)
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
        public async Task BatchEndOfDay_MExportEval(searchMExportEvalModels models)
        {
            if (models == null)
            {
                models = new searchMExportEvalModels();
                models.planID = ""; // Default value or adjust as needed
                models.periodId = null; // Default value or adjust as needed
            }
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var MExportEvalApirespone = new MExportEvalApirespone();
            var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "exportEval" });
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

            var apiResponse = await _serviceApi.GetDataApiAsync(apiParam, models);
            var result = JsonSerializer.Deserialize<MExportEvalApirespone>(apiResponse, options);

            MExportEvalApirespone = result ?? new MExportEvalApirespone();

            if (MExportEvalApirespone.data != null)
            {
                foreach (var item in MExportEvalApirespone.data)
                {
                    try
                    {
                        var existing = await _repository.GetByIdAsync(item.PlanId,item.Seq);

                        if (existing == null)
                        {
                            // Create new record
                            var newData = new MExportEval
                            {
                            
                                Seq = item.Seq,
                                EmpCode = item.EmpCode,
                                Fullname = item.Fullname,
                                Position = item.Position,
                                Division = item.Division,
                                Segment = item.Segment,
                                PlanId = item.PlanId,
                                IndvWeight = item.indv_weight,
                                CoreWeight = item.core_weight,
                                ManagerialWeight = item.ManagerialWeight,
                                UserApprove1 = item.UserApprove1,
                                KpiPlanInvidualApproveId1 = item.KpiPlanInvidualApproveId1,
                                UserApprove2 = item.UserApprove2,
                                KpiPlanInvidualApproveId2 = item.KpiPlanInvidualApproveId2

                            };

                            await _repository.AddAsync(newData);
                            Console.WriteLine($"[INFO] Created new MExportEval");
                        }
                        else
                        {
                            // Update existing record
                            existing.Seq = item.Seq;
                            existing.EmpCode = item.EmpCode;
                            existing.Fullname = item.Fullname;
                            existing.Position = item.Position;
                            existing.Division = item.Division;
                            existing.Segment = item.Segment;
                            existing.PlanId = item.PlanId;
                            existing.IndvWeight = item.indv_weight;
                            existing.CoreWeight = item.core_weight;
                            existing.ManagerialWeight = item.ManagerialWeight;
                            existing.UserApprove1 = item.UserApprove1;
                            existing.KpiPlanInvidualApproveId1 = item.KpiPlanInvidualApproveId1;
                            existing.UserApprove2 = item.UserApprove2;
                            existing.KpiPlanInvidualApproveId2 = item.KpiPlanInvidualApproveId2;
                             
                            await _repository.UpdateAsync(existing);
                            Console.WriteLine($"[INFO] Updated MExportEval");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] Failed to process MExportEval");
                    }
                }
            }



        }
        public async Task<MExportEvalApirespone> GetAllAsyncSearch_MExportEval(searchMExportEvalModels models)
        {
            try
            {
                // Get data from repository
                var Ldata = await _repository.GetAllAsync(models);

                if (Ldata == null || !Ldata.Any())
                {

                    await BatchEndOfDay_MExportEval(models);

                    var Ldata2 = await _repository.GetAllAsync(models);
                    if (Ldata2 == null || !Ldata2.Any())
                    {
                        return new MExportEvalApirespone
                        {
                            status =200,
                            message = "No Data Found",
                            Timestamp = DateTime.UtcNow,
                            data = new List<MExportEvalModels>()
                        };
                    }
                    else
                    {

                        // Replace this block in the GetAllAsyncSearch_MExportEval method
                        var models2 = Ldata2.Select(r => new MExportEvalModels
                        {
                          
                            Seq = r.Seq,
                            EmpCode = r.EmpCode,
                            Fullname = r.Fullname,
                            Position = r.Position,
                            Division = r.Division,
                            Segment = r.Segment,
                            PlanId = r.PlanId,
                            indv_weight = r.IndvWeight,
                            core_weight = r.CoreWeight,
                            ManagerialWeight = r.ManagerialWeight,
                            UserApprove1 = r.UserApprove1,
                            KpiPlanInvidualApproveId1 = r.KpiPlanInvidualApproveId1,
                            UserApprove2 = r.UserApprove2,
                            KpiPlanInvidualApproveId2 = r.KpiPlanInvidualApproveId2


                        }).ToList();

                        // Wrap in a single response object
                        var response = new MExportEvalApirespone
                        {
                            status = 200,
                            message = "OK",
                            Timestamp = DateTime.UtcNow,
                            data = models2
                        };

                        return response;
                    }

                }
                else
                {
                    // Replace this block in the GetAllAsyncSearch_MExportEval method
                    var modelsx = Ldata.Select(r => new MExportEvalModels
                    {
             
                        Seq = r.Seq,
                        EmpCode = r.EmpCode,
                        Fullname = r.Fullname,
                        Position = r.Position,
                        Division = r.Division,
                        Segment = r.Segment,
                        PlanId = r.PlanId,
                        indv_weight = r.IndvWeight,
                        core_weight = r.CoreWeight,
                        ManagerialWeight = r.ManagerialWeight,
                        UserApprove1 = r.UserApprove1,
                        KpiPlanInvidualApproveId1 = r.KpiPlanInvidualApproveId1,
                        UserApprove2 = r.UserApprove2,
                        KpiPlanInvidualApproveId2 = r.KpiPlanInvidualApproveId2
                    }).ToList();

                    // Wrap in a single response object
                    var response = new MExportEvalApirespone
                    {
                        status = 200,
                        message = "OK",
                        Timestamp = DateTime.UtcNow,
                        data = modelsx
                    };

                    return response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to search MDivision: {ex.Message}");
                return new MExportEvalApirespone
                {
                    status = 500,
                    message = "Internal Server Error",
                    Timestamp = DateTime.UtcNow,
                    data = new List<MExportEvalModels>()
                };
            }
        }


    }
}
