using GeneralServer.API.Core.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GeneralServer.API.Core
{
    public static class GeneralServerDbContextExtensions
    {
        public static void CreateTriggers(this GeneralServerDbContext context)
        {
            PropertyInfo[] propertyInfos = typeof(GeneralServerDbContext).GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                Type genericType = propertyInfo.PropertyType;//Type.GetType(propertyInfo.PropertyType.ToString());
                try
                {
                    Type concreteType = genericType.GetGenericArguments().Single();
                    if (concreteType.IsSubclassOf(typeof(BaseModel)))
                    {
                        string triggerCode = GetTriggerSQLCode(propertyInfo.Name);
                        context.Database.ExecuteSqlCommand(triggerCode);
                    }
                }
                catch (Exception)
                {
                    // TODO: add fallback if needed
                }
            }
        }

        private static string GetTriggerSQLCode(string tableName)
        {
            return $"CREATE TRIGGER trigger_updated_{tableName} ON {tableName} AFTER UPDATE AS UPDATE {tableName} SET UpdatedAt = GETDATE() WHERE id IN (SELECT DISTINCT Id FROM Inserted)";
        }
    }
}
