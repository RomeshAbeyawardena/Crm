using AutoMapper;

namespace Crm.Domains.Convertors
{
    public class Base64StringConvertor : IValueConverter<string, string>
    {
        public string Convert(string sourceMember, ResolutionContext context)
        {
            var applicationSettings = context.Options.CreateInstance<ApplicationSettings>();
            var sourceMemberBytes = applicationSettings.Encoding.GetBytes(sourceMember);
            return System.Convert.ToBase64String(sourceMemberBytes);
        }
    }
}
