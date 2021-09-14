#import <Foundation/Foundation.h>
#import "UnityAppController.h"

#include <UserMessagingPlatform/UserMessagingPlatform.h>

static UMPRequestParameters *parameters;
static UMPConsentForm *form;

void Create()
{
    // Create a UMPRequestParameters object.
    parameters = [[UMPRequestParameters alloc] init];
    
    NSLog(@"GDPRHelper Create");
}

void SetTagForUnderAgeOfConsent(BOOL value)
{
    // Set tag for under age of consent. Here NO means users are not under age.
    parameters.tagForUnderAgeOfConsent = value;
}

void SetTestDevices(int geography, const char* id)
{
    UMPDebugSettings *debugSettings = [[UMPDebugSettings alloc] init];
    debugSettings.testDeviceIdentifiers = @[ [NSString stringWithUTF8String:id] ];

    if (geography == 0)
        debugSettings.geography = UMPDebugGeographyDisabled;
    if (geography == 1)
        debugSettings.geography = UMPDebugGeographyEEA;
    if (geography == 2)
        debugSettings.geography = UMPDebugGeographyNotEEA;
    
    parameters.debugSettings = debugSettings;
    
}

void RequestConsentInfoUpdate() {
    
    NSLog(@"GDPRHelper RequestConsentInfoUpdate");
    
    // Request an update to the consent information.
    [UMPConsentInformation.sharedInstance
        requestConsentInfoUpdateWithParameters:parameters
                             completionHandler:^(NSError *_Nullable error) {
                               if (error) {
                                 // Handle the error.
                                  NSLog(@"GDPRHelper RequestConsentInfoUpdate Error");           
                                  UnitySendMessage("IOSGDPRProviderListener", "OnConsentInfoUpdateFailure", "");;
                               } else {
                                 // The consent information state was updated.
                                 // You are now ready to check if a form is
                                 // available.
                                   NSLog(@"GDPRHelper RequestConsentInfoUpdate Success");                                  
                                   UnitySendMessage("IOSGDPRProviderListener", "OnConsentInfoUpdateSuccess", "");;
                               }
                             }];
}

BOOL IsConsentFormAvailabl(){
    UMPFormStatus formStatus =UMPConsentInformation.sharedInstance.formStatus;
    NSLog(@"GDPRHelper GetConsentStatus: %ld", formStatus);
    
    return  (formStatus == UMPFormStatusAvailable);
}

int GetConsentStatus()
{
    UMPConsentStatus consentStatus = UMPConsentInformation.sharedInstance.consentStatus;
    NSLog(@"GDPRHelper GetConsentStatus: %ld", consentStatus);
       
    if (consentStatus == UMPConsentStatusNotRequired)
        return 1;
    if (consentStatus == UMPConsentStatusRequired)
        return 2;
    if (consentStatus == UMPConsentStatusObtained)
        return 3;
    return  0;
}

void LoadForm() {

    NSLog(@"GDPRHelper LoadForm ");
    [UMPConsentForm
      loadWithCompletionHandler:^(UMPConsentForm *loadedForm, NSError *loadError) {
        if (loadError) {
          // Handle the error
            NSLog(@"GDPRHelper LoadForm Error");       
            UnitySendMessage("IOSGDPRProviderListener", "OnConsentFormLoadFailure", "");;
        } else {
          // Present the form          
            form = loadedForm;
            NSLog(@"GDPRHelper LoadForm Success");
            UnitySendMessage("IOSGDPRProviderListener", "OnConsentFormLoadSuccess", "");;
        }
      }];
}

void ShowForm(){
    NSLog(@"GDPRHelper ShowForm ");
    [form
     presentFromViewController: GetAppController().rootViewController
                completionHandler:^(NSError *_Nullable dismissError) {
                    // App can start requesting ads.                  
                    NSLog(@"GDPRHelper ShowForm close");     
                    UnitySendMessage("IOSGDPRProviderListener", "OnConsentFormDismissed", "");;
                }];
}

void Reset(){
    NSLog(@"GDPRHelper Reset");
    [UMPConsentInformation.sharedInstance reset];
}

