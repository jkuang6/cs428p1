using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;

public class temperatureScript : MonoBehaviour
{
    public GameObject temperatureTubeObject;
    string url = "http://api.openweathermap.org/data/2.5/weather?lat=41.88&lon=-87.6&APPID=b90956800d9c4784d08790f1953859c7&units=imperial";
    private float numVal = 0;

    private Vector3 scaleChange, positionChange;

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

            // section data into arrays
            string[] allData = data.Split(',');

            // set keyword and index
            string temp = "\"temp\":";
            int index = 0;

            // go through each list
            foreach (var sentence in allData)
            {
                // if it contains the word temp
                if (sentence.Contains(temp))
                {
                    // get the index of the number and put it into a string
                    index = sentence.IndexOf(temp);
                    string temp1 = sentence.Substring(index + 7);

                    // parse the string into a float and reset the decimal to match the unity units
                    numVal = float.Parse(temp1);
                    numVal = numVal * 100f;
                    numVal = numVal * 0.0001f;
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
        // change size based on the temperature
        temperatureTubeObject.transform.localScale = new Vector3(2.0f, numVal, 2.0f);
    }





}