Sepete Ürün Ekleme API

Uygulamayı başlatmdan önce Redis ayarlarının yapılmalıdır.


Startup/ConfigureServices içerisinde options.Configuration kısmı aşağıdaki şekilde güncelenmeli ve redis server çalışır halde olmalı.

services.AddDistributedRedisCache(options =>
  {
      options.InstanceName = "RedisNetCore";
      options.Configuration = "localhost: 6379"; //Your Redis connection port
  });

