using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMenu : MonoBehaviour
{
    
	public GameObject sourcePrefab;
	public GameObject processorPrefab;
	public GameObject multiprocessorPrefab;
	public GameObject assemblerPrefab;
	public GameObject disassemblerPrefab;
	public GameObject destinationPrefab;
	private GameObject camera;

	public SaveSystem saveSystem;
	

	private Quaternion rotation;

	private Vector3 position;
	private int DistanceToCamera = 2;


	void Start() {
	camera = (GameObject) GameObject.FindWithTag("MainCamera");
	}


	public void SpawnSource()
	{
		position = camera.transform.forward * DistanceToCamera + camera.transform.position;
		rotation = camera.transform.rotation * Quaternion.Euler(0, -90, 0);
		SpawnBlock("Source",  position, rotation);
	}

	public void SpawnProcessor()
	{
		position = camera.transform.forward * DistanceToCamera + camera.transform.position;
		rotation = camera.transform.rotation * Quaternion.Euler(0, -90, 0);
		SpawnBlock("Processor", position, rotation);
	}

	public void SpawnMultiprocessor()
	{
		position = camera.transform.forward * DistanceToCamera + camera.transform.position;
		rotation = camera.transform.rotation * Quaternion.Euler(0, -90, 0);
		SpawnBlock("Multiprocessor", position, rotation);
	}

	public void SpawnAssembler()
	{
		position = camera.transform.forward * DistanceToCamera + camera.transform.position;
		rotation = camera.transform.rotation * Quaternion.Euler(0, -90, 0);
		SpawnBlock("Assembler", position, rotation);
	}

	public void SpawnDisassembler()
	{
		position = camera.transform.forward * DistanceToCamera + camera.transform.position;
		rotation = camera.transform.rotation * Quaternion.Euler(0, -90, 0);
		SpawnBlock("Disassembler", position, rotation);
	}

	public void SpawnDestination()
	{	
		position = camera.transform.forward * DistanceToCamera + camera.transform.position;
		rotation = camera.transform.rotation * Quaternion.Euler(0, -90, 0);
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

	public void SaveAll()
    {
		saveSystem.SaveAllBlocks();
    }

	public void ClearAll()
	{
		saveSystem.RemoveAllBlocks();
	}

	public void LoadAll()
	{
		saveSystem.LoadAllBlocks();
	}

	public void ToggleObjectActive()
	{
		gameObject.SetActive(!gameObject.activeInHierarchy);
	}



	void Update()
    {
        
    }
}
