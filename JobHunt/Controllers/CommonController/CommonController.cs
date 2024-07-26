using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Response.Common;
using JobHunt.Domain.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobHunt.Controllers.CommonController
{
    [Route("api/common")]
    [ApiController]
    [CustomAuthorize("User Company")]
    public class CommonController : ControllerBase
    {
        private readonly IServiceBundle _serviceBundle;

        public CommonController(IServiceBundle serviceBundle)
        {
            _serviceBundle = serviceBundle;
        }

        [HttpGet("view-job/{jobId}")]
        public async Task<IResult> ViewJobDetails(int jobId)
        {
            JobDetails data = await _serviceBundle.CommonService.GetJobDetails(jobId);
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }

        [HttpGet("get-skill")]
        public async Task<IResult> GetAllSkills()
        {
            var data = await _serviceBundle.CommonService.GetAllSkill();
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }

        [HttpGet("get-language")]
        public async Task<IResult> GetAllLanguage()
        {
            var data = await _serviceBundle.CommonService.GetAllLanguage();
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }

        [HttpGet("jobhunt-statistics")]
        public async Task<IResult> JobHuntStatistics()
        {
            var counts = await _serviceBundle.CommonService.Statistics();
            return Results.Ok(ResponseHelper.SuccessResponse(counts));
        }

        [HttpGet("download-resume/{applicationId}")]
        public async Task<IActionResult> DownloadResume(int applicationId)
        {
            var data = await _serviceBundle.CommonService.GetResume(applicationId);

            return File(data.FileBytes!, data.ContentType!, data.FileName);
        }
    }
}
