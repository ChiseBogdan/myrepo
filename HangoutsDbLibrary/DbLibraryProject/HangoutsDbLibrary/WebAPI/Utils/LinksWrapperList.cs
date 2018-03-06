using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Utils
{
    public class LinksWrapperList<T>
    {
        public List<LinksWrapper<T>> Values { get; set; }
    }
}
