using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using LeituraDengue.Data;
using LeituraDengue.Services;

namespace LeituraDengue{
    class Program{
        static async Task Main(string[] args){
            var repository = new AlertRepository("Data Source=dengue.db");
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "LeituraDengue/1.0");

            var service = new DengueService(repository, httpClient);

            try{
                await service.FetchAndPersistAsync();
                Console.WriteLine("\nProcesso concluído com sucesso.");
            } catch (Exception ex) {
                Console.WriteLine($"\nErro durante o processo: {ex.Message}");
            }

            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}