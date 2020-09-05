using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Utilities.DI
{
    public static class DependenciesUtilities
    {
        public static void ConfigureDI(IServiceCollection services)
        {
            services.AddTransient<IGenerateID, GenerateID>();
        }
    }
}
