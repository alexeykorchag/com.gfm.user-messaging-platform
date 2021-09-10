using System;
using UnityEngine;

namespace GFM.UserMessagingPlatform
{
    public class GDPR
    {
        private IGDPRProvider _gdpr;
        private event Action<bool> _callback;

        public GDPR(Action<bool> callback)
        {
            _callback = callback;

            _gdpr = new AndroidGDPRProvider();
        }

        public void SetTagForUnderAgeOfConsent(bool value) => _gdpr.SetTagForUnderAgeOfConsent(value);

        public void AddTestDeviceHashedId(DebugGeography debugGeography, string[] ids) => _gdpr.AddTestDeviceHashedId(debugGeography, ids);

        public void Reset() => _gdpr.Reset();

        public void RequestConsentInfoUpdate()
        {
            var callback = new AndroidPluginCallback();
            callback.Success += OnConsentInfoUpdateSuccess;
            callback.Error += OnConsentInfoUpdateFailure;
            _gdpr.RequestConsentInfoUpdate(callback);
        }

        private void OnConsentInfoUpdateSuccess()
        {
            var isConsentFormAvailable = _gdpr.IsConsentFormAvailable();

            Debug.Log($"GDPRHelper OnConsentInfoUpdateSuccess isConsentFormAvailable: {isConsentFormAvailable}");
            if (isConsentFormAvailable)
            {
                LoadForm();
            }
            else
            {
                var consentStatus = _gdpr.GetConsentStatus();
                Debug.Log($"GDPRHelper OnConsentInfoUpdateSuccess consentStatus: {consentStatus}");
                SetConsent(consentStatus);
            }
        }

        private void OnConsentInfoUpdateFailure(string errorMessage)
        {
            Debug.Log($"GDPRHelper onConsentInfoUpdateFailure errorMessage: {errorMessage}");
            SetConsent(false);
        }

        private void LoadForm()
        {
            var callback = new AndroidPluginCallback();
            callback.Success += OnConsentFormLoadSuccess;
            callback.Error += OnConsentFormLoadFailure;
            _gdpr.LoadForm(callback);
        }

        private void OnConsentFormLoadSuccess()
        {
            var consentStatus = _gdpr.GetConsentStatus();
            Debug.Log($"GDPRHelper OnConsentFormLoadSuccess consentStatus: {consentStatus}");

            if (consentStatus == ConsentStatus.REQUIRED)
            {
                ShowForm();
            }
            else
            {
                SetConsent(consentStatus);
            }
        }

        private void OnConsentFormLoadFailure(string errorMessage)
        {
            Debug.Log($"GDPRHelper OnConsentFormLoadFailure errorMessage: {errorMessage}");
            SetConsent(false);
        }

        private void ShowForm()
        {
            var callback = new AndroidPluginCallback();
            callback.Error += OnConsentFormDismissed;
            _gdpr.Show(callback);
        }

        private void OnConsentFormDismissed(string errorMessage)
        {
            Debug.Log($"GDPRHelper onConsentFormDismissed errorMessage: {errorMessage}");

            LoadForm();
        }

        private void SetConsent(ConsentStatus status)
        {
            var consent = (status == ConsentStatus.OBTAINED) || (status == ConsentStatus.NOT_REQUIRED);
            SetConsent(consent);
        }

        private void SetConsent(bool value)
        {
            _callback?.Invoke(value);
            //_callback = null;
        }

    }
}