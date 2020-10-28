using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoversApi.Services;
using MoversApi.ViewModels;
using static ViewModel.Utility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoversApi.Controllers
{
    [Produces("application/json")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService service;

        public EmailController(IEmailService emailService)
        {
            service = emailService;
        }

        [HttpGet]
        public string Start()
        {
            return "Api has been started";
        }

        [HttpPost]
        public async Task<Response> SendEmail([FromBody] RevisionFormVM revisionFormVM)
        {
            return await service.SendEmail(revisionFormVM);
        }
    }
}
