using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dateScript : MonoBehaviour
{
    public GameObject dateObject;

    // Start is called before the first frame update
    void Start()
    {
        dateObject.GetComponent<TextMeshPro>().text = System.DateTime.Now.ToString("MM/dd/yyyy");
    }

}