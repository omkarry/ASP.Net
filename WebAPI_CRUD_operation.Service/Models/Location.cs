using System.ComponentModel.DataAnnotations;

namespace WebAPI_CRUD_operation.Service.Models
{
    public class Location
    {
        public int Id { get; set; }
        [Required] 
        public string Address { get; set; }
    }
}
