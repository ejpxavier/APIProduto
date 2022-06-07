using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIProduto.Entities.DTOs
{
     public class FornecedorProdutoDto
     {
          public Guid CodigoFornecedor { get; set; }

          public string DescricaoFornecedor { get; set; }
          
          public string CNPJ { get; set; }
     }
}
