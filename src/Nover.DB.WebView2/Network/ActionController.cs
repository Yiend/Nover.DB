using System;
using System.Reflection;

namespace Nover.DB.WebView2.Network
{
    public abstract class ActionController
    {
        private string _routePath;
        private string _name;
        private string _description;

        public string RoutePath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_routePath))
                {
                    SetAttributeInfo();
                }

                return _routePath;
            }
        }

        public string Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_name))
                {
                    SetAttributeInfo();
                }

                return _name;
            }
        }

        public string Description
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_description))
                {
                    SetAttributeInfo();
                }

                return _description;
            }
        }

        private void SetAttributeInfo()
        {
            var attribute = GetType().GetCustomAttribute<ActionControllerAttribute>(true);
            if (attribute != null)
            {
                _routePath = attribute.RoutePath;
                _name = attribute.Name;
                _description = attribute.Description;
            }
        }
    }
}
