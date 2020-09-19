using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;


public class weatherScript : MonoBehaviour
{
    public GameObject weatherTextObject;
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

            // split data into array and declare keyowrd and index
            string[] allData = data.Split(',');
            string temp = "\"temp\":";
            int index = 0;
            
            // for each data in array
            foreach (var sentence in allData)
            {
                // if temp is found
                if (sentence.Contains(temp))
                {
                    // find the temp and display it
                    index = sentence.IndexOf(temp);
                    weatherTextObject.GetComponent<TextMeshPro>().text = sentence.Substring(index + 7) + " F";               
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