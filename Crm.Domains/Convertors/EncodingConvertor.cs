using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Convertors
{
    public class EncodingConvertor : IValueConverter<string, Encoding>
    {
        public Encoding Convert(string sourceMember, ResolutionContext context)
        {
            return Encoding.GetEncoding(sourceMember);
        }
    }
}
