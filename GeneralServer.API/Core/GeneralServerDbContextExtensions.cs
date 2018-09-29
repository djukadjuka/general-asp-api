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
        public static void SeedDatabase(this GeneralServerDbContext context)
        {
            User[] users = new User[]
            {
                new User{Username = "admin",        Password="admin"},
                new User{Username = "rogerS",       Password="pass123"},
                new User{Username = "pat1",         Password="pass123"},
                new User{Username = "st3v3n",       Password="pass123"},
                new User{Username = "wallace",      Password="pass123"},
                new User{Username = "KurtisC",      Password="pass123"},
                new User{Username = "W_StaGG",      Password="pass123"},
                new User{Username = "DennisCXZ",    Password="pass123"}
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }

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
