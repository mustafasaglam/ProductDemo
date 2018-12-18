using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductDemo.Data.Model
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage ="{0} Alanı boş olmaz"),StringLength(25,ErrorMessage ="En fazla 25 karekter")]
     
        [DisplayName("Ürün Adı")]
        public string ProductName { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductFeature> ProductFeatures { get; set; }


        //Navigation property
        [DisplayName("Kategori Adı")]
        public Category Category { get; set; }
      

    }
}
