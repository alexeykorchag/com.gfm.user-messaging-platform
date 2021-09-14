#pragma warning disable IDE0063 // Использовать простой оператор using
#if UNITY_ANDROID
using UnityEngine;

namespace GFM.UserMessagingPlatform
{
    public class AndroidGDPRProvider : IGDPRProvider
    {
        private AndroidJavaObject _helperObject = null;

        public AndroidGDPRProvider()
        {
            using (var activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                var activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
                _helperObject = new AndroidJavaObject("com.gfm.UserMessagingPlatform.GDPRHelper", activityContext);
            }
        }

        public void SetTagForUnderAge(bool value)
        {
            if (_helperObject == null) return;
            _helperObject.Call("setTagForUnderAgeOfConsent", value);
        }

        public void SetTestDevices(DebugGeography debugGeography, string[] ids)
        {
            if (_helperObject == null) return;
            _helperObject.Call("addTestDeviceHashedId", (int) debugGeography, ids);
        }

        public void ResetConsent()
        {
            if (_helperObject == null) return;
            _helperObject.Call("reset");
        }

        public void UpdateConsentInfo(PluginCallback callback)
        {
            _helperObject.Call("requestConsentInfoUpdate", new AndroidPluginCallback(callback));
        }

        public bool ConsentIsAvailable()
        {
            if (_helperObject == null)
                return false;

            return _helperObject.Call<bool>("isConsentFormAvailable");
        }

        public ConsentStatus GetConsentStatus()
        {
            if (_helperObject == null)
                return ConsentStatus.UNKNOWN;

            return (ConsentStatus)_helperObject.Call<int>("getConsentStatus");
        }

        public void LoadForm(PluginCallback callback)
        {
            _helperObject.Call("loadForm", new AndroidPluginCallback(callback));
        }

        public void ShowForm(PluginCallback callback)
        {
            _helperObject.Call("show", new AndroidPluginCallback(callback));
        }

        public override string ToString() => "AndroidGDPRProvider";
    }
}
#endif