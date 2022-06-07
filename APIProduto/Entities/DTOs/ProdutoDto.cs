using APIProduto.Entities.DTOs;
using APIProduto.Entities.Enuns;
using System;
using System.Collections.Generic;

namespace APIProduto.DTOs
{
     public class ProdutoDto
     {
          public Guid CodigoProduto { get; set; }
          public string DescricaoProduto { get; set; }
          public Situacao Situacao { get; set; }
          public DateTime? DataFabricacao { get; set; }
          public DateTime? DataValidade { get; set; }
          public virtual ICollection<FornecedorProdutoDto> Fornecedores { get; set; } = new List<FornecedorProdutoDto>();
     }
}
