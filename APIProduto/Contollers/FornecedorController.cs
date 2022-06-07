using APIProduto.Data;
using APIProduto.DTOs;
using APIProduto.Entities;
using APIProduto.Utilitarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIProduto.Contollers
{
     [Route("api/[controller]")]
     [ApiController]
     public class FornecedorController : Controller
     {
          private readonly APIProdutoDBContext _contexto;
          private readonly IConfiguration _config;

          public FornecedorController(APIProdutoDBContext contexto, IConfiguration configuration)
          {
               _contexto = contexto;
               _config = configuration;
          }

          [HttpGet("ListarTodos")]
          public async Task<List<FornecedorDto>> GetListarTodos()
          {
               return await _contexto.Fornecedores
                    .Select(x => new FornecedorDto
                    {
                         CodigoFornecedor = x.CodigoFornecedor,
                         DescricaoFornecedor = x.DescricaoFornecedor,
                         CNPJ = x.CNPJ,
                         Produtos = x.Produtos.Select(y => new ProdutoDto
                         {
                              CodigoProduto = y.CodigoProduto,
                              DescricaoProduto = y.DescricaoProduto,
                              Situacao = (Entities.Enuns.Situacao) y.Situacao,
                              DataFabricacao = y.DataFabricacao,
                              DataValidade = y.DataValidade,
                         }).ToList()
                    }).ToListAsync();
          }

          [HttpPost("InserirFornecedor")]
          public IActionResult PostInserirFornecedor([FromBody] FornecedorDto fornecedor)
          {
               string msg = "Erro ao cadastrar o Fornecedor: ";

               try
               {
                    if (ModelState.IsValid)
                    {
                         var fornecedorEntity = new Fornecedor
                         {
                              DescricaoFornecedor = fornecedor.DescricaoFornecedor,
                              CNPJ = fornecedor.CNPJ

                         };
                         _contexto.Fornecedores.Add(fornecedorEntity);

                         foreach (var produtoFornecedorDto in fornecedor.Produtos)
                         {
                              var produtoEntity = _contexto.Produtos.Find(produtoFornecedorDto.CodigoProduto);
                              if (produtoEntity != null)
                              {
                                   produtoEntity.Fornecedores.Add(fornecedorEntity);
                                   produtoFornecedorDto.DescricaoProduto = produtoEntity.DescricaoProduto;
                                   produtoFornecedorDto.Situacao = (Entities.Enuns.Situacao) produtoEntity.Situacao;
                                   produtoFornecedorDto.DataFabricacao = produtoEntity.DataFabricacao;
                                   produtoFornecedorDto.DataValidade = produtoEntity.DataValidade;
                              }
                         }

                         _contexto.SaveChanges();

                         fornecedor.CodigoFornecedor = fornecedorEntity.CodigoFornecedor;
                         fornecedor.DescricaoFornecedor = fornecedorEntity.DescricaoFornecedor;
                         fornecedor.CNPJ = fornecedorEntity.CNPJ;
                    }
                    else
                    {
                         return new ObjectResult(new Mensagem(msg + BadRequest(ModelState)));
                    }
               }
               catch (Exception e)
               {
                    return new ObjectResult(new Mensagem(msg + e.Message));
               }

               return Ok(fornecedor);
          }

          [HttpPut("AlterarFornecedor")]
          public async Task<ActionResult<Mensagem>> PutAlterarFornecedor([FromBody] FornecedorDto fornecedor)
          {

               string msg = "Erro ao alterar o Fornecedor: ";

               try
               {
                    var fornecedorEntity = await _contexto.Fornecedores.FindAsync(fornecedor.CodigoFornecedor);

                    if (fornecedorEntity != null)
                    {
                         fornecedorEntity.DescricaoFornecedor = fornecedor.DescricaoFornecedor;
                         fornecedorEntity.CNPJ = fornecedor.CNPJ;

                         await _contexto.SaveChangesAsync();

                         msg = "Fornecedor alterado com sucesso!";
                    }
                    return new Mensagem(msg);
               }
               catch (Exception e)
               {
                    return new Mensagem(msg + e.Message);
               }
          }

     }
}
