using System;
using System.IO;
using System.Text.Json;
using Nover.DB.WebView2.Infrastructure;

namespace Nover.DB.WebView2.Defaults
{
    public partial class DataTransferOptions
    {
        public virtual string ConvertObjectToJson(object value)
        {
            if (value == null)
            {
                return null;
            }

            var stream = value as Stream;
            if (stream != null)
            {
                using (var reader = new StreamReader(stream, Encoding))
                {
                    value = reader.ReadToEnd();
                }
            }

            if (value.IsValidJson())
            {
                return value.ToString();
            }

            return JsonSerializer.Serialize(value, Options);
        }
    }
}
