using System.Text.RegularExpressions;

namespace Tv_Show_Managment.Transformers
{
    public class SlugParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if(value is not string) return null;
            var result = Regex.Replace(value.ToString()!, "[^a-zA-Z-0-9-]+", "-", RegexOptions.CultureInvariant, TimeSpan.FromMicroseconds(5000)).Trim('-');
            return result;
        }
    }
}
