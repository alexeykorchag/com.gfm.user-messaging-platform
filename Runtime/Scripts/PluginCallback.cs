using System;

namespace GFM.UserMessagingPlatform
{
    public class PluginCallback
    {
        public event Action Success;
        public event Action<string> Error;

        public void OnSuccess()
        {
            Success?.Invoke();
        }

        public void OnError(string errorMessage)
        {
            Error?.Invoke(errorMessage);
        }
    }
}