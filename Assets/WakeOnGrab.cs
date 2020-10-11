using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WakeOnGrab : MonoBehaviour
{
    public bool isAwake;

    public UnityEvent onWake;

    OVRGrabbable grabbable;

    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponentInChildren<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAwake && grabbable.isGrabbed) {
            isAwake = true;
            onWake.Invoke();
        }
    }
}
