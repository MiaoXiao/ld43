using System.Collections;
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
    // Use this for initialization
	void Start () {
        writer = new DataInserter();
        reader = this.GetComponent<DataLoader>();
        writer.SetUrl(writeurl);
        reader.SetUrl(readurl);
        StartCoroutine(DisplayResults());

	}

    void SendPairToServer(Statistic statistic)
    {
        writer.InsertIntoTable(statistic.key, statistic.value);
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
                foreach (Statistic s in d)
                {
                    values.text += s.key + ": " + s.value + "\n";
                }
            }
        }
        
    }


    // Update is called once per frame
    void Update () {
		
	}
}
