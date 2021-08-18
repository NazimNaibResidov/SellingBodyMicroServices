using System;

namespace EventBus.Base.Events
{
    public class SubscriptionInfo
    {
        public Type HandleType { get;private set; }

        public SubscriptionInfo(Type handleType)
        {
            HandleType = handleType?? throw new ArgumentNullException($"{nameof(HandleType)} is null");
        }
        public static SubscriptionInfo Typed(Type HandleType)
        {
            return new SubscriptionInfo(HandleType);
        }
    }
}
