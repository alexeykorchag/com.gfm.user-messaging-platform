#pragma warning disable IDE0063 // Использовать простой оператор using

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

        public void SetTagForUnderAgeOfConsent(bool value)
        {
            if (_helperObject == null) return;
            _helperObject.Call("setTagForUnderAgeOfConsent", value);
        }

        public void AddTestDeviceHashedId(DebugGeography debugGeography, string[] ids)
        {
            if (_helperObject == null) return;
            _helperObject.Call("addTestDeviceHashedId", (int) debugGeography, ids);
        }

        public void Reset()
        {
            if (_helperObject == null) return;
            _helperObject.Call("reset");
        }

        public void RequestConsentInfoUpdate(AndroidPluginCallback callback)
        {
            _helperObject.Call("requestConsentInfoUpdate", callback);
        }

        public bool IsConsentFormAvailable()
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

        public void LoadForm(AndroidPluginCallback callback)
        {
            _helperObject.Call("loadForm", callback);
        }

        public void Show(AndroidPluginCallback callback)
        {
            _helperObject.Call("show", callback);
        }

    }
}
