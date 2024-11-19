using UnityEngine;

// Handles the spawning of different block types and manages the spawn menu interface
public class SpawnMenu : MonoBehaviour
{
    [Header("Block Prefabs")]
    public GameObject sourcePrefab;
    public GameObject processorPrefab;
    public GameObject multiprocessorPrefab;
    public GameObject assemblerPrefab;
    public GameObject disassemblerPrefab;
    public GameObject destinationPrefab;
    public GameObject propPrefab;

    // Reference to the save system for managing block persistence
    public SaveSystem saveSystem;

    // Camera reference for spawn positioning
    private GameObject camera;
    private const float BlockDistanceToCamera = 2f;
	private const float MenuDistanceToCamera = 0.6f;
	private const float tiltAngle = 10f; // Adjust this value to change the amount of tilt


    // Structure to hold block prefab information
    // Could be used for future expansion of block types
    private struct BlockPrefabInfo
    {
        public string Type;
        public GameObject Prefab;

        public BlockPrefabInfo(string type, GameObject prefab)
        {
            Type = type;
            Prefab = prefab;
        }
    }


    // Initialize camera reference on start
    void Start()
    {
        camera = GameObject.FindWithTag("MainCamera");
    }


    // Calculates spawn position in front of the camera
    // return Vector3 position for new block spawn
    private Vector3 GetSpawnPosition()
    {
        return camera.transform.forward * BlockDistanceToCamera + camera.transform.position;
    }

    // Calculates rotation for spawned block based on camera orientation
    //returns Quaternion rotation for new block spawn
    private Quaternion GetSpawnRotation()
    {
        return camera.transform.rotation * Quaternion.Euler(0, -90, 0);
    }

    // Convenience methods for spawning each block type
    // These are typically connected to UI buttons
    public void SpawnSource() => SpawnBlock("Source", GetSpawnPosition(), GetSpawnRotation());
    public void SpawnProcessor() => SpawnBlock("Processor", GetSpawnPosition(), GetSpawnRotation());
    public void SpawnMultiprocessor() => SpawnBlock("Multiprocessor", GetSpawnPosition(), GetSpawnRotation());
    public void SpawnAssembler() => SpawnBlock("Assembler", GetSpawnPosition(), GetSpawnRotation());
    public void SpawnDisassembler() => SpawnBlock("Disassembler", GetSpawnPosition(), GetSpawnRotation());
    public void SpawnDestination() => SpawnBlock("Destination", GetSpawnPosition(), GetSpawnRotation());
    public void SpawnProp() => SpawnBlock("Prop", GetSpawnPosition(), GetSpawnRotation());

    // Main block spawning method
    // blockType Type of block to spawn
    // position World position to spawn at
    // rotation Initial rotation of spawned block
    public void SpawnBlock(string blockType, Vector3 position, Quaternion rotation)
    {
        GameObject prefab = GetPrefabForType(blockType);
        if (prefab != null)
        {
            Instantiate(prefab, position, rotation);
        }
        else
        {
            Debug.LogError($"Block Spawn Type Failed: {blockType}");
        }
    }


    // Maps block type strings to their corresponding prefabs
    // blockType -> String identifier for block type
    // GameObject prefab for the specified block type, or null if not found
    private GameObject GetPrefabForType(string blockType)
    {
        return blockType switch
        {
            "Source" => sourcePrefab,
            "Processor" => processorPrefab,
            "Multiprocessor" => multiprocessorPrefab,
            "Assembler" => assemblerPrefab,
            "Disassembler" => disassemblerPrefab,
            "Destination" => destinationPrefab,
            "Prop" => propPrefab,
            _ => null
        };
    }

    // Save System Integration Methods

    // Triggers save operation for all blocks in the scene
    public void SaveAll() => saveSystem.SaveAllBlocks();

    // Removes all blocks from the scene
    public void ClearAll() => saveSystem.RemoveAllBlocks();

	// Loads previously saved blocks into the scene
    public void LoadAll() => saveSystem.LoadAllBlocks();

    // Toggles the visibility of the spawn menu

	[ContextMenu("ShowSpawnMenu")]
    public void ShowSpawnMenu()
    {
		
		gameObject.SetActive(!gameObject.activeInHierarchy);
		
		if (camera != null && gameObject.activeInHierarchy)
		{
			// Position in front of user
			Vector3 position = camera.transform.forward * MenuDistanceToCamera + camera.transform.position;
			gameObject.transform.position = position;
			
			// Get the rotation facing the user
			Quaternion lookAtRotation = camera.transform.rotation;
			
			// Add a tilt by rotating around the X axis
			Vector3 rotationEuler = lookAtRotation.eulerAngles;
			rotationEuler.x += tiltAngle; // Positive tilts back, negative tilts forward
			
			gameObject.transform.rotation = Quaternion.Euler(rotationEuler);
		}
	}





}