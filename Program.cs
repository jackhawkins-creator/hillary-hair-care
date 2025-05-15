using HillarysHairCare.Models;
using HillarysHairCare.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<HillarysHairCareDbContext>(builder.Configuration["HillarysHairCareDbConnectionString"]);

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", builder =>
    {
        builder.WithOrigins("http://localhost:5173", "http://localhost:5174")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();






// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("DevCors");


//Appointments

//GET All Appointments
app.MapGet("/api/appointments", (HillarysHairCareDbContext db) =>
{
    return db.Appointments.Include(a => a.Stylist).Include(a => a.Customer).Include(a => a.AppointmentServices).ThenInclude(aps => aps.Service).Select(a => new AppointmentDTO
    {
        Id = a.Id,
        StylistId = a.StylistId,
        CustomerId = a.CustomerId,
        Time = a.Time,
        Stylist = new StylistDTO
        {
            Id = a.Stylist.Id,
            Name = a.Stylist.Name,
            IsActive = a.Stylist.IsActive
        },
        Customer = new CustomerDTO
        {
            Id = a.Customer.Id,
            Name = a.Customer.Name
        },
        Services = a.AppointmentServices.Select(aps => new ServiceDTO
        {
            Id = aps.Service.Id,
            Name = aps.Service.Name,
            Price = aps.Service.Price
        }).ToList()
    }).ToList();
});

// GET appointment by ID
app.MapGet("/api/appointments/{id}", (HillarysHairCareDbContext db, int id) =>
{
    return db.Appointments.Include(a => a.Stylist).Include(a => a.Customer).Include(a => a.AppointmentServices).ThenInclude(aps => aps.Service)

    .Select(a => new AppointmentDTO
    {
        Id = a.Id,
        StylistId = a.StylistId,
        CustomerId = a.CustomerId,
        Time = a.Time,
        Stylist = new StylistDTO
        {
            Id = a.Stylist.Id,
            Name = a.Stylist.Name,
            IsActive = a.Stylist.IsActive
        },
        Customer = new CustomerDTO
        {
            Id = a.Customer.Id,
            Name = a.Customer.Name
        },
        Services = a.AppointmentServices.Select(aps => new ServiceDTO
        {
            Id = aps.Service.Id,
            Name = aps.Service.Name,
            Price = aps.Service.Price
        }).ToList()
    }).SingleOrDefault(a => a.Id == id);
});

// Add a new appointment
app.MapPost("/api/appointments", (HillarysHairCareDbContext db, AppointmentDTO dto) =>
{
    return Results.Created(
        $"/api/appointments/{(
            db.Appointments.Add(
                new Appointment
                {
                    StylistId = dto.StylistId,
                    CustomerId = dto.CustomerId,
                    Time = dto.Time,
                    AppointmentServices = dto.ServiceIds.Select(serviceId => new AppointmentService
                    {
                        ServiceId = serviceId
                    }).ToList()
                }).Entity.Id
        )}",
        db.SaveChanges()
    );
});


// update services in Appointment
app.MapPatch("/api/appointments/{id}/services", (HillarysHairCareDbContext db, int id, List<int> serviceIds) =>
{
    Appointment appointmentToUpdate = db.Appointments.SingleOrDefault(a => a.Id == id);
    List<AppointmentService> currentServices = db.AppointmentServices.Where(aps => aps.AppointmentId == id).ToList();
    db.AppointmentServices.RemoveRange(currentServices);
    foreach (int serviceId in serviceIds)
    {
        AppointmentService newAppointmentService = new AppointmentService
        {
            AppointmentId = id,
            ServiceId = serviceId
        };
        db.AppointmentServices.Add(newAppointmentService);
    }
    db.SaveChanges();
    return Results.NoContent();
}
);

// Delete an Appointment
app.MapDelete("/api/appointments/{id}", (HillarysHairCareDbContext db, int id) =>
{
    Appointment appointmentToDelete = db.Appointments.SingleOrDefault(a => a.Id == id);
    if (appointmentToDelete == null)
    {
        return Results.NotFound();
    }
    db.Appointments.Remove(appointmentToDelete);
    db.SaveChanges();
    return Results.NoContent();
});


//Customers

//GET All Customers
app.MapGet("/api/customers", (HillarysHairCareDbContext db) =>
{
    return db.Customers.Select(c => new CustomerDTO
    {
        Id = c.Id,
        Name = c.Name
    }).ToList();
});

// GET Customer by Id
app.MapGet("/api/customers/{id}", (HillarysHairCareDbContext db, int id) =>
{
    return db.Customers.Where(c => c.Id == id).Select(c => new CustomerDTO
    {
        Id = c.Id,
        Name = c.Name
    }).SingleOrDefault();
});

// Add a new customer
app.MapPost("/api/customers", (HillarysHairCareDbContext db, Customer customer) =>
{
    db.Customers.Add(customer);
    db.SaveChanges();
    return Results.Created($"/api/customers/{customer.Id}", customer);
});

// Add a new customer
app.MapPost("/api/stylists", (HillarysHairCareDbContext db, Stylist stylist) =>
{
    db.Stylists.Add(stylist);
    db.SaveChanges();
    return Results.Created($"/api/stylists/{stylist.Id}", stylist);
});

//Services

//GET All Services
app.MapGet("/api/services", (HillarysHairCareDbContext db) =>
{
    return db.Services.Select(se => new ServiceDTO
    {
        Id = se.Id,
        Name = se.Name,
        Price = se.Price
    }).ToList();
});

//Stylists

//GET All Stylists
app.MapGet("/api/stylists", (HillarysHairCareDbContext db) =>
{
    return db.Stylists.Select(st => new StylistDTO
    {
        Id = st.Id,
        Name = st.Name,
        IsActive = st.IsActive
    }).ToList();
});

//Get stylist by Id
app.MapGet("/api/stylists/{id}", (HillarysHairCareDbContext db, int id) =>
{
    return db.Stylists.Where(st => st.Id == id).Select(st => new StylistDTO
    {
        Id = st.Id,
        Name = st.Name,
        IsActive = st.IsActive
    }).SingleOrDefault();
});

// update stylist from Active or inactive
app.MapPatch("/api/stylists/{id}", (HillarysHairCareDbContext db, int id, Stylist stylist) =>
{
    Stylist stylistToUpdate = db.Stylists.SingleOrDefault(stylist => stylist.Id == id);
    stylistToUpdate.IsActive = stylist.IsActive;
    db.SaveChanges();
    return Results.NoContent();
}


);

app.Run();

