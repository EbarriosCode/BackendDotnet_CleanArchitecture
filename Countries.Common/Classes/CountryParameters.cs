using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Common.Classes
{
    public class CountryParameters : PagingParameters
    {        
        public string Name { get; set; }
        public string Alpha2 { get; set; }
        public bool OrderByNumericCode { get; set; }
    }
}
