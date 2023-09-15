using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    private float timeSpawned;

    protected override void Start() 
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        DontDestroyOnLoad(gameObject);
        timeSpawned = Time.time;
        xSpeed = 1.0f;
        ySpeed = 1.0f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
    }

    public void ChangeSkin(int skinId) 
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    public void OnLevelUp() 
    {
        maxHealth += 1;
        health = maxHealth;
    }

    public void SetLevel(int level) 
    {
        for (int i = 0; i < level; i++)
            OnLevelUp();
    }

    public float getTimeSpawned() 
    {
        return timeSpawned;
    }
    public void setTimeSpawned(float time) 
    {
        timeSpawned = time;
    }
}
