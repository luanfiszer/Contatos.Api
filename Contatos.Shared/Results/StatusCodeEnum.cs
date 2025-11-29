using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contatos.Shared.Results
{
    public enum StatusCodeEnum
    {
        Ok = 200,
        BusinessError = 422,
        NotFound = 404
    }
}
