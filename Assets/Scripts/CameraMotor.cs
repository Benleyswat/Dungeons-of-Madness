  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    public float boundX = 0.15f;
    public float boundY = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        lookAt = GameObject.Find("Player").transform;
    }

    // This is moving the camera to the right direction if the player moves
    private void LateUpdate()
    {
        
        Vector3 delta = Vector3.zero;

        //Checking the X axis if the player moved left or right
        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX) 
        {
            if (transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else 
            {
                delta.x = deltaX + boundX;
            }
        }
        //Checking the Y axis if the player moved up or down
        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
