using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Response
{
    public class ApiErrorResponse
    {
        public int Status { get; set; }
        public List<ResponseError> Error { get; set; }

        public class ResponseError
        {
            public string? Error { get; set; }
        }
    }
}
