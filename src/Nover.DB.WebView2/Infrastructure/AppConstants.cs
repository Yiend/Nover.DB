namespace Nover.DB.WebView2.Infrastructure
{
    public static class ResponseConstants
    {
        public const string StatusOKText = "OK";
        public const int MinClientErrorStatusCode = 400;
        public const int MaxClientErrorStatusCode = 499;
        public const int MinServerErrorStatusCode = 500;
        public const string Header_AccessControlAllowOrigin = "Access-Control-Allow-Origin";
        public const string Header_CacheControl = "Cache-Control";
        public const string Header_AccessControlAllowMethods = "Access-Control-Allow-Methods";
        public const string Header_AccessControlAllowHeaders = "Access-Control-Allow-Headers";
        public const string Header_ContentType = "Content-Type";
        public const string Header_ContentTypeValue = "application/json";
        public const string ReasonPhrase_NoContent = "NoContent";
        public const string ReasonPhrase_PassThru = "PassThrough";
        public const string ReasonPhrase_Blocked = "Blocked";
    }

    public static class DisapatcherExecuteType
    {
        public const string Reload = nameof(Reload);
        public const string Minimized = nameof(Minimized);
        public const string Normal = nameof(Normal);
        public const string Exit   = nameof(Exit);
    }
}
