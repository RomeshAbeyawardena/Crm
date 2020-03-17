using Crm.Contracts.Services;
using Crm.Domains;
using Crm.Domains.Contracts;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Services
{
    public class DocumentationService : IDocumentationService
    {
        public IDocumentInfo GetDocumentInfo(string version, string title, string license)
        {
            return new DocumentInfo(version, title, license);
        }

        public IEnumerable<IEndPoint> GetEndPoints(IApiDescriptionGroupCollectionProvider apiExplorer, string controller = null)
        {
            var foundGroups = apiExplorer.ApiDescriptionGroups.Items
                .Where(descriptionGroup => descriptionGroup.GroupName
                    .Equals(controller, StringComparison.InvariantCultureIgnoreCase));

            var endpointList = new List<IEndPoint>();

            foreach(var foundGroup in foundGroups)
            {
                foreach (var item in foundGroup.Items)
                {
                    endpointList.Add(new Endpoint {
                        Method = Method.Get( item.HttpMethod,
                        RelativeUrl = 
                    });
                }
            }

        }

        public IEnumerable<IServerInfo> GetServerInfo(params string[] hostUrls)
        {
            throw new NotImplementedException();
        }
    }
}
