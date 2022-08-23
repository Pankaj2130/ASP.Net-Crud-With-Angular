using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class List
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

    }
}
