using APIProduto.Data;
using APIProduto.DTOs;
using APIProduto.Entities;
using APIProduto.Utilitarios;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace APIProduto.Contollers
{
     [Route("api/[controller]")]
     [ApiController]
     public class ProdutoController : ControllerBase
     {
          private readonly APIProdutoDBContext _contexto;
          private readonly IConfiguration _config;

        
          public ProdutoController(APIProdutoDBContext contexto, IConfiguration configuration)
          {
               _contexto = contexto;
               _config = configuration;
          }

          [HttpGet("ListarTodos")]
          public List<Produto> GetListarTodos()
          {
               List<Produto> lista = _contexto.Produtos.ToList();

               return lista;
          }

          [HttpGet("BuscaProduto/{codigoProduto}")]
          public Produto GetBuscaCodigo(Guid codigoProduto)
          {
               Produto produto = _contexto.Produtos
                    .FirstOrDefault(x => x.CodigoProduto == codigoProduto);

               return produto;
          }

          [HttpPost("InserirProduto")]
          public IActionResult PostInserirProduto([FromBody] ProdutoDto produto)
          {
               string msg = "Erro ao cadastrar o Produto: ";

               if ((produto.DataFabricacao != null) && (produto.DataFabricacao >= produto.DataValidade))
               {
                    return new ObjectResult(new Mensagem("Data de Fabricação não pode ser maior ou igual a Data de Validade."));
               }

               try
               {
                    if (ModelState.IsValid)
                    {
                         var produtoEntity = new Produto
                         {
                              DescricaoProduto = produto.DescricaoProduto,
                              Situacao = produto.Situacao > 0 ? produto.Situacao : Entities.Enuns.Situacao.Ativo,
                              DataFabricacao = produto.DataFabricacao,
                              DataValidade = produto.DataValidade

                         };
                         _contexto.Produtos.Add(produtoEntity);

                         foreach (var fornecedorProdutoDto in produto.Fornecedores)
                         {
                              var fornecedorEntity = _contexto.Fornecedores.Find(fornecedorProdutoDto.CodigoFornecedor);
                              if (fornecedorEntity != null)
                              {
                                   fornecedorEntity.Produtos.Add(produtoEntity);
                                   fornecedorProdutoDto.DescricaoFornecedor = fornecedorEntity.DescricaoFornecedor;
                                   fornecedorProdutoDto.CNPJ = fornecedorEntity.CNPJ;
                              }
                         }

                        _contexto.SaveChanges();

                         produto.CodigoProduto = produtoEntity.CodigoProduto;
                         produto.DescricaoProduto = produtoEntity.DescricaoProduto;
                         produto.Situacao = (Entities.Enuns.Situacao) produtoEntity.Situacao;
                         produto.DataFabricacao = produtoEntity.DataFabricacao;
                         produto.DataValidade = produtoEntity.DataValidade;
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

               return Ok(produto);
          }

          [HttpPut("AlterarProduto")]
          public async Task<ActionResult<Mensagem>> PutAlterarProduto([FromBody] ProdutoDto produto)
          {

               string msg = "Erro ao alterar o Produto: ";

               if ((produto.DataFabricacao != null) && (produto.DataFabricacao >= produto.DataValidade))
               {
                    return new ObjectResult(new Mensagem("Data de Fabricação não pode ser maior ou igual a Data de Validade."));
               }

               try
               {
                    var produtoEntity = await _contexto.Produtos.FindAsync(produto.CodigoProduto);

                    if (produtoEntity != null)
                    {
                         produtoEntity.DescricaoProduto = produto.DescricaoProduto;
                         produtoEntity.Situacao = produto.Situacao;
                         produtoEntity.DataFabricacao = produto.DataFabricacao;
                         produtoEntity.DataValidade = produto.DataValidade;

                         await _contexto.SaveChangesAsync();

                         msg = "Produto alterado com sucesso!";
                    }
                    return new Mensagem(msg);
               }
               catch (Exception e)
               {
                    return new Mensagem(msg + e.Message);
               }
          }

          [HttpPut("DeletarProduto/{codigoProduto}")]
          public async Task<ActionResult<Mensagem>> PutDeletarProduto(Guid codigoProduto)
          {
               string msg = "Erro ao alterar o Produto: ";

               try
               {
                    Produto produto = _contexto.Produtos
                         .FirstOrDefault(x => x.CodigoProduto == codigoProduto);

                    var produtoEntity = await _contexto.Produtos.FindAsync(produto.CodigoProduto);

                    if (produtoEntity != null)
                    {
                         produtoEntity.Situacao = Entities.Enuns.Situacao.Inativo;

                         await _contexto.SaveChangesAsync();

                         msg = "Produto Deletado com sucesso!";
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
