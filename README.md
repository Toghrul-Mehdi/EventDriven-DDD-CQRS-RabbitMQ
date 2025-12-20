# DDD-CQRS-EventDriven-RabbitMQ

A Clean DDD monolithic application implementing CQRS pattern and Event-Driven Architecture with RabbitMQ. Features domain events, bounded contexts, aggregates, and asynchronous messaging for scalable enterprise solutions.

## 🏗️ Architecture Overview

This project demonstrates a modern monolithic architecture using:

- **Domain-Driven Design (DDD)** - Tactical patterns including Aggregates, Entities, Value Objects, and Domain Events
- **CQRS Pattern** - Command Query Responsibility Segregation using MediatR
- **Event-Driven Architecture** - Asynchronous communication between bounded contexts via RabbitMQ
- **Clean Architecture** - Separation of concerns with clear layer boundaries

## 🚀 Tech Stack

- **.NET 8** - Latest .NET framework
- **Entity Framework Core** - ORM for data persistence
- **MediatR** - In-process messaging for CQRS implementation
- **RabbitMQ** - Message broker for event-driven communication
- **SQL Server** - Relational database
- **FluentValidation** - Command/Query validation
- **AutoMapper** - Object mapping (optional)

## 📁 Project Structure

```
src/
├── ECommerce.API/              # Presentation Layer (Controllers, Middleware)
├── ECommerce.Application/      # Application Layer (Commands, Queries, Handlers)
├── ECommerce.Domain/           # Domain Layer (Entities, Events, Business Logic)
├── ECommerce.Infrastructure/   # Infrastructure Layer (EF, RabbitMQ, External Services)
└── ECommerce.SharedKernel/     # Shared utilities and base classes
```

## 🎯 Key Features

### Domain-Driven Design
- ✅ Bounded Contexts (Ordering, Payment, Inventory, Products)
- ✅ Aggregates and Aggregate Roots
- ✅ Value Objects
- ✅ Domain Events
- ✅ Repository Pattern
- ✅ Unit of Work Pattern

### CQRS Pattern
- ✅ Separate Command and Query models
- ✅ Command Handlers for write operations
- ✅ Query Handlers for read operations
- ✅ Validation Pipeline with FluentValidation
- ✅ Transaction Management Pipeline

### Event-Driven Architecture
- ✅ Domain Events for in-process communication
- ✅ Integration Events via RabbitMQ
- ✅ Event Handlers for cross-domain reactions
- ✅ Publish/Subscribe pattern
- ✅ Asynchronous message processing

## 🔄 Event Flow Example

```
1. User creates an Order
   └─> OrderCreatedEvent (Domain Event)
       ├─> OrderCreatedEventHandler (Application Layer)
       │   └─> Publishes to RabbitMQ
       │
       ├─> Payment Service subscribes
       │   └─> Creates Payment record
       │   └─> PaymentProcessedEvent
       │
       └─> Inventory Service subscribes
           └─> Reserves Stock
           └─> StockReservedEvent
```

## 🛠️ Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server
- RabbitMQ (Docker recommended)
- Visual Studio 2022 or Rider

### Installation

1. **Clone the repository**
```bash
git clone https://github.com/yourusername/DDD-CQRS-EventDriven-RabbitMQ.git
cd DDD-CQRS-EventDriven-RabbitMQ
```

2. **Start RabbitMQ with Docker**
```bash
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

3. **Update connection strings**
```json
// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ECommerceDB;Trusted_Connection=True;"
  },
  "RabbitMQ": {
    "Host": "localhost",
    "Port": 5672,
    "Username": "guest",
    "Password": "guest"
  }
}
```

4. **Run migrations**
```bash
cd src/ECommerce.API
dotnet ef database update
```

5. **Run the application**
```bash
dotnet run
```

6. **Access the API**
- Swagger UI: `https://localhost:7001/swagger`
- RabbitMQ Management: `http://localhost:15672` (guest/guest)

## 📚 Project Domains

### Ordering Domain
- Create, cancel, and ship orders
- Order status management
- Domain events: `OrderCreatedEvent`, `OrderCancelledEvent`, `OrderShippedEvent`

### Payment Domain
- Process payments
- Handle payment success/failure
- Listens to: `OrderCreatedEvent`
- Publishes: `PaymentSucceededEvent`, `PaymentFailedEvent`

### Inventory Domain
- Stock management
- Reserve and release stock
- Listens to: `OrderCreatedEvent`, `OrderCancelledEvent`
- Publishes: `StockReservedEvent`, `StockReleasedEvent`

### Products Domain
- Product catalog management
- Price updates
- Category management

## 🔍 Code Examples

### Creating a Command

```csharp
public record CreateOrderCommand(
    string UserId,
    List<OrderItemDto> Items) : IRequest<Result<int>>;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<int>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public async Task<Result<int>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var items = request.Items.Select(i => 
            OrderItem.Create(i.ProductId, i.Quantity, i.Price)).ToList();
            
        var order = Order.Create(request.UserId, items);
        await _orderRepository.AddAsync(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<int>.Success(order.Id);
    }
}
```

### Domain Event Handler

```csharp
public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
{
    private readonly IEventBus _eventBus;

    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        // Publish to RabbitMQ for other services
        await _eventBus.PublishAsync(new OrderCreatedIntegrationEvent(
            notification.OrderId,
            notification.UserId,
            notification.TotalAmount
        ));
    }
}
```

## 🧪 Testing

```bash
# Run unit tests
dotnet test tests/ECommerce.UnitTests/

# Run integration tests
dotnet test tests/ECommerce.IntegrationTests/
```

## 📖 Learning Resources

- [Domain-Driven Design by Eric Evans](https://www.domainlanguage.com/ddd/)
- [Implementing DDD by Vaughn Vernon](https://vaughnvernon.com/)
- [CQRS Pattern - Microsoft Docs](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [Event-Driven Architecture](https://martinfowler.com/articles/201701-event-driven.html)

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👤 Author

**Toghrul Mehdiyev**
- GitHub: [@Toghrul-Mehdi](https://github.com/Toghrul-Mehdi)
- LinkedIn: [Togrul Mehdiyev](https://linkedin.com/in/togrul-mehdiyev-ab486335a)

## ⭐ Show your support

Give a ⭐️ if this project helped you learn DDD, CQRS, and Event-Driven Architecture!

---

**Note:** This is a learning/demonstration project. For production use, consider additional aspects like:
- Distributed tracing (OpenTelemetry)
- Circuit breakers (Polly)
- API Gateway
- Authentication/Authorization
- Logging and Monitoring
- Health checks
- Docker containerization
