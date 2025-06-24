// SME_API_KPI/Service/MPlanBudgetYearService.cs
using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Repository;
using SME_API_KPI.Services;
using System.Text.Json;

public class MPlanBudgetYearService
{
    private readonly MPlanBudgetYearRepository _repository;
    private readonly ICallAPIService _serviceApi;
    private readonly IApiInformationRepository _repositoryApi;
    private readonly string _FlagDev;

    public MPlanBudgetYearService(MPlanBudgetYearRepository repository, IConfiguration configuration, ICallAPIService serviceApi, IApiInformationRepository repositoryApi)

    {
        _repository = repository;
        _serviceApi = serviceApi;
        _repositoryApi = repositoryApi;
        _FlagDev = configuration["Devlopment:FlagDev"] ?? throw new ArgumentNullException("FlagDev is missing in appsettings.json");

    }

    public async Task<IEnumerable<MPlanBudgetYear>> GetAllAsync()
    {
        try
        {
            return await _repository.GetAllAsync();
        }
        catch (Exception)
        {
            return Enumerable.Empty<MPlanBudgetYear>();
        }
    }

    public async Task<MPlanBudgetYear?> GetByIdAsync(int id)
    {
        try
        {
            return await _repository.GetByIdAsync(id);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> AddAsync(MPlanBudgetYear entity)
    {
        try
        {
            return await _repository.AddAsync(entity);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(MPlanBudgetYear entity)
    {
        try
        {
            return await _repository.UpdateAsync(entity);
        }
        catch (Exception)
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
        catch (Exception)
        {
            return false;
        }
    }
    public async Task BatchEndOfDay_year()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };
        var MBudgetYearApirespone = new MBudgetYearApirespone();
        var LApi = await _repositoryApi.GetAllAsync(new MapiInformationModels { ServiceNameCode = "GetYear" });
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
        var result = JsonSerializer.Deserialize<MBudgetYearApirespone>(apiResponse, options);

        MBudgetYearApirespone = result ?? new MBudgetYearApirespone();
        if (MBudgetYearApirespone.data != null)
        {
            foreach (var item in MBudgetYearApirespone.data)
            {
                try
                {
                    // Use the correct property name for year (adjust if your model uses a different casing)
                    var existing = await _repository.GetByIdAsync(item.year);

                    if (existing == null)
                    {
                        // Create new record
                        var newData = new MPlanBudgetYear
                        {
                            Year = item.year,
                        };

                        await _repository.AddAsync(newData);
                        Console.WriteLine($"[INFO] Created new MBudgetYear with Year {newData.Year}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Failed to process MBudgetYear Year {item.year}: {ex.Message}");
                }
            }
        }



    }

    public async Task<MBudgetYearApirespone> GetAllAsyncSearch_Year()
    {
        try
        {
            // ดึงข้อมูลจาก repository
            var lData = await _repository.GetAllAsync();

            if (lData == null || !lData.Any())
            {
                await BatchEndOfDay_year(); // เรียกใช้ BatchEndOfDay_year เพื่อดึงข้อมูลจาก API ถ้าไม่มีข้อมูลใน repository
                lData = await _repository.GetAllAsync();
               
            }
            // Mapping ข้อมูล
            var response = new MBudgetYearApirespone
            {
                ResponseCode = "200",
                ResponseMsg = "OK",
                data = lData.Select(r => new MBudgetYearModels
                {
                    year = r.Year
                }).ToList(),
                Timestamp = DateTime.UtcNow
            };

            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Failed to search RiskFactors: {ex.Message}");
            return new MBudgetYearApirespone
            {
                ResponseCode = "500",
                ResponseMsg = "Error",
                data = new List<MBudgetYearModels>(),
                Timestamp = DateTime.UtcNow
            };
        }
    }



}
