using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using market.Data;
using Entity =  market.Domain.Entity;
using Model = market.Models;
using AutoMapper;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
#region Models Mapping
    var autoMapperConfig = new AutoMapper.MapperConfiguration( cfg => {

            cfg.CreateMap<Entity.Produto,Model.Produto>();
            cfg.CreateMap<Model.Produto,Entity.Produto>();
            cfg.CreateMap<Entity.Fornecedor,Model.Fornecedor>();
            cfg.CreateMap<Model.Fornecedor, Entity.Fornecedor>();
            cfg.CreateMap<Model.Categoria,Entity.Categoria>();
            cfg.CreateMap<Entity.Categoria,Model.Categoria>();
            cfg.CreateMap<Model.Promocao,Entity.Promocao>();
            cfg.CreateMap<Entity.Promocao,Model.Promocao>();
            cfg.CreateMap<Model.Estoque,Entity.Estoque>();
            cfg.CreateMap<Entity.Estoque,Model.Estoque>();
            cfg.CreateMap<Model.Saida,Entity.Saida>();
            cfg.CreateMap<Entity.Saida,Model.Saida>();
            cfg.CreateMap<Entity.Venda,Model.Venda>();
            cfg.CreateMap<Model.Venda,Entity.Venda>();

        }
    );

    IMapper mapper = autoMapperConfig.CreateMapper();

#endregion

builder.Services.AddSingleton<IMapper>(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
