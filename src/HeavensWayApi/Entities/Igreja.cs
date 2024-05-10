namespace HeavensWayApi.Entities
{
    public class Igreja
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        
        public int DistritoId {get; set; }
        public Distrito Distrito { get; set; }

        public int EnderecoId {get; set; }
        public Endereco Endereco { get; set; }

        ICollection<Evento> Eventos { get; set; }
    }
}