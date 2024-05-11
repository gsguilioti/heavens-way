namespace HeavensWayApi.Entities
{
    public class Evento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public int TipoEventoId { get; set; }
        public TipoEvento TipoEvento { get; set; }

        public DateTime DataInicio { get; set; }
        public TimeSpan Duracao { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }

        public ICollection<Igreja> Igrejas { get; set; }
    }
}