using System.ComponentModel.DataAnnotations;

namespace PreguntasRec.Models
{
    public class Imagenes
    {
        [Key]
        public int ImagenID { get; set; }
        public int PreguntaID { get; set; }
        public string ImagenURL { get; set; }
    }
}
