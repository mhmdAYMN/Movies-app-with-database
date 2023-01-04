using System.ComponentModel.DataAnnotations;

namespace myfirstMVC_web_App.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required,MaxLength(50)]
        public string Title { get; set; }
        [Required]    
        public int Year { get; set; }
        [Required,Range(1,10)]  
        public double Rate  { get; set; }
        [Required,MaxLength(2000)]  
        public string Story  { get; set; }
        [Required]  
        public byte[] Poster  { get; set; }    
        public byte CategoryId  { get; set; }
        public Categ Category  { get; set; } 

        






    }
}
