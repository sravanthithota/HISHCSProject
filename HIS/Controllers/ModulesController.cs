using BAL.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAL.BL;
using System.Net;
using System.Security.Claims;



namespace HIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private ResponseViewModel res = new ResponseViewModel();
        private ModulesBL bl = new ModulesBL();
        public ModulesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [HttpGet]
        [Route("GetModulesCode")]
        public IActionResult GetSystemCode(int Id)
        {
            try
            {
                return Ok(bl.GetModulesBL(Id));
            }
            catch (Exception ex)
            {
                res.msg = ex.Message;
                res.status = HttpStatusCode.BadRequest.ToString();
                res.responseCode = "1";
                return Ok(res);
            }
        }
       
        [Route("SaveModulesCode")]
        [HttpPost]
        public IActionResult SaveModules(ModuleViewModel module)
        {
            try
            {
                return Ok(bl.SaveModulesBL(module, Convert.ToInt32(User.FindFirstValue("UserId"))));
            }
            catch (Exception ex)
            {
                res.msg = ex.Message;
                res.status = HttpStatusCode.BadRequest.ToString();
                res.responseCode = "1";
                return Ok(res);
            }
        }
       
        
        [Route("DeleteModules")]
        [HttpGet]
        public IActionResult DeleteModules(int Id)
        {
            try
            {
                return Ok(bl.DeleteModuleBL(Id, Convert.ToInt32(User.FindFirstValue("UserId"))));
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
