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

    public GameObject shape;

    public GameObject cubePrefab;
    public GameObject spherePrefab;
    public GameObject cylinderPrefab;
    public GameObject tablePrefab;

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

        shape = FindChildByName("Shape", transform);
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

    [ContextMenu("ToSphere")]
    public void ToSphere()
    {
        if (shape != null && spherePrefab != null)
        {
            // Store the current position and rotation of the old Shape object
            Vector3 oldPosition = shape.transform.position;
            Quaternion oldRotation = shape.transform.rotation;
            Vector3 shapeScale = shape.transform.localScale;
            Material shapeMaterial = shape.GetComponent<MeshRenderer>().material;

            // Destroy the current Shape object
            Destroy(shape);

            // Instantiate the new Shape prefab at the same position and rotation
            GameObject newShape = Instantiate(spherePrefab, oldPosition, oldRotation);
            newShape.transform.localScale = shapeScale;
            newShape.transform.localScale = new Vector3(shapeScale.x * 0.5f, shapeScale.y * 0.5f, shapeScale.z * 0.5f);

            // Optional: Set the new shape as a child of the current block to maintain hierarchy
            newShape.transform.SetParent(transform);

            // Assign the newShape as the current shapeObject reference
            shape = newShape;

            shape.name = "Shape";
            shape.GetComponent<MeshRenderer>().material = shapeMaterial;

            Debug.Log("Shape object replaced with sphere prefab.");
        }
        else
        {
            Debug.LogError("Shape object or new shape prefab is missing!");
        }
    }

    [ContextMenu("ToCube")]
    public void ToCube()
    {
        if (shape != null && cubePrefab != null)
        {
            // Store the current position and rotation of the old Shape object
            Vector3 oldPosition = shape.transform.position;
            Quaternion oldRotation = shape.transform.rotation;
            Vector3 shapeScale = shape.transform.localScale;
            Material shapeMaterial = shape.GetComponent<MeshRenderer>().material;

            // Destroy the current Shape object
            Destroy(shape);

            // Instantiate the new Shape prefab at the same position and rotation
            GameObject newShape = Instantiate(cubePrefab, oldPosition, oldRotation);
            newShape.transform.localScale = shapeScale;
            newShape.transform.localScale = new Vector3(shapeScale.x * 0.5f, shapeScale.y * 0.5f, shapeScale.z * 0.5f);

            // Optional: Set the new shape as a child of the current block to maintain hierarchy
            newShape.transform.SetParent(transform);

            // Assign the newShape as the current shapeObject reference
            shape = newShape;

            shape.name = "Shape";
            shape.GetComponent<MeshRenderer>().material = shapeMaterial;

            Debug.Log("Shape object replaced with cube prefab.");
        }
        else
        {
            Debug.LogError("Shape object or new shape prefab is missing!");
        }
    }

    [ContextMenu("ToCylinder")]
    public void ToCylinder()
    {
        if (shape != null && cylinderPrefab != null)
        {
            // Store the current position and rotation of the old Shape object
            Vector3 oldPosition = shape.transform.position;
            Quaternion oldRotation = shape.transform.rotation;
            Vector3 shapeScale = shape.transform.localScale;
            Material shapeMaterial = shape.GetComponent<MeshRenderer>().material;

            // Destroy the current Shape object
            Destroy(shape);

            // Instantiate the new Shape prefab at the same position and rotation
            GameObject newShape = Instantiate(cylinderPrefab, oldPosition, oldRotation);
            newShape.transform.localScale = shapeScale;
            newShape.transform.localScale = new Vector3(shapeScale.x*0.5f, shapeScale.y*0.25f, shapeScale.z*0.5f);

            // Optional: Set the new shape as a child of the current block to maintain hierarchy
            newShape.transform.SetParent(transform);

            // Assign the newShape as the current shapeObject reference
            shape = newShape;

            shape.name = "Shape";
            shape.GetComponent<MeshRenderer>().material = shapeMaterial;

            Debug.Log("Shape object replaced with cylinder prefab.");
        }
        else
        {
            Debug.LogError("Shape object or new shape prefab is missing!");
        }
    }

    [ContextMenu("ToTable")]
    public void ToTable()
    {
        if (shape != null && tablePrefab != null)
        {
            // Store the current position and rotation of the old Shape object
            Vector3 oldPosition = shape.transform.position;
            Quaternion oldRotation = shape.transform.rotation;
            Vector3 shapeScale = shape.transform.localScale;
            Material shapeMaterial = shape.GetComponent<MeshRenderer>().material;

            // Destroy the current Shape object
            Destroy(shape);

            // Instantiate the new Shape prefab at the same position and rotation
            Quaternion fixedRotation = oldRotation * Quaternion.Euler(-90, 90, 0);
            GameObject newShape = Instantiate(tablePrefab, oldPosition, fixedRotation);
            newShape.transform.localScale = shapeScale;
            newShape.transform.localScale = new Vector3(shapeScale.x * 10f, shapeScale.y * 10f, shapeScale.z * 10f);

            // Optional: Set the new shape as a child of the current block to maintain hierarchy
            newShape.transform.SetParent(transform);

            // Assign the newShape as the current shapeObject reference
            shape = newShape;

            shape.name = "Shape";
            shape.GetComponent<MeshRenderer>().material = shapeMaterial;

            Debug.Log("Shape object replaced with table prefab.");
        }
        else
        {
            Debug.LogError("Shape object or new shape prefab is missing!");
        }
    }





    // Recursively find a child GameObject by name
    private GameObject FindChildByName(string name, Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child.gameObject;
            }
            GameObject found = FindChildByName(name, child);
            if (found != null)
            {
                return found;
            }
        }
        return null;
    }

}
