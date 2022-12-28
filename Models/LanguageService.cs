using Microsoft.Extensions.Localization;
using System.Reflection;

namespace AdminBlog.Models
{
    public class LanguageService
    {
        private readonly IStringLocalizer _localizer;

        public LanguageService(IStringLocalizerFactory factory)
        {
            var type = typeof(ShareResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResoruce", assemblyName.Name);
        }

        public LocalizedString Getkey(String key)
        {
            return _localizer[key];
        }
    }
}
