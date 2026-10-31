using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Shared.Responses
{
    public class GeneralResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> ErrorsList { get; set; }
    }
}
