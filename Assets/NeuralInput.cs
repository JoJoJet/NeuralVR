using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NeuralInput : MonoBehaviour
{
    public abstract double[,] Weights { get; }
}
