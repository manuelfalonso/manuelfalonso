/// <summary>
/// Event Registry with static references
/// </summary>
namespace SombraStudios.Shared.Patterns.Behavioural.Observer.StaticRegistry
{
    /// <summary>
    /// Static class containing Tapestry events with static references.
    /// </summary>
    public class TapestryEventRegistry
    {
        #region Example
        /// <summary>
        /// Reports when an Integer value changed
        /// </summary>
        public static TapestryEvent<int> OnIntegerChangedExample;
        #endregion

        /// <summary>
        /// Static constructor that creates Tapestry events.
        /// </summary>
        static TapestryEventRegistry()
        {
            CreateTapestryEvents();
        }

        /// <summary>
        /// Creates Tapestry events.
        /// </summary>
        private static void CreateTapestryEvents()
        {
            #region Example
            OnIntegerChangedExample = new TapestryEvent<int>();
            #endregion
        }

        /// <summary>
        /// Clears and recreates Tapestry events on destruction to avoid lingering references.
        /// </summary>
        public static void OnDestroy()
        {
            // Creates new events to clear the old ones
            CreateTapestryEvents();
        }
    }
}