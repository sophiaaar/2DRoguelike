using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{

    public VariableManager varMan;
	static string coinFilePath = ReturnPath();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
		if (CheckForExistingFile () == true)
		{
			//SaveCurrency();
			LoadCurrency ();
		}
    }

	static string ReturnPath()
	{
		string coinFilePath = Path.Combine(Application.persistentDataPath, "coins.xml");
		return coinFilePath;
	}


    bool CheckForExistingFile()
    {
        //string coinFilePath = Path.Combine(Application.persistentDataPath, "coins.xml");
        
		if (!File.Exists (coinFilePath))
		{
			//File.Create (coinFilePath);
			return false;
		}
		else
		{
			return true;
		}

    }

	// Saves the current currency amounts 
	void LoadCurrency ()
    {
        Debug.Log("coins was: " + varMan.coins);

        var serializer = new XmlSerializer(typeof(int));
        //string coinFilePath = Path.Combine(Application.persistentDataPath, "coins.xml");
        var stream = new FileStream(coinFilePath, FileMode.Open);
        int container = (int)serializer.Deserialize(stream);
        stream.Close();

        varMan.coins = container;

        Debug.Log("coins: " + varMan.coins);
    }

    public void SaveCurrency()
    {
		//File.Create (coinFilePath);
        var serializer = new XmlSerializer(typeof(int));
        //string coinFilePath = Path.Combine(Application.persistentDataPath, "coins.xml");
        var stream = new FileStream(coinFilePath, FileMode.Create);
        serializer.Serialize(stream, varMan.coins);
        stream.Close();

		string text = stream.ToString ();
		//File.WriteAllText (coinFilePath, text);

        Debug.Log("present coins: " + varMan.coins);
    }
}