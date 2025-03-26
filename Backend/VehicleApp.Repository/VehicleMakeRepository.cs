using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleApp.DAL;
using VehicleApp.Model;
using VehicleApp.Repository.Common;

namespace VehicleApp.Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        private readonly VehicleContext _context;
        private readonly DbSet<VehicleMake> _dbSet;

        public VehicleMakeRepository(VehicleContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<VehicleMake>();
        }

        public async Task<IEnumerable<VehicleMake>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<VehicleMake> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> PostAsync(VehicleMake vehicleMake)
        {
            if (vehicleMake == null)
            {
                return false;
            }

            await _dbSet.AddAsync(vehicleMake);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(VehicleMake vehicleMake)
        {
            if (vehicleMake == null)
            {
                return false;
            }

            _dbSet.Update(vehicleMake);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
