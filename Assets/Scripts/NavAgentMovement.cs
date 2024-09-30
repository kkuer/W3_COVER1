using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentMovement : MonoBehaviour
{
    public NavMeshAgent redAgent;
    public NavMeshAgent blueAgent;
    public NavMeshAgent greenAgent;

    public NavMeshAgent selectedAgent;

    public GameObject selectionArrowRed;
    public GameObject selectionArrowBlue;
    public GameObject selectionArrowGreen;

    public GameObject movementLocation;

    private GameObject newLocation;

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
                    if (hitInfo.collider.gameObject.name == "Red")
                    {
                        selectedAgent = redAgent;
                        selectionArrowRed.SetActive(true);
                        selectionArrowBlue.SetActive(false);
                        selectionArrowGreen.SetActive(false);
                    }
                    else if (hitInfo.collider.gameObject.name == "Blue")
                    {
                        selectedAgent = blueAgent;
                        selectionArrowRed.SetActive(false);
                        selectionArrowBlue.SetActive(true);
                        selectionArrowGreen.SetActive(false);
                    }
                    else if (hitInfo.collider.gameObject.name == "Green")
                    {
                        selectedAgent = greenAgent;
                        selectionArrowRed.SetActive(false);
                        selectionArrowBlue.SetActive(false);
                        selectionArrowGreen.SetActive(true);
                    }

                }
                else
                {
                    if (selectedAgent)
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
        if (selectedAgent != null)
        {
            if (!selectedAgent.pathPending)
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
            selectedAgent = null;
            selectionArrowRed.SetActive(false);
            selectionArrowBlue.SetActive(false);
            selectionArrowGreen.SetActive(false);
        }
    }
}
