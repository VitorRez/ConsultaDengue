using System.Collections.Generic;
using System.Threading.Tasks;
using ConsultaDengue.Models;

namespace ConsultaDengue.Data{
    public interface IDengueAlertRepository{
        Task AddAsync(DengueAlert alert);
        Task AddRangeAsync(IEnumerable<DengueAlert> alerts);
        Task<bool> ExistsAsync(string geocode, int ano, int semana);
        Task<IEnumerable<DengueAlert>> GetByGeocodeAsync(string geocode);
        Task SaveChangesAsync();
    }
}