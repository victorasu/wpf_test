using System.ComponentModel.DataAnnotations;

namespace Akkodis.Model
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public int? ClientId { get; set; }
        public Client? Client { get; set; } = null!;
    }
}