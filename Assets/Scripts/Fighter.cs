using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //Stats
    public int health = 6;
    public int maxHealth = 6;
    public float pushRecoverySpeed = 0.4f;

    protected float immuneTime = 1.0f;
    protected float LastImmune;

    protected Vector3 pushDirection;

    protected virtual void RecieveDamage(Damage dmg) 
    {
        if (Time.time - LastImmune > immuneTime)
        {
            LastImmune = Time.time;
            health -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            //Visuals as being hit
            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 21, Color.red, transform.position, Vector3.up * 30, 1.5f);

            if (health <= 0) 
            {
                health = 0;
                Death();
            }
        }
    }

    protected virtual void Death() 
    {
        Debug.Log("You Died");
    }
}
