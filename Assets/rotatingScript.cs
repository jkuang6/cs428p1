using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
using System.Globalization;

public class rotatingScript : MonoBehaviour
{
    public GameObject rotatingObject;
    string url = "http://api.openweathermap.org/data/2.5/weather?lat=41.88&lon=-87.6&APPID=b90956800d9c4784d08790f1953859c7&units=imperial";
    private string temp1 = "";
    private float degrees = 0;
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

            //split data into array and set keywords and index
            string[] allData = data.Split(',');
            string degree = "\"deg\":";
            int index = 0;

            // for each data in array
            foreach (var sentence in allData)
            {
                // if it contains deg
                if (sentence.Contains(degree))
                {
                    // find index of deg and put it into a string
                    index = sentence.IndexOf(degree);
                    temp1 = sentence.Substring(index + 6);

                    // check to see if the last thing is a , or }
                    if (temp1[temp1.Length - 1] == '}')
                    {
                        temp1 = temp1.Substring(0, temp1.Length - 1);
                    }
   
                    // put it into a float
                    degrees = float.Parse(temp1);

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
        // roatate object based on the degrees
        rotatingObject.transform.localRotation = Quaternion.Euler(x, y, (-degrees));     
    }
}