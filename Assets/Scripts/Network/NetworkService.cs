using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkService : MonoBehaviour
{
    /*
     * URL for request weather
     * API key - 47f8a39ab2adaaee0af9515f2cc7b351
     */
    private const string xmlApi =
        "https://api.openweathermap.org/data/2.5/weather?q=Los%20Angeles,us&mode=xml&APPID=47f8a39ab2adaaee0af9515f2cc7b351";
    private const string jsonApi =
        "https://api.openweathermap.org/data/2.5/weather?q=Los%20Angeles,us&APPID=47f8a39ab2adaaee0af9515f2cc7b351";

    public IEnumerator GetWeatherXML(Action<string> callback)
    {
        return CallAPI(xmlApi, callback);
    }

    public IEnumerator GetWeatherJSON(Action<string> callback)
    {
        return CallAPI(jsonApi, callback);
    }

    private IEnumerator CallAPI(string url, Action<string> callback)
    {
        /*
         * Call services MUST be coroutine. 
         */
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            /*
             * Pause in downloading
             */
            yield return request.SendWebRequest();

            /*
             * Check errors in response
             */
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("network problem: " + request.error);
            } else if (request.responseCode != (long) System.Net.HttpStatusCode.OK)
            {
                Debug.Log("response error: " + request.responseCode);
            }
            else
            {
                /*
                 * Call delegate
                 */
                callback(request.downloadHandler.text);
            }
        }
    }
}
