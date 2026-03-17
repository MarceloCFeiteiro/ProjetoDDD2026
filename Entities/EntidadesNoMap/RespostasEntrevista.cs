namespace Entities.EntidadesNoMap
{
    public class RespostasEntrevista
    {

        public List<RespostaPerguntaDTO> ListaRespostaPergunta { get; set; }


        public RespostasEntrevista()
        {
            ListaRespostaPergunta = new List<RespostaPerguntaDTO>();
        }


    }

    public class RespostaPerguntaDTO
    {
        public uint Id { get; set; }

        public string CpfEntrevistado { get; set; }

        public string NomeEntrevistado { get; set; }

        public DateTime DataResposta { get; set; }

        public uint IdEmpresa { get; set; }

        public uint IdPergunta { get; set; }

        public OpcaoRespostaDTO opcaoResposta { get; set; }
    }

    public class OpcaoRespostaDTO
    {
        public uint Id { get; set; }

        public uint Peso { get; set; }
    }
}
