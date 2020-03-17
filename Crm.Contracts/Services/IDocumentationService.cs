using Crm.Domains.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Contracts.Services
{
    public interface IDocumentationService
    {
        IDocumentInfo GetDocumentInfo(string version, string title, string license);
        IEnumerable<IServerInfo> GetServerInfo(params string[] hostUrls);
        IEnumerable<IEndPoint> GetEndPoints();
    }
}
