using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddonWeb.Data
{
    public interface IUrls
    {
        IEnumerable<UrlMap> getUrls { get; }

    }
}
