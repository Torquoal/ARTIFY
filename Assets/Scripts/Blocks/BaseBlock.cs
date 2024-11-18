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

    [Header("Materials")]
    protected static Material staticGrey;
    protected static Material staticGreen;
    protected static Material staticRed;

    // Instance materials
    public Material grey;
    public Material green;
    public Material red;

    [Header("UI Elements")]
    public GameObject editMenu;
    public TMP_Text titleLabel;
    [SerializeField] protected GameObject titlePanel;

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
    public static GameObject containerPrefab;
    public static GameObject pipesPrefab;
    public static GameObject palletPrefab;
    public static GameObject palletJackPrefab;

    // Method to initialize the static shape prefabs and materials
    public static void InitializeShapePrefabs(
        GameObject cubeShape, 
        GameObject sphereShape, 
        GameObject cylinderShape, 
        GameObject tableShape,
        GameObject containerShape,
        GameObject pipesShape,
        GameObject palletShape,
        GameObject palletJackShape,
        Material greyMaterial,
        Material greenMaterial,
        Material redMaterial)
    {
        cubePrefab = cubeShape;
        spherePrefab = sphereShape;
        cylinderPrefab = cylinderShape;
        tablePrefab = tableShape;
        containerPrefab = containerShape;
        pipesPrefab = pipesShape;
        palletPrefab = palletShape;
        palletJackPrefab = palletJackShape;
        staticGrey = greyMaterial;
        staticGreen = greenMaterial;
        staticRed = redMaterial;

        Debug.Log("Shape prefabs and materials initialized in BaseBlock.");

        // Verify assignments worked
        Debug.Log($"Prefabs after init: Cube={cubePrefab != null}, Sphere={spherePrefab != null}, " +
                 $"Cylinder={cylinderPrefab != null}, Table={tablePrefab != null}");
    
        Debug.Log($"Materials after init: Grey={staticGrey != null}, Green={staticGreen != null}, " +
                 $"Red={staticRed != null}");
    }

    protected virtual void Awake()
    {
        Debug.Log($"[{gameObject.name}] BaseBlock Awake - Initializing materials");
        // Initialize instance materials from static references
        grey = staticGrey;
        green = staticGreen;
        red = staticRed;
        
        Debug.Log($"[{gameObject.name}] Materials after init: Grey={grey != null}, Green={green != null}, Red={red != null}");
        Debug.Log($"[{gameObject.name}] Static materials: Grey={staticGrey != null}, Green={staticGreen != null}, Red={staticRed != null}");
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
        Debug.Log($"[{gameObject.name}] Starting Setup");
        
        // Find and assign SharedLogic reference if not already set
        if (sharedLogic == null)
        {
            sharedLogic = FindObjectOfType<SharedLogic>();
            if (sharedLogic == null)
            {
                Debug.LogError("Could not find SharedLogic in scene!");
                return;
            }
        }

        // Initialize materials
        grey = sharedLogic.greyMaterial;
        green = sharedLogic.greenMaterial;
        red = sharedLogic.redMaterial;

        Debug.Log($"[{gameObject.name}] Setup complete - TitlePanel: {titlePanel != null}, Materials: Grey={grey != null}, Green={green != null}, Red={red != null}");
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

    protected void ReplaceShape(GameObject prefab, bool isCylinder = false)
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab is null!");
            return;
        }

        // Store current scale before destroying shape
        Vector3 currentScale = shape != null ? shape.transform.localScale : Vector3.one;
        
        // Reset scale when changing from cylinder to other shapes
        if (shape != null && shape.CompareTag("Cylinder") && !isCylinder)
        {
            currentScale.y *= 2f; // Undo cylinder height adjustment
        }
        
        // Destroy old shape if it exists
        if (shape != null)
        {
            DestroyImmediate(shape);
        }

        // Create new shape
        GameObject newShape = Instantiate(prefab, transform);
        newShape.name = "Shape";
        newShape.transform.localPosition = Vector3.zero;
        
        // Only adjust scale for new cylinders
        if (isCylinder)
        {
            currentScale.y *= 0.5f;
        }
        
        newShape.transform.localScale = currentScale;
        shape = newShape;
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

    [ContextMenu("To Container")]
    public void ToContainer()
    {
        ReplaceShape(containerPrefab);
    }

    [ContextMenu("To Pipes")]
    public void ToPipes()
    {
        ReplaceShape(pipesPrefab);
    }

    [ContextMenu("To Pallet")]
    public void ToPallet()
    {
        ReplaceShape(palletPrefab);
    }

    [ContextMenu("To PalletJack")]
    public void ToPalletJack()
    {
        ReplaceShape(palletJackPrefab);
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

    protected void UpdatePanelColor()
    {
        if (titlePanel == null) return;
        
        var image = titlePanel.GetComponent<Image>();
        if (image == null) return;

        Material targetMaterial;
        if (!active)
            targetMaterial = grey;
        else if (correct)
            targetMaterial = green;
        else
            targetMaterial = red;
        
        image.material = targetMaterial;
    }

}
