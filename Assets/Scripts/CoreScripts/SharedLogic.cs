using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using TMPro;

public class SharedLogic : MonoBehaviour
{

	private GameObject camera;
	

	private Quaternion rotation;

	private Vector3 position;
	private GameObject editCanvas;
	private TMP_InputField editField;
	private Button enterButton;

	


	// I may want to refactor to remove the repeated block type stuff, can i abstract that out
	// Also, can I remove the need th care about outblocks at all, and just check output1/output2 for dissassembler and multiprocessor, it would remove a lot of work


	void Start () {
		camera = (GameObject) GameObject.FindWithTag("MainCamera");
		editCanvas = camera.transform.Find("TextEntryCanvas").gameObject;
		editField = editCanvas.transform.Find("EditField").GetComponent<TMP_InputField>();
		enterButton = editCanvas.transform.Find("EnterButton").GetComponent<Button>();
	}

	public void SetupLineRenderer(LineRenderer lr, float lineWidth, Material lineMaterial)
    {
        // Set the material for the LineRenderer
        if (lineMaterial != null)
        {
            lr.material = lineMaterial;
        }

        // Set the width for the LineRenderer
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;

        // Set the number of positions (always 2 for a single line segment)
        lr.positionCount = 2;
    }



	public String GetBlockTitle(GameObject block) 
	{
		String blocktype = block.tag;
		switch(blocktype) 
		{
			case "Source":
				Source SourceBlock = block.GetComponent<Source>();
				return SourceBlock.title;
			case "Processor":
				Processor ProcessorBlock = block.GetComponent<Processor>();
				return ProcessorBlock.title;
			case "Multiprocessor":
				Multiprocessor MultiprocessorBlock = block.GetComponent<Multiprocessor>();
				return MultiprocessorBlock.title;
			case "Assembler":
				Assembler AssemblerBlock = block.GetComponent<Assembler>();
				return AssemblerBlock.title;
			case "Disassembler":
				Disassembler DisassemblerBlock = block.GetComponent<Disassembler>();
				return DisassemblerBlock.title;
			case "Destination":
				Destination DestinationBlock = block.GetComponent<Destination>();
				return DestinationBlock.title;
			default:
				return ("Error");
		}
	}


	 public void EditObjectText(GameObject editedObject, String aspect)
    {
		Debug.Log("Edit Object: " + editedObject +  "Aspect: " + aspect);
		switch(aspect) 
		{
			case "title":
				editCanvas.SetActive(true);
				enterButton.onClick.AddListener(delegate{SetObjectTitle(editedObject);});
				break;
			case "input":
				editCanvas.SetActive(true);
				enterButton.onClick.AddListener(delegate{SetObjectInput(editedObject);});
				break;
			case "input1":
				editCanvas.SetActive(true);
				enterButton.onClick.AddListener(delegate { SetObjectInput1(editedObject); });
				break;
			case "input2":
				editCanvas.SetActive(true);
				enterButton.onClick.AddListener(delegate { SetObjectInput2(editedObject); });
				break;
			case "output":
				editCanvas.SetActive(true);
				enterButton.onClick.AddListener(delegate{SetObjectOutput(editedObject);});
				break;
			case "output1":
				editCanvas.SetActive(true);
				enterButton.onClick.AddListener(delegate{SetObjectOutput1(editedObject);});
				break;
			case "output2":
				editCanvas.SetActive(true);
				enterButton.onClick.AddListener(delegate{SetObjectOutput2(editedObject);});
				break;
			case "inblock":
				editCanvas.SetActive(true);
				enterButton.onClick.AddListener(delegate{SetObjectInBlock(editedObject);});
				break;
			case "inblock1":
				editCanvas.SetActive(true);
				enterButton.onClick.AddListener(delegate { SetObjectInBlock1(editedObject); });
				break;
			case "inblock2":
				editCanvas.SetActive(true);
				enterButton.onClick.AddListener(delegate { SetObjectInBlock2(editedObject); });
				break;
			case "outblock1":
				editCanvas.SetActive(true);
				enterButton.onClick.AddListener(delegate{SetObjectOutBlock1(editedObject);});
				break;
			case "outblock2":
				editCanvas.SetActive(true);
				enterButton.onClick.AddListener(delegate{SetObjectOutBlock2(editedObject);});
				break;


			default:
				Debug.Log("String Edit Failed");
				break;   
		}
    }


