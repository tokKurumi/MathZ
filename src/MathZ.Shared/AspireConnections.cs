namespace MathZ.Shared;

public static class AspireConnections
{
    public static class Api
    {
        public const string IdentityApi = "mathz-services-identityapi";
        public const string EmailApi = "mathz-services-emailapi";
        public const string ForumApi = "mathz-services-forumapi";
    }

    public static class Database
    {
        public const string IdentityDatabaseServer = "mathz-databases-identitypostgres";
        public const string IdentityDatabase = "mathz-databases-identitydatabase";
        public const string EmailDatabaseServer = "mathz-databases-emailpostgres";
        public const string EmailDatabase = "mathz-databases-emaildatabase";
        public const string ForumDatabaseServer = "mathz-databases-forumpostgres";
        public const string ForumDatabase = "mathz-databases-forumdatabase";
    }

    public static class MessageBuss
    {
        public const string RabbitMQ = "mathz-messagebus-rabbitmq";
    }
}
