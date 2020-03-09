using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Domains.Convertors
{
    public class Base64StringConvertor : IValueConverter<string, string>
    {
        public string Convert(string sourceMember, ResolutionContext context)
        {
            var sourceMemberBytes = Encoding.UTF8.GetBytes(sourceMember);
            return System.Convert.ToBase64String(sourceMemberBytes);
        }
    }
}
