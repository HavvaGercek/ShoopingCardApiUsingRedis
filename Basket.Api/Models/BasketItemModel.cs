using System.ComponentModel.DataAnnotations;

namespace Basket.Api.Models
{
    public class BasketItemModel
    {
        [Required(ErrorMessage = "ProductId is required.")]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "UnitPrice is required.")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be grater than zero.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "CustomerId is required.")]
        public string CustomerId { get; set; }

        [Required(ErrorMessage = "ProductName is required.")]
        public string ProductName { get; set; }

    }
}
