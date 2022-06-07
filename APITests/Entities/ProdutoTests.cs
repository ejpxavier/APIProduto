using APIProduto.Entities;
using Xunit;

namespace API.Tests.Entities
{
     public class ProdutoTests
     {
          [Fact]
          public void Produto_Valida_TamanhoMinimo()
          {
               var result = Assert.Throws<DomainException>(() => new Produto(
                    "P"));

               //Assert
               Assert.Equal("Descrição deve ter no mínimo 2 e no máximo 255 caracteres!", result.Message);

          }

          [Fact]
          public void Produto_Valida_Cadastro_DescricaoVazia()
          {
               var result = Assert.Throws<DomainException>(() => new Produto(
                   string.Empty));

               //Assert
               Assert.Equal("Descrição não pode ser vazia!", result.Message);

          }

     }
}
