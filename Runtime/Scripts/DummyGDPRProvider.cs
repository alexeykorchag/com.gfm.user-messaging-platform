#pragma warning disable IDE0063 // Использовать простой оператор using


namespace GFM.UserMessagingPlatform
{
    public class DummyGDPRProvider : IGDPRProvider
    {
        public DummyGDPRProvider()
        {

        }

        public void SetTagForUnderAgeOfConsent(bool value) { }

        public void AddTestDeviceHashedId(DebugGeography debugGeography, string[] ids) { }

        public void Reset() { }

        public void RequestConsentInfoUpdate(AndroidPluginCallback callback)
        {
            callback.onError("NotImplementedException");
        }

        public bool IsConsentFormAvailable() => false;

        public ConsentStatus GetConsentStatus() => ConsentStatus.UNKNOWN;

        public void LoadForm(AndroidPluginCallback callback)
        {
            callback.onError("NotImplementedException");
        }

        public void Show(AndroidPluginCallback callback)
        {
            callback.onError("NotImplementedException");
        }

    }
}
