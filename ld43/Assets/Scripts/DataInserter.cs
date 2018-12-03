using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class DataInserter {
    //url of database;
    string url = "http://192.168.0.14:8080/ld43/write.php";

    public int key;
    public int value;
    // Use this for initialization
    
    //This functions references the url of the database and runs the php file
    //assigning num1 and num2 into the php file's variables num1 and num2
    public IEnumerator InsertIntoTable(int key, int value)
    {
        WWWForm ww = new WWWForm();
        ww.AddField("key", key);
        ww.AddField("value", value);
        WWW db = new WWW(url, ww);
        if(db.error != null)
        {
            Debug.Log("Failed to Send");
        }
        yield return db;
        Debug.Log("Inserted " + key + "," + value);
    }
    public void SetUrl(string ur)
    {
        url = ur;
    }
}
