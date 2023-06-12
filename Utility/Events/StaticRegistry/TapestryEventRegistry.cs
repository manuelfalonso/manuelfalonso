/// <summary>
/// Event Registry with static references
/// </summary>
namespace Tapestry
{

    public class TapestryEventRegistry
    {
        #region Example
        /// <summary>
        /// Reports when an Integer value changed
        /// </summary>
        public static TapestryEvent<int> OnIntegerChangedExample;
        #endregion

        static TapestryEventRegistry()
        {
            CreateTapestryEvents();
        }

        private static void CreateTapestryEvents()
        {
            #region Example
            OnIntegerChangedExample = new TapestryEvent<int>();
            #endregion
        }

        public static void OnDestroy()
        {
            // Creates new events to clear the old ones
            CreateTapestryEvents();
        }
    }
}