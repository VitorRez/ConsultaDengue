using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaDengue.Models{
    public class DengueAlert{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public string Geocode {get; set;} = string.Empty;
        public int AnoEpidemiologico {get; set;}
        public int SemanaEpidemiologica {get; set;}
        public DateTime? DataInicio {get; set;}
        public int? CasosEstimados {get; set;}
        public int? CasosNotificados {get; set;}
        public int? NivelAlerta {get; set;}
        public string? NomeCidade {get; set;}
        public string? Uf {get; set;}

        [NotMapped]
        public string SemanaFormatada => $"{AnoEpidemiologico}/{SemanaEpidemiologica:00}";
    }
}