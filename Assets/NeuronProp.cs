using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeuronProp : MonoBehaviour
{
    public Material off, glow;

    [SerializeField]
    Transform canvas;
    public
    Text label;

    [NonSerialized]
    public int layer, neuron;

    [NonSerialized]
    public NetMaster master;

    // Start is called before the first frame update
    void Start()
    {
        master = GetComponentInParent<NetMaster>();   
    }

    // Update is called once per frame
    void Update()
    {
        canvas.LookAt(Camera.main.transform.position);
        canvas.Rotate(Vector3.up, 180);
    }
}