	public void SetObjectTitle(GameObject editedObject)
	{
		enterButton.onClick.RemoveAllListeners();
		String fieldValue = editField.text;
		editedObject.name = fieldValue;
		editCanvas.SetActive(false);
	}

	public void SetObjectInput(GameObject editedObject)
	{
		enterButton.onClick.RemoveAllListeners();
		String fieldValue = editField.text;

		if (editedObject.tag == "Processor"){

			Processor editedBlock = editedObject.GetComponent<Processor>();
			editedBlock.input_required = fieldValue;
			editCanvas.SetActive(false);

		} else if (editedObject.tag == "Disassembler") {

			Disassembler editedBlock = editedObject.GetComponent<Disassembler>();
			editedBlock.input_required = fieldValue;
			editCanvas.SetActive(false);

		} else if (editedObject.tag == "Destination") {

			Destination editedBlock = editedObject.GetComponent<Destination>();
			editedBlock.input_required = fieldValue;
			editCanvas.SetActive(false);

		} else{

			Debug.Log("Invalid object");
			editCanvas.SetActive(false);
		}

	}

	public void SetObjectInput1(GameObject editedObject)
	{
		enterButton.onClick.RemoveAllListeners();
		String fieldValue = editField.text;

		if (editedObject.tag == "Assembler")
		{

			Assembler editedBlock = editedObject.GetComponent<Assembler>();
			editedBlock.input_required1 = fieldValue;
			editCanvas.SetActive(false);


		}
		else if (editedObject.tag == "Multiprocessor")
		{

			Multiprocessor editedBlock = editedObject.GetComponent<Multiprocessor>();
			editedBlock.input_required1 = fieldValue;
			editCanvas.SetActive(false);

		}
		else
		{

			Debug.Log("Invalid object");
			editCanvas.SetActive(false);
		}

	}

	public void SetObjectInput2(GameObject editedObject)
	{
		enterButton.onClick.RemoveAllListeners();
		String fieldValue = editField.text;

		if (editedObject.tag == "Assembler")
		{

			Assembler editedBlock = editedObject.GetComponent<Assembler>();
			editedBlock.input_required2 = fieldValue;
			editCanvas.SetActive(false);


		}
		else if (editedObject.tag == "Multiprocessor")
		{

			Multiprocessor editedBlock = editedObject.GetComponent<Multiprocessor>();
			editedBlock.input_required2 = fieldValue;
			editCanvas.SetActive(false);

		}
		else
		{

			Debug.Log("Invalid object");
			editCanvas.SetActive(false);
		}

	}


	public void SetObjectOutput(GameObject editedObject)
	{
		enterButton.onClick.RemoveAllListeners();
		String fieldValue = editField.text;

		if (editedObject.tag == "Processor"){

			Processor editedBlock = editedObject.GetComponent<Processor>();
			editedBlock.output = fieldValue;
			editCanvas.SetActive(false);
			

		} else if (editedObject.tag == "Assembler") {

			Assembler editedBlock = editedObject.GetComponent<Assembler>();
			editedBlock.output = fieldValue;
			editCanvas.SetActive(false);

		} else if (editedObject.tag == "Source") {

			Source editedBlock = editedObject.GetComponent<Source>();
			editedBlock.output = fieldValue;
			editCanvas.SetActive(false);

		} else{

			Debug.Log("Invalid object");
			editCanvas.SetActive(false);
		}

	}

	public void SetObjectOutput1(GameObject editedObject)
	{
		enterButton.onClick.RemoveAllListeners();
		String fieldValue = editField.text;

		if (editedObject.tag == "Disassembler"){

			Disassembler editedBlock = editedObject.GetComponent<Disassembler>();
			editedBlock.output1 = fieldValue;
			editCanvas.SetActive(false);
			

		} else if (editedObject.tag == "Multiprocessor") {

			Multiprocessor editedBlock = editedObject.GetComponent<Multiprocessor>();
			editedBlock.output1 = fieldValue;
			editCanvas.SetActive(false);

		} else{

			Debug.Log("Invalid object");
			editCanvas.SetActive(false);
		}

	}

		public void SetObjectOutput2(GameObject editedObject)
	{
		enterButton.onClick.RemoveAllListeners();
		String fieldValue = editField.text;

		if (editedObject.tag == "Disassembler"){

			Disassembler editedBlock = editedObject.GetComponent<Disassembler>();
			editedBlock.output2 = fieldValue;
			editCanvas.SetActive(false);
			

		} else if (editedObject.tag == "Multiprocessor") {

			Multiprocessor editedBlock = editedObject.GetComponent<Multiprocessor>();
			editedBlock.output2 = fieldValue;
			editCanvas.SetActive(false);

		} else{

			Debug.Log("Invalid object");
			editCanvas.SetActive(false);
		}

	}

