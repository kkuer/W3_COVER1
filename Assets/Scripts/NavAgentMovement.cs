using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NavAgentMovement : MonoBehaviour
{
    public GameObject agents;

    public NavMeshAgent redAgent;
    public NavMeshAgent blueAgent;
    public NavMeshAgent greenAgent;

    public NavMeshAgent treasure1;
    public NavMeshAgent treasure2;

    public NavMeshAgent selectedAgent;

    public GameObject selectionArrowRed;
    public GameObject selectionArrowBlue;
    public GameObject selectionArrowGreen;

    public GameObject selectionArrowTreasure1;
    public GameObject selectionArrowTreasure2;

    public GameObject movementLocation;

    private GameObject newLocation;

    public TreasureTrigger1 treasureTrigger1;
    public TreasureTrigger2 treasureTrigger2;

    void Start()
    {
        selectedAgent = redAgent;
        selectionArrowRed.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            Ray worldRay = Camera.main.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(worldRay, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.tag == "agent")
                {
                    hitInfo.collider.gameObject.GetComponent<NavMeshAgent>().enabled = true;

                    if (hitInfo.collider.gameObject.name == "Red")
                    {
                        if (selectedAgent != treasure1 || selectedAgent != treasure2)
                        {
                            selectedAgent = redAgent;
                            selectionArrowRed.SetActive(true);
                            selectionArrowBlue.SetActive(false);
                            selectionArrowGreen.SetActive(false);

                            if (treasure1)
                            {
                                selectionArrowTreasure1.SetActive(false);
                            }
                            if (treasure2)
                            {
                                selectionArrowTreasure2.SetActive(false);
                            }
                        }
                    }
                    else if (hitInfo.collider.gameObject.name == "Blue")
                    {
                        if (selectedAgent != treasure1 || selectedAgent != treasure2)
                        {
                            selectedAgent = blueAgent;
                            selectionArrowRed.SetActive(false);
                            selectionArrowBlue.SetActive(true);
                            selectionArrowGreen.SetActive(false);

                            if (treasure1)
                            {
                                selectionArrowTreasure1.SetActive(false);
                            }
                            if (treasure2)
                            {
                                selectionArrowTreasure2.SetActive(false);
                            }
                        }
                    }
                    else if (hitInfo.collider.gameObject.name == "Green")
                    {
                        if (selectedAgent != treasure1 || selectedAgent != treasure2)
                        {
                            selectedAgent = greenAgent;
                            selectionArrowRed.SetActive(false);
                            selectionArrowBlue.SetActive(false);
                            selectionArrowGreen.SetActive(true);

                            if (treasure1)
                            {
                                selectionArrowTreasure1.SetActive(false);
                            }
                            if (treasure2)
                            {
                                selectionArrowTreasure2.SetActive(false);
                            }
                        }
                    }
                }
                if (hitInfo.collider.tag == "treasure")
                {
                    if (hitInfo.collider.gameObject.name == "Treasure1")
                    {
                        if (selectedAgent && treasureTrigger1.treasure1Connections.Count != 2)
                        {
                            selectedAgent.SetDestination(treasure1.gameObject.transform.position);
                        }

                        selectedAgent = treasure1;
                        selectionArrowRed.SetActive(false);
                        selectionArrowBlue.SetActive(false);
                        selectionArrowGreen.SetActive(false);

                        if (treasure1)
                        {
                            selectionArrowTreasure1.SetActive(true);
                        }
                        if (treasure2)
                        {
                            selectionArrowTreasure2.SetActive(false);
                        }
                    }
                    else if (hitInfo.collider.gameObject.name == "Treasure2")
                    {
                        if (selectedAgent && treasureTrigger2.treasure2Connections.Count != 2)
                        {
                            selectedAgent.SetDestination(treasure2.gameObject.transform.position);
                        }

                        selectedAgent = treasure2;
                        selectionArrowRed.SetActive(false);
                        selectionArrowBlue.SetActive(false);
                        selectionArrowGreen.SetActive(false);

                        if (treasure1)
                        {
                            selectionArrowTreasure1.SetActive(false);
                        }
                        if (treasure2)
                        {
                            selectionArrowTreasure2.SetActive(true);
                        }
                    }
                }
                else
                {
                    if (selectedAgent)
                    {
                        if (selectedAgent == treasure1)
                        {
                            if (treasureTrigger1.treasure1Connections.Count == 2)
                            {
                                selectedAgent.SetDestination(hitInfo.point);
                                if (newLocation != null)
                                {
                                    Destroy(newLocation.gameObject);
                                }
                                newLocation = null;
                                GameObject newPoint = Instantiate(movementLocation, hitInfo.point + new Vector3(0, 0.15f, 0), Quaternion.identity);
                                newLocation = newPoint;
                            }
                        }
                        else if (selectedAgent == treasure2)
                        {
                            if (treasureTrigger2.treasure2Connections.Count == 2) 
                            {
                                selectedAgent.SetDestination(hitInfo.point);
                                if (newLocation != null)
                                {
                                    Destroy(newLocation.gameObject);
                                }
                                newLocation = null;
                                GameObject newPoint = Instantiate(movementLocation, hitInfo.point + new Vector3(0, 0.15f, 0), Quaternion.identity);
                                newLocation = newPoint;
                            }
                        }
                        else if (selectedAgent != treasure1 || selectedAgent != treasure2)
                        {
                            selectedAgent.SetDestination(hitInfo.point);
                            if (newLocation != null)
                            {
                                Destroy(newLocation.gameObject);
                            }
                            newLocation = null;
                            GameObject newPoint = Instantiate(movementLocation, hitInfo.point + new Vector3(0, 0.15f, 0), Quaternion.identity);
                            newLocation = newPoint;
                        }  
                    }
                }
            }
        }
        if (selectedAgent != null)
        {
            if (!selectedAgent.pathPending)
            {
                if (selectedAgent.enabled == true)
                {
                    if (selectedAgent.remainingDistance <= selectedAgent.stoppingDistance)
                    {
                        if (!selectedAgent.hasPath || selectedAgent.velocity.sqrMagnitude == 0f)
                        {
                            if (newLocation != null)
                            {
                                Destroy(newLocation.gameObject);
                            }
                        }
                    }
                }
                else
                {
                    if (newLocation != null)
                    {
                        Destroy(newLocation.gameObject);
                    }
                    selectedAgent = null;
                }
            }
        }
        else if (selectedAgent == null)
        {
            if (newLocation != null)
            {
                Destroy(newLocation.gameObject);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePosition = Input.mousePosition;
            Ray worldRay = Camera.main.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(worldRay, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.tag == "treasure")
                {
                    if (hitInfo.collider.gameObject.name == "Treasure1")
                    {
                        foreach (GameObject agent in treasureTrigger1.treasure1Connections)
                        {
                            agent.transform.SetParent(agents.transform);
                            agent.transform.Find("StateIndicators").Find("Carrying").gameObject.SetActive(false);
                            agent.transform.Find("StateIndicators").Find("Trying").gameObject.SetActive(false);
                            agent.GetComponent<NavMeshAgent>().enabled = true;
                        }

                        treasureTrigger1.treasure1Connections.Clear();
                    }
                    if (hitInfo.collider.gameObject.name == "Treasure2")
                    {
                        foreach (GameObject agent in treasureTrigger2.treasure2Connections)
                        {
                            agent.transform.SetParent(agents.transform);
                            agent.transform.Find("StateIndicators").Find("Carrying").gameObject.SetActive(false);
                            agent.transform.Find("StateIndicators").Find("Trying").gameObject.SetActive(false);
                            agent.GetComponent<NavMeshAgent>().enabled = true;
                        }

                        treasureTrigger2.treasure2Connections.Clear();
                    }
                }
            }
            selectedAgent = null;

            selectionArrowRed.SetActive(false);
            selectionArrowBlue.SetActive(false);
            selectionArrowGreen.SetActive(false);

            if (treasure1)
            {
                selectionArrowTreasure1.SetActive(false);
            }
            if (treasure2)
            {
                selectionArrowTreasure2.SetActive(false);
            }
        }
    }
}
