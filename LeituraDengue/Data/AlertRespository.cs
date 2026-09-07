using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using LeituraDengue.Models;

namespace LeituraDengue.Data{
    public class AlertRepository{
        private readonly string _connectionString;

        public AlertRepository(string connectionString = "Data source=dengue.db"){
            _connectionString = connectionString;
        }

        public async Task CreateTableAsync(){
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            string sql = @"
                CREATE TABLE IF NOT EXISTS AlertaDengue(
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Semana INTEGER NOT NULL,
                    Ano INTEGER NOT NULL,
                    DataIni TEXT NOT NULL,
                    DataFim TEXT NOT NULL,
                    CasosEst INTEGER NOT NULL,
                    CasosNot INTEGER NOT NULL,
                    Nivel INTEGER NOT NULL,
                    Geocode TEXT NOT NULL,
                    DataConsulta TEXT NOT NULL,
                    UNIQUE(Semana, Ano, Geocode)
                )
            ";

            await connection.ExecuteAsync(sql);
        }

        public async Task UsertAsync(AlertaDengue alerta){
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            string sql = @"
                INSERT INTO AlertaDengue 
                    (Semana, Ano, DataIni, DataFim, CasosEst, CasosNot, Nivel, Geocode, DataConsulta)
                VALUES 
                    (@Semana, @Ano, @DataIni, @DataFim, @CasosEst, @CasosNot, @Nivel, @Geocode, @DataConsulta)
                ON CONFLICT(Semana, Ano, Geocode) DO UPDATE SET
                    DataIni = excluded.DataIni,
                    DataFim = excluded.DataFim,
                    CasosEst = excluded.CasosEst,
                    CasosNot = excluded.CasosNot,
                    Nivel = excluded.Nivel,
                    DataConsulta = excluded.DataConsulta;
            ";

            await connection.ExecuteAsync(sql, new{
                alerta.Semana,
                alerta.Ano,
                alerta.DataIni,
                alerta.DataFim,
                alerta.CasosEst,
                alerta.CasosNot,
                alerta.Nivel,
                Geocode = "3106200",
                DataConsulta = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });
        }

        public async Task UpsertManyAsync(IEnumerable<AlertaDengue> alertas){
            foreach(var alerta in alertas){
                await UsertAsync(alerta);
            }
        }
    }
}