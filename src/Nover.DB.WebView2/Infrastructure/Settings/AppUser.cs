
namespace Nover.DB.WebView2.Infrastructure
{
    public abstract class AppUser
    {
        private static AppUser instance;
        public static AppUser App
        {
            get
            {
                if (instance == null)
                {
                    //Ambient Context can't return null, so we assign Local Default
                    instance = new CurrentAppSettings();
                }

                return instance;
            }
            set
            {
                instance = (value == null) ? new CurrentAppSettings() : value;
            }
        }

        public virtual IAppSettings Properties { get; set; }
    }
}