	public void SetObjectInBlock(GameObject editedObject)
	{
		Debug.Log(editedObject.name);
		enterButton.onClick.RemoveAllListeners();
		String fieldValue = editField.text;
		GameObject InBlock = GameObject.Find(fieldValue);

		if (InBlock == null) {
			Debug.Log("invalid block entered, aborting assignment");
			editCanvas.SetActive(false);

		} else if (editedObject.tag == "Processor"){

			Processor editedBlock = editedObject.GetComponent<Processor>();
			editedBlock.inputSource = InBlock;
			editCanvas.SetActive(false);
			

		} else if (editedObject.tag == "Destination") {

			Destination editedBlock = editedObject.GetComponent<Destination>();
			editedBlock.inputSource = InBlock;
			editCanvas.SetActive(false);

		} else if (editedObject.tag == "Disassembler") {

			Disassembler editedBlock = editedObject.GetComponent<Disassembler>();
			editedBlock.inputSource = InBlock;
			editCanvas.SetActive(false);


		} else{

			Debug.Log("Invalid object");
			// maybe add an error sound here for feedback
			editCanvas.SetActive(false);
		}

	}

	public void SetObjectInBlock1(GameObject editedObject)
	{
		Debug.Log(editedObject.name);
		enterButton.onClick.RemoveAllListeners();
		String fieldValue = editField.text;
		GameObject InBlock = GameObject.Find(fieldValue);

		if (InBlock == null)
		{
			Debug.Log("invalid block entered, aborting assignment");
			editCanvas.SetActive(false);

		}
		else if (editedObject.tag == "Assembler")
		{

			Assembler editedBlock = editedObject.GetComponent<Assembler>();
			editedBlock.inputSource1 = InBlock;
			editCanvas.SetActive(false);


		}
		else if (editedObject.tag == "Multiprocessor")
		{

			Multiprocessor editedBlock = editedObject.GetComponent<Multiprocessor>();
			editedBlock.inputSource1 = InBlock;
			editCanvas.SetActive(false);

		}
		else
		{

			Debug.Log("Invalid object");
			editCanvas.SetActive(false);
		}

	}

	public void SetObjectInBlock2(GameObject editedObject)
	{
		Debug.Log(editedObject.name);
		enterButton.onClick.RemoveAllListeners();
		String fieldValue = editField.text;
		GameObject InBlock = GameObject.Find(fieldValue);

		if (InBlock == null)
		{
			Debug.Log("invalid block entered, aborting assignment");
			editCanvas.SetActive(false);

		}
		else if (editedObject.tag == "Assembler")
		{

			Assembler editedBlock = editedObject.GetComponent<Assembler>();
			editedBlock.inputSource2 = InBlock;
			editCanvas.SetActive(false);


		}
		else if (editedObject.tag == "Multiprocessor")
		{

			Multiprocessor editedBlock = editedObject.GetComponent<Multiprocessor>();
			editedBlock.inputSource2 = InBlock;
			editCanvas.SetActive(false);

		}
		else
		{

			Debug.Log("Invalid object");
			editCanvas.SetActive(false);
		}

	}

	public void SetObjectOutBlock1(GameObject editedObject)
	{
		Debug.Log(editedObject.name);
		enterButton.onClick.RemoveAllListeners();
		String fieldValue = editField.text;
		GameObject OutBlock1 = GameObject.Find(fieldValue);

		if (OutBlock1 == null) {
			Debug.Log("invalid block entered, aborting assignment");
			editCanvas.SetActive(false);

		} else if (editedObject.tag == "Disassembler") {

			Disassembler editedBlock = editedObject.GetComponent<Disassembler>();
			editedBlock.outputBlock1 = OutBlock1;
			editCanvas.SetActive(false);

		} else if (editedObject.tag == "Multiprocessor") {

			Multiprocessor editedBlock = editedObject.GetComponent<Multiprocessor>();
			editedBlock.outputBlock1 = OutBlock1;
			editCanvas.SetActive(false);


		} else{

			Debug.Log("Invalid object");
			editCanvas.SetActive(false);
		}

	}

