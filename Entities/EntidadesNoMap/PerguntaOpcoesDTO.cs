namespace Entities.EntidadesNoMap
{
    public class PerguntaOpcoesDTO
    {
        public uint Id { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public uint IdPesquisa { get; set; }

        public List<OpcaoDTO> Opcoes { get; set; }
    }

    public class OpcaoDTO
    {
        public uint Id { get; set; }

        public string Nome { get; set; }

        public uint Peso { get; set; }

        public bool Ativo { get; set; }

        public uint IdPergunta { get; set; }
    }
}
