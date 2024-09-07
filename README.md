# E-Commerce Microservice Projesi

Bu proje, mikroservis mimarisi kullanılarak geliştirilmiş bir e-ticaret uygulamasıdır. Uygulama, bağımsız çalışan servislerin birleşimiyle çalışır ve yüksek ölçeklenebilirlik ile bağımsız geliştirme avantajları sunar.

## Kullanılan Teknolojiler
- **.NET Core**: Servislerin geliştirilmesinde kullanılan ana framework.
- **PostgreSQL**: Veritabanı yönetimi.
- **Entity Framework Core**: Veritabanı işlemleri için.
- **Repository Pattern**: Veritabanı erişim yönetimi için.
- **MassTransit**: Mesajlaşma için.
- **RabbitMQ**: Servisler arası mesajlaşma ve olay tabanlı iletişim için.
- **Polly**: RabbitMQ consumer içinde hata yönetimi ve tekrar deneme stratejileri için.
- **Serilog & Elasticsearch**: Loglama ve logların Elasticsearch'e gönderilmesi için.
- **Refit**: HTTP isteklerini kolaylaştırmak için.
- **Docker**: RabbitMQ, PostgreSQL, Elasticsearch ve Kibana Docker üzerinde oluşturulup çalıştırıldı.

## Mikroservisler
1. **OrderService**: Siparişlerin yönetimi.
2. **StockService**: Stokların takibi ve yönetimi. Worker servis içerisinde bir RabbitMQ consumer bulunur. Bu consumer, gelen mesajları alır ve HTTP isteği ile stok servisine iletir. Ayrıca, HTTP istekleri için **Refit** kullanılır ve hata yönetimi için **Polly** entegrasyonu sağlanmıştır.
3. **NotificationService**: Bildirimlerin gönderilmesi. Worker servis içinde bir RabbitMQ consumer bulunur ve bu consumer, gelen mesajları alarak ilgili Notification API'ye HTTP isteği atar.

## Kurulum Talimatları
1. Projeyi klonlayın:
   ```bash
   git clone https://github.com/cem-altinay/ECommerceMicroservice.git

## Bağımlılıklar
- **RabbitMQ**: Mesajlaşma altyapısı.
- **PostgreSQL**: Veritabanı yönetimi.
- **Elasticsearch**: Log verilerinin depolanması.
