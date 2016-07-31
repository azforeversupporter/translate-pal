using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TranslatePal.Data.SqlServer
{
    [Table("Applications")]
    public class Application
    {
        public Application()
        {
            Languages = new List<string>();
            Elements = new List<Element>();
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLengthAttribute(7)]
        public string DefaultLanguage { get; set; }

        private string languages 
        {
            get
            {
                return JsonConvert.SerializeObject(Languages);
            }
            set
            {
                Languages = JsonConvert.DeserializeObject<List<string>>(value);
            }
        }

        [NotMapped]
        public List<string> Languages { get; set; }

        public virtual ICollection<Element> Elements { get; set; }
    }
}
