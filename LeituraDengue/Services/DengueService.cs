using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LeituraDengue.Models;
using LeituraDengue.Data;

namespace LeituraDengue.Services{
    public class DengueService{
        private readonly AlertRepository _repository;
        private readonly HttpClient _httpClient;

        public DengueService(AlertRepository repository, HttpClient httpClient){
            _repository = repository;
            _httpClient = httpClient;
        }

        public async Task FetchAndPersistAsync(){
            await _repository.CreateTableAsync();

            string geocode = "3106200";
            string disease = "dengue";
            int ano = DateTime.Now.Year;
            int ewStart = 1;
            int ewEnd = 53;

            string url = $"https://info.dengue.mat.br/api/alertcity?geocode={geocode}&disease={disease}&format=json&ew_start={ewStart}&ew_end={ewEnd}&ey_start={ano}&ey_end={ano}";
        
            Console.WriteLine($"Consultando API: {url}\n");

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            var alertas = JsonSerializer.Deserialize<List<AlertaDengue>>(jsonResponse, options);

            if(alertas == null || alertas.Count == 0){
                Console.WriteLine("Nenhum dado retornado da API.");
                return;
            }

            DateTime dataLimite = DateTime.Now.AddMonths(-6);
            var alertasFiltrados = alertas
                .Where(a => a.DataIni >= dataLimite)
                .ToList();

            Console.WriteLine($"Total de registros do ano: {alertas.Count}");
            Console.WriteLine($"Registros nos últimos 6 meses: {alertasFiltrados.Count}");

            if(alertasFiltrados.Any()){
                await _repository.UpsertManyAsync(alertasFiltrados);
                Console.WriteLine($"{alertasFiltrados.Count} registros salvos/atualizados no banco.");
            } else {
                Console.WriteLine("Nenhum registro para salvar.");
            }
        }
    }
}