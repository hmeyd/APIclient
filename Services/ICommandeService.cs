using ClientApi.Models;
using System.Collections.Generic;

namespace ClientApi.Services
{
    public interface ICommandeService
    {
        IEnumerable<Commande> GetAll();
        Commande GetById(int id);
        Commande Create(Commande commande);
        bool Update(int id, CommandeUpdateDto commande);
        bool Delete(int id);
    }
}
