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
        public string Name { get; set; }

        public string Comment { get; set; }

        [Required]
        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
