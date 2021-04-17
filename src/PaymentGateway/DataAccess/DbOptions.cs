namespace PaymentGateway.DataAccess
{
    public record DbOptions
    {
        public string MYSQL_HOST { get; init; }
        public string MYSQL_DB { get; init; }
        public string MYSQL_PORT { get; init; }
        public string MYSQL_USER { get; init; }
        public string MYSQL_PASSWORD { get; init; }

        public string ConnectionString => $"Server={MYSQL_HOST};Database={MYSQL_DB};Uid={MYSQL_USER};Pwd={MYSQL_PASSWORD};Port={MYSQL_PORT};CharSet=utf8;ConnectionLifeTime=120;Pooling=True";
    }
}