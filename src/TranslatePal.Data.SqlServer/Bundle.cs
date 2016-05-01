using System.ComponentModel.DataAnnotations;

namespace TranslatePal.Data.SqlServer
{
    public class Bundle
    {
        public Bundle()
        {
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public int ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
