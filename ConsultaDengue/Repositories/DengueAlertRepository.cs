using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConsultaDengue.Data;
using ConsultaDengue.Models;

namespace ConsultaDengue.Repositories{
    public class DengueAlertRepository : IDengueAlertRepository{
        private readonly AppDbContext _context;

        public DengueAlertRepository(AppDbContext context){
            _context = context;
        }

        public async Task AddAsync(DengueAlert alerts){
            await _context.DengueAlerts.AddRangeAsync(alerts);
        }

        public async Task AddRangeAsync(IEnumerable<DengueAlert> alerts){
            await _context.DengueAlerts.AddRangeAsync(alerts);
        }

        public async Task<bool> ExistsAsync(string geocode, int ano, int semana){
            return await _context.DengueAlerts.AnyAsync(a =>
                a.Geocode == geocode &&
                a.AnoEpidemiologico == ano &&
                a.SemanaEpidemiologica == semana
            );
        }

        public async Task<IEnumerable<DengueAlert>> GetByGeocodeAsync(string geocode){
            return await _context.DengueAlerts
                .Where(a => a.Geocode == geocode)
                .OrderBy(a => a.AnoEpidemiologico)
                .ThenBy(a => a.SemanaEpidemiologica)
                .ToListAsync();
        }

        public async Task SaveChangesAsync(){
            await _context.SaveChangesAsync();
        }
    }
}