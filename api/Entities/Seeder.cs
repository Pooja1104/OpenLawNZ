using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace api.Entities
{
    /// <summary>
    /// Used to seed some test data for the development environment
    /// </summary>
    static class Seeder
    {
        internal static IHost Seed(this IHost host)
        {
            var env = (IWebHostEnvironment)host.Services.GetService(typeof(IWebHostEnvironment));
            if (!env.EnvironmentName.Equals("Development")) return host;

            var app = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));
            using (var scope = app.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<BackendContext>())
                {
                    context.Database.EnsureCreated();

                    if (!context.Folders.Any())
                    {
                        AddJoe(context);
                        AddMary(context);

                        context.SaveChanges();
                    }
                }
            }

            return host;
        }

        private static void AddJoe(BackendContext context)
        {
            var cases = new List<long> { 7215, 9867, 15690, 8238, 23036, 3646, 1181 };
            var folder = new Folder { FolderName = "Economic Disparity", OwnerId = "Joe", FolderStatus = FolderType.Private };

            foreach (var c in cases) folder.Cases.Add(new LegalCase { CaseId = c });
            context.Folders.Add(folder);
        }

        private static void AddMary(BackendContext context)
        {
            var cases = new List<long> { 3065, 17453, 28690, 7377, 8827, 12792, 12695, 1158, 24803, 2842 };
            var folder = new Folder { FolderName = "Right of way - costs", OwnerId = "Joe", FolderStatus = FolderType.Listed };

            foreach (var c in cases) folder.Cases.Add(new LegalCase { CaseId = c });
            context.Folders.Add(folder);
        }
    }
}