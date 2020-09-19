using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
using System.Globalization;

public class mainWeatherScript : MonoBehaviour
{


    public GameObject clearSky;
    public GameObject fewClouds;
    public GameObject scatteredClouds;
    public GameObject brokenClouds;
    public GameObject rainShowers;
    public GameObject rain;
    public GameObject thunderStorm;
    public GameObject snow;
    public GameObject mist;

    public GameObject[] array;
    private int index;
    public string icon;

    //public GameObject humidityTubeObject;
    string url = "http://api.openweathermap.org/data/2.5/weather?lat=41.88&lon=-87.6&APPID=b90956800d9c4784d08790f1953859c7&units=imperial";


    // Start is called before the first frame update
    void Start()
    {

        clearSky.gameObject.SetActive(false);
        fewClouds.gameObject.SetActive(false);
        scatteredClouds.gameObject.SetActive(false);
        brokenClouds.gameObject.SetActive(false);
        rainShowers.gameObject.SetActive(false);
        rain.gameObject.SetActive(false);
        thunderStorm.gameObject.SetActive(false);
        snow.gameObject.SetActive(false);
        mist.gameObject.SetActive(false);

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
            string icon = "\"icon\":";
            int index = 0;

            // for each data in array
            foreach (var sentence in allData)
            {
                // if it contains humidity
                if (sentence.Contains(icon))
                {
                    // make number into a string and display it
                    index = sentence.IndexOf(icon);
                    string temp1 = sentence.Substring(index + 8);
                    icon = temp1.Substring(0, temp1.Length - 3);

                }
            }

            // choose the correct icon and set active
            if (icon == "01d" || icon == "01n")
            {
                array[0].SetActive(true);
                this.index = 0;
            }
            else if (icon == "02d" || icon == "02n")
            {
                array[1].SetActive(true);
                this.index = 1;
            }
            else if (icon == "03d" || icon == "03n")
            {
                array[2].SetActive(true);
                this.index = 2;
            }
            else if (icon == "04d" || icon == "04n")
            {
                array[3].SetActive(true);
                this.index = 3;
            }
            else if (icon == "09d" || icon == "09n")
            {
                array[4].SetActive(true);
                this.index = 4;
            }
            else if (icon == "10d" || icon == "10n")
            {
                array[5].SetActive(true);
                this.index = 5;
            }
            else if (icon == "11d" || icon == "11n")
            {
                array[6].SetActive(true);
                this.index = 6;
            }
            else if (icon == "13d" || icon == "13n")
            {
                array[7].SetActive(true);
                this.index = 7;
            }
            else if (icon == "50d" || icon == "50n")
            {
                array[8].SetActive(true);
                this.index = 8;
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

    void ResetAll()
    {
        clearSky.gameObject.SetActive(false);
        fewClouds.gameObject.SetActive(false);
        scatteredClouds.gameObject.SetActive(false);
        brokenClouds.gameObject.SetActive(false);
        rainShowers.gameObject.SetActive(false);
        rain.gameObject.SetActive(false);
        thunderStorm.gameObject.SetActive(false);
        snow.gameObject.SetActive(false);
        mist.gameObject.SetActive(false);

    }
    
    

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ResetAll();
            if (index == 8)
            {
                index = 0;
                array[index].SetActive(true);
            }
            else
            {
                array[index + 1].SetActive(true);
                index = index + 1;
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ResetAll();
            if (index == 0)
            {
                index = 8;
                array[index].SetActive(true);
            }
            else
            {
                array[index -1].SetActive(true);
                index = index - 1;
            }

        }
    }
}
