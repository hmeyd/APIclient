using System.Text.Json.Serialization;
namespace ClientApi.Models
{
    public class Commande
    {
        public int Id { get; set; }
        public int NumeroCommande { get; set; }

        public DateTime DateCommande { get; set; }

        public decimal MontantTotal { get; set; }

        public string? Statut { get; set; }

        public int ClientId { get; set; }

        [JsonIgnore]
        public Client Client { get; set; }
    }


    public class CommandeUpdateDto
    {
        public int NumeroCommande { get; set; }
        public DateTime DateCommande { get; set; }
        public decimal MontantTotal { get; set; }
        public string? Statut { get; set; }
        public int ClientId { get; set; }
    }
}