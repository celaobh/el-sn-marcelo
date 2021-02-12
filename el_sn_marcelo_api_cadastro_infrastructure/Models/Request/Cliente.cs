using System;
using System.Collections.Generic;
using System.Text;

namespace el_sn_marcelo_api_cadastro_infrastructure.Models.Request
{
    public class Cliente
    {
        public string nome { get; set; }
        public string cpf { get; set; } = "";
        public string senha { get; set; }
        public string aniversario { get; set; }
        public string role { get; set; }
        public string token { get; set; }
        public string cep { get; set; } = "";
        public string logradouro { get; set; } = "";
        public int? numero { get; set; } = 0;
        public string complemento { get; set; } = "";
        public string cidade { get; set; } = "";
        public string estado { get; set; } = "";
    }
}
