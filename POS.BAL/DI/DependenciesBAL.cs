using Microsoft.Extensions.DependencyInjection;
using POS.BAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.BAL.DI
{
    public static class DependenciesBAL
    {
        public static void ConfigureDI(IServiceCollection services)
        {
            services.AddTransient<IPosRepo, PosRepo>();
        }
    }
}
