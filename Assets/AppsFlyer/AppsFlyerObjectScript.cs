using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppsFlyerSDK;
using System;

// This class is intended to be used the the AppsFlyerObject.prefab

public class AppsFlyerObjectScript : MonoBehaviour , IAppsFlyerConversionData
{

    // These fields are set from the editor so do not modify!
    //******************************//
    public string devKey;
    public string appID;
    public string UWPAppID;
    public string macOSAppID;
    public bool isDebug;
    public bool getConversionData;
    //******************************//


    void Start()
    {
        // These fields are set from the editor so do not modify!
        //******************************//
       DontDestroyOnLoad(gameObject);
       

        AppsFlyer.setIsDebug(isDebug);
#if UNITY_WSA_10_0 && !UNITY_EDITOR
        AppsFlyer.initSDK(devKey, UWPAppID, getConversionData ? this : null);
#elif UNITY_STANDALONE_OSX && !UNITY_EDITOR
    AppsFlyer.initSDK(devKey, macOSAppID, getConversionData ? this : null);
#else
        AppsFlyer.initSDK(devKey, appID, getConversionData ? this : null);
#endif
        //******************************/

        AppsFlyer.setConsentData(

           new AppsFlyerConsent(isUserSubjectToGDPR: false,
           hasConsentForAdsPersonalization: true,
           hasConsentForDataUsage: true,
           hasConsentForAdStorage: true)
);



        AppsFlyer.enableTCFDataCollection(true);
        //AppsFlyer.setCustomerUserId("someId");
        AppsFlyer.startSDK();
    }


    void Update()
    {

    }

