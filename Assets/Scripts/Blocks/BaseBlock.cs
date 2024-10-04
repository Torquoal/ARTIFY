using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using TMPro;

public abstract class BaseBlock : MonoBehaviour, IBlock
{
    [Header("Base Settings")]
    [SerializeField] public string title;

    public bool active = true;
    public bool correct = false;
    public bool hovered = false;

    public Material red;
    public Material green;
    public Material grey;
    public GameObject editMenu;
    public TMP_Text titleLabel;
    public SharedLogic sharedLogic;
    public float lineWidth = 0.02f;
    public Material lineMaterial;
    private Vector3 scaleChange = new Vector3(0.01f,0.01f,0.01f);
    private float moveSpeed = 0.02f;
    public Vector3 cameraPos;

    public virtual void Awake()
    {
        

    }

    public virtual void Update()
    {
        float stickX = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;
        float stickY = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;

        if (Mathf.Abs(stickY) > 0.5f)
        {
            Upscale();
        }
    }

    public virtual void SetName()
    {
        title = gameObject.name;
        Transform child = transform.Find("TitleCanvas");
        TMP_Text t = child.GetComponent<TMP_Text>();
        t.text = title;
    }

    public virtual void SetTitle()
    {
        sharedLogic.EditObjectText(gameObject, "title");
    }

    public virtual void Setup()
    {
        cameraPos = GameObject.FindWithTag("MainCamera").transform.position;
		sharedLogic = GameObject.FindWithTag("ScriptHost").GetComponent<SharedLogic>();
		Transform child = transform.Find("TitleCanvas");
		TMP_Text t = child.GetComponent<TMP_Text>();
		t.text = title;
    }

    [ContextMenu("Actuate")]
    public virtual void Actuate()
    {
        active = !active;
    }

    [ContextMenu("Toggle Edit Menu")]
    public void ToggleEditMenu()
	{
		editMenu.SetActive(!editMenu.activeInHierarchy);
	}

    [ContextMenu("Upscale")]
    public void Upscale()
    {
        if (editMenu.activeInHierarchy){
            transform.localScale += scaleChange;
            //add upper and lower limits
        }
    }

    [ContextMenu("Downscale")]
    public void Downscale()
    {
        if (editMenu.activeInHierarchy){
            transform.localScale -= scaleChange;
        }
    }

    // Move the block closer to the player
    public void Closer()
    {
        if (editMenu.activeInHierarchy)
        {
            // Calculate direction to the player
            Vector3 directionToPlayer = (cameraPos - transform.position).normalized;

            // Move the block closer to the player
            transform.position += directionToPlayer * moveSpeed;

            Debug.Log("Moving block closer to the player.");
        }
    }

    // Move the block farther away from the player
    public void Farther()
    {
        if (editMenu.activeInHierarchy)
        {
            // Calculate direction to the player
            Vector3 directionToPlayer = (cameraPos - transform.position).normalized;

            // Move the block farther away from the player (opposite direction)
            transform.position -= directionToPlayer * moveSpeed;

            Debug.Log("Moving block farther from the player.");
        }
    }

}
