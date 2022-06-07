using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIProduto.Entities
{
     public class AssertionConcern
     {
          /// <summary>
          /// Validação de tamanho máximo de string
          /// </summary>
          /// <param name="stringValue"></param>
          /// <param name="maximum"></param>
          /// <param name="message"></param>
          public static void AssertArgumentLenght(string stringValue, int maximum, string message) 
          {
               int lenght = stringValue.Trim().Length;
               if (lenght > maximum) 
               {
                    throw new DomainException(message);
               }
          }
          /// <summary>
          /// Validacao de tamanho minimo e máximo (precisa estar ente o mínimo e máximo)
          /// </summary>
          /// <param name="stringValue"></param>
          /// <param name="minimum"></param>
          /// <param name="maximum"></param>
          /// <param name="message"></param>
          public static void AssertArgumentLenght(string stringValue, int minimum, int maximum, string message) 
          {
               int lenght = stringValue.Trim().Length;
               if (lenght < minimum || lenght > maximum)
               {
                    throw new DomainException(message);
               }
          }
          /// <summary>
          /// Validação de String Vazia
          /// </summary>
          /// <param name="stringValue"></param>
          /// <param name="message"></param>
          public static void AssertArgumentNoEmpty(string stringValue, string message) 
          {
               if (stringValue == null || stringValue.Trim().Length == 0) 
               {
                    throw new DomainException(message);
               }

          }
          /// <summary>
          /// Validadar se o objeto está null
          /// </summary>
          /// <param name="object1"></param>
          /// <param name="message"></param>
          public static void AssertArgumentNotNull(object object1, string message) 
          {
               if (object1 == null) 
               {
                    throw new DomainException(message);
               }
          }
     }
}
