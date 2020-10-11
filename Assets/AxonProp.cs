using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxonProp : MonoBehaviour
{
    [SerializeField] Transform canvas;
    [SerializeField] Text tooltip;

    public Material low, high;

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
        var thisLayer = master.neuronProps[layer];
        var nextLayer = master.neuronProps[layer+1];
        var from = thisLayer[fromInd];
        if(toInd >= nextLayer.Length)
            Debug.LogError($"{toInd} is longer than {nextLayer.Length}, at layer x={layer}-{layer+1}.");
        var to = nextLayer[toInd];

        line.SetPosition(0, from.transform.position);
        line.SetPosition(1, to.transform.position);

        if(isInspected) {
            canvas.gameObject.SetActive(true);
            canvas.transform.position = (to.transform.position + from.transform.position) / 2;
            canvas.LookAt(Camera.main.transform.position);
            canvas.Rotate(Vector3.up, 180);
            var w = master.net.layers[layer].weights[fromInd, toInd];
            tooltip.text = ((int)(w * 100) / 100f).ToString();
        }
        else {
            canvas.gameObject.SetActive(false);
        }

        var weight = (float)master.net.layers[layer].weights[fromInd, toInd];
        var rend = GetComponentInChildren<Renderer>();
        rend.material.Lerp(low, high, weight);
        if(isInspected) {
            rend.material.color = Color.Lerp(rend.material.color, Color.green, 0.5f);
        }
    }
}
