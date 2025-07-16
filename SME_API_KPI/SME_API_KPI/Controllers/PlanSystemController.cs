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
        private readonly MPlanTargetDescriptionService  _mPlanTargetDescriptionService;
        private readonly MPlanKpiDescriptionService _mPlanKpiDescriptionService;
        private readonly MPlanKpiAssignService _mPlanKpiAssignService;
        private readonly MPlanweightService _mPlanweightService;    
        private readonly MPlanResultService _mPlanResultService;
        private readonly MPlanKpiListService _mPlanKpiListService;
        private readonly MPlanKpiTargetService  _mPlanKpiTargetService;
        private readonly MPlanKpiService _mPlanKpiService;
        // Uncomment if you need to use MPlanNameService
        //private readonly MPlanNameService _mPlanNameService;

        public PlanSystemController(MPlanPeriodService mPlanPeriodService
             , MPlanTargetDescriptionService mPlanTargetService
            , MPlanKpiDescriptionService mPlanKpiDescriptionService
            , MPlanKpiAssignService mPlanKpiAssignService, MPlanweightService mPlanweightService, MPlanResultService mPlanResultService
            , MPlanKpiListService mPlanKpiListService, MPlanKpiTargetService mPlanKpiTargetService
            , MPlanKpiService mPlanKpiService

            )
        {
            // _mPlanNameService = mPlanNameService;
            _mPlanPeriodService = mPlanPeriodService;
            _mPlanTargetDescriptionService = mPlanTargetService;
            _mPlanKpiDescriptionService = mPlanKpiDescriptionService;
            _mPlanKpiAssignService = mPlanKpiAssignService;
            _mPlanweightService = mPlanweightService;
            _mPlanResultService = mPlanResultService;
            _mPlanKpiListService = mPlanKpiListService;
            _mPlanKpiTargetService = mPlanKpiTargetService;
            _mPlanKpiService = mPlanKpiService;
        }
        [HttpPost("PlanSystem/Getlist")]
        public async Task<IActionResult> Getlist(searchMPlanKpiListModels models )
        {
           

            var result = await _mPlanKpiListService.GetAllAsyncSearch_MPlanKpiList(models);
            return Ok(result);
        }


        [HttpGet("PlanSystem/GetKpiTarget")]
        public async Task<IActionResult> GetKpiTarget(int planid, int kpiid)
        {
            var models = new searchMPlanKpiTargetModels
            {
                Planid = planid,
                Kpiid = kpiid
            };


            var result = await _mPlanKpiTargetService.GetAllAsyncSearch_MPlanKpiTarget(models);
            return Ok(result);
        }
        [HttpGet("PlanSystem/batch-MPlanKpiTarget")]
        public async Task<IActionResult> BatchEndOfDay_MPlanKpiTarget(int planid, int kpiid)
        {
            var models = new searchMPlanKpiTargetModels
            {
                Planid = planid,
                Kpiid = kpiid
            };


            await _mPlanKpiTargetService.BatchEndOfDay_MPlanKpiTarget(models);
            return Ok();
        }



        [HttpPost("PlanSystem/GetKPI")]
        public async Task<IActionResult> GetKPI(searchMPlanKpiModels models)
        {
            var result = await _mPlanKpiService.GetAllAsyncSearch_MPlanKpi(models);
            return Ok(result);
        }
        [HttpGet("PlanSystem/batch-GetKPI")]
        public async Task<IActionResult> BatchEndOfDay_GetKPI(int planid, int dimensionid)
        {
            var models = new searchMPlanKpiModels
            {
                Planid = planid,
                dimensionid = dimensionid
            };


            await _mPlanKpiService.BatchEndOfDay_MPlanKpi(models);
            return Ok();
        }

        [HttpGet("PlanSystem/getPeriod")]
        public async Task<IActionResult> GetPeriod(int planYear, string planTypeId, int periodID)
        {
            var models = new searchMPlanPeriodModels
            {
                PlanYear = planYear,
                PlanTypeId = planTypeId,
                PeriodId = periodID
            };

            var result = await _mPlanPeriodService.GetAllAsyncSearch_MPlanPeriod(models);
            return Ok(result);
        }


        [HttpGet("PlanSystem/gettargetdescription")]
        public async Task<IActionResult> gettargetdescription(int planid, int kpiid)
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
        public async Task<IActionResult> getkpidescription(int planid, int kpiid)
        {
            var models = new searchMPlanKpiDescriptionModels
            {
                Planid = planid,
                Kpiid = kpiid
                
            };


            var result = await _mPlanKpiDescriptionService.GetAllAsyncSearch_MPlanKpiDescription(models);
            return Ok(result);
        }

        [HttpGet("PlanSystem/GetKpiAssign")]
        public async Task<IActionResult> GetKpiAssign(int planid, int kpiid)
        {
            var models = new searchMPlanKpiAssignModels
            {
                Planid = planid,
                Kpiid = kpiid
            };


            var result = await _mPlanKpiAssignService.GetAllAsyncSearch_MPlanKpiAssign(models);
            return Ok(result);
        }
        [HttpGet("PlanSystem/Getweight")]
        public async Task<IActionResult> Getweight(int planid, int kpiid)
        {
            var models = new searchMPlanweightModels
            {
                Planid = planid,
                Kpiid = kpiid
            };


            var result = await _mPlanweightService.GetAllAsyncSearch_MPlanweight(models);
            return Ok(result);
        }
      
        
    }


}
