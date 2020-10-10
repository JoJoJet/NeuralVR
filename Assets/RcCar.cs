using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RcCar : NeuralInput
{
    public NetMaster neuralNet;
    public Vector3 up;

    public float turnSpeed = 180, moveSpeed = 20;

    [SerializeField]
    Transform[] eyes;

    [SerializeField]
    bool[] state;


    Rigidbody rb;

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
        rb = GetComponent<Rigidbody>();
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


        var output = neuralNet.net.Run(Weights);
        double max = 0;
        for(int i = 0; i < output.GetLength(1); i++) {
            max += output[0, i];
        }

        var output2 = new double[output.GetLength(1)];
        for(int i = 0; i < output.GetLength(1); i++) {
            output2[i] = output[0, i] / max;
        }

        float turnDir = output2[0] > 0.80 ? -1f
                      : output2[1] > 0.80 ? 1f
                      : 0f;


        float gas = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;
        var rot = rb.transform.rotation.eulerAngles;
        rb.MoveRotation(Quaternion.Euler(rot.x, rot.y + turnDir * turnSpeed * gas * Time.deltaTime, rot.z));
        rb.MovePosition(transform.position + transform.forward * moveSpeed * gas * Time.deltaTime);

    }
}
