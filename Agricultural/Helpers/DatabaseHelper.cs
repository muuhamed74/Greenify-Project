
using Npgsql;

namespace Agricultural
{
    public static class DatabaseHelper
    {
        public static string GetConnectionString(IConfiguration configuration, IHostEnvironment environment)
        {
            // Check if DATABASE_URL environment variable exists (set by the hosting platform in production)
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            // If DATABASE_URL exists, use it (this will happen during deployment  deployment)
            if (!string.IsNullOrEmpty(databaseUrl))
            {
                return BuildConnectionString(databaseUrl);
            }

            // Otherwise, fall back to appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found in configuration or environment variables.");
            }

            return connectionString;
        }

        private static string BuildConnectionString(string databaseUrl)
        {
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port == -1 ? 5432 : databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };
            return builder.ToString();
        }
    }
}