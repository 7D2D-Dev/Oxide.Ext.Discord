namespace Oxide.Ext.Discord.Exceptions.Entities.Guild.ScheduledEvents
{
    /// <summary>
    /// Represents an exception in guild scheduled events
    /// </summary>
    public class InvalidGuildScheduledEventException : BaseDiscordException
    {
        private InvalidGuildScheduledEventException(string message) : base(message) { }
        
        internal static void ThrowIfInvalidName(string name, bool allowNullOrEmpty)
        {
            if (!allowNullOrEmpty && string.IsNullOrEmpty(name))
            {
                throw new InvalidGuildScheduledEventException("Name cannot be less than 1 character");
            }
            
            if (name.Length > 100)
            {
                throw new InvalidGuildScheduledEventException("Name cannot be more than 100 characters");
            }
        }
        
        internal static void ThrowIfInvalidDescription(string description)
        {
            if (!string.IsNullOrEmpty(description) && description.Length > 1000)
            {
                throw new InvalidGuildScheduledEventException("Description cannot be more than 1000 characters");
            }
        }
        
        internal static void ThrowIfInvalidLocation(string location)
        {
            if (!string.IsNullOrEmpty(location) && location.Length > 100)
            {
                throw new InvalidGuildScheduledEventException("Location cannot be more than 100 characters");
            }
        }
    }
}