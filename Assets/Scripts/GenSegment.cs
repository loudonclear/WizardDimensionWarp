using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenSegment : MonoBehaviour
{
    public GameObject[] pathPrefabs;
    public float spawnDistance = 30;
    private GameObject[] pathSegments;
    private float[] pathLengths;

    private float endLength = -20;
    private int nextPlatform = 1;

    public Transform player;
    private System.Random rnd;

    void Start()
    {
        rnd = new System.Random();

        pathSegments = new GameObject[pathPrefabs.Length];
        pathLengths = new float[pathPrefabs.Length];

        for (int i = 0; i < pathPrefabs.Length; i++)
        {
            Vector3 pos = transform.position + new Vector3(0, 0, endLength);

            GameObject pathSegment = (GameObject)Instantiate(pathPrefabs[i], pos, transform.rotation);
            pathSegment.transform.SetParent(transform);

            float len = CalculateZSize(pathSegment);
            pathLengths[i] = len;
            pathSegments[i] = pathSegment;

            if (i != 0) endLength += len;
        }
    }

    private float CalculateZSize(GameObject obj)
    {
        Bounds bounds = new Bounds(obj.transform.position, Vector3.zero);

        foreach (Renderer renderer in obj.GetComponentsInChildren<Renderer>())
        {
            bounds.Encapsulate(renderer.bounds);
        }

        Vector3 localCenter = bounds.center - this.transform.position;
        bounds.center = localCenter;
        return bounds.size.z;
    }


    void FixedUpdate()
    {
        Vector3 distance = player.position - transform.position;
        double zDist = Vector3.Dot(distance, transform.forward.normalized);

        if (zDist + spawnDistance > endLength)
        {
            placeNextPlatform();
        }
    }

    void placeNextPlatform()
    {
        pathSegments[nextPlatform].transform.position = new Vector3(0, 0, endLength);
        pathSegments[nextPlatform].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 120 * rnd.Next(0, 3)));
        endLength += pathLengths[nextPlatform];
        nextPlatform = (nextPlatform + 1) % pathSegments.Length;
    }
}
