using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
using System.Globalization;

public class humidityTextScript : MonoBehaviour
{
    public GameObject humidityTextObject;
    string url = "http://api.openweathermap.org/data/2.5/weather?lat=41.88&lon=-87.6&APPID=b90956800d9c4784d08790f1953859c7&units=imperial";

    void Start()
    {

        // wait a couple seconds to start and then refresh every 900 seconds

        InvokeRepeating("GetDataFromWeb", 2f, 900f);

    }

    void GetDataFromWeb()
    {

        StartCoroutine(GetRequest(url));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            string data = webRequest.downloadHandler.text;

            // split data into array and set index and keyword
            string[] allData = data.Split(',');
            string humidity = "\"humidity\":";
            int index = 0;

            // for each data in array
            foreach (var sentence in allData)
            {
                // if it contains humidity
                if (sentence.Contains(humidity))
                {
                    // make number into a string and display it
                    index = sentence.IndexOf(humidity);
                    string temp1 = sentence.Substring(index + 11);
                    temp1 = temp1.Substring(0, temp1.Length - 1);
                    humidityTextObject.GetComponent<TextMeshPro>().text = temp1 + " %";
                }
            }

            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                // print out the weather data to make sure it makes sense
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }

  
}
