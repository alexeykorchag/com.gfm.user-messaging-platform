using System;
using UnityEngine;

namespace GFM.UserMessagingPlatform
{
    public class AndroidPluginCallback : AndroidJavaProxy
    {
        public event Action Success;
        public event Action<string> Error;

        public AndroidPluginCallback() : base("com.gfm.UserMessagingPlatform.PluginCallback") { }

        public void onSuccess()
        {
            Success?.Invoke();
        }

        public void onError(string errorMessage)
        {
            Error?.Invoke(errorMessage);
        }
    }
}