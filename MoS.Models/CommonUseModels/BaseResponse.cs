﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Models.CommonUseModels
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}
