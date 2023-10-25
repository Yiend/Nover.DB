using System;

namespace Nover.DB.WebView2.Infrastructure
{
    public abstract class Dispatcher
    {
        private static Dispatcher instance;
        public static Dispatcher Browser
        {
            get
            {
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public abstract void Execute(string actionName);
        public abstract void Post(Action action);
        public abstract void PostMessage(Func<MessageResult> action);
        public abstract void Dispatch();
    }
}

