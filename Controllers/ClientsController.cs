using Microsoft.AspNetCore.Mvc;
using ClientApi.Models;
using ClientApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;


namespace ClientApi.Controllers
{
    
    [Route("api/[controller]")]
   
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet("AfficherAll")]
        
        public ActionResult<IEnumerable<Client>> AfficherAll()
        {
            var clients = _clientService.GetAll();
            return Ok(clients);
        }
        [Authorize(Roles = "User, Admin")]
        [HttpGet("{id}")]
       
        public async Task<ActionResult<Client>> Afficher(int id)
        {
            var client = _clientService.GetById(id);
            if (client == null) return NotFound();
            return Ok(client);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
       
        public IActionResult Create([FromBody] Client client)
        {
            var createdClient = _clientService.Create(client);
            return Ok(createdClient);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        
        public IActionResult Update([FromRoute] int id, [FromBody] Client updatedClient)
        {
            var success = _clientService.Update(id, updatedClient);
            if (!success) return NotFound();
            return Ok(updatedClient);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        
        public IActionResult Supprimer(int id)
        {
            var success = _clientService.Delete(id);
            if (!success) return NotFound();
            return Ok(new { message = "Client supprimé avec succès." });
        }

    }
}
