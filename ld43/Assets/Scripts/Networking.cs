﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Networking : MonoBehaviour {

    [SerializeField]
    string writeurl;
    [SerializeField]
    string readurl;
    DataInserter writer;
    DataLoader reader;
    List<Statistic> sortedList;
    [SerializeField]
    Text values;

    private void Awake()
    {
        writer = new DataInserter();
        reader = this.GetComponent<DataLoader>();
        writer.SetUrl(writeurl);
        reader.SetUrl(readurl);
        StartCoroutine(DisplayResults());
    }


    // Use this for initialization
    void Start () {

	}
    public void SendPairToServer(int key, int value)
    {
        StartCoroutine(writer.InsertIntoTable(key, value));
        Debug.Log("Write complete");
    }

    IEnumerator GetAllStatisticsFromServerOrdered()
    {
        yield return StartCoroutine(reader.Read());
    }
    List<Statistic> Read()
    {
        return reader.GetSorted();
    }
    IEnumerator DisplayResults()
    {
        WWW www = new WWW("http://google.com");
        yield return www;
        if (www.error != null)
        {
            
            yield break;
        }
        else
        {
            yield return GetAllStatisticsFromServerOrdered();
            List<Statistic> d = Read();
            values.text = "";
            if (d.Count != 0)
            {
                string egg;
                foreach (Statistic s in d)
                {
                    if(s.key >= 0 && s.key <= 2)
                    {
                        egg = "Cracked Egg";
                    }
                    else if(s.key >= 3 && s.key <= 6)
                    {
                        egg = "Purple Egg";
                    }
                    else
                    {
                        egg = "Winged Egg";
                    }
                    values.text += egg + ": " + s.value + "\n";
                }
            }
        }
        
    }
    // Update is called once per frame
    void Update () {
	}
}
