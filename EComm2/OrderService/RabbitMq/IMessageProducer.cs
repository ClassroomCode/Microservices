namespace OrderService.RabbitMq;

public interface IMessageProducer
{
    void SendMessage<T>(T message);
}

// RabbitMQ Docker container:
// docker run -d --hostname my-rabbitmq-server --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
