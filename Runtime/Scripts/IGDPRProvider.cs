namespace GFM.UserMessagingPlatform
{
    public interface IGDPRProvider
    {
        void SetTagForUnderAgeOfConsent(bool value);
        void AddTestDeviceHashedId(DebugGeography debugGeography, string[] ids);
        void Reset();
        void RequestConsentInfoUpdate(AndroidPluginCallback callback);
        bool IsConsentFormAvailable();
        ConsentStatus GetConsentStatus();
        void LoadForm(AndroidPluginCallback callback);
        void Show(AndroidPluginCallback callback);
    }
}