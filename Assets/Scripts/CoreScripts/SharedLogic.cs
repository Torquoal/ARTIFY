using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Handles shared functionality between different types of blocks in the system

public class SharedLogic : MonoBehaviour
{
	// UI and camera references
    private GameObject camera;
    [SerializeField] private GameObject editCanvas;
    private TMP_InputField editField;
    private Button enterButton;

	[Header("Shape Prefabs")]
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private GameObject spherePrefab;
    [SerializeField] private GameObject cylinderPrefab;
    [SerializeField] private GameObject tablePrefab;

    [Header("Materials")]
    [SerializeField] public Material greyMaterial;
    [SerializeField] public Material greenMaterial;
    [SerializeField] public Material redMaterial;

    private const float CanvasDistance = 1.5f; // Closer than block spawn distance

	private void Awake()
    {
        Debug.Log($"SharedLogic Awake - Materials before init: Grey={greyMaterial != null}, Green={greenMaterial != null}, Red={redMaterial != null}");
        
        BaseBlock.InitializeShapePrefabs(
            cubePrefab, 
            spherePrefab,
            cylinderPrefab,
            tablePrefab,
            greyMaterial,
            greenMaterial,
            redMaterial
        );
        
        Debug.Log("SharedLogic Awake - Materials initialized");
    }
    void Start()
    {
    
		camera = GameObject.FindWithTag("MainCamera");
        //editCanvas = GameObject.Find("TextEntryCanvas");
        if (editCanvas != null){
			editField = editCanvas.transform.Find("EditField").GetComponent<TMP_InputField>();
			enterButton = editCanvas.transform.Find("EnterButton").GetComponent<Button>();
		} else {
			Debug.LogError("Edit Canvas not found");
		}
    }

	

	// Configures a LineRenderer component with standard settings
    public void SetupLineRenderer(LineRenderer lr, float lineWidth, Material lineMaterial)
    {
        if (lineMaterial != null)
        {
            lr.material = lineMaterial;
        }
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.positionCount = 2;
    }

	// Retrieves the title of a block
    public string GetBlockTitle(GameObject block)
    {
        var baseBlock = block.GetComponent<BaseBlock>();
        return baseBlock != null ? baseBlock.title : "Error";
    }

	// Main entry point for editing block properties through the UI
    public void EditObjectText(GameObject editedObject, string aspect)
    {
        Debug.Log($"Edit Object: {editedObject} Aspect: {aspect}");
        ShowEditCanvas();
        enterButton.onClick.RemoveAllListeners();
        enterButton.onClick.AddListener(() => UpdateBlockField(editedObject, aspect));
    }

	// Routes the edit operation to the appropriate update method based on the aspect being edited
    private void UpdateBlockField(GameObject editedObject, string aspect)
    {
        string fieldValue = editField.text;
        bool success = true;

        switch (aspect)
        {
            case "title":
                editedObject.name = fieldValue.ToLower();
                break;

            case "input":
                success = UpdateInputField(editedObject, fieldValue);
                break;

            case "input1":
                success = UpdateInput1Field(editedObject, fieldValue);
                break;

            case "input2":
                success = UpdateInput2Field(editedObject, fieldValue);
                break;

            case "output":
                success = UpdateOutputField(editedObject, fieldValue);
                break;

            case "output1":
                success = UpdateOutput1Field(editedObject, fieldValue);
                break;

            case "output2":
                success = UpdateOutput2Field(editedObject, fieldValue);
                break;

            case "inblock":
                success = UpdateInBlockField(editedObject, fieldValue);
                break;

            case "inblock1":
                success = UpdateInBlock1Field(editedObject, fieldValue);
                break;

            case "inblock2":
                success = UpdateInBlock2Field(editedObject, fieldValue);
                break;

            case "outblock1":
                success = UpdateOutBlock1Field(editedObject, fieldValue);
                break;

            case "outblock2":
                success = UpdateOutBlock2Field(editedObject, fieldValue);
                break;

            default:
                Debug.Log("Unknown edit operation");
                success = false;
                break;
        }

        if (!success)
        {
            Debug.Log($"Failed to update {aspect} for {editedObject.name}");
        }

        editCanvas.SetActive(false);
        enterButton.onClick.RemoveAllListeners();
    }

	// Updates the input field for blocks that accept a single input
    private bool UpdateInputField(GameObject obj, string value)
    {
        switch (obj.tag)
        {
            case "Processor":
                obj.GetComponent<Processor>().input_required = value;
                return true;
            case "Disassembler":
                obj.GetComponent<Disassembler>().input_required = value;
                return true;
            case "Destination":
                obj.GetComponent<Destination>().input_required = value;
                return true;
            default:
                return false;
        }
    }
	
