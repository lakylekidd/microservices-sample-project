namespace Microservices.Library.IntegrationEventLogEF
{
    /// <summary>
    /// Defines the different states of an integration event log
    /// </summary>
    public enum EventStateEnum
    {
        /// <summary>
        /// The log is not published yet
        /// </summary>
        NotPublished = 0,

        /// <summary>
        /// The log publishing is in progress
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// The log has been published
        /// </summary>
        Published = 2,

        /// <summary>
        /// The publishing of the log has failed
        /// </summary>
        PublishedFailed = 3
    }
}
