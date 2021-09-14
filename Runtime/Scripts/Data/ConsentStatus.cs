namespace GFM.UserMessagingPlatform
{
    public enum ConsentStatus
    {
        /// <summary>
        /// Unknown consent status.
        /// </summary>
        UNKNOWN = 0,

        /// <summary>
        /// User consent required but not yet obtained.
        /// </summary>
        NOT_REQUIRED = 1,

        /// <summary>
        /// User consent not required. For example, the user is not in the EEA or the UK.
        /// </summary>
        REQUIRED = 2,

        /// <summary>
        /// User consent obtained. Personalization not defined.
        /// </summary>
        OBTAINED = 3,
    }
}