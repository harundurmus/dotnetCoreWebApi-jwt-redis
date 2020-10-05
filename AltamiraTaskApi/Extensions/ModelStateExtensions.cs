using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltamiraTaskApi.Extensions
{
    public static class ModelStateExtensions
    {
        // Dönecek hataları listelemek için kullanılır
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(m => m.Value.Errors).Select(x => x.ErrorMessage).ToList();
        }
    }
}
