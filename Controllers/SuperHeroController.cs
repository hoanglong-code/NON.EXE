using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NON.EXE.Data;
using System.Data;
using NON.EXE.Models;

namespace NON.EXE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;
        

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpGet("GetPigigate/{page}")]
        public async Task<ActionResult<List<SuperHero>>> GetPigigate(int page)
        {
            if (_context.SuperHeroes == null)
                return NotFound();
            var pageResults = 5f;
            var pageCount = Math.Ceiling(_context.SuperHeroes.Count() / pageResults);
            var superhero = await _context.SuperHeroes
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();
            var response = new SuperHeroResponseDTO
            {
                SuperHeros = superhero,
                CurrentPage = page,
                Pages = (int)pageCount
            };
            return Ok(response);
        }
        [HttpGet("GetID/{id}")]
        public async Task<ActionResult<List<SuperHero>>> GetID(int id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("hero not found");
            return Ok(dbHero);
        }
        [HttpGet("GetHero/{name}")]
        public async Task<ActionResult<List<SuperHero>>> GetHero(string name)
        {
            var superhero = await (from p in _context.SuperHeroes
                                   where EF.Functions.Like(p.Name, "%"+name+"%")
                                  select p)
               .ToListAsync();
            if (superhero == null)
                return BadRequest("hero not found");
            return Ok(superhero);
        }
        
        [HttpPost("AddHero")]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpPut("update/{id}")]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null)
                return BadRequest("hero not found");
            dbHero.Name = request.Name;
            dbHero.Firstname = request.Firstname;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero == null) return BadRequest("hero not found");
            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
