using System.ComponentModel.DataAnnotations.Schema;

namespace myfirstMVC_web_App.Models
{
    public class Categ
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public byte Id { get; set; }
        public string Name { get; set; }
    }
}
