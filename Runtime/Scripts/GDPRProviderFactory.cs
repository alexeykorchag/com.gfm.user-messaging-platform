namespace GFM.UserMessagingPlatform
{
    public class GDPRProviderFactory
    {
        public static IGDPRProvider CreateInstance()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            return new AndroidGDPRProvider();
#elif UNITY_IOS
            return new IOSGDPRProvider();
#else
            return new DummyGDPRProvider();
#endif
        }
    }
}