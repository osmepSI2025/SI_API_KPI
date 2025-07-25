﻿using Microsoft.AspNetCore.Mvc;
using SME_API_KPI.Models;
using SME_API_KPI.Service;

namespace SME_API_KPI.Controllers
{
    [ApiController]
    [Route("api/SYS-KPI")]
    public class KpiSystemController : ControllerBase
    {
        //private readonly MPlanPeriodService _mPlanPeriodService;
        private readonly MPlanTargetDescriptionService  _mPlanTargetDescriptionService;
        private readonly MPlanKpiDescriptionService _mPlanKpiDescriptionService;
      //  private readonly MPlanKpiAssignService _mPlanKpiAssignService;
        private readonly MKpiSystemWeightService _mKpiSystemWeightService;    
        private readonly MPlanResultService _mPlanResultService;
        private readonly MPlanKpiListService _mPlanKpiListService;
        //private readonly MPlanKpiTargetService _mPlanKpiTargetService;
     //   private readonly MPlanKpiService _mPlanKpiService;
        // Uncomment if you need to use MPlanNameService
        //private readonly MPlanNameService _mPlanNameService;
        private readonly MExportEvalSystemService _mExportEvalSystemService;
        private readonly MKpiSystemKpiTargetService _mKpiSystemKpiTargetService;
        private readonly MKpiSystemAssignService _mKpiSystemAssignService;
        private readonly KpiSystemResultService _kpiSystemResultService;
        public KpiSystemController(
            //MPlanPeriodService mPlanPeriodService
            // ,
            MPlanTargetDescriptionService mPlanTargetService
            , MPlanKpiDescriptionService mPlanKpiDescriptionService
          //  , MPlanKpiAssignService mPlanKpiAssignService
            , MKpiSystemWeightService mKpiSystemWeightService,
            MPlanResultService mPlanResultService
            , MPlanKpiListService mPlanKpiListService
            , MKpiSystemKpiTargetService mKpiSystemKpiTargetService
        //    , MPlanKpiService mPlanKpiService
            , MExportEvalSystemService mExportEvalSystemService
            ,MKpiSystemAssignService mKpiSystemAssignService
            , KpiSystemResultService mKpiSystemResultService
            )
        {
            // _mPlanNameService = mPlanNameService;
          //  _mPlanPeriodService = mPlanPeriodService;
            _mPlanTargetDescriptionService = mPlanTargetService;
            _mPlanKpiDescriptionService = mPlanKpiDescriptionService;
           // _mPlanKpiAssignService = mPlanKpiAssignService;
            _mKpiSystemAssignService = mKpiSystemAssignService;
            _mPlanResultService = mPlanResultService;
            _mPlanKpiListService = mPlanKpiListService;
          
        //    _mPlanKpiService = mPlanKpiService;
            _mExportEvalSystemService = mExportEvalSystemService;
            _mKpiSystemKpiTargetService = mKpiSystemKpiTargetService;
            _mKpiSystemAssignService = mKpiSystemAssignService;
            _mKpiSystemWeightService = mKpiSystemWeightService;
            _kpiSystemResultService = mKpiSystemResultService;
        }

        [HttpPost("kpiSystem/exportEval")]
        public async Task<IActionResult> exportEval(searchMExportEvalModels models)
        {
           

            var result = await _mExportEvalSystemService.GetAllAsyncSearch_MExportEval(models);
            return Ok(result);
        }
        //[HttpGet("kpiSystem/Batch-exportEval")]
        //public async Task<IActionResult> Batch_exportEval()
        //{
        //    await _mExportEvalSystemService.BatchEndOfDay_MExportEval(null);
        //    return Ok();
        //}


        [HttpPost("KpiSystem/GetKpiTarget")]
        public async Task<IActionResult> GetKpiTarget(searchMPlanKpiTargetModels models)
        {


            var result = await _mKpiSystemKpiTargetService.GetAllAsyncSearch_MPlanKpiTarget(models);
            return Ok(result);
        }

        //[HttpGet("KpiSystem/batch-MPlanKpiTarget")]
        //public async Task<IActionResult> BatchEndOfDay_MPlanKpiTarget()
        //{
        //    var models = new searchMPlanKpiTargetModels
        //    {
        //        Planid = "",
        //        Kpiid = ""
        //    };


        //    await _mPlanKpiTargetService.BatchEndOfDay_MPlanKpiTarget(models);
        //    return Ok();
        //}





        [HttpPost("kpiSystem/GetKpiAssign")]
        public async Task<IActionResult> GetKpiAssign(searchMPlanKpiAssignModels models)
        {
            var result = await _mKpiSystemAssignService.GetAllAsyncSearch_MPlanKpiAssign(models);
            return Ok(result);
        }
        [HttpPost("kpiSystem/Getweight")]
        public async Task<IActionResult> Getweight(searchMPlanweightModels models)
        {
         

            var result = await _mKpiSystemWeightService.GetAllAsyncSearch_MPlanweight(models);
            return Ok(result);
        }

        [HttpPut("kpiSystem/result")]
        public async Task<IActionResult> kpiSystem_result(KpiSystemResultModels models)
        {
            var result = await _kpiSystemResultService.KpiSystem_PutResult(models);
            return Ok(result);
        }

    }


}
