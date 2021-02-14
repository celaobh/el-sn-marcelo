using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace el_sn_marcelo_api_cadastro_application.Ports.Database
{
    public interface ICadastroVeiculoPort
    {
        Task CadastraAsync(string placa, int id_marca, int id_modelo, int ano, decimal valor_hora, string combustivel, int limite_porta_malas, string categoria,string foto1,string foto2,string foto3);
    }
}
