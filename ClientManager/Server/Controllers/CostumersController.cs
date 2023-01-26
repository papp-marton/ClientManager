global using ClientManager.Shared;
using ClientManager.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostumersController : ControllerBase
    {
        private readonly DataContext _context;

        public CostumersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Costumer>>> GetCostumers()
        {
            var costumers = await _context.Costumers.ToListAsync();
            return Ok (costumers);
        }

        [HttpGet]
        [Route ("{id}")]
        public async Task<ActionResult<Costumer>> GetSingleCostumer(int id)
        {
            var costumer = await _context.Costumers.FirstOrDefaultAsync(cm => cm.Id == id);
            if (costumer == null)
            {
                return NotFound("costumer not found");
            }
            return Ok(costumer);
        }

        [HttpPost]
        public async Task<ActionResult<List<Costumer>>> CreateCostumer(Costumer costumer)
        {
            try
            {
                _context.Costumers.Add(costumer);
                await _context.SaveChangesAsync();

                return Ok(await GetDbCostumers());
            }
            catch (DbUpdateException exception) when (exception?.InnerException?.Message.Contains("Cannot insert duplicate key row in object") ?? false)
            {
                return BadRequest("Costumer already set");
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<List<Costumer>>> UpdateCostumer(Costumer costumer, int id)
        {
            var dbCostumer = await _context.Costumers
                .FirstOrDefaultAsync(cm => cm.Id == id);
            if (dbCostumer == null)
                return NotFound("Sorry, but no costumer for you.");

            dbCostumer.IdNum = costumer.IdNum;
            dbCostumer.IsCompany = costumer.IsCompany;
            dbCostumer.FirstName = costumer.FirstName;
            dbCostumer.LastName = costumer.LastName;
            dbCostumer.Phone = costumer.Phone;
            dbCostumer.Address = costumer.Address;            

            await _context.SaveChangesAsync();

            return Ok(await GetDbCostumers());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Costumer>>> DeleteCostumer(int id)
        {
            var dbCostumer = await _context.Costumers
                .FirstOrDefaultAsync(cm => cm.Id == id);
            if (dbCostumer == null)
                return NotFound("Sorry, but no costumer for you.");

            _context.Costumers.Remove(dbCostumer);
            await _context.SaveChangesAsync();

            return Ok(await GetDbCostumers());
        }


        private async Task<List<Costumer>> GetDbCostumers()
        {
            return await _context.Costumers.ToListAsync();
        }
    }
}
