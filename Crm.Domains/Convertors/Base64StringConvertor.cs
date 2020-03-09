using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Convertors
{
    public class Base64StringConvertor : IValueConverter<string, IEnumerable<byte>>
    {
        public IEnumerable<byte> Convert(string sourceMember, ResolutionContext context)
        {
            return System.Convert.FromBase64String(sourceMember);
        }
    }
}
