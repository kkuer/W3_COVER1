using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TreasureTrigger2 : MonoBehaviour
{
    public Sprite none;
    public Sprite half;
    public Sprite full;

    public Image stateVisuals;

    public GameObject agentsObject;

    public List<GameObject> treasure2Connections;

    void Update()
    {
        if (treasure2Connections.Count == 0)
        {
            stateVisuals.sprite = none;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        NavMeshAgent collisionNav = other.gameObject.GetComponent<NavMeshAgent>();

        if (other.gameObject.tag == "agent" && treasure2Connections.Count != 2 && collisionNav.enabled == true)
        {
            collisionNav.ResetPath();
            treasure2Connections.Add(other.gameObject);
            other.gameObject.transform.SetParent(gameObject.transform);
            other.GetComponent<NavMeshAgent>().enabled = false;

            if (stateVisuals.sprite == none)
            {
                stateVisuals.sprite = half;

                foreach (GameObject agent in treasure2Connections)
                {
                    agent.transform.Find("StateIndicators").Find("Carrying").gameObject.SetActive(false);
                    agent.transform.Find("StateIndicators").Find("Trying").gameObject.SetActive(true);
                }
            }
            else if (stateVisuals.sprite == half)
            {
                stateVisuals.sprite = full;

                foreach (GameObject agent in treasure2Connections)
                {
                    agent.transform.Find("StateIndicators").Find("Carrying").gameObject.SetActive(true);
                    agent.transform.Find("StateIndicators").Find("Trying").gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "agent")
        {
            if (treasure2Connections.Contains(other.gameObject) || treasure2Connections.Count == 0)
            {
                if (stateVisuals.sprite == full)
                {
                    stateVisuals.sprite = half;

                    foreach (GameObject agent in treasure2Connections)
                    {
                        if (agent != other.gameObject)
                        {
                            agent.transform.Find("StateIndicators").Find("Carrying").gameObject.SetActive(false);
                            agent.transform.Find("StateIndicators").Find("Trying").gameObject.SetActive(true);
                        }
                        else
                        {
                            agent.transform.Find("StateIndicators").Find("Carrying").gameObject.SetActive(false);
                            agent.transform.Find("StateIndicators").Find("Trying").gameObject.SetActive(false);
                        }
                    }
                }
                else if (stateVisuals.sprite == half)
                {
                    stateVisuals.sprite = none;

                    foreach (GameObject agent in treasure2Connections)
                    {
                        agent.transform.Find("StateIndicators").Find("Carrying").gameObject.SetActive(false);
                        agent.transform.Find("StateIndicators").Find("Trying").gameObject.SetActive(false);
                    }
                }
            }

            other.gameObject.transform.SetParent(agentsObject.transform);
            other.GetComponent<NavMeshAgent>().enabled = true;
            treasure2Connections.Remove(other.gameObject);
        }
    }
}
