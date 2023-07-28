namespace Informedebasedatos.Models
{
    public class Informe
    {
        public int InformeId { get; set; }
        public string Titulo { get; set; }
        public DateTime Fecha { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
