using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Goal : MonoBehaviour
{
    public GameObject winText;

    public int count;

    public TMP_Text countText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countText.text = count.ToString() + "/3";
        if (count == 3)
        {
            winText.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "agent")
        {
            count++;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "agent")
        {
            count--;
        }
    }
}
