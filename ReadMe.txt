Adding Product to Shopping Card API

Before starting app, must do settings for Redis.

part of options.Configuration in Startup/ConfigureServices must update like below and redis server must be started.

services.AddDistributedRedisCache(options =>
  {
      options.InstanceName = "RedisNetCore";
      options.Configuration = "localhost: 6379"; //Your Redis connection port
  });

For API Usage:  http://localhost:15350/swagger/index.html 
