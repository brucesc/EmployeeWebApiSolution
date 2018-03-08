using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeWebApiProject.Utility
{
    public class JsonMessage
    {
        public string Result { get; set; } // success or failure
        public string Message { get; set; } // if failure this will be a detail of why

        public JsonMessage(string result, string message)
        {
            this.Result = result;
            this.Message = message;
        }
    }
}