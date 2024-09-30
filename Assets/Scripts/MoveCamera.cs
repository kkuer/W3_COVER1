using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement += new Vector3(-1, 0, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movement += new Vector3(1, 0, -1);
        }
        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            movement += new Vector3(1, 0, 1);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement += new Vector3(-1, 0, -1);
        }

        movement *= speed * Time.deltaTime;

        transform.Translate(movement, Space.World);
    }
}
