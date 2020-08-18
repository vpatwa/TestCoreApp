using System;
using System.Collections.Generic;
using System.Text;

namespace TestCoreApp.Domain.Contracts.Response
{
    public class DataRequestResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
