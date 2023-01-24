namespace Smiosoft.PASS.RabbitMQ
{
    /// <summary>
    /// Extensions to register everything PASS for RabbitMQ
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Scope registration to only RabbitMQ related PASS types
        /// </summary>
        /// <param name="source">PASS service configuration</param>
        /// <returns>PASS service configuration</returns>
        public static PassServiceConfiguration WithRabbitMq(this PassServiceConfiguration source)
        {
            source.WithEvaluator((type) => typeof(IRabbitMq).IsAssignableFrom(type));
            return source;
        }
    }
}
