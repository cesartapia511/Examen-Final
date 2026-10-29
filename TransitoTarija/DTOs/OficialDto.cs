using System.ComponentModel.DataAnnotations;

namespace TransitoTarija.DTOs
{
    public class OficialDto
    {
        [Required]
        [StringLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Grado { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string NumeroEscalafon { get; set; } = string.Empty;

        [Required]
        [StringLength(80)]
        public string Cargo { get; set; } = string.Empty;

        [StringLength(80)]
        public string? Unidad { get; set; }

        public bool Activo { get; set; } = true;
    }
}
