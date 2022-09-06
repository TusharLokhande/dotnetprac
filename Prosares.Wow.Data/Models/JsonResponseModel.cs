﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prosares.Wow.Data.Models
{
        public class JsonResponseModel
        {
            public ApiStatus Status { get; set; }

            public string Message { get; set; }

            public object Data { get; set; }
        }

        public enum ApiStatus
        {
            OK,
            Error,
            OutOfTime,
            AccessDenied
        }
    }
