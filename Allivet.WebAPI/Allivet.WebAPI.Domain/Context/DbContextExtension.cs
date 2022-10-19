using System.Collections.Generic;
using System.IO;
using System.Linq;
using Allivet.WebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;

namespace Allivet.WebAPI.Domain.Context
{
    public static class DbContextExtension
    {
        public static bool AllMigrationsApplied(this AllivetWebAPIDbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this AllivetWebAPIDbContext context)
        {
            if (!context.VeterinaryLocations.Any())
            {
                var data = JsonConvert.DeserializeObject<List<VeterinaryLocation>>(File.ReadAllText("common/seeddata" + Path.DirectorySeparatorChar + "veterinarylocations.json"));
                context.AddRange(data);
                context.SaveChanges();
            }
        }
    }
}
