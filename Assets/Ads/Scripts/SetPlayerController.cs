using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to set the player's animations dependant on the theme that is enabled.
public class SetPlayerController : MonoBehaviour {

    VariableManager varMan;                                         //Reference to the Variable Manger.                                 (set in Awake()      )
    public RuntimeAnimatorController normalController;              //Reference to the normal animation conroller                       (set in the inspector)
    public RuntimeAnimatorController festiveController;             //Reference to the festive animation conroller                      (set in the inspector)

    void Awake ()
    {
        //Grab reference to the "VariableManager" script.
        varMan = GameObject.FindGameObjectWithTag("VariableManager").GetComponent<VariableManager>();

        //Set the animation controller dependant of the theme being used.
        if (varMan.fesiveSkin)
        {
            //Set animation controller to show festive animations.
            this.GetComponent<Animator>().runtimeAnimatorController = festiveController;
        }
        else
            //Set animation controller to show normal animations.
            this.GetComponent<Animator>().runtimeAnimatorController = normalController;

    }
}
