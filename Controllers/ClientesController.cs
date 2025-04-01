using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientesAPI.Data;
using ClientesAPI.Models;

namespace ClientesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClientesDbContext _context;

        public ClientesController(ClientesDbContext context)
        {
            _context = context;
        }

        // GET /clientes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return Ok(clientes);
        }

        // GET /clientes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        // GET /clientes/search?nombre=valor
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string nombre) 
        {
            var clientes = await _context.Clientes
                .Where(c => c.Nombres.Contains(nombre))
                .ToListAsync();
            return Ok(clientes);
        }

        // POST /clientes
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Cliente cliente)

        {
            if (cliente.FechaNacimiento > DateTime.Today)
                return BadRequest("La fecha de nacimiento no puede ser futura.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _context.Clientes.AnyAsync(c => c.ID == cliente.ID))
                return Conflict("El ID ya existe."); 

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = cliente.ID }, cliente);
        }

        // PUT /clientes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Cliente cliente)
        {
            if (cliente.FechaNacimiento > DateTime.Today)
                return BadRequest("La fecha de nacimiento no puede ser futura.");
            if (id != cliente.ID)
                return BadRequest("El ID del cliente no coincide.");

            _context.Entry(cliente).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Clientes.AnyAsync(c => c.ID == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }
    }
}
