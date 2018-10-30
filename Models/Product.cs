using System.ComponentModel.DataAnnotations;

namespace Models
{
    public partial class Product
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string EmailAddress { get; set; }
    }

    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
        public string ID { get; set; }
        public string Description { get; set; }
    }

    public class ProductMetadata
    {
        [Required(ErrorMessage = "ID is required.")]
        [RegularExpression("[A-Z]{2}[0-9]{4}", 
            ErrorMessage = "Product ID must be 2capital letters and 4 numbers.")]

        public object ID;

        [StringLength(100, ErrorMessage = "Description must be less than 100 chars.")]
        public object Description;
    }
}
