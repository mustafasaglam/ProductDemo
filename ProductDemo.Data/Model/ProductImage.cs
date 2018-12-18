using System.ComponentModel.DataAnnotations;

namespace ProductDemo.Data.Model
{
    public class ProductImage
    {
        [Key]
        public int ProductImageId { get; set; }
        [Required]
        public string ImageName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