	// Updates the first input field for blocks that accept dual inputs
    private bool UpdateInput1Field(GameObject obj, string value)
    {
        switch (obj.tag)
        {
            case "Assembler":
                obj.GetComponent<Assembler>().input_required1 = value;
                return true;
            case "Multiprocessor":
                obj.GetComponent<Multiprocessor>().input_required1 = value;
                return true;
            default:
                return false;
        }
    }

	// Updates the second input field for blocks that accept dual inputs
    private bool UpdateInput2Field(GameObject obj, string value)
    {
        switch (obj.tag)
        {
            case "Assembler":
                obj.GetComponent<Assembler>().input_required2 = value;
                return true;
            case "Multiprocessor":
                obj.GetComponent<Multiprocessor>().input_required2 = value;
                return true;
            default:
                return false;
        }
    }

	// Updates the output field for blocks with single output
    private bool UpdateOutputField(GameObject obj, string value)
    {
        switch (obj.tag)
        {
            case "Processor":
                obj.GetComponent<Processor>().output = value;
                return true;
            case "Assembler":
                obj.GetComponent<Assembler>().output = value;
                return true;
            case "Source":
                obj.GetComponent<Source>().output = value;
                return true;
            default:
                return false;
        }
    }

	// Updates the first output field for blocks with dual outputs
    private bool UpdateOutput1Field(GameObject obj, string value)
    {
        switch (obj.tag)
        {
            case "Disassembler":
                obj.GetComponent<Disassembler>().output1 = value;
                return true;
            case "Multiprocessor":
                obj.GetComponent<Multiprocessor>().output1 = value;
                return true;
            default:
                return false;
        }
    }

	// Updates the second output field for blocks with dual outputs
	private bool UpdateOutput2Field(GameObject obj, string value)
    {
        switch (obj.tag)
        {
            case "Disassembler":
                obj.GetComponent<Disassembler>().output2 = value;
                return true;
            case "Multiprocessor":
                obj.GetComponent<Multiprocessor>().output2 = value;
                return true;
            default:
                return false;
        }
    }

	// Updates the input block reference for blocks with single input
    private bool UpdateInBlockField(GameObject obj, string value)
    {
        GameObject inBlock = GameObject.Find(value.ToLower());
        if (inBlock == null)
        {
            Debug.Log("Invalid block entered, aborting assignment");
            return false;
        }

        switch (obj.tag)
        {
            case "Processor":
                obj.GetComponent<Processor>().inputSource = inBlock;
                return true;
            case "Destination":
                obj.GetComponent<Destination>().inputSource = inBlock;
                return true;
            case "Disassembler":
                obj.GetComponent<Disassembler>().inputSource = inBlock;
                return true;
            default:
                return false;
        }
    }

	// Updates the first input block reference for blocks with dual inputs
    private bool UpdateInBlock1Field(GameObject obj, string value)
    {
        GameObject inBlock = GameObject.Find(value.ToLower());
        if (inBlock == null)
        {
            Debug.Log("Invalid block entered, aborting assignment");
            return false;
        }

        switch (obj.tag)
        {
            case "Assembler":
                obj.GetComponent<Assembler>().inputSource1 = inBlock;
                return true;
            case "Multiprocessor":
                obj.GetComponent<Multiprocessor>().inputSource1 = inBlock;
                return true;
            default:
                return false;
        }
    }

    private bool UpdateInBlock2Field(GameObject obj, string value)
    {
        GameObject inBlock = GameObject.Find(value.ToLower());
        if (inBlock == null)
        {
            Debug.Log("Invalid block entered, aborting assignment");
            return false;
        }

        switch (obj.tag)
        {
            case "Assembler":
                obj.GetComponent<Assembler>().inputSource2 = inBlock;
                return true;
            case "Multiprocessor":
                obj.GetComponent<Multiprocessor>().inputSource2 = inBlock;
                return true;
            default:
                return false;
        }
    }

    private bool UpdateOutBlock1Field(GameObject obj, string value)
    {
        GameObject outBlock = GameObject.Find(value.ToLower());
        if (outBlock == null)
        {
            Debug.Log("Invalid block entered, aborting assignment");
            return false;
        }

        switch (obj.tag)
        {
            case "Disassembler":
                obj.GetComponent<Disassembler>().outputBlock1 = outBlock;
                return true;
            case "Multiprocessor":
                obj.GetComponent<Multiprocessor>().outputBlock1 = outBlock;
                return true;
            default:
                return false;
        }
    }

