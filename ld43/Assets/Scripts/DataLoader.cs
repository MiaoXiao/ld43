using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public struct Statistic
{
    public int key;
    public int value;
}

public class DataLoader : MonoBehaviour {

    //To use this class you create a DataLoader object instance. Then call the ReadFromDB function.
    //EX
    // DataLoader dl = new DataLoader();
    // dl.ReadFromDB();

    //url format http://ipaddress/ld43/phpfile.php
    string url = "http://192.168.0.14:8080/ld43/read.php"; //location of database and script file to run.
    string[] results;
    List<Statistic> sorted; //sorted list of key value pairs

    //the url will run the script which is a SELECT * From Statistics command
    //data will hold the results of the SELECT command.
    public IEnumerator ReadFromDB () {
        WWW data = new WWW(url);
        //Checks for connection
        if(data.error != null)
        {
            Debug.Log("No internet connection");
            yield break;
        }
        else
        {
            yield return data;
            //print(data.text);
            //Stores the results
            results = data.text.Split(';');
            Array.Resize(ref results, results.Length - 1);

            foreach (string s in results)
            {
                print(s);
            }
            SortDescending();
        }
        
	}
    
    void SortDescending()
    {
        List<Statistic> st = new List<Statistic>();
        Statistic temp = new Statistic();
        foreach (string ss in results)
        {
            string[] tempstring = ss.Split(',');
            temp.key = Int32.Parse(tempstring[0]);
            temp.value = Int32.Parse(tempstring[1]);
            st.Add(temp);
        }
        foreach (Statistic c in st)
        {
            print(c.key + ", " + c.value);
        }
        sorted = st.OrderByDescending(o => o.value).ToList();
        print("\n");
        foreach (Statistic c in sorted)
        {
            print(c.key + ", " + c.value);
        }

    }
    public IEnumerator Read()
    {
        
        yield return StartCoroutine(ReadFromDB());
    }
    public List<Statistic> GetSorted()
    {
        return sorted;
    }
    public void SetUrl(string ur)
    {
        url = ur;
    }
}
