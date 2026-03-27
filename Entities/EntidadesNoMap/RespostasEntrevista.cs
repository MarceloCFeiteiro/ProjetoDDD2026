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
        public int Id { get; set; }

        public string CpfEntrevistado { get; set; }

        public string NomeEntrevistado { get; set; }

        public DateTime DataResposta { get; set; }

        public int IdEmpresa { get; set; }

        public int IdPergunta { get; set; }

        public OpcaoRespostaDTO opcaoResposta { get; set; }
    }

    public class OpcaoRespostaDTO
    {
        public int Id { get; set; }

        public int Peso { get; set; }
    }
}
