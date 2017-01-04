using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GenerateMenuArt : MonoBehaviour {

    public float sizeX;                                             //The (X) size of the grid of tiles created.                        (set in the inspector)
    public float sizeY;                                             //The (Y) size of the grid of tiles created.                        (set in the inspector)
    public GameObject bgParent;
    public GameObject varManObj;
    VariableManager varMan;                                         //Reference to the Variable Manger.                                 (set in Awake()      )
    public List<Sprite> tiles = new List<Sprite>();                 //List of normal sprites for the background.                        (set in the inspector)
    public List<Sprite> festiveTiles = new List<Sprite>();          //List of festive sprites for the background.                       (set in the inspector)
    public GameObject menuTile;                                     //tile prefab, used to apply sprites to and place in the grid.      (set in the inspector)

    void Awake ()
    {
        if (GameObject.FindGameObjectWithTag("VariableManager") == null)
        {
            Instantiate(varManObj);
        }
        //Grab reference to the "VariableManager" script.
        varMan = GameObject.FindGameObjectWithTag("VariableManager").GetComponent<VariableManager>();

        //Generate background tiles based on the Theme being used.
        if (varMan.fesiveSkin)
        {
            PlaceTiles(true);
        }
        else
            PlaceTiles(false);
	}

    //Places the background tiles that appear on the menu.
    public void PlaceTiles (bool festive)
    {
        if(GameObject.FindGameObjectWithTag("BGParent") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("BGParent"));
        }

        GameObject parent = Instantiate(bgParent, this.transform.position, Quaternion.identity);

        Debug.Log("Generated Background (" + festive + ")");
        //Generate festive background tiles.
        if (festive)
        {
            Vector3 pos = this.transform.position;
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    GameObject t = Instantiate(menuTile, pos, Quaternion.identity);
                    t.GetComponent<SpriteRenderer>().sprite = festiveTiles[Random.Range(0, festiveTiles.Count)];
                    pos = new Vector3(pos.x + 1, pos.y, pos.z);
                    t.transform.SetParent(parent.transform);
                }

                pos = new Vector3(this.transform.position.x, pos.y - 1, pos.z);
            }
        }
        // Generate normal background tiles.
        else
        {
            Vector3 pos = this.transform.position;
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    GameObject t = Instantiate(menuTile, pos, Quaternion.identity);
                    t.GetComponent<SpriteRenderer>().sprite = tiles[Random.Range(0, tiles.Count)];
                    pos = new Vector3(pos.x + 1, pos.y, pos.z);
                    t.transform.SetParent(parent.transform);
                }

                pos = new Vector3(this.transform.position.x, pos.y - 1, pos.z);
            }
        }
	}
}
