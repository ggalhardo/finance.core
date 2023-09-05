using System;

namespace Finance.Core.Logging
{
    public class LoggingTracking
    {
        public Guid TrackingId {  get; private set; }

        public LoggingTracking(Guid trackingId)
        {
            this.TrackingId = trackingId;
        }
    }
}
