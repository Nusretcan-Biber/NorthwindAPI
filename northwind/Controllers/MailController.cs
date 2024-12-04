using Microsoft.AspNetCore.Mvc;
using northwind.Models;
using Northwind.Utils.Mail;

namespace northwind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailHelper _mailHelper;

        public MailController(IMailHelper mailHelper)
        {
            _mailHelper = mailHelper;
        }

        [HttpPost(nameof(SendEmail))]
        public async Task<IActionResult> SendEmail([FromBody] MailModel model)
        {
            try
            {
                // E-posta gönderme işlemini burada gerçekleştirin
                await _mailHelper.SendMailAsync(model.Subject, model.Body, model.Recepients);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
