﻿using System;
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
            _gdpr = GDPRProviderFactory.CreateInstance();

            Debug.Log($"GDPRHelper Create: {_gdpr}");
        }

        public void SetTagForUnderAgeOfConsent(bool value)
        {
            Debug.Log($"GDPRHelper SetTagForUnderAgeOfConsent value: {value}");
            _gdpr.SetTagForUnderAge(value);
        }

        public void AddTestDeviceHashedId(DebugGeography debugGeography, string[] ids)
        {
            Debug.Log($"GDPRHelper AddTestDeviceHashedId DebugGeography: {debugGeography} ids: [{string.Join(";", ids)}]");
            _gdpr.SetTestDevices(debugGeography, ids);
        }

        public void Reset()
        {
            Debug.Log($"GDPRHelper ResetConsent");
            _gdpr.ResetConsent();
        }

        public void RequestConsentInfoUpdate()
        {
            Debug.Log($"GDPRHelper UpdateConsentInfo Started...");

            var callback = new PluginCallback();
            callback.Success += OnConsentInfoUpdateSuccess;
            callback.Error += OnConsentInfoUpdateFailure;
            _gdpr.UpdateConsentInfo(callback);
        }

        private void OnConsentInfoUpdateSuccess()
        {
            var isConsentFormAvailable = _gdpr.ConsentIsAvailable();

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
            Debug.Log($"GDPRHelper LoadForm Started...");

            var callback = new PluginCallback();
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
            Debug.Log($"GDPRHelper ShowForm Started...");

            var callback = new PluginCallback();
            callback.Error += OnConsentFormDismissed;
            _gdpr.ShowForm(callback);
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