using Microsoft.EntityFrameworkCore;
using HillarysHairCare.Models;

public class HillarysHairCareDbContext : DbContext
{
    public DbSet<Stylist> Stylists { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    public DbSet<AppointmentService> AppointmentServices { get; set; }

    public HillarysHairCareDbContext(DbContextOptions<HillarysHairCareDbContext> context) : base(context)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed stylists
        modelBuilder.Entity<Stylist>().HasData(new Stylist[]
        {
            new Stylist { Id = 1, Name = "Hillary", IsActive = true },
            new Stylist { Id = 2, Name = "Tina", IsActive = true },
            new Stylist { Id = 3, Name = "Sarah", IsActive = true },
            new Stylist { Id = 4, Name = "Joesph", IsActive = true },
            new Stylist { Id = 5, Name = "Mallory", IsActive = true }
        });

        // Seed services
        modelBuilder.Entity<Service>().HasData(new Service[]
        {
            new Service { Id = 1, Name = "Haircut", Price = 30.00M },
            new Service { Id = 2, Name = "Coloring", Price = 50.00M },
            new Service { Id = 3, Name = "Beard Trim", Price = 20.00M },
            new Service { Id = 4, Name = "Eyebrow Waxing", Price = 10.00M },
            new Service { Id = 5, Name = "Perm", Price = 100.00M }
        });

        // Seed customers
        modelBuilder.Entity<Customer>().HasData(new Customer[]
        {
        new Customer { Id = 1, Name = "Jane Doe"},
        new Customer { Id = 2, Name = "John Smith"},
        new Customer { Id = 3, Name = "Alice Johnson", },
        new Customer { Id = 4, Name = "Bob Lee" },
        new Customer { Id = 5, Name = "Carmen Lopez"}
        });
        // Seed appointments
        modelBuilder.Entity<Appointment>().HasData(new Appointment[]
        {
        new Appointment
        {
            Id = 1,
            Time = new DateTime(2025, 6, 1, 10, 0, 0),
            StylistId = 1,
            CustomerId = 1,
        },
        new Appointment
        {
            Id = 2,
            Time = new DateTime(2025, 6, 2, 14, 30, 0),
            StylistId = 2,
            CustomerId = 2,
        },
        new Appointment
        {
        Id = 3,
        Time = new DateTime(2025, 6, 3, 9, 0, 0),
        StylistId = 1,
        CustomerId = 3,
        },
        new Appointment
        {
        Id = 4,
        Time = new DateTime(2025, 6, 4, 11, 30, 0),
        StylistId = 2,
        CustomerId = 4,
        },
        new Appointment
        {
        Id = 5,
        Time = new DateTime(2025, 6, 5, 15, 45, 0),
        StylistId = 1,
        CustomerId = 5,
        }
        });
        // Seed appointment services (many-to-many)
        modelBuilder.Entity<AppointmentService>().HasData(new AppointmentService[]
        {
        new AppointmentService { Id = 1, AppointmentId = 1, ServiceId = 1 },
        new AppointmentService { Id = 2, AppointmentId = 2, ServiceId = 2 },
        new AppointmentService { Id = 3, AppointmentId = 2, ServiceId = 1 },
        new AppointmentService { Id = 4, AppointmentId = 3, ServiceId = 1 },
        new AppointmentService { Id = 5, AppointmentId = 4, ServiceId = 2 },
        new AppointmentService { Id = 6, AppointmentId = 5, ServiceId = 1 }
        });

    }
}

