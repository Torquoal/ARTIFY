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


    //UI Elements
    public GameObject editMenu;
    public TMP_Text titleLabel;

    // Shared Components
    public SharedLogic sharedLogic;
    public float lineWidth = 0.02f;
    public Material lineMaterial;
    private Vector3 scaleChange = new Vector3(0.01f,0.01f,0.01f);
    private Vector3 rotationChangeLeft = new Vector3(0f, 1f, 0f);
    private Vector3 rotationChangeRight = new Vector3(0f, -1f, 0f);
    private float moveSpeed = 0.02f;
    public Vector3 cameraPos;

    // Shapes
    public GameObject shape;
    public static GameObject cubePrefab;
    public static GameObject spherePrefab;
    public static GameObject cylinderPrefab;
    public static GameObject tablePrefab;

    // Materials
    public static Material red;
    public static Material green;
    public static Material grey;

    // Method to initialize the static shape prefabs and materials
    public static void InitializeShapePrefabs(
        GameObject cubeShape, 
        GameObject sphereShape, 
        GameObject cylinderShape, 
        GameObject tableShape,
        Material greyMaterial,
        Material greenMaterial,
        Material redMaterial)
    {
        cubePrefab = cubeShape;
        spherePrefab = sphereShape;
        cylinderPrefab = cylinderShape;
        tablePrefab = tableShape;
        grey = greyMaterial;
        green = greenMaterial;
        red = redMaterial;

        Debug.Log("Shape prefabs and materials initialized in BaseBlock.");

        // Verify assignments worked
        Debug.Log($"Prefabs after init: Cube={cubePrefab != null}, Sphere={spherePrefab != null}, " +
                 $"Cylinder={cylinderPrefab != null}, Table={tablePrefab != null}");
    
        Debug.Log($"Materials after init: Grey={grey != null}, Green={green != null}, " +
                 $"Red={red != null}");
    }

    protected virtual void Awake()
    {
        

    }

    public virtual void Update()
    {
        
    }



    public virtual void SetName()
    {
        title = gameObject.name;
        Transform child = transform.Find("TitleCanvas");
        TMP_Text t = child.GetComponent<TMP_Text>();
        t.text = title;
    }

    [ContextMenu("Set Title")]
    public virtual void SetTitle()
    {
        sharedLogic.EditObjectText(gameObject, "title");
    }

    public virtual void InitializeLineRenderer(LineRenderer lineRenderer)
    {
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            sharedLogic.SetupLineRenderer(lineRenderer, lineWidth, lineMaterial);
        }
    }

    public virtual void UpdateLabels()
    {
        if (titleLabel != null) titleLabel.text = title;
    }

    public virtual void UpdateLineRenderer(LineRenderer lineRenderer, GameObject source, GameObject target)
    {
        if (lineRenderer == null) return;
        
        Vector3 start = source.transform.position;

        // If target is null, use the source position as the end point
        Vector3 end = target != null ? target.transform.position : start; 
        
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

    [ContextMenu("Setup")]
    public virtual void Setup()
    {
        cameraPos = GameObject.FindWithTag("MainCamera").transform.position;
		sharedLogic = GameObject.FindWithTag("ScriptHost").GetComponent<SharedLogic>();
		Transform child = transform.Find("TitleCanvas");
		TMP_Text t = child.GetComponent<TMP_Text>();
		t.text = title;
        shape = FindChildByName("Shape", transform);
    }
    
    public virtual void Actuate()
    {
        active = !active;
        Debug.Log("1");
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

    public void RotateLeft()
    {
        if (editMenu.activeInHierarchy)
        {
            shape.transform.Rotate(rotationChangeLeft);

        } 
    }

    public void RotateAllLeft()
    {
        if (editMenu.activeInHierarchy)
        {
            transform.Rotate(rotationChangeLeft);

        }
    }

    public void RotateRight()
    {
        if (editMenu.activeInHierarchy)
        {
            shape.transform.Rotate(rotationChangeRight);

        }
        
    }

    public void RotateAllRight()
    {
        if (editMenu.activeInHierarchy)
        {
            transform.Rotate(rotationChangeRight);

        }

    }

    protected void ReplaceShape(GameObject newShapePrefab, bool isCylinder = false)
    {
        if (newShapePrefab == null)
        {
            Debug.LogError($"Shape prefab is missing for {gameObject.name}!");
            return;
        }

        // Store current scale (or use default if no shape exists)
        Vector3 currentScale;
        if (shape != null)
        {
            currentScale = shape.transform.localScale;
            Debug.Log($"[{gameObject.name}] Storing current scale: {currentScale}");
        }
        else
        {
            currentScale = isCylinder ? 
                new Vector3(0.5f, 0.25f, 0.5f) : 
                new Vector3(0.5f, 0.5f, 0.5f);
            Debug.Log($"[{gameObject.name}] Using default scale: {currentScale}");
        }

        // Handle cylinder conversion
        if (shape != null && shape.CompareTag("Cylinder") && !isCylinder)
        {
            // Converting from cylinder to regular shape
            currentScale.y *= 2f;
        }
        else if (shape != null && !shape.CompareTag("Cylinder") && isCylinder)
        {
            // Converting to cylinder
            currentScale.y *= 0.5f;
        }

        // Destroy old shape if it exists
        if (shape != null)
        {
            DestroyImmediate(shape);
        }

        // Create new shape
        GameObject newShape = Instantiate(newShapePrefab, transform.position, transform.rotation);
        newShape.transform.SetParent(transform);
        newShape.name = "Shape";
        newShape.transform.localScale = currentScale;
        shape = newShape;

        // Apply material based on block state and type
        Material materialToUse = grey;
        if (this is Source sourceBlock)
        {
            materialToUse = active ? sourceBlock.blue : grey;
        }

        if (newShape.CompareTag("Table"))
        {
            GameObject table = shape.transform.Find("lod1").gameObject;
            table.GetComponent<MeshRenderer>().material = materialToUse;
        }
        else
        {
            shape.GetComponent<MeshRenderer>().material = materialToUse;
        }
    }

    [ContextMenu("To Sphere")]
    public void ToSphere()
    {
        Debug.Log($"Checking Prefab Presence: Sphere={spherePrefab != null}");
        ReplaceShape(spherePrefab);
    }

    [ContextMenu("To Cube")]
    public void ToCube()
    {
        ReplaceShape(cubePrefab);
    }

    [ContextMenu("To Cylinder")]
    public void ToCylinder()
    {
        Debug.Log($"Cylinder Replace where shape={shape != null}");
        ReplaceShape(cylinderPrefab, true);
    }

    [ContextMenu("To Table")]
    public void ToTable()
    {
        ReplaceShape(tablePrefab);
    }

    private Material GetBlockMaterial()
    {
        Material shapeMaterial = null;
        if ((shape.tag != "Table")){	
	        shapeMaterial = shape.GetComponent<MeshRenderer>().material;
            
	    }
	    else 
        {
			GameObject table = shape.transform.Find("lod1").gameObject;
			shapeMaterial = table.GetComponent<MeshRenderer>().material;
        }
        return shapeMaterial;
	}


    // Recursively find a child GameObject by name
    public GameObject FindChildByName(string name, Transform parent)
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
