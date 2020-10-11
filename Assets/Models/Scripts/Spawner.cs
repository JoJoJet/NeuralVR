using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int numberToSpawn;
    public List<GameObject> spawnPool;
    public Transform quad;
    public int WaitTime = 1;
    public int upper = 2;
    public int distanceAbove = -5;
    public int Xoffset = 0;

    private int Picker;

    public void Start()
    {
        StartCoroutine("CoRoute");
    }

    public void spawnObjects()
    {
        Vector3 nothing = new Vector3(Xoffset, distanceAbove, 0);
        Picker = UnityEngine.Random.Range(0, spawnPool.Count);
        GameObject tree = (GameObject)Instantiate(spawnPool[Picker]);
        tree.transform.position = transform.position + nothing;
       // tree.SetParent(quad);
    }

    IEnumerator CoRoute()
    {
        for (; ;)
        {
            WaitTime = UnityEngine.Random.Range(0, upper);
            spawnObjects();
            yield return new WaitForSeconds((float) WaitTime);
        }
    }

}
