using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Utils
{
    public class LinksWrapper<T>
    {
        public T Value { get; set; }
        public List<LinkInfo> Links { get; set; }
    }
}
