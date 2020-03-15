using AutoMapper;
using System.Collections.Generic;

namespace Crm.Domains.Convertors
{
    public class Base64StringByteConvertor : IValueConverter<string, IEnumerable<byte>>
    {
        public IEnumerable<byte> Convert(string sourceMember, ResolutionContext context)
        {
            return System.Convert.FromBase64String(sourceMember);
        }
    }
}
