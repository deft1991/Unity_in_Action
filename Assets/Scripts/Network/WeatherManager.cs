using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class WeatherManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private NetworkService _networkService;

    /*
     * We can change it only after network request.
     * In other places only for read.
     */
    public float cloudValue { get; private set; }

    public void Startup(NetworkService networkService)
    {
        Debug.Log("Weather manager starting...");

        _networkService = networkService;

        /*
         * Start download content
         */
        StartCoroutine(_networkService.GetWeatherJSON(OnJSONDataLoaded));

        status = ManagerStatus.Initializing;
    }

    /**
     * Callback for _networkService.GetWeatherXML();
     */
    public void OnXMLDataLoaded(string data)
    {
        XmlDocument doc = new XmlDocument();

        /*
         * Create xml document with possibility to search in it
         */
        doc.LoadXml(data);

        XmlNode root = doc.DocumentElement;

        /*
         * Inject only one node
         */
        if (root != null)
        {
            XmlNode node = root.SelectSingleNode("clouds");
            if (node != null)
            {
                if (node.Attributes != null)
                {
                    string value = node.Attributes["value"].Value;

                    /*
                     * Convert to 0...1
                     */
                    cloudValue = Convert.ToInt32(value) / 100f;
                    Debug.Log("cloud value: " + cloudValue);
                }
            }
        }

        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);
        status = ManagerStatus.Started;
    }

    public void OnJSONDataLoaded(string data)
    {
        var jObject = JObject.Parse(data);
        var cloudsToken = jObject["clouds"];
        if (cloudsToken != null)
        {
            cloudValue = (cloudsToken["all"] ?? 0).Value<long>() / 100f;
        }

        Debug.Log("clouds value: " + cloudValue);

        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);
    }
}
