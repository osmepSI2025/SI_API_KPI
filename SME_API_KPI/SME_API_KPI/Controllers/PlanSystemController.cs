using Microsoft.AspNetCore.Mvc;
using SME_API_KPI.Models;
using SME_API_KPI.Service;

namespace SME_API_KPI.Controllers
{
    [ApiController]
    [Route("api/SYS-KPI")]
    public class PlanSystemController : ControllerBase
    {
         private readonly MPlanPeriodService _mPlanPeriodService;
        private readonly MPlanTargetDescriptionService _mPlanTargetDescriptionService;
        private readonly MPlanKpiDescriptionService _mPlanKpiDescriptionService;
      
       
        private readonly MPlanResultService _mPlanResultService;
        private readonly MPlanKpiListService _mPlanKpiListService;

        private readonly MPlanKpiService _mPlanKpiService;
        // Uncomment if you need to use MPlanNameService
        //private readonly MPlanNameService _mPlanNameService;

        public PlanSystemController(
            MPlanPeriodService mPlanPeriodService,
            MPlanTargetDescriptionService mPlanTargetService,
            MPlanKpiDescriptionService mPlanKpiDescriptionService

          , MPlanResultService mPlanResultService
            , MPlanKpiListService mPlanKpiListService

            , MPlanKpiService mPlanKpiService

            )
        {
          
              _mPlanPeriodService = mPlanPeriodService;
            _mPlanTargetDescriptionService = mPlanTargetService;
            _mPlanKpiDescriptionService = mPlanKpiDescriptionService;


            _mPlanResultService = mPlanResultService;
            _mPlanKpiListService = mPlanKpiListService;

            _mPlanKpiService = mPlanKpiService;
        }
        [HttpPost("PlanSystem/Getlist")]
        public async Task<IActionResult> Getlist(searchMPlanKpiListModels models )
        {
           

            var result = await _mPlanKpiListService.GetAllAsyncSearch_MPlanKpiList(models);
            return Ok(result);
        }
        [HttpGet("PlanSystem/Batch-Getlist")]
        public async Task<IActionResult> Batch_Getlist()
        {
            searchMPlanKpiListModels models = new searchMPlanKpiListModels();

           await _mPlanKpiListService.BatchEndOfDay_MPlanKpiList(models);
            return Ok();
        }


        //ค่อยกลับมาทำ
        [HttpGet("PlanSystem/getPeriod")]
        public async Task<IActionResult> GetPeriod(int planYear, string planTypeId, int periodID)
        {

            searchMPlanPeriodModels models = new searchMPlanPeriodModels
            {
                PlanYear = planYear,
                PlanTypeId = planTypeId,
                PeriodId = periodID
            };
            var result = await _mPlanPeriodService.GetAllAsyncSearch_MPlanPeriod(models);
            return Ok(result);
        }
        //[HttpGet("PlanSystem/Batch-getPeriod")]
        //public async Task<IActionResult> Batch_GetPeriod()
        //{
        //    var models = new searchMPlanPeriodModels
        //    {
        //        PlanYear = 0,
        //        PlanTypeId = "",
        //         PeriodId =null    
        //    };

        //    await _mPlanPeriodService.BatchEndOfDay_MPlanPeriod(models);
        //    return Ok();
        //}
        ////ค่อยกลับมาทำ





        [HttpPost("PlanSystem/GetKPI")]
        public async Task<IActionResult> GetKPI(searchMPlanKpiModels models)
        {
            var result = await _mPlanKpiService.GetAllAsyncSearch_MPlanKpi(models);
            return Ok(result);
        }
        //[HttpGet("PlanSystem/batch-GetKPI")]
        //public async Task<IActionResult> BatchEndOfDay_GetKPI(int planid, int dimensionid)
        //{
        //    var models = new searchMPlanKpiModels
        //    {
        //        Planid = planid,
        //        dimensionid = dimensionid
        //    };


        //    await _mPlanKpiService.BatchEndOfDay_MPlanKpi(models);
        //    return Ok();
        //}




        [HttpGet("PlanSystem/gettargetdescription")]
        public async Task<IActionResult> gettargetdescription(string planid, string kpiid)
        {
            var models = new searchMPlanTargetDescriptionModels
            {
                Planid = planid,
                Kpiid = kpiid
            };


            var result = await _mPlanTargetDescriptionService.GetAllAsyncSearch_MPlanTargetDescription(models);
            return Ok(result);
        }



        [HttpGet("PlanSystem/getkpidescription")]
        public async Task<IActionResult> getkpidescription(string planid, string kpiid)
        {
            var models = new searchMPlanKpiDescriptionModels
            {
                Planid = planid,
                Kpiid = kpiid
                
            };


            var result = await _mPlanKpiDescriptionService.GetAllAsyncSearch_MPlanKpiDescription(models);
            return Ok(result);
        }

    }


}
