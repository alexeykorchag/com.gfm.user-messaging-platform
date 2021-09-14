using UnityEngine;

namespace GFM.UserMessagingPlatform
{
    public class AndroidPluginCallback : AndroidJavaProxy
    {
        private PluginCallback _callback;
       
        public AndroidPluginCallback(PluginCallback callback) : base("com.gfm.UserMessagingPlatform.PluginCallback") {
            _callback = callback;
        }

        public void onSuccess() => _callback.OnSuccess();       
        public void onError(string errorMessage) =>_callback.OnError(errorMessage);
        
    }
}