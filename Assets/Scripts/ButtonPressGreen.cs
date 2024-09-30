using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressGreen : MonoBehaviour
{
    public GameObject obstacle;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "agent")
        {
            if (other.name == "Green")
            {
                obstacle.SetActive(false);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "agent")
        {
            if (other.name == "Green")
            {
                obstacle.SetActive(true);
            }
        }
    }
}
