﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarCitizen.StarMap.Internal
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}