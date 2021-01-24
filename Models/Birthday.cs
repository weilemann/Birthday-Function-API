using System;
using System.ComponentModel.DataAnnotations;

namespace BirthdayAPI.Models
{
    public class Birthday
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(1, ErrorMessage = "Este campo deve conter apenas 1 caractere.")]
        public string Gender { get; set; }

        public long Phone { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int IntimacyLevel { get; set; }

        public long TwitterId { get; set; }

        [MaxLength(1024, ErrorMessage = "Este campo deve conter menos de 1024 caracteres")]
        public string Email { get; set; }

        [MaxLength(1024, ErrorMessage = "Este campo deve conter menos de 1024 caracteres")]
        public string TwitterName { get; set; }
    }
}