using ClientApi.Models;
using System.Collections.Generic;

namespace ClientApi.Services
{
    public interface IClientService
    {
        IEnumerable<Client> GetAll();
        Client GetById(int id);
        Client Create(Client client);
        bool Update(int id, Client updatedClient);
        bool Delete(int id);
    }
}
