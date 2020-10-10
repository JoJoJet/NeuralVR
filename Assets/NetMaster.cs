using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetMaster : MonoBehaviour
{
    public NeuronProp neuronPrefab;
    public AxonProp axonPrefab;

    public NeuralNet net;

    public NeuronProp[][] neuronProps;

    // Start is called before the first frame update
    void Start()
    {
        net = new NeuralNet(3, 2, 2);

        neuronProps = new NeuronProp[3][];

        float maxHeight = Math.Max(net.input.weights.GetLength(0), net.hidden.weights.GetLength(0));

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
        
    }
}
