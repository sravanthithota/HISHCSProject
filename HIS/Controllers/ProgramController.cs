using BAL.BL;
using BAL.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {

        private readonly IConfiguration Configuration;
        private ResponseViewModel res = new ResponseViewModel();
        private ProgramBL bl = new ProgramBL();
        public ProgramController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [HttpGet]
        [Route("GetProgramsCode")]
        public IActionResult GetSystemCode(int Id,int isPanel)
        {
            try
            {
                return Ok(bl.GetProgramsBL(Id, isPanel));
            }
            catch (Exception ex)
            {
                res.msg = ex.Message;
                res.status = HttpStatusCode.BadRequest.ToString();
                res.responseCode = "1";
                return Ok(res);
            }
        }

        [Route("SaveProgramsCode")]
        [HttpPost]
        public IActionResult SaveModules(ProgramViewModel module)
        {
            try
            {
                return Ok(bl.SaveProgramsBL(module, Convert.ToInt32(User.FindFirstValue("UserId"))));
            }
            catch (Exception ex)
            {
                res.msg = ex.Message;
                res.status = HttpStatusCode.BadRequest.ToString();
                res.responseCode = "1";
                return Ok(res);
            }
        }


        [Route("DeletePrograms")]
        [HttpGet]
        public IActionResult DeleteModules(int Id)
        {
            try
            {
                return Ok(bl.DeleteProgramBL(Id, Convert.ToInt32(User.FindFirstValue("UserId"))));
            }
            catch (Exception ex)
            {
                res.msg = ex.Message;
                res.status = HttpStatusCode.BadRequest.ToString();
                res.responseCode = "1";
                return Ok(res);
            }
        }

    }
}
