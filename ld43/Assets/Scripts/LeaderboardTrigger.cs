using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardTrigger : MonoBehaviour {

    [SerializeField]
    GameObject leaderboard;
	// Use this for initialization
    
	
	// Update is called once per frame
	void Update () {
		
	}
    int eggid;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            eggid = other.GetComponent<Player>().GetCurrentEggId();
            leaderboard.SetActive(true);
            StartCoroutine(Send());
            
        }
        Debug.Log("Trigger");
    }
    IEnumerator Send()
    {
        
        for (int i = 0; i < 8; i++)
        {
            if (eggid != i)
            {
                Statistic pair;
                pair.key = i;
                pair.value = 1;
                leaderboard.GetComponent<Networking>().SendPairToServer(pair.key, pair.value);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
