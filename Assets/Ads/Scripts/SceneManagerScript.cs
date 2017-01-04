using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour {

    //Change to scene (X) where X is the scene's int index.
    public void ChangeLevel(int x)
    {
        SceneManager.LoadScene(x);
    }
}
