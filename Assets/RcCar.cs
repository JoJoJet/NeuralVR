using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RcCar : NeuralInput
{
    public NetMaster neuralNet;
    public Vector3 up;

    [SerializeField]
    Transform[] eyes;

    [SerializeField]
    bool[] state;

    public override double[,] Weights {
        get {
            var z = new double[1, state.Length];
            for(int i = 0; i < state.Length; i++) {
                z[0, i] = state[i] ? 1f : 0f;
            }
            return z;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        state = new bool[eyes.Length];
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < eyes.Length; i++) {
            var ray = new Ray(eyes[i].transform.position, eyes[i].transform.rotation * up);
            state[i] = Physics.SphereCast(ray, 0.1f, 2f);

            if(state[i])
                eyes[i].GetComponentInChildren<Renderer>().material.color = Color.red;
            else
                eyes[i].GetComponentInChildren<Renderer>().material.color = Color.white;
        }
    }
}
