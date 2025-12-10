using System.Text.Json.Serialization;
namespace ClientApi.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Email { get; set; }
        public string? Telephone { get; set; }
        public string? Adresse { get; set; }
        public DateTime DateCreation { get; set; }


        [JsonIgnore]
        public ICollection<Commande> Commandes { get; set; } = new List<Commande>();


        public string GetFullName()
        {
            return $"{Prenom} {Nom}";
        }
    }
}

