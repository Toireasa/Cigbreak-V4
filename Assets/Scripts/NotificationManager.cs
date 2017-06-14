
namespace Plugins.Android
{
    using UnityEngine;

    public static class NotificationManager
    {
        public static void Create(string title, string text)
        {
            Create(title, text, "app_icon");
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        public static void Create(string title, string text, string appIcon)
        {
            notificationManagerClass.CallStatic("createNotification", new object[] { title, text, appIcon });
        }

        public static void CancelAll()
        {
            notificationManagerClass.CallStatic("cancelAll");
        }

        private static AndroidJavaClass notificationManagerClass = new AndroidJavaClass("com.sonia_paul.notifications.NotificationManager");
#else
        public static void Create(string title, string text, string appIcon)
        {
            Debug.Log(string.Format("Create Notification\nTitle: {0}\nText: {1}\nIcon: {2}", title, text, appIcon));
        }

        public static void CancelAll()
        {
            Debug.Log("Cancel All Notifications");
        }
#endif
    }
}