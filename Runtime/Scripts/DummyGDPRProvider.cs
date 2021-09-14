namespace GFM.UserMessagingPlatform
{
    public class DummyGDPRProvider : IGDPRProvider
    {
        public DummyGDPRProvider()
        {

        }

        public void SetTagForUnderAge(bool value) { }

        public void SetTestDevices(DebugGeography debugGeography, string[] ids) { }

        public void ResetConsent() { }

        public void UpdateConsentInfo(PluginCallback callback)
        {
            callback.OnError("NotImplementedException");
        }

        public bool ConsentIsAvailable() => false;

        public ConsentStatus GetConsentStatus() => UserMessagingPlatform.ConsentStatus.UNKNOWN;

        public void LoadForm(PluginCallback callback)
        {
            callback.OnError("NotImplementedException");
        }

        public void ShowForm(PluginCallback callback)
        {
            callback.OnError("NotImplementedException");
        }

        public override string ToString() => "DummyGDPRProvider";

    }
}
