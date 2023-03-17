#nullable enable
using System.ComponentModel.DataAnnotations;

namespace WebAPI_CRUD_operation.Service.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required] 
        public string FirstName { get; set; }
        [Required] 
        public string LastName { get; set; }
        public List<Location>? Locations { get; set; }
    }
}
