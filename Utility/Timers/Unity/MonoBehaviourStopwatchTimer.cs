using SombraStudios.Shared.Utility.Timers.Core;

namespace SombraStudios.Shared.Utility.Timers.Unity
{
    public class MonoBehaviourStopwatchTimer : MonoBehaviourTimer<StopwatchTimer>
    {
        public float ElapsedTime => _timer.ElapsedTime;

        protected override void InitializeTimer()
        {
            _timer = new StopwatchTimer();
        }
    }
}
