using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNet
{
    public Layer input, hidden;

    public NeuralNet(int inputCount, int hiddenCount, int outputCount)
    {
        input = new Layer(inputCount, hiddenCount);
        hidden = new Layer(hiddenCount, outputCount);
    }

    public Layer GetLayer(int index)
    {
        if(index == 0) return input;
        if(index == 1) return hidden;
        throw new Exception();
    }

    public double[,] Run(double[,] inputs)
    {
        var between = Multiply(inputs, input.weights);
        var output = Multiply(between, hidden.weights);
        return output;
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
