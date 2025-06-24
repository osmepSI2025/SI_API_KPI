using Microsoft.AspNetCore.Mvc;
using SME_API_KPI.Service;

namespace SME_API_KPI.Controllers
{
    [ApiController]
    [Route("api/SYS-KPI")]
    public class DivisionSystemController : ControllerBase
    {
        private readonly MDivisionService  _mDivisionService;
        public DivisionSystemController(MDivisionService  mDivisionService)
        {
            _mDivisionService = mDivisionService;
        }
        [HttpGet("divisionSystem/getdivision")]
        public async Task<IActionResult> GetAllgetdivision()
        {
            var result = await _mDivisionService.GetAllAsyncSearch_MDivision();
            return Ok(result);
        }
        [HttpGet("divisionSystem/getdivision-batch")]
        public async Task<IActionResult> batchgetdivision()
        {
            await _mDivisionService.BatchEndOfDay_MDivision();
            return Ok();
        }
    }
}
