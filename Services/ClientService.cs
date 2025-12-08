using ClientApi.Data;
using ClientApi.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace ClientApi.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;

        public ClientService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Client> GetAll()
        {
            return _context.Clients.ToList();
        }

        public Client GetById(int id)
        {
            return _context.Clients.FirstOrDefault(c => c.Id == id);
        }

        public Client Create(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
            return client;
        }

        public bool Update(int id, Client updatedClient)
        {
            _context.Clients.Update(updatedClient);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var client = _context.Clients.Include(c => c.Commandes)
                                         .FirstOrDefault(c => c.Id == id);

            if (client == null)
                return false;

            _context.Commandes.RemoveRange(client.Commandes);
            _context.Clients.Remove(client);

            _context.SaveChanges();
            return true;
        }
    }
}
