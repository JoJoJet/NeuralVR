using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int numberToSpawn;
    public List<GameObject> spawnPool;
    public GameObject quad;
    public int WaitTime = 1;

    private int Picker;

    public void Start()
    {
        StartCoroutine("CoRoute");
    }

    public void spawnObjects()
    {
        Vector3 nothing = new Vector3(0, -5, 0);
        Picker = UnityEngine.Random.Range(0, spawnPool.Count);
        GameObject tree = (GameObject)Instantiate(spawnPool[Picker]);
        tree.transform.position = transform.position + nothing;
    }

    IEnumerator CoRoute()
    {
        for (; ;)
        {
            WaitTime = UnityEngine.Random.Range(0, 9);
            spawnObjects();
            yield return new WaitForSeconds((float) WaitTime);
        }
    }

}
