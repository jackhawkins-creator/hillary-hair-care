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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Appointments

//GET All Appointments
app.MapGet("/api/appointments", (HillarysHairCareDbContext db) =>
{
    return db.Appointments.Select(a => new AppointmentDTO
    {
        Id = a.Id,
        StylistId = a.StylistId,
        CustomerId = a.CustomerId,
        Time = a.Time
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


app.Run();

