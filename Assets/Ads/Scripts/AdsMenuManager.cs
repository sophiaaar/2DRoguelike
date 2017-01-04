using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Advertisements;
using UnityEngine.UI;
using Completed;

public class AdsMenuManager : MonoBehaviour {

    private int coinsReward;         //used to store the amount of coins rewarded.
    [Header("AdsPanel References")]
    public GameObject adsUI;        //The Ads Reward Panel.                                         (set in the inspector)
    public GameObject optionPanel;  //The Ads Options panel.                                        (set in the inspector)
    public Animator anim;           //Animatior for crate opening.                                  (set in the inspector)
    VariableManager varMan;         //The Variable Manager                                          (set in Awake()      )
    Text notification;              //The Text used to display notifications in the ads Panel.      (set in Awake()      )    

    void Awake()
    {
        //Grab reference to the "VariableManager" script.
        varMan = GameObject.FindGameObjectWithTag("VariableManager").GetComponent<VariableManager>();

        //Grab a reference to the notification Text element.
        notification = adsUI.transform.GetChild(1).gameObject.GetComponent<Text>();

        varMan.AddCoins(0);
    }

    //Toggles the ads menu on and off.
	public void ToggleAdsMenu (bool toggle)
    {
        adsUI.SetActive(toggle);
	}
	
    //Generates coins to be rewarded to the player.
	public void RevealReward()
    {
        //Play the crate opening animation.
        anim.SetTrigger("OpenCrate");

        //Generate a reward from 25 to 150 coins.
        coinsReward = Random.Range(25, 150);

        //Set text on panel to show the player's reward.
        notification.text = "You have found " + coinsReward + " gold coins!";

        //Enable the Options Panel.
        adsUI.transform.GetChild(2).gameObject.SetActive(true);
    }

    //Invoked when the player decides to continue the game.
    public void ResumeGame()
    {
        //Add rewarded coins to the player's wallet
        varMan.AddCoins(coinsReward);

        //Set coins to 0
        coinsReward = 0;

        // Disable the 
        adsUI.transform.GetChild(2).gameObject.SetActive(false);
        notification.text = "You found a supply cache!";
        ToggleAdsMenu(false);
    }

    //Invoked when the player successfully watches a rewarded ad.
    public void DoubleCoins()
    {
        //Double the number of the coins previously rewarded.
        coinsReward = coinsReward * 2;
        
        //Display the new amount of coins rewarded.
        notification.text = "You have found " + coinsReward + " gold coins!";

        //Disable the Show Ads button.
        optionPanel.transform.GetChild(0).gameObject.SetActive(false);

        //Disable the "OR" text.
        optionPanel.transform.GetChild(1).gameObject.SetActive(false);
    }

}
