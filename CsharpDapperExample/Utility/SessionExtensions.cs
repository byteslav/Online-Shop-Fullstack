using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace CsharpDapperExample.Utility
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
        
        public static List<T> GetItemsListFromSession<T>(this ISession session, string key)
        {
            var itemsList = session.Get<List<T>>(key);
            if (itemsList != null && itemsList.Any())
            {
                return itemsList;
            }

            return new List<T>();
        }
    }
}