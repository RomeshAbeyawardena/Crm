using AutoMapper;
using System.Text;

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
