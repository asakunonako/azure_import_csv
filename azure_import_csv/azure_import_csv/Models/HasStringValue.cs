using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azure_import_csv.Models
{
    public static class HasStringValue
    {
        public static bool IsNullOrEmptyCheck(string val)
        {
            return (!string.IsNullOrEmpty(val));
        }
    }
}
