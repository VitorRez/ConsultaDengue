using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


class Program{
    static async Task Main(string[] args){
        DateTime today = DateTime.Today;

        Console.WriteLine($"{today.Month}");

        string DataStart = $"{today.Year}{today.Month}";
        string DataEnd = $"{today.AddMonths(-10).Year}{today.AddMonths(-10).Month}";

        Console.WriteLine(DataStart);
        Console.WriteLine(DataEnd);

        string url = $"https://info.dengue.mat.br/api/alertcity?geocode=3106200&disease=dengue&format=json&ew_start={DataStart}&ew_end={DataEnd}";

        using var client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(30);

        Console.WriteLine("Consultando a API AlertaDengue...\n");

        try{
            HttpResponseMessage response = await client.GetAsync(url);

            if(response.IsSuccessStatusCode){
                string json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Resposta da API (JSON bruto):\n");
                Console.WriteLine(json);
            }else{
                Console.WriteLine($"Erro na requisição: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
        catch (Exception ex){
            Console.WriteLine($"Erro ao acessar a API: {ex.Message}");
        }
    }
}

