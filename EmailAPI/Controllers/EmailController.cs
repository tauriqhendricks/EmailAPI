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
            this._emailMessageService = emailMessageService;
        }

        [HttpPost]
        public IActionResult SendEmail([FromBody] EmailModel model)
        {
            try
            {
                string result = this._emailMessageService.SendMessage(model);

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("error sending email");
            }
        }
    }
}
