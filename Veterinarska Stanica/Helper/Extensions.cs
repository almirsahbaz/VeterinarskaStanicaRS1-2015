using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Veterinarska_Stanica.Helper
{
    public static class Extensions
    {
        public static ISession Set<TEntity>(this ISession session, string key, TEntity entity)
        {
            session.SetString(key, JsonConvert.SerializeObject(entity));
            return session;
        }

        public static TEntity Get<TEntity>(this ISession session, string key) where TEntity : class
        {
            var obj = session.GetString(key);

            return obj != null ? JsonConvert.DeserializeObject<TEntity>(obj) : null;
        }
    }
}
