using System;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using static el_sn_marcelo_api_cadastro_infrastructure.Comum.Enumerators;

namespace el_sn_marcelo_api_cadastro_infrastructure.Models
{
    public class Usuario
    {
        public Usuario()
        {

        }

        public int id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; } = "";
        public int? matricula { get; set; }

        [IgnoreDataMember]
        public string senha { get; set; }
        public string aniversario { get; set; }
        public string role { get; set; }
        public string token { get; set; }
        public Endereco endereco { get; set; } = new Endereco();

        public Usuario CriarHashPassword(string senha)
        {
            Usuario novoUsuario = (Usuario)this.MemberwiseClone();
            novoUsuario.senha = CreateMD5(senha);

            if (!string.IsNullOrEmpty(this.cpf))
            {
                novoUsuario.id = this.id;
                novoUsuario.nome = string.Copy(this.nome);
                novoUsuario.aniversario = string.Copy(this.aniversario);
                novoUsuario.cpf = string.Copy(this.cpf);
                novoUsuario.endereco = new Endereco()
                {
                    cep = string.Copy(this.endereco.cep),
                    cidade = string.Copy(this.endereco.cidade),
                    complemento = string.Copy(this.endereco.complemento),
                    estado = string.Copy(this.endereco.estado),
                    logradouro = string.Copy(this.endereco.logradouro),
                    numero = this.endereco.numero
                };
            }
            else if(this.matricula.HasValue)
                novoUsuario.matricula = this.matricula;

            return novoUsuario;
        }


        private string CreateMD5(string senha)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(senha);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }


    }
}
