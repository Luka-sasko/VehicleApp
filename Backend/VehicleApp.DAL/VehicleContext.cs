using Microsoft.EntityFrameworkCore;
using VehicleApp.Model;

namespace VehicleApp.DAL
{
    public class VehicleContext : DbContext
    {
        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }

        public VehicleContext(DbContextOptions<VehicleContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleMake>()
             .HasIndex(vm => vm.Name) 
             .IsUnique();

            modelBuilder.Entity<VehicleMake>()
                .HasKey(vm => vm.Id);

            modelBuilder.Entity<VehicleModel>()
                .HasIndex(vm => vm.Name) 
                .IsUnique();

            modelBuilder.Entity<VehicleModel>()
                .HasKey(vm => vm.Id);

            modelBuilder.Entity<VehicleModel>()
                .HasOne(vm => vm.Make)         
                .WithMany(vm => vm.Models)     
                .HasForeignKey(vm => vm.MakeId); 
        }
    }
}