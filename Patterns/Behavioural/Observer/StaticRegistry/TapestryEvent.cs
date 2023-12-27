/// <summary>
/// An event that stores a list of subscribers. 
/// </summary>
namespace SombraStudios.Shared.Patterns.Behavioural.Observer.StaticRegistry
{
    /// <summary>
    /// Represents a basic event without parameters.
    /// </summary>
    public class TapestryEvent
    {
        System.Action repeatUseDelegate;
        System.Action singleUseDelegate;

        /// <summary>
        /// Invokes the event, triggering all subscribed methods.
        /// </summary>
        public void Invoke()
        {
            if (repeatUseDelegate != null)
            {
                repeatUseDelegate();
            }
            if (singleUseDelegate != null)
            {
                singleUseDelegate();
                singleUseDelegate = null;
            }
        }

        /// <summary>
        /// Subscribes a method to the event.
        /// </summary>
        /// <param name="inAction">The method to subscribe.</param>
        /// <param name="isSingleUse">Whether the subscription is for a single use.</param>
        public void SubscribeMethod(System.Action inAction, bool isSingleUse)
        {
            if (isSingleUse)
            {
                singleUseDelegate += inAction;
            }
            else
            {
                repeatUseDelegate += inAction;
            }
        }

        /// <summary>
        /// Removes a repeating method from the event's subscriptions.
        /// </summary>
        /// <param name="inAction">The method to remove.</param>
        public void RemoveRepeatingMethod(System.Action inAction)
        {
            repeatUseDelegate -= inAction;
        }

        /// <summary>
        /// Removes a one-shot method from the event's subscriptions.
        /// </summary>
        /// <param name="inAction">The method to remove.</param>
        public void RemoveOneShotMethod(System.Action inAction)
        {
            singleUseDelegate -= inAction;
        }

        /// <summary>
        /// Gets the total count of subscribed methods for this event.
        /// </summary>
        /// <returns>The total count of subscribed methods.</returns>
        public int GetInvocationCount()
        {
            return (
                (repeatUseDelegate == null ? 0 : repeatUseDelegate.GetInvocationList().Length)
                + (singleUseDelegate == null ? 0 : singleUseDelegate.GetInvocationList().Length));
        }
    }

    /// <summary>
    /// Represents an event with a single parameter.
    /// </summary>
    public class TapestryEvent<T>
    {
        System.Action<T> repeatUseDelagate;
        System.Action<T> singleUseDelegate;


        /// <summary>
        /// Invokes the event, triggering all subscribed methods.
        /// </summary>
        public void Invoke(T inParams)
        {
            if (repeatUseDelagate != null)
            {
                repeatUseDelagate(inParams);
            }
            if (singleUseDelegate != null)
            {
                singleUseDelegate(inParams);
                singleUseDelegate = null;
            }
        }

        /// <summary>
        /// Subscribes a method to the event.
        /// </summary>
        /// <param name="inAction">The method to subscribe.</param>
        /// <param name="isSingleUse">Whether the subscription is for a single use.</param>
        public void SubscribeMethod(System.Action<T> inAction, bool isSingleUse = false)
        {
            if (isSingleUse)
            {
                singleUseDelegate += inAction;
            }
            else
            {
                repeatUseDelagate += inAction;
            }
        }

        /// <summary>
        /// Removes a repeating method from the event's subscriptions.
        /// </summary>
        /// <param name="inAction">The method to remove.</param>
        public void RemoveRepeatingMethod(System.Action<T> inAction)
        {
            repeatUseDelagate -= inAction;
        }

        /// <summary>
        /// Removes a one-shot method from the event's subscriptions.
        /// </summary>
        /// <param name="inAction">The method to remove.</param>
        public void RemoveOneShotMethod(System.Action<T> inAction)
        {
            singleUseDelegate -= inAction;
        }

        /// <summary>
        /// Gets the total count of subscribed methods for this event.
        /// </summary>
        /// <returns>The total count of subscribed methods.</returns>
        public int GetInvocationCount()
        {
            return (
                (repeatUseDelagate == null ? 0 : repeatUseDelagate.GetInvocationList().Length)
                + (singleUseDelegate == null ? 0 : singleUseDelegate.GetInvocationList().Length));
        }
    }

    /// <summary>
    /// Represents an event with two parameters.
    /// </summary>
    public class TapestryEvent<T1, T2>
    {
        System.Action<T1, T2> repeatUseDelagate;
        System.Action<T1, T2> singleUseDelagate;


        /// <summary>
        /// Invokes the event, triggering all subscribed methods.
        /// </summary>
        public void Invoke(T1 inParams1, T2 inParams2)
        {
            if (repeatUseDelagate != null)
            {
                repeatUseDelagate(inParams1, inParams2);
            }
            if (singleUseDelagate != null)
            {
                singleUseDelagate(inParams1, inParams2);
                singleUseDelagate = null;
            }
        }

        /// <summary>
        /// Subscribes a method to the event.
        /// </summary>
        /// <param name="inAction">The method to subscribe.</param>
        /// <param name="isSingleUse">Whether the subscription is for a single use.</param>
        public void SubscribeMethod(System.Action<T1, T2> inAction, bool isSingleUse)
        {
            if (isSingleUse)
            {
                singleUseDelagate += inAction;
            }
            else
            {
                repeatUseDelagate += inAction;
            }
        }

