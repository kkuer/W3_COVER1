using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;

public class Goal : MonoBehaviour
{
    public GameObject winText;

    public GameObject agents;

    public int count;

    public TMP_Text countText;

    public TreasureTrigger1 trigger1;
    public TreasureTrigger2 trigger2;

    // Update is called once per frame
    void Update()
    {
        countText.text = count.ToString() + "/2";
        if (count == 2)
        {
            winText.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "treasure")
        {
            if (other.gameObject.name == "Treasure1")
            {
                foreach (GameObject agent in trigger1.treasure1Connections)
                {
                    agent.transform.SetParent(agents.transform);
                    agent.transform.Find("StateIndicators").Find("Carrying").gameObject.SetActive(false);
                    agent.transform.Find("StateIndicators").Find("Trying").gameObject.SetActive(false);
                    agent.GetComponent<NavMeshAgent>().enabled = true;
                }

                trigger1.treasure1Connections.Clear();
                Destroy(other.gameObject);
                count++;
            }
            else if (other.gameObject.name == "Treasure2")
            {
                foreach (GameObject agent in trigger2.treasure2Connections)
                {
                    agent.transform.SetParent(agents.transform);
                    agent.transform.Find("StateIndicators").Find("Carrying").gameObject.SetActive(false);
                    agent.transform.Find("StateIndicators").Find("Trying").gameObject.SetActive(false);
                    agent.GetComponent<NavMeshAgent>().enabled = true;
                }

                trigger2.treasure2Connections.Clear();
                Destroy(other.gameObject);
                count++;
            }
        }
    }
}
