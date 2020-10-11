using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstInputs : NeuralInput
{
    public double[] inputs = new double[1];

    public override double[,] Weights {
        get {
            var z = new double[1, inputs.Length];
            for(int i = 0; i < inputs.Length; i++)
                z[0, i] = inputs[i];
            return z;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
