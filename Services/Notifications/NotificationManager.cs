using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.Android;
#if REQUIRES_EXTERNAL_PACKAGE
// Mobile Notifications package
using Unity.Notifications.Android;
#endif
#endif
#if UNITY_IOS
#if REQUIRES_EXTERNAL_PACKAGE
// Mobile Notifications package
using Unity.Notifications.iOS;
#endif
#endif

namespace SombraStudios.Shared.Services.Notifications
{
    /// <summary>
    /// Unity Mobile Notifications documentation:
    /// https://docs.unity3d.com/Packages/com.unity.mobile.notifications@2.3/manual/index.html
    /// </summary>
    public class NotificationManager : MonoBehaviour
    {
        private static string ANDROID_NOTIFICATION_PERMISSION = "android.permission.POST_NOTIFICATIONS";

        private void Start()
        {
            SendTestNotification();
        }


        /// <summary>
        /// Check Android Permission
        /// Initialize Android Notification Channel
        /// </summary>
        private void SendTestNotification()
        {
#if UNITY_ANDROID
            if (!Permission.HasUserAuthorizedPermission(ANDROID_NOTIFICATION_PERMISSION))
            {
                Permission.RequestUserPermission(ANDROID_NOTIFICATION_PERMISSION);
            }

#if REQUIRES_EXTERNAL_PACKAGE
            var channel = new AndroidNotificationChannel()
            {
                Id = "channel_id",
                Name = "Default Channel",
                Importance = Importance.Default,
                Description = "Generic notifications",
            };

            AndroidNotificationCenter.RegisterNotificationChannel(channel);
#endif
            SendNotification("Test notification", "This is a test notification", 10);
#endif
        }


        /// <summary>
        /// Send Test Notification for either Android or iOS
        /// </summary>
        /// <param name="title">Notification Title</param>
        /// <param name="message">Notification body message</param>
        /// <param name="time">Time in minutes since the method is called to send the Notification</param>
        public void SendNotification(string title, string message, float time)
        {
#if REQUIRES_EXTERNAL_PACKAGE
#if UNITY_ANDROID
            var AndroidNotification = new AndroidNotification()
            {
                Title = title,
                Text = message,
                SmallIcon = "icon",
                LargeIcon = "logo",
                FireTime = DateTime.Now.AddMinutes(time),
            };

            AndroidNotificationCenter.SendNotification(AndroidNotification, "channel_id");
#elif UNITY_IOS
            var timeTrigger = new iOSNotificationTimeIntervalTrigger()
            {
                TimeInterval = TimeSpan.FromMinutes(time),
                Repeats = false
            };

            var notification = new iOSNotification()
            {
                Identifier = "_notification_01",
                Title = title,
                Body = message,
                Subtitle = "Test Subtitle",
                ShowInForeground = true,
                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                CategoryIdentifier = "category_id",
                Trigger = timeTrigger,
            };

            iOSNotificationCenter.ScheduleNotification(notification);
#endif
#endif
        }
    }
}