    private bool UpdateOutBlock2Field(GameObject obj, string value)
    {
        GameObject outBlock = GameObject.Find(value.ToLower());
        if (outBlock == null)
        {
            Debug.Log("Invalid block entered, aborting assignment");
            return false;
        }

        switch (obj.tag)
        {
            case "Disassembler":
                obj.GetComponent<Disassembler>().outputBlock2 = outBlock;
                return true;
            case "Multiprocessor":
                obj.GetComponent<Multiprocessor>().outputBlock2 = outBlock;
                return true;
            default:
                return false;
        }
    }

	// Validates input connections between blocks
	// Returns true if the input matches requirements and the source block is active/correct

    public bool CheckInput(GameObject thisObject, GameObject inputSource, bool correct, string input_required)
    {
        if (inputSource == null) return false;

        switch (inputSource.tag)
        {
            case "Source":
                var source = inputSource.GetComponent<Source>();
                return source.active && 
                       string.Equals(source.output, input_required, StringComparison.OrdinalIgnoreCase);

            case "Processor":
            case "Assembler":
                var block = inputSource.GetComponent<BaseBlock>();
                return block.active && block.correct && 
                       string.Equals(GetBlockOutput(inputSource), input_required, StringComparison.OrdinalIgnoreCase);

            case "Disassembler":
            case "Multiprocessor":
                return CheckDualOutputBlock(thisObject, inputSource, input_required);

            default:
                Debug.Log("No Valid Tag Found");
                return false;
        }
    }

	// Helper method to get the output value from a block
    private string GetBlockOutput(GameObject block)
    {
        switch (block.tag)
        {
            case "Processor":
                return block.GetComponent<Processor>().output;
            case "Assembler":
                return block.GetComponent<Assembler>().output;
            case "Source":
                return block.GetComponent<Source>().output;
            default:
                return string.Empty;
        }
    }

	// Special check for blocks with dual outputs (Disassembler and Multiprocessor)
	// Verifies which output is connected to the requesting block
    private bool CheckDualOutputBlock(GameObject thisObject, GameObject inputSource, string input_required)
    {
        BaseBlock block = inputSource.GetComponent<BaseBlock>();
        if (!block.active || !block.correct) return false;

        if (inputSource.tag == "Disassembler")
        {
            var disassembler = inputSource.GetComponent<Disassembler>();
            if (disassembler.outputBlock1 == thisObject)
                return string.Equals(disassembler.output1, input_required, StringComparison.OrdinalIgnoreCase);
            if (disassembler.outputBlock2 == thisObject)
                return string.Equals(disassembler.output2, input_required, StringComparison.OrdinalIgnoreCase);
        }
        else if (inputSource.tag == "Multiprocessor")
        {
            var multiprocessor = inputSource.GetComponent<Multiprocessor>();
            if (multiprocessor.outputBlock1 == thisObject)
                return string.Equals(multiprocessor.output1, input_required, StringComparison.OrdinalIgnoreCase);
            if (multiprocessor.outputBlock2 == thisObject)
                return string.Equals(multiprocessor.output2, input_required, StringComparison.OrdinalIgnoreCase);
        }

        return false;
    }

	// Applies the specified material to a block's shape
	// Handles both standard shapes and table (or expand to other unique meshes) shapes differently
    public void ChangeMaterial(GameObject thisObject, Material colour)
    {
        var shape = thisObject.transform.Find("Shape");
        if (shape == null) return;

        if (shape.CompareTag("Table"))
        {
            var tableLod = shape.Find("lod1")?.GetComponent<MeshRenderer>();
            if (tableLod != null) tableLod.material = colour;
        }
        else
        {
            var renderer = shape.GetComponent<MeshRenderer>();
            if (renderer != null) renderer.material = colour;
        }
    }

	[ContextMenu("ShowEditCanvas")]
    private void ShowEditCanvas()
    {
		if (camera != null)
		{
			// Position in front of user
			Vector3 position = camera.transform.forward * CanvasDistance + camera.transform.position;
			editCanvas.transform.position = position;
			
			// Get the rotation facing the user
			Quaternion lookAtRotation = camera.transform.rotation;
			
			// Add a tilt by rotating around the X axis
			float tiltAngle = 20f; // Adjust this value to change the amount of tilt
			Vector3 rotationEuler = lookAtRotation.eulerAngles;
			rotationEuler.x += tiltAngle; // Positive tilts back, negative tilts forward
			
			editCanvas.transform.rotation = Quaternion.Euler(rotationEuler);
			editCanvas.SetActive(true);
		}
	}
}