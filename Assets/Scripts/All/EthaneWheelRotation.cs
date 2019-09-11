using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthaneWheelRotation : MonoBehaviour {

    public GameObject CarbonBond;
    public GameObject hoverHydrogen;
    public Transform[] RotatingHydrogens;
    //private Vector3[] RotHydrogensPositions;
    public Transform[] StationaryHydrogens;
    //private Vector3[] StatHydrogensPositions;
    public GameObject StationaryCarbon;
    //private Vector3 StatCarbonPosition;
    public GameObject WheelCarbon;
    private bool isHovering = false;

    private void Awake()
    {
        //StatCarbonPosition = new Vector3(0, 0, 0);
        //StatCarbonPosition = StationaryCarbon.transform.position;

        //RotHydrogensPositions = new Vector3[RotatingHydrogens.Length];
        //StatHydrogensPositions = new Vector3[StationaryHydrogens.Length];

        //for (int i = 0; i < RotatingHydrogens.Length; i++)
        //{
        //    RotHydrogensPositions[i] = RotatingHydrogens[i].transform.position;
        //}
        //for (int i = 0; i < StationaryHydrogens.Length; i++)
        //{
        //    StatHydrogensPositions[i] = StationaryHydrogens[i].transform.position;
        //}

    }
    
    //Use this for initialization

    void Start () {
        //StatCarbonPosition.transform = StationaryCarbon.transform;
        int count = 0;
        foreach (Transform child in StationaryCarbon.transform)
        {
            if(child.tag == "Hydrogen")
            {                
                StationaryHydrogens[count] = child;
                count++;
            }
        }
        Debug.Log("Stationary Child Object Count: " + StationaryHydrogens.Length);

        count = 0;
        foreach (Transform child in WheelCarbon.transform)
        {
            if (child.tag == "Hydrogen")
            {
                RotatingHydrogens[count] =child;
                count++;
            }
        }
        Debug.Log("Rot Child Object Count: " + RotatingHydrogens.Length);
        
    }
	
	// Update is called once per frame
	void Update () {
        if(hoverHydrogen != null)
        {
            isHovering = hoverHydrogen.GetComponent<VRHoverable>().IsHovering;
        }
        if (isHovering)
        {
            StationaryCarbon.transform.SetParent(CarbonBond.transform);
            WheelCarbon.transform.SetParent(CarbonBond.transform);
            CarbonBond.transform.eulerAngles = new Vector3(-90, 180, 180);
            //StationaryCarbon.transform.eulerAngles = new Vector3(StationaryCarbon.trans, 60, 0);
            Debug.Log("Hovering");
            //Vector3 offset = new Vector3(0.5f ,0, 0.5f);
            //StationaryCarbon.transform.position = WheelCarbon.transform.position + offset; // + WheelCarbon.transform.TransformDirection(new Vector3(0, 0.5f, 0));
        }

    }
}
