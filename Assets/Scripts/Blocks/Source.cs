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

	[Header("Settings")]
	[SerializeField] public string title;
	[SerializeField] public string output;


	public bool active = true; // is the component currently turned on
	public Material grey;
	public Material blue;
	public SharedLogic sharedLogic;
	public GameObject editMenu;
	public TMP_Text titleLabel;
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
	void Update()
    {
		SetName();	
		titleLabel.text = title;
		outputLabel.text = "prod: " + output;

	}

	void SetName()
    {
		Transform child = transform.Find("TitleCanvas");
		TMP_Text t = child.GetComponent<TMP_Text>();
		title = gameObject.name;
		t.text = title;
	}

	public void SetTitle()
    {
		sharedLogic.EditObjectText(gameObject, "title");
	}

	public void SetOutput()
    {
		sharedLogic.EditObjectText(gameObject, "output");
	}

	[ContextMenu("Actuate")]
	public void Actuate()
	{
		active = !active;
	}

	public void ToggleEditMenu()
    {
        editMenu.SetActive(!editMenu.activeInHierarchy);
    }

}
