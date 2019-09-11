using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: This script is a placeholder. All functionality of this
//			script is in MoleculeCreateInspector.cs.
// ===============================
public class MoleculeCreateScript : MonoBehaviour
{

    public GameObject CreateMolecule(string p_path)
    {
        GameObject parent = gameObject; // molecule.gameObject;

        Material bromine = Resources.Load("Materials/Bromine") as Material;
        Material boron = Resources.Load("Materials/Boron") as Material;
        Material carbon = Resources.Load("Materials/Carbon") as Material;
        Material chlorine = Resources.Load("Materials/Chlorine") as Material;
        Material fluorine = Resources.Load("Materials/Fluorine") as Material;
        Material hydrogen = Resources.Load("Materials/Hydrogen") as Material;
        Material iodine = Resources.Load("Materials/Iodine") as Material;
        Material nitrogen = Resources.Load("Materials/Nitrogen") as Material;
        Material oxygen = Resources.Load("Materials/Oxygen") as Material;
        Material sulfur = Resources.Load("Materials/Sulfur") as Material;
        Material nobleGas = Resources.Load("Materials/NobleGas") as Material;
        Material phosphorus = Resources.Load("Materials/Phosphorus") as Material;
        Material alkaliMetals = Resources.Load("Materials/AlkaliMetals") as Material;
        Material alkalineEarthMetals = Resources.Load("Materials/AlkalineEarthMetals") as Material;
        Material titanium = Resources.Load("Materials/Titanium") as Material;
        Material iron = Resources.Load("Materials/Iron") as Material;
        Material other = Resources.Load("Materials/Other") as Material;

        string[] lines = File.ReadAllLines(p_path);

        //calculate average xyz coords to meanshift the atoms in the molecule as to center at xyz=0,0,0
        float[] averages = new float[3];
        float minY = float.PositiveInfinity;
        for (int i = 1; i < lines.Length; i++)
        { //sum up all the xyz coords into averages arr
            string[] tokens = lines[i].Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < 3; j++)
                averages[j] += float.Parse(tokens[j + 2]);
            if (float.Parse(tokens[3]) < minY)
                minY = float.Parse(tokens[3]);
        }
        for (int i = 0; i < averages.Length; i++)
            averages[i] /= (lines.Length - 1); //div sums in averages by # atoms

        GameObject[] atoms = new GameObject[lines.Length];
        GameObject atom;
        for (int i = 1; i < lines.Length; i++)
        {
            string[] tokens = lines[i].Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries);

            atom = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            atom.transform.position = new Vector3(float.Parse(tokens[2]) - averages[0],  //set position of atom with a meanshift
                                                    float.Parse(tokens[3]) - averages[1], // + Mathf.Abs ( minY ) + 1, //add absolute val of MinY and then some to y so it doesn't go into the ground 
                                                    float.Parse(tokens[4]) - averages[2]);


            MeshRenderer render = atom.GetComponent<MeshRenderer>();
            render.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            render.receiveShadows = false;



           GameObject label = new GameObject();
           label.transform.parent = atom.transform;
           label.transform.localScale = atom.transform.localScale/16;
           label.transform.position = atom.transform.position;
            label.transform.Translate((float)-.2,(float).2,0);
            label.transform.LookAt(Camera.main.transform);




            TextMesh t = label.AddComponent<TextMesh>();
            t.text = tokens[1];
            t.fontSize = 60;
          

            switch (tokens[0])
            {
                case "C":
                    atom.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    render.material = carbon;
                    break;
                case "O":
                    atom.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    render.material = oxygen;
                    break;
                case "N":
                    atom.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    render.material = nitrogen;
                    break;
                case "S":
                    atom.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    render.material = sulfur;
                    break;
                case "Cl":
                    atom.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    render.material = chlorine;
                    break;
                case "F":
                    atom.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    render.material = fluorine;
                    break;
                case "Br":
                    atom.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    render.material = bromine;
                    break;
                case "I":
                    atom.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    render.material = iodine;
                    break;
                case "B":
                    atom.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    render.material = boron;
                    break;
                case "He":
                case "Ne":
                case "Ar":
                case "Xe":
                case "Kr":
                    atom.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    render.material = nobleGas;
                    break;
                case "P":
                    atom.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    render.material = phosphorus;
                    break;
                case "Li":
                case "Na":
                case "K":
                case "Rb":
                case "Cs":
                case "Fr":
                    atom.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    render.material = alkaliMetals;
                    break;
                case "Be":
                case "Mg":
                case "Ca":
                case "Sr":
                case "Ba":
                case "Ra":
                    atom.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    render.material = alkalineEarthMetals;
                    break;
                case "Ti":
                    atom.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    render.material = titanium;
                    break;
                case "Fe":
                    atom.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    render.material = iron;
                    break;
                case "H":
                    atom.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    render.material = hydrogen;
                    break;
                default:
                    atom.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    render.material = other;
                    break;
            }

            atom.transform.SetParent(parent.transform);
            atoms[i] = atom;
        }

        GameObject bond;
        for (int i = 1; i < atoms.Length; i++)
        {
            string[] tokens = lines[i].Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length > 6)
            {
                for (int j = 6; j < tokens.Length; j++)
                {
                    int index = int.Parse(tokens[j]);
                    if (i > index)
                    {
                        continue;
                    }

                    bond = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

                    bond.transform.position = Vector3.Lerp(atoms[i].transform.position,
                                                             atoms[index].transform.position,
                                                             0.5f);
                    bond.transform.localScale = new Vector3(0.1f,
                                                              Vector3.Distance(atoms[i].transform.position,
                                                                                 atoms[index].transform.position) / 2,
                                                              0.1f);
                    bond.transform.up = atoms[index].transform.position - atoms[i].transform.position;

                    MeshRenderer render = bond.GetComponent<MeshRenderer>();
                    render.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    render.receiveShadows = false;

                    bond.transform.SetParent(parent.transform);
                }
            }
        }

        string[] pathTokens = p_path.Split(new string[] { "/", "\\" }, StringSplitOptions.RemoveEmptyEntries);
        /*for ( int i = 0; i < pathTokens.Length; i++ ) {
			Debug.Log ( pathTokens [ i ] );
		}*/

        parent.name = pathTokens[pathTokens.Length - 1].Substring(0,
                                                                       pathTokens[pathTokens.Length - 1].IndexOf(".cc1"));

        DestroyImmediate(parent.GetComponent<MoleculeCreateScript>());
        parent.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        return parent;
    }
    
}
