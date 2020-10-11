using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class NetMaster : MonoBehaviour
{

    public NeuronProp neuronPrefab;
    public AxonProp axonPrefab;

    [SerializeField]
    public int inputWidth = 2, hiddenWidth = 1, hiddenDepth = 1, outputWidth = 1;

    public NeuralInput inputs;

    public bool isMutable = true;

    public NeuralNet net;

    public NeuronProp[][] neuronProps;

    public void SetMutable(bool m) => isMutable = m;

    // Start is called before the first frame update
    void Start()
    {
        net = new NeuralNet(inputWidth, hiddenWidth, hiddenDepth, outputWidth);

        Debug.Assert(net.layers.Last().weights.GetLength(1) == outputWidth);

        neuronProps = new NeuronProp[net.layers.Length + 1][];

        float maxHeight = net.layers.Max(l => l.weights.GetLength(0));

        float centerX = (net.layers.Length) / 2;

        for(int x = 0; x < net.layers.Length; x++) {
            var l = net.layers[x];

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

        var lastLayer = net.layers.Last();
        float centerYOut = (float)(lastLayer.weights.GetLength(1) - 1) / 2 - (maxHeight - 1) / 2;
        neuronProps[net.layers.Length] = new NeuronProp[lastLayer.weights.GetLength(1)];
        for(int y = 0; y < lastLayer.weights.GetLength(1); y++) {
            neuronProps[net.layers.Length][y] = Instantiate(neuronPrefab, this.transform)
                .GetComponent<NeuronProp>();
            neuronProps[net.layers.Length][y].transform.localPosition = new Vector2(net.layers.Length - centerX, y - centerYOut);
            neuronProps[net.layers.Length][y].layer = net.layers.Length;
            neuronProps[net.layers.Length][y].neuron = y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var current = inputs.Weights;
        for(int x = 0; x < net.layers.Length; x++) {
            for(int y = 0; y < current.GetLength(1); y++) {
                var thisLayer = neuronProps[x];
                var n = thisLayer[y];
                n.GetComponentInChildren<Renderer>().material.Lerp(n.off, n.glow, (float)current[0, y]);

            }
            if(x < net.layers.Length-1) {
                current = NeuralNet.Multiply(current, net.layers[x].weights);
            }
        }
    }
}
