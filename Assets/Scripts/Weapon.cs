using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage stats
    public int[] damagePoint = {1, 2, 3, 4};
    public float[] pushForce = {3.0f, 3.3f, 3.6f, 4.2f};

    //Upgrade 
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    //Swing
    private Animator animator;
    private float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start() 
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void Update() 
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (Time.time - lastSwing > cooldown) 
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D collider) 
    {
        if (collider.tag == "Fighter" && collider.name != "Player")
        {
            //Create a Damage class and send it to the one we've hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            collider.SendMessage("RecieveDamage", dmg);
            Debug.Log(collider.name);
        }
    }

    private void Swing() 
    {
        animator.SetTrigger("Swing");
    }

    public void UpgradeWeapon() 
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    public void SetWeaponLevel(int level) 
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
    