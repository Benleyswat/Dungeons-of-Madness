using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;

    protected float xSpeed = 0.75f;
    protected float ySpeed = 0.75f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    protected virtual void UpdateMotor(Vector3 input) 
    {
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        //Swap sprite direction, facing where the player's going
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        //Add pushVector, if there is any
        moveDelta += pushDirection;

        //Reduce pushForce until immunity lasts
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);


        //Below this is why the BoyCollider2D is sometimes buggy (when the collide Box is too far from the Sprite border they collide before 
        //Moving on y axis
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Blocking", "Creatures"));
        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }
        //Moving on x axis
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Blocking", "Creatures"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }


}
