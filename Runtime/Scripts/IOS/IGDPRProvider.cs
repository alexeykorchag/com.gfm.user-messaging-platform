namespace GFM.UserMessagingPlatform
{
    public interface IGDPRProvider
    {
        void SetTagForUnderAge(bool value);
        void SetTestDevices(DebugGeography debugGeography, string[] ids);
        void ResetConsent();
        void UpdateConsentInfo(PluginCallback callback);
        bool ConsentIsAvailable();
        ConsentStatus GetConsentStatus();
        void LoadForm(PluginCallback callback);
        void ShowForm(PluginCallback callback);
    }
}