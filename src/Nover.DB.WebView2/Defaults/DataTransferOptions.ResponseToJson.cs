﻿namespace Nover.DB.WebView2.Defaults
{
    public partial class DataTransferOptions
    {
        public virtual string ConvertResponseToJson(object response)
        {
            return ConvertObjectToJson(response);
        }
    }
}
