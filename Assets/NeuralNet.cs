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

    public double[,] Run(double[,] inputs)
    {
        var between = Multiply(input.weights, inputs);
        var output = Multiply(hidden.weights, between);
        return output;
    }


    public static double[,] Multiply(double[,] A, double[,] B)
    {
        int rA = A.GetLength(0);
        int cA = A.GetLength(1);
        int rB = B.GetLength(0);
        int cB = B.GetLength(1);
        Debug.Assert(cA == rB);

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
    }

}
