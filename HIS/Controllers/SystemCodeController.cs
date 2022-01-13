
using BAL.BL;
using BAL.ViewModel;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Controllers
{
  //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SystemCodeController : ControllerBase
    {

        private readonly IConfiguration Configuration;
      private ResponseViewModel res =new ResponseViewModel();
      private SystemCodeBL bl=new SystemCodeBL();
        public SystemCodeController(IConfiguration configuration)
        {
            Configuration = configuration;
      
        }
        [HttpGet]
        [Route("GetSystemCodeParent")]
        public IActionResult GetSystemCodeParent(string CategoryCode)
        {

            try
            {
                return Ok(bl.GetSystemCodeParent(CategoryCode));
            }
            catch (Exception ex)
            {
                res.msg = ex.Message;
                res.status = HttpStatusCode.BadRequest.ToString();
                res.responseCode = "1";
                return Ok(res);
            }

        }
        [HttpGet]
        [Route("GetSystemCode")]
        public IActionResult GetSystemCode(int Id)
        {

            try
            {
                return Ok(bl.GetSystemCode(Id));
            }
            catch (Exception ex)
            {
                res.msg = ex.Message;
                res.status = HttpStatusCode.BadRequest.ToString();
                res.responseCode = "1";
                return Ok(res);
            }

        }
        [HttpGet]
        [Route("GetSystemCodeMaster")]
        public IActionResult GetSystemCodeMaster(int Id)
        {

            try
            {
                return Ok(bl.GetSystemCodeMaster(Id));
            }
            catch (Exception ex)
            {
                res.msg = ex.Message;
                res.status = HttpStatusCode.BadRequest.ToString();
                res.responseCode = "1";
                return Ok(res);
            }

        }
        [HttpGet]
        [Route("DeleteSystemCode")]
        public IActionResult DeleteSystemCode(int Id)
        {

            try
            {
                return Ok(bl.DeleteSystemCode(Id));
            }
            catch (Exception ex)
            {
                res.msg = ex.Message;
                res.status = HttpStatusCode.BadRequest.ToString();
                res.responseCode = "1";
                return Ok(res);
            }

        }
        [HttpPost]
        [Route("SaveSystemCode")]
        public IActionResult SaveSystemCode([FromBody] SystemCodeViewModel model)
        {

            try
            {
                return Ok(bl.SaveSystemCode(model, Convert.ToInt32(User.FindFirstValue("UserId"))));
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
