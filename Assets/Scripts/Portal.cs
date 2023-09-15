using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public string sceneName;
    public string sceneDirection;

    private Animator animator;
    private bool portalOpen;

    //TODO Only Collidable if no enemy is left alive in the room


    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //Debug.Log(Time.time + " - " + GameManager.instance.player.getTimeSpawned());
        if (Time.time - GameManager.instance.player.getTimeSpawned() > 5.0f)
            OpenPortal();
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player") 
        {
            //Save the game before going to the next Room
            GameManager.instance.SaveState(null);                           //TODO null -> dir where the player coming from
            //Teleport the player to the target Room and reset some stats before that
            GameManager.instance.player.setTimeSpawned(Time.time);
            if (portalOpen)
            {
                ClosePortal();
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    private void OpenPortal() 
    {
        animator.SetTrigger("Open");
        portalOpen = true;
    }

    private void ClosePortal() 
    {
        animator.SetTrigger("Close");
        portalOpen = false;
    }
}
