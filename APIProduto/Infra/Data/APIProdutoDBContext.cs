using APIProduto.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIProduto.Data
{
     public class APIProdutoDBContext : DbContext
     {
          public APIProdutoDBContext()
          {
          }

          public APIProdutoDBContext(DbContextOptions<APIProdutoDBContext> options) : base(options)
          {
               Database.EnsureCreated();
          }
          public DbSet<Fornecedor> Fornecedores { get; set; }

          public DbSet<Produto> Produtos { get; set; }
     }
}
