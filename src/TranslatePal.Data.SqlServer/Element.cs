using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TranslatePal.Data.SqlServer
{
    public class Element
    {
        public Element()
        {
            Resources = new List<Resource>();
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string ElementName { get; set; }

        public string Comment { get; set; }

        [Required]
        public int BundleId { get; set; }
        public Bundle Bundle { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
