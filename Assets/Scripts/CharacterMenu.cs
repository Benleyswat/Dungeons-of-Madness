 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //Text Fields 
    public Text levelText;
    public Text hpText;
    public Text moneyText;
    public Text upgradeCostText;
    public Text xpText;


    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //CharacterSelection
    public void OnArrowClick(bool right) 
    {
        if (right)
        {
            currentCharacterSelection++;

            //At the end of the list it needs to go to the start
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChanged();
        }
        else 
        {
            currentCharacterSelection--;

            //At the end of the list it needs to go to the start
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            OnSelectionChanged();
        }
    }

    private void OnSelectionChanged() 
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.ChangeSkin(currentCharacterSelection);
    }

    //Weapon Upgrade
    public void OnUpgradeClick() 
    {
        if (GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }

    //Update the character stats
    public void UpdateMenu() 
    {
        //Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count) 
            upgradeCostText.text = "MAX";
        else 
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        //Stats
        hpText.text = GameManager.instance.player.health.ToString();
        moneyText.text = GameManager.instance.money.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();

        //xp
        int currentLevel = GameManager.instance.GetCurrentLevel();
        if (currentLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + "total experience points";
            xpBar.localScale = Vector3.one;
        }
        else 
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currentLevel - 1);
            int currentLevelXp = GameManager.instance.GetXpToLevel(currentLevel);

            int diff = currentLevelXp - prevLevelXp;
            int currentXpGot = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currentXpGot / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currentXpGot.ToString() + " / " + diff;
        }
    }
}