	public void SetObjectOutBlock2(GameObject editedObject)
	{
		Debug.Log(editedObject.name);
		enterButton.onClick.RemoveAllListeners();
		String fieldValue = editField.text;
		GameObject OutBlock2 = GameObject.Find(fieldValue);

		if (OutBlock2 == null) {
			Debug.Log("invalid block entered, aborting assignment");
			editCanvas.SetActive(false);

		} else if (editedObject.tag == "Disassembler") {

			Disassembler editedBlock = editedObject.GetComponent<Disassembler>();
			editedBlock.outputBlock2 = OutBlock2;
			editCanvas.SetActive(false);

		} else if (editedObject.tag == "Multiprocessor") {

			Multiprocessor editedBlock = editedObject.GetComponent<Multiprocessor>();
			editedBlock.outputBlock2 = OutBlock2;
			editCanvas.SetActive(false);

		} else{

			Debug.Log("Invalid object");
			editCanvas.SetActive(false);
		}

	}


	public bool CheckInput(GameObject thisObject, GameObject inputSource, bool correct, String input_required)
	{
		if (inputSource == null)
        {
			correct = false;
        }
		else if (inputSource.tag == "Disassembler")
		{
			Disassembler inputAttributes = inputSource.GetComponent<Disassembler>();

			if ((inputAttributes.correct) && (inputAttributes.active))
			{
				if (inputAttributes.outputBlock1 == thisObject)
				{
					correct = string.Equals(inputAttributes.output1, input_required, StringComparison.OrdinalIgnoreCase);
				}
				else if (inputAttributes.outputBlock2 == thisObject)
				{
					correct = string.Equals(inputAttributes.output2, input_required, StringComparison.OrdinalIgnoreCase);
				}
				else
				{
					correct = false;
					Debug.Log("InBlock `Output Block Error");
				}
			}
			else
			{
				correct = false;
				Debug.Log("InBlock Off or Incorrect");
			}

		} 
		else if (inputSource.tag == "Multiprocessor")
		{
			Multiprocessor inputAttributes = inputSource.GetComponent<Multiprocessor>();
			//Debug.Log("this: " + thisObject.name + " inputObject: " + inputSource.name + " inputReq: " + input_required + " inputAttributes.output: " + inputAttributes.output1 + " correct: " + correct + " inputIsActive: " + inputAttributes.active);
			if ((inputAttributes.correct) && (inputAttributes.active))
			{
				if (inputAttributes.outputBlock1 == thisObject)
				{
					correct = string.Equals(inputAttributes.output1, input_required, StringComparison.OrdinalIgnoreCase);
				}
				else if (inputAttributes.outputBlock2 == thisObject)
				{
					correct = string.Equals(inputAttributes.output2, input_required, StringComparison.OrdinalIgnoreCase);
				}
				else
				{
					correct = false;
					Debug.Log("InBlock Output Block Error");
				}
			}
			else
			{
				correct = false;
				Debug.Log("InBlock Off or Incorrect");
			}

		}
		else if (inputSource.tag == "Processor")
		{
			Processor inputAttributes = inputSource.GetComponent<Processor>();
			correct = ((string.Equals(inputAttributes.output, input_required, StringComparison.OrdinalIgnoreCase)) && (inputAttributes.correct) && (inputAttributes.active));

		}
		else if (inputSource.tag == "Assembler")
		{
			Assembler inputAttributes = inputSource.GetComponent<Assembler>();
			correct = ((string.Equals(inputAttributes.output, input_required, StringComparison.OrdinalIgnoreCase)) && (inputAttributes.correct) && (inputAttributes.active));
		}
		else if (inputSource.tag == "Source")
		{
			Source inputAttributes = inputSource.GetComponent<Source>();
			correct = ((string.Equals(inputAttributes.output, input_required, StringComparison.OrdinalIgnoreCase)) && (inputAttributes.active));
		}
		else
		{
			Debug.Log("No Valid Tag Found");
		}
		
		return correct;
	}

	public void ManageColour(GameObject thisObject, bool active, bool correct, Material grey, Material green, Material red)
	{
		if (active == false)
		{
			ChangeMaterial(thisObject, grey);
		}
		else if (correct == true)
		{
			ChangeMaterial(thisObject, green);
		}
		else
		{
			ChangeMaterial(thisObject, red);
		}
	}

	void ChangeMaterial(GameObject thisObject, Material colour)
	{
		var children = thisObject.GetComponentsInChildren<Transform>();
		foreach (var child in children)
			if (child.name == "Cube")
				child.GetComponent<MeshRenderer>().material = colour;
	}
}
