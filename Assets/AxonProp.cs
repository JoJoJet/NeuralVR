using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxonProp : MonoBehaviour
{
    public int layer;
    public int fromInd, toInd;

    LineRenderer line;
    NetMaster master;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        master = GetComponentInParent<NetMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        var from = master.neuronProps[layer][fromInd];
        var to = master.neuronProps[layer+1][toInd];

        line.SetPosition(0, from.transform.position);
        line.SetPosition(1, to.transform.position);
    }
}
