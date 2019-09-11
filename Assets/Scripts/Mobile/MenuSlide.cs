using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSlide : MonoBehaviour {
    public GameObject slideMenuPanel;
    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = slideMenuPanel.GetComponent<Animator>();
        anim.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void slideOut()
    {
        anim.enabled = true;
        anim.Play("MenuSlideOut");
    }

    public void slideIn()
    {
        anim.Play("MenuSlideIn");
    }
}
