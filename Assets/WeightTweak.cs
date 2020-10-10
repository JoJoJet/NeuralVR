using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(OVRHand))]
public class WeightTweak : MonoBehaviour
{
    [SerializeField]
    Transform fingertip;

    OVRHand handController;

    AxonProp tweaking;

    // Start is called before the first frame update
    void Start()
    {
        handController = GetComponent<OVRHand>();
    }

    // Update is called once per frame
    void Update()
    {
        const float TweakRange = 0.1f;

        var close = from axon in FindObjectsOfType<AxonProp>()
                    let d = ShortDistance(axon.master.neuronProps[axon.layer][axon.fromInd].transform.position,
                                          axon.master.neuronProps[axon.layer+1][axon.toInd].transform.position,
                                          fingertip.position)
                    where d <= TweakRange
                    orderby d ascending
                    select axon;
        close = close.ToList();

        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && close.Any()) {
            tweaking = close.First();
            tweaking.isInspected = true;
        }
        if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && tweaking != null) {
            var vel = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);

            ref double dest = ref tweaking.master.net.GetLayer(tweaking.layer).weights[tweaking.fromInd, tweaking.toInd];
            dest += vel.y * Time.deltaTime;
            if(dest < -1) dest = -1;
            if(dest > 1)  dest =  1;
        }
        if(OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) && tweaking != null) {
            tweaking.isInspected = false;
            tweaking = null;
        }

        Debug.Log(close.Count());
    }


    // calculate shortest dist. from point to line 
    float ShortDistance(Vector3 line_point1, Vector3 line_point2,
                        Vector3 point)
    {
        var AB = line_point2 - line_point1;
        var AC = point - line_point1;

        if(Vector3.Dot(AC, AB) <= 0.0)
            return AC.magnitude;

        var BC = point - line_point2;

        if(Vector3.Dot(BC, AB) >= 0.0)
            return BC.magnitude;

        return Vector3.Cross(AB, AC).magnitude / AB.magnitude;
    }
}
