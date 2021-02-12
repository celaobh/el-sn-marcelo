namespace el_sn_marcelo_api_cadastro_infrastructure.Models
{
    public class Endereco
    {
        public Endereco()
        {

        }

        public string cep { get; set; } = "";
        public string logradouro { get; set; } = "";
        public int? numero { get; set; } = 0;
        public string complemento { get; set; } = "";
        public string cidade { get; set; } = "";
        public string estado { get; set; } = "";
    }
}