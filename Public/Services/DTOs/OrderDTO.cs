using System.ComponentModel.DataAnnotations;

namespace Public.Services.DTOs
{
    public class OrderDTO
    {
        [Required]
        public string OrderNumber { get; set; }
    }
}
