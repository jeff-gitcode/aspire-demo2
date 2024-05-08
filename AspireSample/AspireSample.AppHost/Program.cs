var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

// var postgresServer = builder.AddPostgres("postgres-db").WithPgAdmin();
// var db1 = postgresServer.AddDatabase("db1");
// var db2 = postgresServer.AddDatabase("db2");
// var db3 = postgresServer.AddDatabase("db3");
// var db4 = postgresServer.AddDatabase("db4");

// var redisCache1 = builder.AddRedis("redis-cache").WithRedisCommander();
// var redisCache2 = builder.AddRedis("redis-cache-2").WithRedisCommander();
// var redisCache3 = builder.AddRedis("redis-cache-3").WithRedisCommander();

// var mongoContainer = builder.AddMongoDB("mongo-db").WithMongoExpress();
// var mongoDb1 = mongoContainer.AddDatabase("mongo-db-1");
// var mongoDb2 = mongoContainer.AddDatabase("mongo-db-2");
// var mongoDb3 = mongoContainer.AddDatabase("mongo-db-3");
// var mongoDb4 = mongoContainer.AddDatabase("mongo-db-4");

// var kafka = builder.AddKafka("kafka");

// var mySqlDbServer = builder.AddMySql("mysql").WithPhpMyAdmin();
// var mySqlDb1 = mySqlDbServer.AddDatabase("mysql-db-1");
// var mySqlDb2 = mySqlDbServer.AddDatabase("mysql-db-2");
// var mySqlDb3 = mySqlDbServer.AddDatabase("mysql-db-3");

var apiService = builder.AddProject<Projects.AspireSample_ApiService>("apiservice");
builder.AddProject<Projects.TodoApi>("TodoApi");

// builder.AddProject<Projects.AspireSample_ApiService>("microservice01").WithReference(db1).WithReference(redisCache1);
// builder.AddProject<Projects.AspireSample_ApiService>("microservice02").WithReference(db2).WithReference(redisCache2).WithReference(kafka);
// builder.AddProject<Projects.AspireSample_ApiService>("microservice03").WithReference(db2).WithReference(redisCache3).WithReference(kafka);
// builder.AddProject<Projects.AspireSample_ApiService>("microservice04").WithReference(db2).WithReference(mySqlDb1).WithReference(kafka);
// builder.AddProject<Projects.AspireSample_ApiService>("microservice05").WithReference(db2).WithReference(mySqlDb2).WithReference(kafka);
// builder.AddProject<Projects.AspireSample_ApiService>("microservice06").WithReference(db2).WithReference(mySqlDb1).WithReference(kafka);
// builder.AddProject<Projects.AspireSample_ApiService>("microservice07").WithReference(db2).WithReference(mySqlDb1).WithReference(kafka);


builder.AddProject<Projects.AspireSample_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService);

builder.Build().Run();
