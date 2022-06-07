using System;

namespace APIProduto.Entities
{
     public class DomainException : Exception
     {
          /// <summary>
          /// Criar somente uma instancia
          /// </summary>
          public DomainException() { }

          /// <summary>
          /// passa uma mensagem personalizada
          /// </summary>
          /// <param name="message"></param>
          public DomainException(String message) : base(message) { }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="message"></param>
          /// <param name="innerException"></param>
          public DomainException(String message, Exception innerException) : base(message, innerException) { }
     }
}
