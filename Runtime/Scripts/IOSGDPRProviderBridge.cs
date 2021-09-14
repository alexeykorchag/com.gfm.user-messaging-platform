#if UNITY_IOS
using System.Runtime.InteropServices;

namespace GFM.UserMessagingPlatform
{
    public static class IOSGDPRProviderBridge
    {
        [DllImport("__Internal")]
        private static extern void Create();

        [DllImport("__Internal")]
        private static extern void SetTagForUnderAgeOfConsent(bool value);

        [DllImport("__Internal")]
        private static extern void SetTestDevices(int geography, string array);

        [DllImport("__Internal")]
        private static extern void RequestConsentInfoUpdate();

        [DllImport("__Internal")]
        private static extern bool IsConsentFormAvailabl();

        [DllImport("__Internal")]
        private static extern int GetConsentStatus();

        [DllImport("__Internal")]
        private static extern void LoadForm();

        [DllImport("__Internal")]
        private static extern void ShowForm();

        [DllImport("__Internal")]
        private static extern void Reset();

        public static void Init() => Create();
        public static void SetTagForUnderAge(bool value) => SetTagForUnderAgeOfConsent(value);
        public static void SetTestDevices(DebugGeography debugGeography, string ids) => SetTestDevices((int)debugGeography, ids);
        public static void ResetConsent() => Reset();
        public static void UpdateConsentInfo() => RequestConsentInfoUpdate();
        public static bool IsAvailable() => IsConsentFormAvailabl();
        public static ConsentStatus ConsentStatus() => (ConsentStatus)GetConsentStatus();
        public static void Load() => LoadForm();
        public static void Show() => ShowForm();

    }
}
#endif