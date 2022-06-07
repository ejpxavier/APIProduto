using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIProduto.Entities
{
     public class Fornecedor
     {

          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public Guid CodigoFornecedor { get; set; } = Guid.NewGuid();

          [DataType(DataType.Text)]
          [Required(ErrorMessage = "O campo descrição é obrigatório!")]
          public string DescricaoFornecedor { get; set; }

          [DataType(DataType.Text)]
          public string CNPJ { get; set; }

          public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
     }
}
