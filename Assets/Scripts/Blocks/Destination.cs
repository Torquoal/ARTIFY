using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Destination : BaseBlock
{
	[Header("Settings")]
	[SerializeField] public GameObject inputSource;
	[SerializeField] public string input_required;

	public TMP_Text inputLabel;
	public TMP_Text inBlockLabel;

	private LineRenderer lineRenderer;


	void Awake()
	{
		SaveSystem.destinations.Add(this);
	}

	void OnDestroy()
	{
		Debug.Log(this.title + "deleted");
		SaveSystem.destinations.Remove(this);

	}
	// Start is called before the first frame update
	void Start()
	{
		sharedLogic = GameObject.FindWithTag("ScriptHost").GetComponent<SharedLogic>();
		Transform child = transform.Find("TitleCanvas");
		TMP_Text t = child.GetComponent<TMP_Text>();
		t.text =  title;
		// Get the LineRenderer component attached to this GameObject
        lineRenderer = GetComponent<LineRenderer>();
        // Set the number of positions to 2 (start and end points)
        lineRenderer.positionCount = 2;



	}

	// Update is called once per frame
	new void Update()
	{
		SetName();
		correct = sharedLogic.CheckInput(gameObject, inputSource, correct, input_required);
		sharedLogic.ManageColour(gameObject, active, correct, grey, green, red);
		titleLabel.text = title;
		inputLabel.text = "req: " + input_required;
		if(inputSource!=null){
			inBlockLabel.text = "in: " + sharedLogic.GetBlockTitle(inputSource);
			// Update the positions of the LineRenderer to connect the blocks
			lineRenderer.SetPosition(0, transform.position);             // Start at this block's position
			lineRenderer.SetPosition(1, inputSource.transform.position); // End at the InputSource block's position
			
		} else {
			inBlockLabel.text = "in: None";
			// If InputSource is null, disable the line by setting both positions to the same point
			lineRenderer.SetPosition(0, transform.position);
			lineRenderer.SetPosition(1, transform.position);
		}
	}

	public void SetInput()
    {
		sharedLogic.EditObjectText(gameObject, "input");
	}

	public void SetInputBlock()
    {
		sharedLogic.EditObjectText(gameObject, "inblock");
	}

}
