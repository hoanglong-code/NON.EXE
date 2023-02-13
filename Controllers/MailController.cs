using NON.EXE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using NON.EXE.Data;
using Microsoft.EntityFrameworkCore;
using NON.EXE.Services;

namespace NON.EXE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly DataContext _context;

        public MailController(IMailService mailService, DataContext context)
        {
            _mailService = mailService;
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<MailRequest>>> Get()
        {
            return Ok(await _context.MailRequests.ToListAsync());
        }
        [HttpPost("Send")]
        public async Task<IActionResult> SendMail([FromQuery] MailRequest request)
        {
            await _mailService.SendEmailAsync(request);
            await _context.MailRequests.AddAsync(request);
            await _context.SaveChangesAsync();
            return Ok(await _context.MailRequests.ToListAsync());
        }
        
    }
}
