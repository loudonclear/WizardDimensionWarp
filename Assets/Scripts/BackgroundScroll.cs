using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    public float offset = 100;
    public float moveSpeed = -10;
    public Transform player;

	void Update () {
        transform.position += new Vector3(0, 0, Time.deltaTime * moveSpeed);

        if (transform.position.z < player.position.z)
        {
            transform.position += new Vector3(0, 0, offset);
        }
    }
}
