using static el_sn_marcelo_api_cadastro_infrastructure.Comum.Enumerators;

namespace el_sn_marcelo_api_cadastro_infrastructure.Models
{
    public class Veiculo
    {
        public Veiculo()
        {

        }

        public int id { get; set; }
        public string placa { get; set; }
        public int id_marca { get; set; }
        public int id_modelo { get; set; }
        public string ano { get; set; }
        public decimal valor_hora { get; set; }
        public TipoCombustivel combustivel { get; set; }
        public int limite_porta_malas { get; set; }
        public TipoCategoria categoria { get; set; }
    }

    public class Modelo
    {
        public Modelo()
        {

        }

        public int id { get; set; }
        public string nome { get; set; }
    }

    public class Marca
    {
        public Marca()
        {

        }

        public int id { get; set; }
        public string nome { get; set; }
        public string logotipo { get; set; }
    }
}
