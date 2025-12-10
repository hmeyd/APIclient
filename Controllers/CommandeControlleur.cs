using Microsoft.AspNetCore.Mvc; // Pour ControllerBase, ApiController, IActionResult, Ok(), NotFound(), etc.
using ClientApi.Models;          // Pour Commande et Client
using ClientApi.Services;        // Pour ICommandeService
using System.Collections.Generic; // Pour IEnumerable<>
using Microsoft.AspNetCore.Authorization;

[Authorize]
[Route("api/[controller]")]
public class CommandeController : ControllerBase
{
    private readonly ICommandeService _commandeService;

    public CommandeController(ICommandeService commandeService)
    {
        _commandeService = commandeService;
    }
    [Authorize(Roles = "User,Admin")]
    [HttpGet("AfficherAll")]
    public ActionResult<IEnumerable<Commande>> AfficherAll()
    {
        var commandes = _commandeService.GetAll();
        return Ok(commandes);
    }
    [Authorize(Roles = "User,Admin")]
    [HttpGet("{id}")]
    public ActionResult<Commande> Afficher(int id)
    {
        var commande = _commandeService.GetById(id);
        if (commande == null) return NotFound();
        return Ok(commande);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost("Create")]
    public IActionResult Create([FromBody] Commande commande)
    {   
        var createdCommande = _commandeService.Create(commande);
        return Ok(createdCommande);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public IActionResult Update([FromRoute]int id, [FromBody] CommandeUpdateDto updatedCommande)
    {
        var success = _commandeService.Update(id, updatedCommande);
        if (!success) return NotFound();
        return Ok(updatedCommande);
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult Supprimer(int id)
    {
        var success = _commandeService.Delete(id);
        if (!success) return NotFound();
        return Ok(new { Message = "le commande a été bien supprimée" });
    }
}
