namespace GARP.Application.DTO.Auto
{
    public class DtoGetAutoById
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Anio { get; set; }
        public int IdMarca { get; set; }
        public int IdTipoAuto { get; set; }
        public string Color { get; set; }
        public decimal Precio { get; set; }
        public decimal Kilometraje { get; set; }
        public int Estado { get; set; }
    }
}
