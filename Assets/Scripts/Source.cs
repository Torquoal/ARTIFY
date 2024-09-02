using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Source : MonoBehaviour
{

    public string title;
	public string output;
	public bool active = true; // is the component currently turned on
	public Material grey;
	public Material blue;
	public SharedLogic sharedLogic;



	// Start is called before the first frame update
	void Start()
    {
		Transform child = transform.Find("TitleCanvas");
		TMP_Text t = child.GetComponent<TMP_Text>();
		t.text = "Sou: "+ title;
	}

	// Update is called once per frame
	void Update()
    {
		SetAttributes();		

	}



	[ContextMenu("SetTitle")]
	void SetAttributes()
    {
		gameObject.name = title;
		// make a UI or something that allows text to be entered and then set to the attributes
		//gameObject.input_required = 
		//gameObject.output = 
	}

	[ContextMenu("GetTitle")]
	String GetTitle()
	{
		return title;
	}

	[ContextMenu("Actuate")]
	void Actuate()
	{
		active = !active;
	}

}
