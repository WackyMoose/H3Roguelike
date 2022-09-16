using Autofac;
using Microsoft.AspNetCore.Mvc;
using Autofac.Extensions.DependencyInjection;
using WebAPI.H3Roguelite.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews(controller =>
{
    //controller.Filters.Add(new AuthorizeFilter());
    controller.Filters.Add<UnitOfWorkAttribute>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterUnitOfWorkFactory();

    var h3RogueliteOptionsSection = builder.Configuration.GetSection(H3RogueliteOptions.H3Roguelite);
    containerBuilder.RegisterH3Roguelite(h3RogueliteOptionsSection.Get<H3RogueliteOptions>());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
