namespace el_sn_marcelo_api_infrastructure.Models
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
        public int ano { get; set; }
        public decimal valor_hora { get; set; }
        public string combustivel { get; set; }
        public int limite_porta_malas { get; set; }
        public string categoria { get; set; }
        public string foto1 { get; set; }
        public string foto2 { get; set; }
        public string foto3 { get; set; }
        public Marca marca { get; set; }
        public Modelo modelo { get; set; }
    }

    public class Modelo
    {
        public Modelo()
        {

        }

        public int id { get; set; }
        public int id_marca { get; set; }
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
