using System.Text.Json.Serialization;

namespace LeituraDengue.Models{
    public class AlertaDengue{
        [JsonPropertyName("SE")]
        public int Semana {get; set;}

        [JsonPropertyName("ano")]
        public int Ano {get; set;}

        [JsonPropertyName("data_iniSE")]
        public long DataIniTimestamp {get; set;}
        public DateTime DataIni => DateTimeOffset.FromUnixTimeMilliseconds(DataIniTimestamp).DateTime;

        [JsonPropertyName("data_fimSE")]
        public long DataFimTimestamp {get; set;}
        public DateTime DataFim => DateTimeOffset.FromUnixTimeMilliseconds(DataFimTimestamp).DateTime;

        [JsonPropertyName("casos_est")]
        public double CasosEst {get; set;}

        [JsonPropertyName("casos")]
        public double CasosNot {get; set;}

        [JsonPropertyName("nivel")]
        public int Nivel {get; set;}

        public override string ToString(){
            return $"Semana {Semana} - Data: {DataIni:dd/MM/yyyy} - Estimados: {CasosEst} - Notificados: {CasosNot} - Nível: {Nivel}";
        }
    }
}