namespace ProyectoDeSeguridad.Models
{
    public class Activo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Valor { get; set; }
        public decimal Riesgo { get; set; }
        public decimal Valoracion { get; set; }
    }
}