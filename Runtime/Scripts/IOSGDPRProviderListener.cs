#if UNITY_IOS

using UnityEngine;

namespace GFM.UserMessagingPlatform
{
    public class IOSGDPRProviderListener : MonoBehaviour
    {
        private PluginCallback _requestConsentInfoUpdateCallback;
        private PluginCallback _loadFormCallback;
        private PluginCallback _showCallback;


        public void SetUpdateConsentInfo(PluginCallback callback)
        {
            _requestConsentInfoUpdateCallback = callback;
        }
        public void SetLoadForm(PluginCallback callback)
        {
            _loadFormCallback = callback;
        }
        public void SetShowForm(PluginCallback callback)
        {
            _showCallback = callback;
        }

        private void OnConsentInfoUpdateSuccess(string msg)
        {
            _requestConsentInfoUpdateCallback.OnSuccess();
        }

        private void OnConsentInfoUpdateFailure(string msg)
        {
            _requestConsentInfoUpdateCallback.OnError(msg);
        }

        private void OnConsentFormLoadSuccess(string msg)
        {
            _loadFormCallback.OnSuccess();
        }

        private void OnConsentFormLoadFailure(string msg)
        {
            _loadFormCallback.OnError(msg);
        }


        private void OnConsentFormDismissed(string msg)
        {
            _showCallback.OnError(msg);
        }
    }

}
#endif