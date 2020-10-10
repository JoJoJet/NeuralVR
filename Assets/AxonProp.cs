using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxonProp : MonoBehaviour
{
    [SerializeField] Transform canvas;
    [SerializeField] Text tooltip;

    public int layer;
    public int fromInd, toInd;

    LineRenderer line;

    [NonSerialized]
    public NetMaster master;

    [NonSerialized]
    public bool isInspected;

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

        if(isInspected) {
            canvas.gameObject.SetActive(true);
            canvas.transform.position = (to.transform.position + from.transform.position) / 2;
            canvas.LookAt(Camera.main.transform.position);
            canvas.Rotate(Vector3.up, 180);
            var w = master.net.GetLayer(layer).weights[fromInd, toInd];
            tooltip.text = ((int)(w * 100) / 100f).ToString();
        }
        else {
            canvas.gameObject.SetActive(false);
        }

        var weight = (float)master.net.GetLayer(layer).weights[fromInd, toInd];
        if(isInspected) {
            GetComponentInChildren<Renderer>().material.color = Color.Lerp(Color.blue, Color.red, weight);
        }
        else {
            GetComponentInChildren<Renderer>().material.color = Color.Lerp(Color.white, Color.black, weight);
        }
    }
}
