using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TranslatePal.Data.SqlServer
{
    public class Application
    {
        public Application()
        {
            Bundles = new List<Bundle>();
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string DisplayName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(7)]
        public string DefaultLanguage { get; set; }

        [Required]
        public string Languages
        {
            get
            {
                return string.Join(",", AvailableLanguages);
            }
            set
            {
                AvailableLanguages = value.Split(',').ToList();
            }
        }

        [NotMapped]
        public List<string> AvailableLanguages { get; set; }

        public virtual ICollection<Bundle> Bundles { get; set; }
    }
}
