using System.ComponentModel.DataAnnotations;

namespace TranslatePal.Data.SqlServer
{
    public class Resource
    {
        public Resource()
        { 
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Language { get; set; }

        [Required]
        public string Translation { get; set; }

        public string Comment { get; set; }

        [Required]
        public int ElementId { get; set; }
        public Element Element { get; set; }
    }
}
