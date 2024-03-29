﻿using Confluent.Kafka;

namespace Training.NG.KafkaHelper
{
    public class KafkaProducer<K, V>
    {
        readonly IProducer<K, V> kafkaHandle;
        public KafkaProducer(KafkaClientHandle handle)
        {
            kafkaHandle = new DependentProducerBuilder<K, V>(handle.Handle).Build();
        }
        public Task ProduceAsync(string topic, Message<K, V> message) =>
            this.kafkaHandle.ProduceAsync(topic, message);
        public void Produce(
            string topic,
            Message<K, V> message,
            Action<DeliveryReport<K, V>> deliveryHandler = null
        ) => this.kafkaHandle.Produce(topic, message, deliveryHandler);
        public void Flush(TimeSpan timeout) => this.kafkaHandle.Flush(timeout);
    }
}
