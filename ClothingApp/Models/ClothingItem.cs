using System.ComponentModel.DataAnnotations;

namespace ClothingApp.Models
{
    public class ClothingItem
    {
        public int Id { get; set; }
        public string ClothingType { get; set; } = "Tops";
        [Required]
        public string ClothingTypeSpecific { get; set; } = "T-shirt";
        [Required]
        [StringLength(30, ErrorMessage = "Size is too long")]
        public string Size { get; set; } = "Other";
        public string Name { get; set; } = string.Empty;
        [StringLength(100, ErrorMessage = "Description is too long")]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Condition {  get; set; } = "New with tags";
        [Required]
        [StringLength(30, ErrorMessage = "Brand is too long")]
        public string Brand { get; set; } = "Other";
        public DateOnly PurchaseDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public decimal PurchasePrice { get; set; }
        [Required]
        public DateOnly LastWorn {  get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Required]
        [Range(0, 10000, ErrorMessage = "Value for must be between {1} and {2}.")]
        public int NumberWorn { get; set; }
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Color { get; set; } = "#FFFFFF";
    }
}
