using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingObject : MonoBehaviour {

    public Transform endPoint;
    private Vector3 startPos;
    private Vector3 endPos;
    public float slideSpeed = 1.0f;

	// Use this for initialization
	void Start () {
        startPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        endPos = new Vector3(endPoint.transform.position.x, endPoint.transform.position.y, endPoint.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenDoor()
    {
        StopAllCoroutines();
        StartCoroutine(Open());
    }

    public void CloseDoor()
    {
        StopAllCoroutines();
        StartCoroutine(Close());
    }


    IEnumerator Open()
    {
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * slideSpeed;
            transform.position = Vector3.Lerp(transform.position, endPos, time);
            yield return null;
        }

    }

    IEnumerator Close()
    {
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * slideSpeed;
            transform.position = Vector3.Lerp(transform.position, startPos, time);
            yield return null;
        }

    }
}
