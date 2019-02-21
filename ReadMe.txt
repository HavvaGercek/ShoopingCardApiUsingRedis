Sepete Ürün Ekleme API

Uygulamayý baþlatmdan önce Redis ayarlarýnýn yapýlmasý gerekmektedir.


Startup/ConfigureServices içerisinde options.Configuration kýsmý güncelenmeli ve redis server çalýþýr halde olmalýdýr.

services.AddDistributedRedisCache(options =>
  {
      options.InstanceName = "RedisNetCore";
      options.Configuration = "localhost: 6379"; //Your Redis connection port
  });


Apý Kullanýmý için:  http://localhost:15350/swagger/index.html 
