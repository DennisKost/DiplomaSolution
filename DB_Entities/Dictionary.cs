using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC_Diploma.Entities
{
    public class Dictionary
    {
        [Key]
        public string Key{ get; set; }

        [Required]
        public string Value{ get; set; }
    }
}
