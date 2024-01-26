using Confluent.Kafka;

namespace Training.NG.KafkaHelper
{
    public class KafkaClientHandle : IDisposable
    {
        readonly IProducer<byte[], byte[]> kafkaProducer;
        public KafkaClientHandle(KafkaConfig config)
        {
            var conf = new ProducerConfig
            {
                BootstrapServers = config.BoostrapServers
            };
            this.kafkaProducer = new ProducerBuilder<byte[], byte[]>(conf).Build();
        }
        public Handle Handle
        {
            get => this.kafkaProducer.Handle;
        }
        public void Dispose()
        {
            kafkaProducer.Flush();
            kafkaProducer.Dispose();
        }
    }
}
