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

	public GameObject sourcePrefab;
	public GameObject processorPrefab;
	public GameObject multiprocessorPrefab;
	public GameObject assemblerPrefab;
	public GameObject disassemblerPrefab;
	public GameObject destinationPrefab;
	private GameObject camera;
	

	private Quaternion rotation;

	private Vector3 position;
	private int DistanceToCamera = 4;
	public Transform creation;
	public GameObject editCanvas;
	public TMP_InputField editField;
	public Button enterButton;


	// I may want to refactor to remove the repeated block type stuff, can i abstract that out
	// Also, can I remove the need th care about outblocks at all, and just check output1/output2 for dissassembler and multiprocessor, it would remove a lot of work


	void Start () {
		camera = (GameObject) GameObject.FindWithTag("MainCamera");
	}


	[ContextMenu("SpawnSource")]
	public void SpawnSource()
	{
		position = camera.transform.forward * DistanceToCamera + camera.transform.position;
		rotation = camera.transform.rotation;
		SpawnBlock("Source",  position, rotation);
	}

	[ContextMenu("SpawnProcessor")]
	public void SpawnProcessor()
	{
		position = camera.transform.forward * DistanceToCamera + camera.transform.position;
		rotation = camera.transform.rotation;
		SpawnBlock("Processor", position, rotation);
	}

	[ContextMenu("SpawnMultiprocessor")]
	public void SpawnMultiprocessor()
	{
		position = camera.transform.forward * DistanceToCamera + camera.transform.position;	
		rotation = camera.transform.rotation;
		SpawnBlock("Multiprocessor", position, rotation);
	}

	[ContextMenu("SpawnAssembler")]
	public void SpawnAssembler()
	{
		position = camera.transform.forward * DistanceToCamera + camera.transform.position;	
		rotation = camera.transform.rotation;
		SpawnBlock("Assembler", position, rotation);
	}

	[ContextMenu("SpawnDisassembler")]
	public void SpawnDisassembler()
	{
		position = camera.transform.forward * DistanceToCamera + camera.transform.position;	
		rotation = camera.transform.rotation;
		SpawnBlock("Disassembler", position, rotation);
	}

	[ContextMenu("SpawnDestination")]
	public void SpawnDestination()
	{	
		position = camera.transform.forward * DistanceToCamera + camera.transform.position;
		rotation = camera.transform.rotation;
		SpawnBlock("Destination", position, rotation);
	}

	public void SpawnBlock(string blockType, Vector3 position, Quaternion rotation) 
	{

		switch(blockType) 
		{
			case "Source":
				Instantiate(sourcePrefab, position, rotation);
				break;
			case "Processor":
				Instantiate(processorPrefab, position, rotation);
				break;
			case "Multiprocessor":
				Instantiate(multiprocessorPrefab, position, rotation);
				break;
			case "Assembler":
				Instantiate(assemblerPrefab, position, rotation);
				break;
			case "Disassembler":
				Instantiate(disassemblerPrefab, position, rotation);
				break;
			case "Destination":
				Instantiate(destinationPrefab, position, rotation);
				break;
			default:
				Debug.Log("Block Spawn Type Failed");
				break;
		}
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
		Debug.Log(editedObject.name);
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
			case "output":
				editCanvas.SetActive(true);
				enterButton.onClick.AddListener(delegate{SetObjectOutput(editedObject);});
				break;
			case "inblock":
				editCanvas.SetActive(true);
				enterButton.onClick.AddListener(delegate{SetObjectInBlock(editedObject);});
				break;
			// outblock 


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

			// Multiprocessor - setting inblocks for multiple outputs on current block
			// Disassembler - setting inblocks for multiple outputs on current block

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
		Debug.Log(InBlock);

		if (editedObject.tag == "Processor"){

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

		// Multiprocessor - setting inblocks for multiple inputs on current block
		// Assembler - setting inblocks for multiple inputs on current block


		} else{

			Debug.Log("Invalid object");
			editCanvas.SetActive(false);
		}

	}


	public bool CheckInput(GameObject thisObject, GameObject inputSource, bool correct, String input_required)
	{
		if (inputSource.tag == "Disassembler")
		{
			Disassembler inputAttributes = inputSource.GetComponent<Disassembler>();

			if ((inputAttributes.correct) && (inputAttributes.active))
			{
				if (inputAttributes.outputBlock1 == thisObject)
				{
					correct = (inputAttributes.output1 == input_required);
				}
				else if (inputAttributes.outputBlock2 == thisObject)
				{
					correct = (inputAttributes.output2 == input_required);
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
		else if (inputSource.tag == "Multiprocessor")
		{
			Multiprocessor inputAttributes = inputSource.GetComponent<Multiprocessor>();
			//Debug.Log("this: " + thisObject.name + " inputObject: " + inputSource.name + " inputReq: " + input_required + " inputAttributes.output: " + inputAttributes.output1 + " correct: " + correct + " inputIsActive: " + inputAttributes.active);
			if ((inputAttributes.correct) && (inputAttributes.active))
			{
				if (inputAttributes.outputBlock1 == thisObject)
				{
					correct = (inputAttributes.output1 == input_required);
				}
				else if (inputAttributes.outputBlock2 == thisObject)
				{
					correct = (inputAttributes.output2 == input_required);
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
			correct = ((inputAttributes.output == input_required) && (inputAttributes.correct) && (inputAttributes.active));
		}
		else if (inputSource.tag == "Assembler")
		{
			Assembler inputAttributes = inputSource.GetComponent<Assembler>();
			correct = ((inputAttributes.output == input_required) && (inputAttributes.correct) && (inputAttributes.active));
		}
		else if (inputSource.tag == "Source")
		{
			Source inputAttributes = inputSource.GetComponent<Source>();
			correct = ((inputAttributes.output == input_required) && (inputAttributes.active));
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
