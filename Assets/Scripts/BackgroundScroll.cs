using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    public float moveSpeed = -10;
    public Transform player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(0, 0, Time.deltaTime * moveSpeed);

        if (transform.position.z < player.position.z)
        {
            transform.position += new Vector3(0, 0, 100);
        }
    }
}
