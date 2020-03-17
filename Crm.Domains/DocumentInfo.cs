using Crm.Domains.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains
{
    public class DocumentInfo : IDocumentInfo
    {
        public string Version { get; set; }
        public string Title { get; set; }
        public string License { get; set; }

        public DocumentInfo(string version, string title, string license)
        {
            Version = version;
            Title = title;
            License = license;
        }
    }
}
