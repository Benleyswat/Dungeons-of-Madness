using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Rescources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;

    //Game Data
    public int money;
    public int experience;
    
    private void Awake() 
    {
        if (GameManager.instance != null) 
        {
            Destroy(gameObject);
            return; 
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    
    public void ShowText(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(message, fontSize, color, position, motion, duration);
    }

    //Upgrade Weapon
    public bool TryUpgradeWeapon() 
    {
        //is a weapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if (money >= weaponPrices[weapon.weaponLevel])
        {
            money -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    // Experiecing
    public int GetCurrentLevel() 
    {
        int level = 0;
        int add = 0;

        while (experience >= add) 
        {
            add += xpTable[level];
            level++;

            if (level == xpTable.Count) // Max level return
                return level;
        }
        return level;
    }

    public int GetXpToLevel(int level) 
    {
        int var = 0;
        int xp = 0;

        while (var < level) 
        {
            xp += xpTable[var];
            var++;
        }
        return xp;
    }

    public void GrantXp(int xp) 
    {
        int currentLvl = GetCurrentLevel();
        experience += xp;
        if (currentLvl < GetCurrentLevel())
            OnLevelUp();
    }

    public void OnLevelUp()
    {
        player.OnLevelUp();
    }

    //Save state
    public void SaveState(string direction) 
    {
        string save = "";

        save += direction + "|";
        save += money.ToString() + "|";
        save += experience.ToString() + "|";
        save += weapon.weaponLevel.ToString();
        
        PlayerPrefs.SetString("SaveState", save);
    }

    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        if(!PlayerPrefs.HasKey("SaveState")) { return; }

        //"north|30|24|2" to "north","30","24","2"
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        Debug.Log(data[0]);
        //Spawning the player in the right place of a room
        player.transform.position = GameObject.Find("SpawnFromSouth").transform.position;

        //Change player's skin
        money = int.Parse(data[1]);

        //Set xp and Level correctly
        experience = int.Parse(data[2]);
        if(GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());

        //Set player weapon in hand and in the menu
        weapon.SetWeaponLevel(int.Parse(data[3]));
    }

    private void SpawnPlayer(string dir) 
    {
        if (dir == null || dir == "0")
            player.transform.position = GameObject.Find("Spawn").transform.position;
        else if (dir == "North") //Going North == coming from South
            player.transform.position = GameObject.Find("SpawnFromSouth").transform.position;
        else if (dir == "South") //Going South == coming from North
            player.transform.position = GameObject.Find("SpawnFromNorth").transform.position;
        else if (dir == "West") //Going West == coming from East
            player.transform.position = GameObject.Find("SpawnFromEast").transform.position;
        else if (dir == "East") //Going East == coming from West
            player.transform.position = GameObject.Find("SpawnFromWest").transform.position;
        else
            player.transform.position = Vector3.zero;
    }
}
