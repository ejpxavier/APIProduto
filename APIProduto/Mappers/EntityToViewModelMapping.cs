using APIProduto.DTOs;
using APIProduto.Entities;
using AutoMapper;

namespace APIProduto.Mappers
{
     public class EntityToViewModelMapping : Profile
     {
          public EntityToViewModelMapping(string profileName) : base(profileName)
          {
               CreateMap<Produto, ProdutoDto>();

               CreateMap<Fornecedor, FornecedorDto>();
          }
     }
}
