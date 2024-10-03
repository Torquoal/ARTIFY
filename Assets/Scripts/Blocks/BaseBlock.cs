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

    public virtual void Awake()
    {
        // Any shared Awake logic
        sharedLogic = GameObject.FindWithTag("ScriptHost").GetComponent<SharedLogic>();

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

}
