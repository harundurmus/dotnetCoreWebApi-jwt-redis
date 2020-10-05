using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltamiraTaskApi.Domain.Responses
{
    public class BaseResponse<T> where T : class
    {
        public T Extra { get; set; }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public BaseResponse(T extra)
        {
            this.Success = true;
            this.Extra = extra;
        }

        public BaseResponse(string errorMessage)
        {
            this.Success = false;
            this.ErrorMessage = errorMessage;
        }
    }
}
