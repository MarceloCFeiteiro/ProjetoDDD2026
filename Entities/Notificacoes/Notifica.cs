using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Notificacoes
{

    public class Notifica
    {

        [JsonIgnore] // Não aparece no swagger
        [NotMapped]
        public List<Notifica>? Notificacoes;

        [JsonIgnore] // Não aparece no swagger
        [NotMapped]
        public string? NomePropriedade { get; set; }

        [JsonIgnore] // Não aparece no swagger
        [NotMapped]
        public string? Mensagem { get; set; }

        public Notifica()
        {
            Notificacoes = new List<Notifica>();
        }

        public bool ValidarPropriedadesString(string valor, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notificacoes.Add(new Notifica
                {
                    Mensagem = "Campo Obrigatório",
                    NomePropriedade = nomePropriedade
                });

                return false;
            }

            return true;
        }

        public bool ValidarPropriedadesInt(int valor, string nomePropriedade)
        {
            if (valor > 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notificacoes.Add(new Notifica
                {
                    Mensagem = "Campo Obrigatório",
                    NomePropriedade = nomePropriedade
                });

                return false;
            }

            return true;
        }
    }
}
