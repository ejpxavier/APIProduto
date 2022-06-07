using APIProduto.Entities.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIProduto.Entities
{
     public class Produto
     {
          public Produto()
          {
          }

          public Produto(string descricaoProduto)
          {
               DescricaoProduto = descricaoProduto;

               ValidadeEntity();
          }

          public Produto(string descricaoProduto, Situacao situacao, DateTime dataFabricacao, DateTime dataValidade) : this(descricaoProduto)
          {
               Situacao = situacao;
               DataFabricacao = dataFabricacao;
               DataValidade = dataValidade;
          }

          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public Guid CodigoProduto { get; set; } = Guid.NewGuid();

          [DataType(DataType.Text)]
          [Required(ErrorMessage = "O campo descrição é obrigatório!", AllowEmptyStrings = false)]
          public string DescricaoProduto { get; set; }
          
          public Situacao? Situacao { get; set; }

          [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
          [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
          public DateTime? DataFabricacao { get; set; }

          [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
          [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
          public DateTime? DataValidade { get; set; }

          public virtual ICollection<Fornecedor> Fornecedores { get; set; } = new List<Fornecedor>();

          public void ValidadeEntity() {
               AssertionConcern.AssertArgumentNoEmpty(DescricaoProduto, "Descrição não pode ser vazia!");

               AssertionConcern.AssertArgumentLenght(DescricaoProduto, 2, 255, "Descrição deve ter no mínimo 2 e no máximo 255 caracteres!");
          }
     }
}
