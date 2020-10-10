using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NetMaster : MonoBehaviour
{
    public NeuronProp neuronPrefab;
    public AxonProp axonPrefab;

    public NeuralInput inputs;

    public NeuralNet net;

    public NeuronProp[][] neuronProps;

    // Start is called before the first frame update
    void Start()
    {
        net = new NeuralNet(3, 2, 2);

        neuronProps = new NeuronProp[3][];

        float maxHeight = Math.Max(net.input.weights.GetLength(1), net.hidden.weights.GetLength(1));

        float centerX = (3-1) / 2;

        var layers = new[] { net.input, net.hidden };
        for(int x = 0; x < layers.Length; x++) {
            var l = layers[x];

            float centerY = (float)(l.weights.GetLength(0) - 1) / 2 - (maxHeight - 1) / 2;

            neuronProps[x] = new NeuronProp[l.weights.GetLength(0)];
            for(int y = 0; y < l.weights.GetLength(0); y++) {
                var n = Instantiate(neuronPrefab, this.transform).GetComponent<NeuronProp>();
                n.transform.localPosition = new Vector2(x - centerX, y - centerY);
                n.layer = x;
                n.neuron = y;
                neuronProps[x][y] = n;
                for(int z = 0; z < l.weights.GetLength(1); z++) {
                    var axon = Instantiate(axonPrefab, this.transform).GetComponent<AxonProp>();
                    axon.layer = x;
                    axon.fromInd = y;
                    axon.toInd = z;
                }
            }
        }

        float centerYOut = (float)(net.hidden.weights.GetLength(1) - 1) / 2 - (maxHeight - 1) / 2;
        neuronProps[2] = new NeuronProp[net.hidden.weights.GetLength(0)];
        for(int y = 0; y < net.hidden.weights.GetLength(0); y++) {
            neuronProps[2][y] = Instantiate(neuronPrefab, this.transform)
                .GetComponent<NeuronProp>();
            neuronProps[2][y].transform.localPosition = new Vector2(2 - centerX, y - centerYOut);
            neuronProps[2][y].layer = 2;
            neuronProps[2][y].neuron = y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var current = inputs.Weights;

        Debug.Log("Inputs:");
        for(int i = 0; i < current.GetLength(1); i++)
            Debug.Log(current[0, i]);

        for(int x = 0; x < 3; x++) {
            for(int y = 0; y < current.GetLength(1); y++) {
                neuronProps[x][y].GetComponentInChildren<Renderer>().material.color
                    = Color.Lerp(Color.gray, Color.blue, (float)current[0, y]);

            }
            if(x < 2) {
                current = NeuralNet.Multiply(current, net.GetLayer(x).weights);
            }
        }
    }
}
