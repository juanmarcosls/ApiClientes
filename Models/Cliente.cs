using System;
using System.ComponentModel.DataAnnotations;

namespace ClientesAPI.Models
{
    
    public class Cliente
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Nombres { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Apellidos { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El CUIT es obligatorio.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "El CUIT debe tener exactamente 11 dígitos.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El CUIT solo debe contener números.")]
        public string? CUIT { get; set; }

        [MaxLength(200)]
        public string? Domicilio { get; set; }

        [Required(ErrorMessage = "El teléfono celular es obligatorio.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El teléfono solo debe contener números.")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "El teléfono debe tener entre 8 y 15 dígitos.")]
        public string? TelefonoCelular { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Email { get; set; }
    }
}
