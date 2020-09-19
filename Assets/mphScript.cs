using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
using System.Globalization;

public class mphScript : MonoBehaviour
{
    public GameObject rotatingObject;
    string url = "http://api.openweathermap.org/data/2.5/weather?lat=41.88&lon=-87.6&APPID=b90956800d9c4784d08790f1953859c7&units=imperial";
    private string temp1 = "";
    private float speed = 0;
    private float x;
    private float y;

    void Start()
    {
        // wait a couple seconds to start and then refresh every 900 seconds

        InvokeRepeating("GetDataFromWeb", 2f, 900f);

        x = 0.0f;
        y = 0.0f;
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

            // split data into array and declare the index and keyword
            string[] allData = data.Split(',');
            string speedText = "\"speed\":";
            int index = 0;

            // for each data in array
            foreach (var sentence in allData)
            {
                // if it has speed
                if (sentence.Contains(speedText))
                {
                    // get the index and put the data into a string
                    index = sentence.IndexOf(speedText);
                    temp1 = sentence.Substring(index + 8);

                    // convert string to float
                    speed = float.Parse(temp1);
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

    void Update()
    {
        // rotate based on the speed
        rotatingObject.transform.localRotation = Quaternion.Euler(x, y, (-speed));
    }
}
