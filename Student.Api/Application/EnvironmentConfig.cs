namespace Students.Api.Application
{
    public static class EnvironmentConfig
    {
        public static string GetConnectionString(WebApplicationBuilder builder)
        {
            var host = Environment.GetEnvironmentVariable("DB_Host");
            var dbName = Environment.GetEnvironmentVariable("DB_Name");
            var dbPassword = Environment.GetEnvironmentVariable("DB_Password");
            return $"Server={host};Database={dbName};User ID=sa;Password={dbPassword};TrustServerCertificate=True;";
        }
    }
}
