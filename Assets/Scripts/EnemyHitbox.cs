using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    public int damage = 1;
    public float pushForce = 3.0f;

    protected override void OnCollide(Collider2D collider) 
    {
        if (collider.tag == "Fighter" && collider.name == "Player") 
        {
            //Damage to the player
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };

            collider.SendMessage("RecieveDamage", dmg);
        }
    }
     
}
