using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Models.CommonUseModels
{
    public class ExceptionResponse
    {
        public bool Success { get; set; } = false;
        public string ErrorType { get; set; }
        public int ErrorMessage { get; set; }
    }
}
