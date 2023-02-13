using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NON.EXE.Data;
using System.Net;
using NON.EXE.Models;

namespace NON.EXE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly DataContext _context;

        public UploadImageController(DataContext context, IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<UploadImage>>> GetImage()
        {
            return Ok(await _context.UploadImages.ToListAsync());
        }
        [HttpPost("UploadFile")]
        public async Task<ActionResult<List<UploadImage>>> UploadFile(List<IFormFile> files)
        {
            //List<UploadImage> uploadResults = new List<UploadImage>();
            foreach (var file in files)
            {
                Console.Write(file);
                var uploadResult = new UploadImage();
                string trustedFileNameForFileStorage;
                var untrustedFileName = file.FileName;
                uploadResult.origin_url = untrustedFileName;
                var trustedFileNameForDisPlay = WebUtility.HtmlDecode(untrustedFileName);
                trustedFileNameForFileStorage = Path.GetRandomFileName();
                var path = Path.Combine(_env.ContentRootPath, "uploads", trustedFileNameForDisPlay);

                await using FileStream fs = new(path, FileMode.Create);
                await file.CopyToAsync(fs);

                uploadResult.origin_url = untrustedFileName;
                await _context.UploadImages.AddAsync(uploadResult);
                await _context.SaveChangesAsync();
            }
            return Ok(await _context.UploadImages.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<UploadImage>>> DeleteImage(int id)
        {
            var dbImage = await _context.UploadImages.FindAsync(id);
            if (dbImage == null) return BadRequest("image not found");
            _context.UploadImages.Remove(dbImage);
            await _context.SaveChangesAsync();
            return Ok(await _context.UploadImages.ToListAsync());
        }
        [HttpGet("GetImage/{imageid}")]
        public async Task<ActionResult<List<UploadImage>>> GetImage(int imageid)
        {
            var dbImage = await _context.UploadImages.FindAsync(imageid);
            if (dbImage == null)
                return BadRequest("image not found");
            return Ok(dbImage);
        }
        [HttpGet("GetPigigate/{page}")]
        public async Task<ActionResult<List<UploadImage>>> GetPigigate(int page)
        {
            if (_context.UploadImages == null)
                return NotFound();
            var pageResults = 5f;
            var pageCount = Math.Ceiling(_context.UploadImages.Count() / pageResults);
            var image = await _context.UploadImages
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();
            var response = new ImageResponseDTO
            {
                UploadImages = image,
                CurrentPage = page,
                Pages = (int)pageCount
            };
            return Ok(response);
        }
    }
}
