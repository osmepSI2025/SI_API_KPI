using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SME_API_KPI.Entities;
using SME_API_KPI.Models;
using SME_API_KPI.Service;
using SME_API_KPI.Services;
using System;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

public class JobSchedulerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public JobSchedulerService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<KPIDBContext>();
                var now = DateTime.Now;
                var jobs = await db.MScheduledJobs
                    .Where(j => j.IsActive == true && j.RunHour == now.Hour && j.RunMinute == now.Minute)
                    .ToListAsync(stoppingToken);

                foreach (var job in jobs)
                {
                    _ = RunJobAsync(job.JobName, scope.ServiceProvider);
                }
            }

            // Check every minute
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private async Task RunJobAsync(string jobName, IServiceProvider serviceProvider)
    {
        switch (jobName)
        {
            case "getdimenson":
                await serviceProvider.GetRequiredService<MDimensionSystemService>().BatchEndOfDay_MDimensionSystem();
                break;
            case "getdivision":
                await serviceProvider.GetRequiredService<MDivisionService>().BatchEndOfDay_MDivision();
                break;

            case "GetStatus":
                await serviceProvider.GetRequiredService<MStatusService>().BatchEndOfDay_Mstatus();
                break;
            case "GetYear":
                await serviceProvider.GetRequiredService<MPlanBudgetYearService>().BatchEndOfDay_year();
                break;
            case "GetkpiType":
                await serviceProvider.GetRequiredService<MKpiTypeService>().BatchEndOfDay_MKpiType();
                break;
            case "Getmeasure":
                await serviceProvider.GetRequiredService<MMeasureService>().BatchEndOfDay_MMeasure();
                break;
            case "inputFormate":
                await serviceProvider.GetRequiredService<MInputFormateService>().BatchEndOfDay_MInputFormate();
                break;


            case "GetKpiTarget":
                var mkpit = new searchMPlanKpiTargetModels
                {
                    Planid = "",
                    Kpiid = ""
                };
            //    await serviceProvider.GetRequiredService<MPlanKpiTargetService>().BatchEndOfDay_MPlanKpiTarget(mkpit);
                break;
            case "GetKPI":
                var mkpi = new searchMPlanKpiModels
                {
                    Planid = "",
                     dimensionid = 0
                };
                //await serviceProvider.GetRequiredService<MPlanKpiService>().BatchEndOfDay_MPlanKpi(mkpi);
                break;
            case "getPeriod":
                var mpp = new searchMPlanPeriodModels
                {
                    PlanYear = 0,
                    PlanTypeId = "",
                    PeriodId = null
                };

               await serviceProvider.GetRequiredService<MPlanPeriodService>().BatchEndOfDay_MPlanPeriod(mpp);
                break;
            case "gettargetdescription":
                var mptd = new searchMPlanTargetDescriptionModels
                {
                    Planid = "",
                    Kpiid = ""
                };

                await serviceProvider.GetRequiredService<MPlanTargetDescriptionService>().BatchEndOfDay_MPlanTargetDescription(mptd);
                break;

            case "getkpidescription":
                var mpkd = new searchMPlanKpiDescriptionModels
                {
                    Planid = "",
                    Kpiid = ""
                };

                await serviceProvider.GetRequiredService<MPlanKpiDescriptionService>().BatchEndOfDay_MPlanKpiDescription(mpkd);
                break;

            case "GetKpiAssign":
                var mpa = new searchMPlanKpiDescriptionModels
                {
                    Planid = "",
                    Kpiid = ""
                };

                await serviceProvider.GetRequiredService<MPlanKpiDescriptionService>().BatchEndOfDay_MPlanKpiDescription(mpa);
                break;

            case "Getweight":
                var mw = new searchMPlanweightModels
                {
                    Planid = "",
                    Kpiid = ""
                };

                await serviceProvider.GetRequiredService<MKpiSystemWeightService>().BatchEndOfDay_MPlanweight(mw);
                break;
            case "exportEval":
                var mwexportEval = new searchMExportEvalModels
                {
                     periodId = 0,
                     planID = ""
                };

                await serviceProvider.GetRequiredService<MExportEvalSystemService>().BatchEndOfDay_MExportEval(mwexportEval);
                break;
            default:
                // Optionally log unknown job
                break;
        }
    }
}