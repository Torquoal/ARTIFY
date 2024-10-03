using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Source : BaseBlock
{

	[Header("Settings")]
	[SerializeField] public string output;


	public Material blue;
	public TMP_Text outputLabel;


	void Awake()
	{
		SaveSystem.sources.Add(this);
	}

	void OnDestroy()
	{
		Debug.Log(this.title + "deleted");
		SaveSystem.sources.Remove(this);

	}
	// Start is called before the first frame update
	void Start()
    {
		sharedLogic = GameObject.FindWithTag("ScriptHost").GetComponent<SharedLogic>();
		Transform child = transform.Find("TitleCanvas");
		TMP_Text t = child.GetComponent<TMP_Text>();
		t.text =  title;
		
	}

	// Update is called once per frame
	new void Update()
    {
		SetName();	
		titleLabel.text = title;
		outputLabel.text = "prod: " + output;

	}


	public void SetOutput()
    {
		sharedLogic.EditObjectText(gameObject, "output");
	}

	override public void Actuate()
	{
		active = !active;

		var children = this.GetComponentsInChildren<Transform>();
		foreach (var child in children)
		{
			if (child.name == "Shape")
			{
				if (!active)
				{
					child.GetComponent<MeshRenderer>().material = grey;
				} 
				else if (active)
				{
					child.GetComponent<MeshRenderer>().material = blue;
				}
			}
		}
	}
}
