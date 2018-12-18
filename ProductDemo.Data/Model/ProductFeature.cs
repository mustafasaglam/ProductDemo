using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductDemo.Data.Model
{
    public class ProductFeature
    {
        [Key]
        public int ProductFeatureId { get; set; }
        [Required(ErrorMessage ="{0} boş olmamalıdır")]
        [DisplayName("Özellik Adı")]
        public string FeatureName { get; set; }
        [DisplayName("Özellik Değeri")]
        public string FeatureValue { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
