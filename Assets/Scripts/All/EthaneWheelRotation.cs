using System.Collections;

using UnityEngine;

public class EthaneWheelRotation : MonoBehaviour {

    //public SteamVR_TrackedObject TrackedObject;

    public GameObject CarbonBond;
    public GameObject StationaryCarbon;
    public GameObject WheelCarbon;
    public VRHoverable Hoverable;
    
    public bool isRotated = false;
            
    //Use this for initialization
    void Start () {
        
        

    }

    // Update is called once per frame
    void Update () {

        
        if (Hoverable.IsHovering)
        {
            StationaryCarbon.transform.SetParent(CarbonBond.transform);
            WheelCarbon.transform.SetParent(CarbonBond.transform);
            CarbonBond.transform.eulerAngles = new Vector3(-90, 180, 180);       
            if (!isRotated)
            {
                StationaryCarbon.transform.Rotate(0, -90, 0, Space.Self);
                isRotated = true;
            }
            //Debug.Log(controller.GetHairTriggerDown());
            WheelRotationUpdate();



        }

    }

    public void ResetEthane()
    {
        StationaryCarbon.transform.Rotate(0, 90, 0, Space.Self);
    }

    private void WheelRotationUpdate()
    {
        WheelCarbon.transform.Rotate(0, -1, 0, Space.Self);
        
        //Debug.Log(TrackedObject.name + " moving");

    }
}
