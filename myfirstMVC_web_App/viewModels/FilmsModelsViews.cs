using myfirstMVC_web_App.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace myfirstMVC_web_App.viewModels
{
    public class FilmsModelsViews
    {
        public  int Id { get; set; }    
        [Required(ErrorMessage="title of the movie this is required"),StringLength(50)]
        public string Title { get; set; }
        [Required]
        public int Year { get; set; }
        [Required, Range(1, 10, ErrorMessage="range should be from 1-10")]
        public double Rate { get; set; }
        [Required, StringLength(2000)]
        public string Story { get; set; }
        [Display(Name ="Select poster")]  
        public byte[] Poster { get; set; }
        [Display(Name ="Category")] 
        public byte CategoryId { get; set; } 
        // list of all category :
        public IEnumerable<Categ> Categs { get; set; }
        // categ of one film for edit : : 
        public Categ Categoffilm { get; set; } 

    }
}
