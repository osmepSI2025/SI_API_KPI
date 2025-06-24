using Microsoft.AspNetCore.Mvc;
using SME_API_KPI.Service;

namespace SME_API_KPI.Controllers
{
    [ApiController]
    [Route("api/SYS-KPI")]
    public class MasterController : ControllerBase
    {
        private readonly MStatusService _mStatusService;
        private readonly MPlanBudgetYearService _mPlanBudgetYearService;
        private readonly MKpiTypeService _mKpiTypeService;
        private readonly MMeasureService _mMeasureService;
        private readonly MInputFormateService _mInputFormateService;


        public MasterController(MStatusService mStatusService, MPlanBudgetYearService mPlanBudgetYearService, MKpiTypeService mKpiTypeService, MMeasureService mMeasureService, MInputFormateService mInputFormateService)
        {
            _mStatusService = mStatusService;
            _mPlanBudgetYearService = mPlanBudgetYearService;
            _mKpiTypeService = mKpiTypeService;
            _mMeasureService = mMeasureService;
            _mInputFormateService = mInputFormateService;
        }

        [HttpGet("Master/GetStatus")]
        public async Task<IActionResult> GetAllMStatus()
        {
            var result = await _mStatusService.GetAllAsyncSearch_MStatus();
            return Ok(result);
        }
        [HttpGet("Master/GetStatus-Batch")]
        public async Task<IActionResult> batchGetStatus()
        {
         await _mStatusService.BatchEndOfDay_Mstatus();
            return Ok();
        }


        [HttpGet("Master/GetYear")]
        public async Task<IActionResult> GetAllYear()
        {
            var result = await _mPlanBudgetYearService.GetAllAsyncSearch_Year();
            return Ok(result);
        }
        [HttpGet("Master/GetYear-batch")]
        public async Task<IActionResult> batchYear()
        {
             await _mPlanBudgetYearService.BatchEndOfDay_year();
            return Ok();
        }


        [HttpGet("Master/GetkpiType")]
        public async Task<IActionResult> GetAllGetkpiType()
        {
            var result = await _mKpiTypeService.GetAllAsyncSearch_MKpiType();
            return Ok(result);
        }
        [HttpGet("Master/GetkpiType-batch")]
        public async Task<IActionResult> batchGetkpiType()
        {
            await _mKpiTypeService.BatchEndOfDay_MKpiType();
            return Ok();
        }


        [HttpGet("Master/Getmeasure")]
        public async Task<IActionResult> GetAllGetmeasure()
        {
            var result = await _mMeasureService.GetAllAsyncSearch_MMeasure();
            return Ok(result);
        }
        [HttpGet("Master/Getmeasure-batch")]
        public async Task<IActionResult> batchGetmeasure()
        {
            await _mMeasureService.BatchEndOfDay_MMeasure();
            return Ok();
        }


        [HttpGet("Master/inputFormate")]
        public async Task<IActionResult> GetAllinputFormate()
        {
            var result = await _mInputFormateService.GetAllAsyncSearch_MInputFormate();
            return Ok(result);
        }
        [HttpGet("Master/inputFormate-batch")]
        public async Task<IActionResult> batchGetinputFormate()
        {
            await _mInputFormateService.BatchEndOfDay_MInputFormate();
            return Ok();
        }
        // Add other CRUD actions as needed
    }
}
