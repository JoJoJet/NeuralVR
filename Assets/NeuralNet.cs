using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNet
{
    public Layer[] layers;

    public NeuralNet(int inputWidth, int hiddenWidth, int hiddenDepth, int outputWidth)
    {
        layers = new Layer[hiddenDepth + 1];
        if(hiddenDepth > 1) {
            layers[0] = new Layer(inputWidth, hiddenWidth);
            for(int dx = 0; dx < hiddenDepth-1; dx++) {
                layers[dx+1] = new Layer(hiddenWidth, hiddenWidth);
            }
            layers[layers.Length-1] = new Layer(hiddenWidth, outputWidth);
        }
        else if(hiddenDepth > 0) {
            layers[0] = new Layer(inputWidth, hiddenWidth);
            layers[1] = new Layer(hiddenWidth, outputWidth);
        }
        else {
            layers[0] = new Layer(inputWidth, outputWidth);
        }
    }

    public double[,] Run(double[,] inputs)
    {
        for(int x = 0; x < layers.Length; x++) {
            inputs = Multiply(inputs, layers[x].weights);
        }
        return inputs;
    }


    public static double[,] Multiply(double[,] A, double[,] B)
    {
        int rA = A.GetLength(0);
        int cA = A.GetLength(1);
        int rB = B.GetLength(0);
        int cB = B.GetLength(1);
        if(cA != rB) {
            Debug.LogError($"Invalid inputs: [{rA}, {cA}][{rB}, {cB}]");
        }

        double[,] z = new double[rA, cB];
        for(int i = 0; i < rA; i++) {
            for(int j = 0; j < cB; j++) {
                double temp = 0;
                for(int k = 0; k < cA; k++) {
                    temp += A[i, k] * B[k, j];
                }
                z[i, j] = temp;
            }
        }
        return z;
    }


}

public struct Layer
{
    public double[,] weights;

    public Layer(int neuronCount, int axonCount)
    {
        weights = new double[neuronCount, axonCount];
        for(int x = 0; x < axonCount; x++) {
            for(int y = 0; y < neuronCount; y++) {
                weights[y, x] = 1;
            }
        }
    }

}
