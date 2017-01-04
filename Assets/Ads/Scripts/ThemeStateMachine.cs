using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeStateMachine : MonoBehaviour {

    VariableManager varMan;
    public GenerateMenuArt gmaScript;
    public GameObject themePanel;

    public Color enabledColor;
    public Color disabledColor;

    public List<Theme> themes = new List<Theme>();
    
	void Awake ()
    {
        //Grab reference to the "VariableManager" script.
        varMan = GameObject.FindGameObjectWithTag("VariableManager").GetComponent<VariableManager>();

        LoadThemes();
    }

    public void SetThemeState(int themeIndex)
    {
        if (themes[themeIndex].state == Theme.ThemeState.Unpurchased)
        {
            //check the avalible coins and process purchase.
            if (themes[themeIndex].coinCost <= varMan.coins)
            {
                //allow the purchase
                varMan.coins -= themes[themeIndex].coinCost;
                //enable this theme
                EnableNewTheme(themeIndex);
            }
            else
            {
                // they do not have enough coins to purchase this item
                Debug.Log("Cannot afford theme purchase.");
            }
        }
        else if(themes[themeIndex].state == Theme.ThemeState.Disabled)
        {
            //enable this theme and disable all others.
            EnableNewTheme(themeIndex);
        }

        varMan.AssignThemeStates(themes[0], themes[1]);
    }

    public void EnableNewTheme(int index_m)
    {
        //set backing to enabled color.
        themePanel.transform.GetChild(index_m).GetComponent<Image>().color = enabledColor;
        //set the button as not interactable.
        themePanel.transform.GetChild(index_m).GetChild(1).GetComponent<Button>().interactable = false;
        //clear the text
        themePanel.transform.GetChild(index_m).GetChild(1).GetChild(0).GetComponent<Text>().text = "";
        //set new state
        themes[index_m].state = Theme.ThemeState.Enabled;

        if (index_m == 0)
        {
            //check the status of the other theme is enabled. If so, disable it.
            if (themes[1].state == Theme.ThemeState.Enabled)
            {
                //set the theme in the variable manager.
                varMan.fesiveSkin = false;
                gmaScript.PlaceTiles(false);
                DisableTheme(1);
            }
        }
        else if (index_m == 1)
        {
            //check the status of the other theme is enabled. If so, disable it.
            if (themes[0].state == Theme.ThemeState.Enabled)
            {
                //set the theme in the variable manager.
                varMan.fesiveSkin = true;
                gmaScript.PlaceTiles(true);
                DisableTheme(0);
            }
        }

        varMan.AssignThemeStates(themes[0], themes[1]);
    }

    public void DisableTheme(int index_m)
    {
        //set backing to disabled color.
        themePanel.transform.GetChild(index_m).GetComponent<Image>().color = disabledColor;
        //Set the state to disabled
        themes[index_m].state = Theme.ThemeState.Disabled;
        //set the button as interactable.
        themePanel.transform.GetChild(index_m).GetChild(1).GetComponent<Button>().interactable = true;
        //populate the text.
        themePanel.transform.GetChild(index_m).GetChild(1).GetChild(0).GetComponent<Text>().text = "Enable";

        varMan.AssignThemeStates(themes[0], themes[1]);
    }

    public void LoadThemes()
    {
        themes[0] = varMan.themes[0];
        themes[1] = varMan.themes[1];

        for (int i = 0; i < 2; i++)
        {
            Debug.Log(i);
            if (themes[i].state == Theme.ThemeState.Enabled)
            {
                //set backing to enabled color.
                themePanel.transform.GetChild(i).GetComponent<Image>().color = enabledColor;
                //set the button as not interactable.
                themePanel.transform.GetChild(i).GetChild(1).GetComponent<Button>().interactable = false;
                //clear the text
                themePanel.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<Text>().text = "";
            }
            else if (themes[i].state == Theme.ThemeState.Disabled)
            {
                //set backing to disabled color.
                themePanel.transform.GetChild(i).GetComponent<Image>().color = disabledColor;
                //set the button as interactable.
                themePanel.transform.GetChild(i).GetChild(1).GetComponent<Button>().interactable = true;
                //populate the text.
                themePanel.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<Text>().text = "Enable";
            }
        }
    }
}
