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
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            leaderboard.SetActive(true);
        }
    }
}
