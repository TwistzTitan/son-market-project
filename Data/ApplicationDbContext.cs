using market.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace market.Data;

public class ApplicationDbContext : IdentityDbContext
{   
    DbSet<Produto> Produtos {get; set;}
    DbSet<Categoria> Categorias {get; set;}
    DbSet<Fornecedor> Fornecedores {get; set;}
    DbSet<Promocao> Promocoes {get; set;}
    DbSet<Estoque> Estoques {get; set;}
    DbSet<Saida> Saidas {get; set;}
    DbSet<Venda> Vendas {get; set;}
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
