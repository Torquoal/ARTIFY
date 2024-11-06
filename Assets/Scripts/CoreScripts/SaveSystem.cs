using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    [Header("Block Prefabs")]
    [SerializeField] Processor processorPrefab;
    [SerializeField] Source sourcePrefab;
    [SerializeField] Destination destinationPrefab;
    [SerializeField] Assembler assemblerPrefab;
    [SerializeField] Disassembler disassemblerPrefab;
    [SerializeField] Multiprocessor multiprocessorPrefab;

    // Static lists to track all blocks
    public static List<Processor> processors = new List<Processor>();
    public static List<Source> sources = new List<Source>();
    public static List<Destination> destinations = new List<Destination>();
    public static List<Assembler> assemblers = new List<Assembler>();
    public static List<Disassembler> disassemblers = new List<Disassembler>();
    public static List<Multiprocessor> multiprocessors = new List<Multiprocessor>();

    // File path constants
    private const string PROCESSOR_SUB = "/processor";
    private const string PROCESSOR_COUNT = "/processor.count";
    private const string SOURCE_SUB = "/source";
    private const string SOURCE_COUNT = "/source.count";
    private const string DESTINATION_SUB = "/destination";
    private const string DESTINATION_COUNT = "/destination.count";
    private const string ASSEMBLER_SUB = "/assembler";
    private const string ASSEMBLER_COUNT = "/assembler.count";
    private const string DISASSEMBLER_SUB = "/disassembler";
    private const string DISASSEMBLER_COUNT = "/disassembler.count";
    private const string MULTIPROCESSOR_SUB = "/multiprocessor";
    private const string MULTIPROCESSOR_COUNT = "/multiprocessor.count";

    [ContextMenu("SaveAllBlocks")]
    public void SaveAllBlocks()
    {
        SaveProcessors();
        SaveSources();
        SaveDestinations();
        SaveAssemblers();
        SaveDisassemblers();
        SaveMultiprocessors();
        Debug.Log("Save all completed");
    }

    private void SaveBlocks<T>(List<T> blocks, string subPath, string countPath) where T : MonoBehaviour
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + subPath + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + countPath + SceneManager.GetActiveScene().buildIndex;

        using (FileStream countStream = new FileStream(countpath, FileMode.Create))
        {
            formatter.Serialize(countStream, blocks.Count);
        }

        for (int i = 0; i < blocks.Count; i++)
        {
            using (FileStream stream = new FileStream(path + i, FileMode.Create))
            {
                var data = CreateBlockData(blocks[i]);
                formatter.Serialize(stream, data);
            }
        }
    }

    private object CreateBlockData(MonoBehaviour block)
    {
        return block switch
        {
            Processor p => new ProcessorData(p),
            Source s => new SourceData(s),
            Destination d => new DestinationData(d),
            Assembler a => new AssemblerData(a),
            Disassembler d => new DisassemblerData(d),
            Multiprocessor m => new MultiprocessorData(m),
            _ => throw new System.ArgumentException($"Unknown block type: {block.GetType()}")
        };
    }

    private void SaveProcessors() => SaveBlocks(processors, PROCESSOR_SUB, PROCESSOR_COUNT);
    private void SaveSources() => SaveBlocks(sources, SOURCE_SUB, SOURCE_COUNT);
    private void SaveDestinations() => SaveBlocks(destinations, DESTINATION_SUB, DESTINATION_COUNT);
    private void SaveAssemblers() => SaveBlocks(assemblers, ASSEMBLER_SUB, ASSEMBLER_COUNT);
    private void SaveDisassemblers() => SaveBlocks(disassemblers, DISASSEMBLER_SUB, DISASSEMBLER_COUNT);
    private void SaveMultiprocessors() => SaveBlocks(multiprocessors, MULTIPROCESSOR_SUB, MULTIPROCESSOR_COUNT);

    [ContextMenu("LoadAllBlocks")]
    public void LoadAllBlocks()
    {
        RemoveAllBlocks();
        LoadSources();
        LoadProcessors();
        LoadDestinations();
        LoadAssemblers();
        LoadDisassemblers();
        LoadMultiprocessors();
    }

    private void ApplyBlockShape(BaseBlock block, BlockShape shape)
{
    if (block == null)
    {
        Debug.LogError("Cannot apply shape to null block");
        return;
    }

    Debug.Log($"Applying shape {shape} to block {block.name} where shape={block.shape != null}");
    
    // Ensure we create the shape first
    switch (shape)
    {
        case BlockShape.Sphere:
            block.ToSphere();
            break;
        case BlockShape.Cylinder:
            Debug.Log($"Cylinder where shape={block.shape != null}");
            block.ToCylinder();
            break;
        case BlockShape.Table:
            block.ToTable();
            break;
        case BlockShape.Cube:
        default:
            block.ToCube();
            break;
    }
}

    void LoadProcessors()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + PROCESSOR_SUB + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + PROCESSOR_COUNT + SceneManager.GetActiveScene().buildIndex;
        int processorCount = LoadCount(countpath);

        for (int i = 0; i < processorCount; i++)
        {
            if (File.Exists(path + i))
            {
                using (FileStream stream = new FileStream(path + i, FileMode.Open))
                {
                    ProcessorData data = formatter.Deserialize(stream) as ProcessorData;
                    InstantiateProcessor(data);
                }
            }
        }
    }

    void LoadSources()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SOURCE_SUB + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + SOURCE_COUNT + SceneManager.GetActiveScene().buildIndex;
        int sourceCount = LoadCount(countpath);

        for (int i = 0; i < sourceCount; i++)
        {
            if (File.Exists(path + i))
            {
                using (FileStream stream = new FileStream(path + i, FileMode.Open))
                {
                    SourceData data = formatter.Deserialize(stream) as SourceData;
                    InstantiateSource(data);
                }
            }
        }
    }

    void LoadDestinations()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + DESTINATION_SUB + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + DESTINATION_COUNT + SceneManager.GetActiveScene().buildIndex;
        int count = LoadCount(countpath);

        for (int i = 0; i < count; i++)
        {
            if (File.Exists(path + i))
            {
                using (FileStream stream = new FileStream(path + i, FileMode.Open))
                {
                    DestinationData data = formatter.Deserialize(stream) as DestinationData;
                    InstantiateDestination(data);
                }
            }
        }
    }

    void LoadAssemblers()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + ASSEMBLER_SUB + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + ASSEMBLER_COUNT + SceneManager.GetActiveScene().buildIndex;
        int count = LoadCount(countpath);

        for (int i = 0; i < count; i++)
        {
            if (File.Exists(path + i))
            {
                using (FileStream stream = new FileStream(path + i, FileMode.Open))
                {
                    AssemblerData data = formatter.Deserialize(stream) as AssemblerData;
                    InstantiateAssembler(data);
                }
            }
        }
    }

    void LoadDisassemblers()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + DISASSEMBLER_SUB + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + DISASSEMBLER_COUNT + SceneManager.GetActiveScene().buildIndex;
        int count = LoadCount(countpath);

        for (int i = 0; i < count; i++)
        {
            if (File.Exists(path + i))
            {
                using (FileStream stream = new FileStream(path + i, FileMode.Open))
                {
                    DisassemblerData data = formatter.Deserialize(stream) as DisassemblerData;
                    InstantiateDisassembler(data);
                }
            }
        }
    }

    void LoadMultiprocessors()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + MULTIPROCESSOR_SUB + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + MULTIPROCESSOR_COUNT + SceneManager.GetActiveScene().buildIndex;
        int count = LoadCount(countpath);

        for (int i = 0; i < count; i++)
        {
            if (File.Exists(path + i))
            {
                using (FileStream stream = new FileStream(path + i, FileMode.Open))
                {
                    MultiprocessorData data = formatter.Deserialize(stream) as MultiprocessorData;
                    InstantiateMultiprocessor(data);
                }
            }
        }
    }

    private void InstantiateProcessor(ProcessorData data)
    {
        Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
        Vector3 blockScale = new Vector3(data.scale[0], data.scale[1], data.scale[2]);

        Processor processor = Instantiate(processorPrefab, position, Quaternion.identity);
        processor.transform.localScale = blockScale;
        processor.Setup();

        processor.name = data.name;
        processor.title = data.title;
        processor.input_required = data.input_required;
        processor.output = data.output;

        ApplyBlockShape(processor, data.shape);

        if (data.inputSourceName != null)
        {
            GameObject InBlock = GameObject.Find(data.inputSourceName);
            processor.inputSource = InBlock;
        }
    }

    private void InstantiateSource(SourceData data)
    {
        Debug.Log($"Loading source with saved scale: {new Vector3(data.scale[0], data.scale[1], data.scale[2])}");
        
        Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
        Vector3 blockScale = new Vector3(data.scale[0], data.scale[1], data.scale[2]);

        Source source = Instantiate(sourcePrefab, position, Quaternion.identity);
        source.name = data.name;
        source.title = data.title;
        source.output = data.output;

        // First apply the shape
        ApplyBlockShape(source, data.shape);
        
        // Then scale the block
        source.transform.localScale = blockScale;
        
        Debug.Log($"[{source.name}] Loaded with block scale: {source.transform.localScale}, shape scale: {source.shape.transform.localScale}");
    }

    private void InstantiateDestination(DestinationData data)
    {
        Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
        Vector3 blockScale = new Vector3(data.scale[0], data.scale[1], data.scale[2]);

        Destination destination = Instantiate(destinationPrefab, position, Quaternion.identity);
        destination.transform.localScale = blockScale;
        destination.Setup();

        destination.name = data.name;
        destination.title = data.title;
        destination.input_required = data.input_required;

        ApplyBlockShape(destination, data.shape);

        if (data.inputSourceName != null)
        {
            GameObject InBlock = GameObject.Find(data.inputSourceName);
            destination.inputSource = InBlock;
        }
    }

    private void InstantiateAssembler(AssemblerData data)
    {
        Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
        Vector3 blockScale = new Vector3(data.scale[0], data.scale[1], data.scale[2]);

        Assembler assembler = Instantiate(assemblerPrefab, position, Quaternion.identity);
        assembler.transform.localScale = blockScale;
        assembler.Setup();

        assembler.name = data.name;
        assembler.title = data.title;
        assembler.input_required1 = data.input_required1;
        assembler.input_required2 = data.input_required2;
        assembler.output = data.output;

        ApplyBlockShape(assembler, data.shape);

        if (data.inputSourceName1 != null)
        {
            GameObject InBlock1 = GameObject.Find(data.inputSourceName1);
            assembler.inputSource1 = InBlock1;
        }
        if (data.inputSourceName2 != null)
        {
            GameObject InBlock2 = GameObject.Find(data.inputSourceName2);
            assembler.inputSource2 = InBlock2;
        }
    }

    private void InstantiateDisassembler(DisassemblerData data)
    {
        Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
        Vector3 blockScale = new Vector3(data.scale[0], data.scale[1], data.scale[2]);

        Disassembler disassembler = Instantiate(disassemblerPrefab, position, Quaternion.identity);
        disassembler.transform.localScale = blockScale;
        disassembler.Setup();
        disassembler.name = data.name;
        disassembler.title = data.title;
        disassembler.input_required = data.input_required;
        disassembler.output1 = data.output1;
        disassembler.output2 = data.output2;

        ApplyBlockShape(disassembler, data.shape);

        if (data.inputSourceName != null)
        {
            GameObject InBlock = GameObject.Find(data.inputSourceName);
            disassembler.inputSource = InBlock;
        }
        if (data.outputBlockName1 != null)
        {
            GameObject OutBlock1 = GameObject.Find(data.outputBlockName1);
            disassembler.outputBlock1 = OutBlock1;
        }
        if (data.outputBlockName2 != null)
        {
            GameObject OutBlock2 = GameObject.Find(data.outputBlockName2);
            disassembler.outputBlock2 = OutBlock2;
        }
    }

    private void InstantiateMultiprocessor(MultiprocessorData data)
    {
        Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
        Vector3 blockScale = new Vector3(data.scale[0], data.scale[1], data.scale[2]);

        Multiprocessor multiprocessor = Instantiate(multiprocessorPrefab, position, Quaternion.identity);
        multiprocessor.transform.localScale = blockScale;
        multiprocessor.Setup();
        multiprocessor.name = data.name;
        multiprocessor.title = data.title;
        multiprocessor.input_required1 = data.input_required1;
        multiprocessor.input_required2 = data.input_required2;
        multiprocessor.output1 = data.output1;
        multiprocessor.output2 = data.output2;

        ApplyBlockShape(multiprocessor, data.shape);

        if (data.inputSourceName1 != null)
        {
            GameObject InBlock1 = GameObject.Find(data.inputSourceName1);
            multiprocessor.inputSource1 = InBlock1;
        }
        if (data.inputSourceName2 != null)
        {
            GameObject InBlock2 = GameObject.Find(data.inputSourceName2);
            multiprocessor.inputSource2 = InBlock2;
        }
        if (data.outputBlockName1 != null)
        {
            GameObject OutBlock1 = GameObject.Find(data.outputBlockName1);
            multiprocessor.outputBlock1 = OutBlock1;
        }
        if (data.outputBlockName2 != null)
        {
            GameObject OutBlock2 = GameObject.Find(data.outputBlockName2);
            multiprocessor.outputBlock2 = OutBlock2;
        }
    }

    private int LoadCount(string countpath)
    {
        if (!File.Exists(countpath))
        {
            Debug.LogError($"Count file not found at {countpath}");
            return 0;
        }

        using (FileStream countStream = new FileStream(countpath, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return (int)formatter.Deserialize(countStream);
        }
    }

    [ContextMenu("ClearBlocks")]
    public void RemoveAllBlocks()
    {
        foreach (var processor in GameObject.FindGameObjectsWithTag("Processor"))
            Destroy(processor);
        foreach (var source in GameObject.FindGameObjectsWithTag("Source"))
            Destroy(source);
        foreach (var destination in GameObject.FindGameObjectsWithTag("Destination"))
            Destroy(destination);
        foreach (var assembler in GameObject.FindGameObjectsWithTag("Assembler"))
            Destroy(assembler);
        foreach (var disassembler in GameObject.FindGameObjectsWithTag("Disassembler"))
            Destroy(disassembler);
        foreach (var multiprocessor in GameObject.FindGameObjectsWithTag("Multiprocessor"))
            Destroy(multiprocessor);
    }
}