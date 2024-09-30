using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressBlue : MonoBehaviour
{
    public GameObject obstacle;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "agent")
        {
            if (other.name == "Blue")
            {
                obstacle.SetActive(false);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "agent")
        {
            if (other.name == "Blue")
            {
                obstacle.SetActive(true);
            }
        }
    }
}
