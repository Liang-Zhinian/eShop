using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using Microsoft.AspNetCore.Mvc;
using Sms.API.Infrastructure;

namespace Sms.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class SmsController : Controller
    {

        // POST api/values
        [HttpPost]
        public IActionResult Post(string phoneNumber, string code)
        {
            try
            {
                SmsSender sms = new SmsSender();
                SendSmsResponse response = sms.Send(phoneNumber, code);
                return Ok(response);
            } catch(ClientException e) {

                return BadRequest(e);
            }
        }
    }
}
