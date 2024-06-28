using Students.BuildingBlock.Exceptions;

namespace Students.BuildingBlock
{
    public static class ShareEnvironmentConfig
    {
        public static string GetRabbitMQHost()
        {
            var host = Environment.GetEnvironmentVariable("RabbitMQHost");

            if (string.IsNullOrWhiteSpace(host))
                throw new AppException("RabbitMQHost environment variable is not set.");

            return host;
        }
        public static string GetRabbitMQUser()
        {
            var host = Environment.GetEnvironmentVariable("RABBITMQUser");

            if (string.IsNullOrWhiteSpace(host))
                throw new AppException("RABBITMQUser environment variable is not set.");

            return host;
        }
        public static string GetRABBITMQPassword()
        {
            var host = Environment.GetEnvironmentVariable("RABBITMQPassword");

            if (string.IsNullOrWhiteSpace(host))
                throw new AppException("RABBITMQPassword environment variable is not set.");

            return host;
        }
    }
}
