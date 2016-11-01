using System.ComponentModel.DataAnnotations;

namespace TheDocsNetCore.Models
{
    public class Series
    {
        public int ID { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
    }
}
