using ClientApi.Data;
using ClientApi.Models;
using System.Collections.Generic;
using System.Linq;
using ClientApi.Services;


public class CommandeService : ICommandeService
{
    private readonly AppDbContext _context;

    public CommandeService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Commande> GetAll()
    {
        return _context.Commandes.ToList();
    }

    public Commande GetById(int id)
    {
        return _context.Commandes.FirstOrDefault(c => c.Id == id);
    }

    public Commande Create(Commande commande)
    {
        _context.Commandes.Add(commande);
        _context.SaveChanges();
        return commande;
    }

    public bool Update(int id, CommandeUpdateDto updatedCommande)
    {
        var existing = _context.Commandes.Find(id);
        if (existing == null) return false;  // juste un bool pour dire que ce n'est pas trouvé

        existing.NumeroCommande = updatedCommande.NumeroCommande;
        existing.DateCommande = updatedCommande.DateCommande;
        existing.MontantTotal = updatedCommande.MontantTotal;
        existing.Statut = updatedCommande.Statut;
        existing.ClientId = updatedCommande.ClientId;

        _context.SaveChanges(); // synchrone
        return true;
    }



    public bool Delete(int id)
    {
        var commande = _context.Commandes.FirstOrDefault(c => c.Id == id);
        if (commande == null) return false;

        _context.Commandes.Remove(commande);
        _context.SaveChanges();
        return true;
    }
}
