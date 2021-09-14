#if UNITY_IOS

using UnityEngine;

namespace GFM.UserMessagingPlatform
{

    public class IOSGDPRProvider : IGDPRProvider
    {
        private IOSGDPRProviderListener _listener;

        public IOSGDPRProvider()
        {
            var obj = new GameObject("IOSGDPRProviderListener");
            MonoBehaviour.DontDestroyOnLoad(obj);

            _listener = obj.AddComponent<IOSGDPRProviderListener>();

            IOSGDPRProviderBridge.Init();
        }

        public void SetTagForUnderAge(bool value)
            => IOSGDPRProviderBridge.SetTagForUnderAge(value);


        // TODO: Дописать чтоб заработали массивы
        public void SetTestDevices(DebugGeography debugGeography, string[] ids)
            => IOSGDPRProviderBridge.SetTestDevices(debugGeography, ids[0]);

        public void ResetConsent()
            => IOSGDPRProviderBridge.ResetConsent();

        public void UpdateConsentInfo(PluginCallback callback)
        {
            _listener.SetUpdateConsentInfo(callback);
            IOSGDPRProviderBridge.UpdateConsentInfo();
        }

        public bool ConsentIsAvailable() => IOSGDPRProviderBridge.IsAvailable();

        public ConsentStatus GetConsentStatus() => IOSGDPRProviderBridge.ConsentStatus();

        public void LoadForm(PluginCallback callback)
        {
            _listener.SetLoadForm(callback);
            IOSGDPRProviderBridge.Load();
        }

        public void ShowForm(PluginCallback callback)
        {
            _listener.SetShowForm(callback);
            IOSGDPRProviderBridge.Show();
        }
    }

}
#endif