Sepete Ürün Ekleme API

Uygulamayı başlatmdan önce Redis ayarları yapılmalıdır.


Startup/ConfigureServices içerisinde options.Configuration kısmı aşağıdaki şekilde güncelenmeli ve redis server çalışır halde olmalı.

services.AddDistributedRedisCache(options =>
  {
      options.InstanceName = "RedisNetCore";
      options.Configuration = "localhost: 6379"; //Your Redis connection port
  });

Api Kullanımı için:  http://localhost:15350/swagger/index.html 
