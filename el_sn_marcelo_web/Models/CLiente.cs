using System.ComponentModel.DataAnnotations;

namespace el_sn_marcelo_web.Models
{
    public class Cliente
    {
        [Required]
        [StringLength(11)]
        public string cpf { get; set; }
        [Required]
        public string nome { get; set; }
        [Required]
        public string aniversario { get; set; }
        [Required]
        [StringLength(8)]
        public string cep { get; set; }
        [Required]
        public string logradouro { get; set; }
        [Required]
        public int numero { get; set; }
        public string complemento { get; set; }
        [Required]
        public string cidade { get; set; }
        [Required]
        public string estado { get; set; }
        public string senha { get; set; }
    }
}
