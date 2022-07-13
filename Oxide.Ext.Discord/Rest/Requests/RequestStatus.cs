namespace Oxide.Ext.Discord.Rest.Requests
{
    /// <summary>
    /// Discord API Request Status
    /// </summary>
    public enum RequestStatus
    {
        /// <summary>
        /// Request is in the queue waiting to be processed
        /// </summary>
        InQueue,
        
        /// <summary>
        /// Requesting is waiting for bucket to be ready
        /// </summary>
        PendingBucket,
        
        /// <summary>
        /// Waiting to create request data
        /// </summary>
        PendingCreateData,
        
        /// <summary>
        /// Request is waiting to start
        /// </summary>
        PendingStart,
        
        /// <summary>
        /// Request is in progress
        /// </summary>
        InProgress,
        
        /// <summary>
        /// Request completed and was not cancelled
        /// </summary>
        Completed
    }
}