using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotationScript : MonoBehaviour {

    public float rate;
    public Transform molecule;
    public Vector3 rotation;
    // Use this for initialization
    void Start()
    {
        rate = (float)(Random.value * 20 + 5); //random value between 5 and 25
        rotation = Random.onUnitSphere;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        molecule.Rotate(rotation * Time.deltaTime * rate);
        molecule.Rotate(Vector3.up * Time.deltaTime * rate / 2);
    }
}