    // Mark AppsFlyer CallBacks
    public void onConversionDataSuccess(string conversionData)
    {
        AppsFlyer.AFLog("didReceiveConversionData", conversionData);

        Dictionary<string, object> conversionDataDictionary = AppsFlyer.CallbackStringToDictionary(conversionData);


        foreach (KeyValuePair<string, object> entry in conversionDataDictionary)
        {
            //  Debug.Log(entry.Key + " "+ entry.Value.ToString());
        }


        //if (conversionDataDictionary.ContainsKey("install_time"))
        //{

        //    if (conversionDataDictionary["install_time"] != null)
        //        RequestController.InstallTime = conversionDataDictionary["install_time"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("af_status"))
        //{
        //    if (conversionDataDictionary["af_status"] != null)
        //        RequestController.AfStatus = conversionDataDictionary["af_status"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("af_message"))
        //{
        //    if (conversionDataDictionary["af_message"] != null)
        //        RequestController.AfMessage = conversionDataDictionary["af_message"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("adset"))
        //{
        //    if (conversionDataDictionary["adset"] != null)
        //        RequestController.adset = conversionDataDictionary["adset"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("af_adset"))
        //{
        //    if (conversionDataDictionary["af_adset"] != null)
        //        RequestController.af_adset = conversionDataDictionary["af_adset"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("adgroup"))
        //{
        //    if (conversionDataDictionary["adgroup"] != null)
        //        RequestController.adgroup = conversionDataDictionary["adgroup"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("campaign_id"))
        //{
        //    if (conversionDataDictionary["campaign_id"] != null)
        //        RequestController.campaign_id = conversionDataDictionary["campaign_id"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("agency"))
        //{
        //    if (conversionDataDictionary["agency"] != null)
        //        RequestController.agency = conversionDataDictionary["agency"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("af_sub3"))
        //{
        //    if (conversionDataDictionary["af_sub3"] != null)
        //        RequestController.af_sub3 = conversionDataDictionary["af_sub3"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("af_siteid"))
        //{
        //    if (conversionDataDictionary["af_siteid"] != null)
        //        RequestController.af_siteid = conversionDataDictionary["af_siteid"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("adset_id"))
        //{
        //    if (conversionDataDictionary["adset_id"] != null)
        //        RequestController.adset_id = conversionDataDictionary["adset_id"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("is_fb"))
        //{
        //    if (conversionDataDictionary["is_fb"] != null)
        //        RequestController.is_fb = conversionDataDictionary["is_fb"].ToString() == "true";
        //}

        //if (conversionDataDictionary.ContainsKey("is_first_launch"))
        //{
        //    if (conversionDataDictionary["is_first_launch"] != null)
        //        RequestController.is_first_launch = conversionDataDictionary["is_first_launch"].ToString() == "true";
        //}

        //if (conversionDataDictionary.ContainsKey("click_time"))
        //{
        //    if (conversionDataDictionary["click_time"] != null)
        //        RequestController.click_time = conversionDataDictionary["click_time"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("iscache"))
        //{
        //    if (conversionDataDictionary["iscache"] != null)
        //        RequestController.iscache = conversionDataDictionary["iscache"].ToString() == "true";
        //}

        //if (conversionDataDictionary.ContainsKey("ad_id"))
        //{
        //    if (conversionDataDictionary["ad_id"] != null)
        //        RequestController.ad_id = conversionDataDictionary["ad_id"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("af_sub1"))
        //{
        //    if (conversionDataDictionary["af_sub1"] != null)
        //        RequestController.af_sub1 = conversionDataDictionary["af_sub1"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("campaign"))
        //{
        //    if (conversionDataDictionary["campaign"] != null)
        //        RequestController.campaign = conversionDataDictionary["campaign"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("is_paid"))
        //{
        //    if (conversionDataDictionary["is_paid"] != null)
        //        RequestController.is_paid = conversionDataDictionary["is_paid"].ToString() == "true";
        //}

        //if (conversionDataDictionary.ContainsKey("af_sub4"))
        //{
        //    if (conversionDataDictionary["af_sub4"] != null)
        //        RequestController.af_sub4 = conversionDataDictionary["af_sub4"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("adgroup_id"))
        //{
        //    if (conversionDataDictionary["adgroup_id"] != null)
        //        RequestController.adgroup_id = conversionDataDictionary["adgroup_id"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("is_mobile_data_terms_signed"))
        //{
        //    if (conversionDataDictionary["is_mobile_data_terms_signed"] != null)
        //        RequestController.is_mobile_data_terms_signed = conversionDataDictionary["is_mobile_data_terms_signed"].ToString() == "true";
        //}

        //if (conversionDataDictionary.ContainsKey("af_channel"))
        //{
        //    if (conversionDataDictionary["af_channel"] != null)
        //        RequestController.af_channel = conversionDataDictionary["af_channel"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("af_sub5"))
        //{
        //    if (conversionDataDictionary["af_sub5"] != null)
        //        RequestController.af_sub5 = conversionDataDictionary["af_sub5"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("media_source"))
        //{
        //    if (conversionDataDictionary["media_source"] != null)
        //        RequestController.media_source = conversionDataDictionary["media_source"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("af_sub2"))
        //{
        //    if (conversionDataDictionary["af_sub2"] != null)
        //        RequestController.af_sub2 = conversionDataDictionary["af_sub2"].ToString();
        //}


        //////////////


        //if (conversionDataDictionary.ContainsKey("redirect_response_data"))
        //{
        //    if (conversionDataDictionary["redirect_response_data"] != null)
        //        RequestController.redirect_response_data = conversionDataDictionary["redirect_response_data"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("engmnt_source"))
        //{
        //    if (conversionDataDictionary["engmnt_source"] != null)
        //        RequestController.engmnt_source = conversionDataDictionary["engmnt_source"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("is_incentivized"))
        //{
        //    if (conversionDataDictionary["is_incentivized"] != null)
        //        RequestController.is_incentivized = conversionDataDictionary["is_incentivized"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("retargeting_conversion_type"))
        //{
        //    if (conversionDataDictionary["retargeting_conversion_type"] != null)
        //        RequestController.retargeting_conversion_type = conversionDataDictionary["retargeting_conversion_type"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("orig_cost"))
        //{
        //    if (conversionDataDictionary["orig_cost"] != null)
        //        RequestController.orig_cost = conversionDataDictionary["orig_cost"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("af_click_lookback"))
        //{
        //    if (conversionDataDictionary["af_click_lookback"] != null)
        //        RequestController.af_click_lookback = conversionDataDictionary["af_click_lookback"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("CB_preload_equal_priority_enabled"))
        //{
        //    if (conversionDataDictionary["CB_preload_equal_priority_enabled"] != null)
        //        RequestController.CB_preload_equal_priority_enabled = conversionDataDictionary["CB_preload_equal_priority_enabled"].ToString() == "true";
        //}

        //if (conversionDataDictionary.ContainsKey("CB_preload_equal_priority_enabled"))
        //{
        //    if (conversionDataDictionary["af_cpi"] != null)
        //        RequestController.af_cpi = conversionDataDictionary["af_cpi"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("is_branded_link"))
        //{
        //    if (conversionDataDictionary["is_branded_link"] != null)
        //        RequestController.is_branded_link = conversionDataDictionary["is_branded_link"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("match_type"))
        //{
        //    if (conversionDataDictionary["match_type"] != null)
        //        RequestController.match_type = conversionDataDictionary["match_type"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("af_pmod_lookback_window"))
        //{
        //    if (conversionDataDictionary["af_pmod_lookback_window"] != null)
        //        RequestController.af_pmod_lookback_window = conversionDataDictionary["af_pmod_lookback_window"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("advertising_id"))
        //{
        //    if (conversionDataDictionary["advertising_id"] != null)
        //        RequestController.advertising_id = conversionDataDictionary["advertising_id"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("cost_cents_USD"))
        //{
        //    if (conversionDataDictionary["cost_cents_USD"] != null)
        //        RequestController.cost_cents_USD = conversionDataDictionary["cost_cents_USD"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("esp_name"))
        //{
        //    if (conversionDataDictionary["esp_name"] != null)
        //        RequestController.esp_name = conversionDataDictionary["esp_name"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("http_referrer"))
        //{
        //    if (conversionDataDictionary["http_referrer"] != null)
        //        RequestController.http_referrer = conversionDataDictionary["http_referrer"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("is_universal_link"))
        //{
        //    if (conversionDataDictionary["is_universal_link"] != null)
        //        RequestController.is_universal_link = conversionDataDictionary["is_universal_link"].ToString();
        //}

        //if (conversionDataDictionary.ContainsKey("is_retargeting"))
        //{
        //    if (conversionDataDictionary["is_retargeting"] != null)
        //        RequestController.is_retargeting = conversionDataDictionary["is_retargeting"].ToString();
        //}







        //RequestController.AppsFlyerId = AppsFlyer.getAppsFlyerId();


        //RequestController.AppsFlyerDataRecieved = true;
        //RequestController.Check();
    }

    public void onConversionDataFail(string error)
    {
        Debug.Log("didReceiveConversionDataWithError");
        AppsFlyer.AFLog("didReceiveConversionDataWithError", error);
    }

    public void onAppOpenAttribution(string attributionData)
    {
        AppsFlyer.AFLog("onAppOpenAttribution", attributionData);
        Dictionary<string, object> attributionDataDictionary = AppsFlyer.CallbackStringToDictionary(attributionData);
        // add direct deeplink logic here
    }

    public void onAppOpenAttributionFailure(string error)
    {
        AppsFlyer.AFLog("onAppOpenAttributionFailure", error);
    }

}
