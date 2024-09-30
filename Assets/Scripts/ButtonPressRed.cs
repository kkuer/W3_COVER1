using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressRed : MonoBehaviour
{
    public GameObject obstacle;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "agent")
        {
            if (other.name == "Red")
            {
                obstacle.SetActive(false);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "agent")
        {
            if (other.name == "Red")
            {
                obstacle.SetActive(true);
            }
        }
    }
}
