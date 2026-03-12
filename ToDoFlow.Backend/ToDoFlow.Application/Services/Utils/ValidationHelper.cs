using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoFlow.Application.Services.Utils
{
    public static class ValidationHelper
    {
        public static void ValidateId(int id, string entityName)
        {
            if (id < 0)
            {
                throw new ArgumentException($"Invalid {entityName} ID: {id}");
            }
        }

        public static void ValidateObject(object obj, string entityName)
        {
            if (obj == null)
            {
                throw new KeyNotFoundException($"{entityName} not found");
            }
        }

    }
}
