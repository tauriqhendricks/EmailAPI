using Microsoft.AspNetCore.Mvc;
using System;
using EmailAPI.Services;
using EmailAPI.Models;

namespace EmailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailMessageService _emailMessageService;

        public EmailController(EmailMessageService emailMessageService)
        {
            _emailMessageService = emailMessageService;
        }

        [HttpPost]
        public IActionResult SendEmail([FromBody] EmailModel model)
        {
            try
            {
                EmailModel result = _emailMessageService.SendMessage(model);

                if (result == null)
                {
                    return BadRequest("error sending email");
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("error sending email");
            }
        }
    }
}
