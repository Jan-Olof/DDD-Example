using System;

namespace CLI.Controllers
{
    public abstract class BaseController
    {
        protected BaseController(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }
        }
    }
}