        /// <summary>
        /// Removes a repeating method from the event's subscriptions.
        /// </summary>
        /// <param name="inAction">The method to remove.</param>
        public void RemoveRepeatingMethod(System.Action<T1, T2> inAction)
        {
            repeatUseDelagate -= inAction;
        }

        /// <summary>
        /// Removes a one-shot method from the event's subscriptions.
        /// </summary>
        /// <param name="inAction">The method to remove.</param>
        public void RemoveOneShotMethod(System.Action<T1, T2> inAction)
        {
            singleUseDelagate -= inAction;
        }

        /// <summary>
        /// Gets the total count of subscribed methods for this event.
        /// </summary>
        /// <returns>The total count of subscribed methods.</returns>
        public int GetInvocationCount()
        {
            return (
                (repeatUseDelagate == null ? 0 : repeatUseDelagate.GetInvocationList().Length)
                + (singleUseDelagate == null ? 0 : singleUseDelagate.GetInvocationList().Length));
        }
    }

    /// <summary>
    /// Represents an event with three parameters.
    /// </summary>
    public class TapestryEvent<T1, T2, T3>
    {
        System.Action<T1, T2, T3> repeatUseDelegate;
        System.Action<T1, T2, T3> singleUseDelegate;


        /// <summary>
        /// Invokes the event, triggering all subscribed methods.
        /// </summary>
        public void Invoke(T1 inParams1, T2 inParams2, T3 inParams3)
        {
            if (repeatUseDelegate != null)
            {
                repeatUseDelegate(inParams1, inParams2, inParams3);
            }
            if (singleUseDelegate != null)
            {
                singleUseDelegate(inParams1, inParams2, inParams3);
                singleUseDelegate = null;
            }
        }

        /// <summary>
        /// Subscribes a method to the event.
        /// </summary>
        /// <param name="inAction">The method to subscribe.</param>
        /// <param name="isSingleUse">Whether the subscription is for a single use.</param>
        public void SubscribeMethod(System.Action<T1, T2, T3> inAction, bool isSingleUse)
        {
            if (isSingleUse)
            {
                singleUseDelegate += inAction;
            }
            else
            {
                repeatUseDelegate += inAction;
            }
        }

        /// <summary>
        /// Removes a repeating method from the event's subscriptions.
        /// </summary>
        /// <param name="inAction">The method to remove.</param>
        public void RemoveRepeatingMethod(System.Action<T1, T2, T3> inAction)
        {
            repeatUseDelegate -= inAction;
        }

        /// <summary>
        /// Removes a one-shot method from the event's subscriptions.
        /// </summary>
        /// <param name="inAction">The method to remove.</param>
        public void RemoveOneShotMethod(System.Action<T1, T2, T3> inAction)
        {
            singleUseDelegate -= inAction;
        }

        /// <summary>
        /// Gets the total count of subscribed methods for this event.
        /// </summary>
        /// <returns>The total count of subscribed methods.</returns>
        public int GetInvocationCount()
        {
            return (
                (repeatUseDelegate == null ? 0 : repeatUseDelegate.GetInvocationList().Length)
                + (singleUseDelegate == null ? 0 : singleUseDelegate.GetInvocationList().Length));
        }
    }

    /// <summary>
    /// Represents an event with five parameters.
    /// </summary>
    public class TapestryEvent<T1, T2, T3, T4, T5>
    {
        System.Action<T1, T2, T3, T4, T5> repeatUseDelegate;
        System.Action<T1, T2, T3, T4, T5> singleUseDelegate;


        /// <summary>
        /// Invokes the event, triggering all subscribed methods.
        /// </summary>
        public void Invoke(T1 inParams1, T2 inParams2, T3 inParams3, T4 inParams4, T5 inParam5)
        {
            if (repeatUseDelegate != null)
            {
                repeatUseDelegate(inParams1, inParams2, inParams3, inParams4, inParam5);
            }
            if (singleUseDelegate != null)
            {
                singleUseDelegate(inParams1, inParams2, inParams3, inParams4, inParam5);
                singleUseDelegate = null;
            }
        }

        /// <summary>
        /// Subscribes a method to the event.
        /// </summary>
        /// <param name="inAction">The method to subscribe.</param>
        /// <param name="isSingleUse">Whether the subscription is for a single use.</param>
        public void SubscribeMethod(System.Action<T1, T2, T3, T4, T5> inAction, bool isSingleUse)
        {
            if (isSingleUse)
            {
                singleUseDelegate += inAction;
            }
            else
            {
                repeatUseDelegate += inAction;
            }
        }

        /// <summary>
        /// Removes a repeating method from the event's subscriptions.
        /// </summary>
        /// <param name="inAction">The method to remove.</param>
        public void RemoveRepeatingMethod(System.Action<T1, T2, T3, T4, T5> inAction)
        {
            repeatUseDelegate -= inAction;
        }

        /// <summary>
        /// Removes a one-shot method from the event's subscriptions.
        /// </summary>
        /// <param name="inAction">The method to remove.</param>
        public void RemoveOneShotMethod(System.Action<T1, T2, T3, T4, T5> inAction)
        {
            singleUseDelegate -= inAction;
        }

        /// <summary>
        /// Gets the total count of subscribed methods for this event.
        /// </summary>
        /// <returns>The total count of subscribed methods.</returns>
        public int GetInvocationCount()
        {
            return (
                (repeatUseDelegate == null ? 0 : repeatUseDelegate.GetInvocationList().Length)
                + (singleUseDelegate == null ? 0 : singleUseDelegate.GetInvocationList().Length));
        }
    }
}
