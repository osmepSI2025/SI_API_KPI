using Microsoft.AspNetCore.Mvc;
using SME_API_KPI.Service;

namespace SME_API_KPI.Controllers
{
    [ApiController]
    [Route("api/SYS-KPI")]
    public class DimensionSystemController : ControllerBase
    {
       private readonly MDimensionSystemService _mDimensionSystemService;
        public DimensionSystemController(MDimensionSystemService mDimensionSystemService)
        {
            _mDimensionSystemService = mDimensionSystemService;
        }
        [HttpGet("DimensionSystem/getdimenson")]
        public async Task<IActionResult> GetAllMgetdimenson()
        {
            var result = await _mDimensionSystemService.GetAllAsyncSearch_MDimensionSystem();
            return Ok(result);
        }
        [HttpGet("DimensionSystem/getdimenson-batch")]
        public async Task<IActionResult> batchgetdimenson()
        {
            await _mDimensionSystemService.BatchEndOfDay_MDimensionSystem();
            return Ok();
        }
    }
}
