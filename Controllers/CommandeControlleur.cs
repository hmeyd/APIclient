using Microsoft.AspNetCore.Mvc; // Pour ControllerBase, ApiController, IActionResult, Ok(), NotFound(), etc.
using ClientApi.Models;          // Pour Commande et Client
using ClientApi.Services;        // Pour ICommandeService
using System.Collections.Generic; // Pour IEnumerable<>



[ApiController]
[Route("api/[controller]")]
public class CommandeController : ControllerBase
{
    private readonly ICommandeService _commandeService;

    public CommandeController(ICommandeService commandeService)
    {
        _commandeService = commandeService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Commande>> AfficherAll()
    {
        var commandes = _commandeService.GetAll();
        return Ok(commandes);
    }

    [HttpGet("{id}")]
    public ActionResult<Commande> Afficher(int id)
    {
        var commande = _commandeService.GetById(id);
        if (commande == null) return NotFound();
        return Ok(commande);
    }

    [HttpPost]
    public ActionResult<Commande> Create(Commande commande)
    {
        var createdCommande = _commandeService.Create(commande);
        if (createdCommande == null)
            return BadRequest("Le client n'existe pas.");
        return Ok(createdCommande);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, CommandeUpdateDto updatedCommande)
    {
        var success = _commandeService.Update(id, updatedCommande);
        if (!success) return NotFound();
        return NoContent();
    }



     [HttpDelete("{id}")]
    public IActionResult Supprimer(int id)
    {
        var success = _commandeService.Delete(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
