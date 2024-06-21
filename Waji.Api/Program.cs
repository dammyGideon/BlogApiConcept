using Waji.Api.Data.ExtentionMethod;
using Waji.Api.CQRS.Extention;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using Waji.Api.Shared.ValidationExtentionMethod;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.DatabaseConfigure(builder.Configuration);
builder.Services.ApplicationServices();

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.ConfigureCqrsServices();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Waji.Api", Version = "v1" });
});


builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(opt => opt.AddDefaultPolicy(des => {
    des.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddValidationServices();

var app = builder.Build();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
   
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Waji.Api v1"));
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

