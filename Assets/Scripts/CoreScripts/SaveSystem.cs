using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveSystem : MonoBehaviour
{

    // Also this now needs expanded out to the other 6 block types, which will need different requirements.
    // Then make an ingame GUI for Saving, Clearing and Loading, where Clearing is also part of the Loading process

    [SerializeField] Processor processorPrefab;
    public static List<Processor> processors = new List<Processor>();
    const string processor_sub = "/processor";
    const string processorCount_sub = "/processor.count";

    [SerializeField] Source sourcePrefab;
    public static List<Source> sources = new List<Source>();
    const string source_sub = "/source";
    const string sourceCount_sub = "/source.count";

    [SerializeField] Destination destinationPrefab;
    public static List<Destination> destinations = new List<Destination>();
    const string destination_sub = "/destination";
    const string destinationCount_sub = "/destination.count";

    [SerializeField] Assembler assemblerPrefab;
    public static List<Assembler> assemblers = new List<Assembler>();
    const string assembler_sub = "/assembler";
    const string assemblerCount_sub = "/assembler.count";

    [SerializeField] Disassembler disassemblerPrefab;
    public static List<Disassembler> disassemblers = new List<Disassembler>();
    const string disassembler_sub = "/disassembler";
    const string disassemblerCount_sub = "/disassembler.count";

    [SerializeField] Multiprocessor multiprocessorPrefab;
    public static List<Multiprocessor> multiprocessors = new List<Multiprocessor>();
    const string multiprocessor_sub = "/multiprocessor";
    const string multiprocessorCount_sub = "/multiprocessor.count";

    void Awake()
    {

    }


    [ContextMenu("SaveAllBlocks")]
    public void SaveAllBlocks()
    {
        SaveProcessors();
        SaveSources();
        SaveDestinations();
        SaveAssemblers();
        SaveDisassemblers();
        SaveMultiprocessors();
    }

    [ContextMenu("SaveProcessors")]
    void SaveProcessors()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + processor_sub + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + processorCount_sub + SceneManager.GetActiveScene().buildIndex;
        FileStream countStream = new FileStream(countpath, FileMode.Create);
        formatter.Serialize(countStream, processors.Count);
        countStream.Close();
        //Debug.Log(path);
        //Debug.Log(countpath);

        for (int i = 0; i < processors.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            ProcessorData data = new ProcessorData(processors[i]);
            Debug.Log(data);
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    [ContextMenu("SaveSources")]
    void SaveSources()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + source_sub + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + sourceCount_sub + SceneManager.GetActiveScene().buildIndex;
        FileStream countStream = new FileStream(countpath, FileMode.Create);
        formatter.Serialize(countStream, sources.Count);
        countStream.Close();
        //Debug.Log(path);
        //Debug.Log(countpath);

        for (int i = 0; i < sources.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            SourceData data = new SourceData(sources[i]);
            Debug.Log(data);
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    [ContextMenu("SaveDestinations")]
    void SaveDestinations()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + destination_sub + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + destinationCount_sub + SceneManager.GetActiveScene().buildIndex;
        FileStream countStream = new FileStream(countpath, FileMode.Create);
        formatter.Serialize(countStream, destinations.Count);
        countStream.Close();
        //Debug.Log(path);
        //Debug.Log(countpath);

        for (int i = 0; i < destinations.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            DestinationData data = new DestinationData(destinations[i]);
            Debug.Log(data);
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    [ContextMenu("SaveAssemblers")]
    void SaveAssemblers()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + assembler_sub + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + assemblerCount_sub + SceneManager.GetActiveScene().buildIndex;
        FileStream countStream = new FileStream(countpath, FileMode.Create);
        formatter.Serialize(countStream, assemblers.Count);
        countStream.Close();
        //Debug.Log(path);
        //Debug.Log(countpath);

        for (int i = 0; i < assemblers.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            AssemblerData data = new AssemblerData(assemblers[i]);
            Debug.Log(data);
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    [ContextMenu("SaveDisassemblers")]
    void SaveDisassemblers()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + disassembler_sub + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + disassemblerCount_sub + SceneManager.GetActiveScene().buildIndex;
        FileStream countStream = new FileStream(countpath, FileMode.Create);
        formatter.Serialize(countStream, disassemblers.Count);
        countStream.Close();
        //Debug.Log(path);
        //Debug.Log(countpath);

        for (int i = 0; i < disassemblers.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            DisassemblerData data = new DisassemblerData(disassemblers[i]);
            Debug.Log(data);
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    [ContextMenu("SaveMultiprocessors")]
    void SaveMultiprocessors()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + multiprocessor_sub + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + multiprocessorCount_sub + SceneManager.GetActiveScene().buildIndex;
        FileStream countStream = new FileStream(countpath, FileMode.Create);
        formatter.Serialize(countStream, disassemblers.Count);
        countStream.Close();
        //Debug.Log(path);
        //Debug.Log(countpath);

        for (int i = 0; i < multiprocessors.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            MultiprocessorData data = new MultiprocessorData(multiprocessors[i]);
            Debug.Log(data);
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }



    [ContextMenu("ClearBlocks")]
    public void RemoveAllBlocks()
    {

        string[] allTags = { "Processor", "Source", "Destination", "Assembler", "Disassembler", "Multiprocessor" };

        foreach (string Tag in allTags)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag(Tag))
            {
                Destroy(obj);
            }
        }
    }

    [ContextMenu("LoadAllBlocks")]
    public void LoadAllBlocks()
    {
        LoadProcessors();
        LoadSources();
        LoadDestinations();
        LoadAssemblers();
        LoadDisassemblers();
        LoadMultiprocessors();
    }

    [ContextMenu("LoadProcessors")]
    void LoadProcessors()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + processor_sub + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + processorCount_sub + SceneManager.GetActiveScene().buildIndex;
        int processorCount = 0;

        if (File.Exists(countpath))
        {
            FileStream countStream = new FileStream(countpath, FileMode.Open);
            processorCount = (int)formatter.Deserialize(countStream);
            Debug.Log("load count: " + processorCount);
            countStream.Close();
        }
        else
        {
            Debug.LogError("Processor countPath not found " + countpath);
        }


        for (int i = 0; i < processorCount; i++)
        {
            if (File.Exists(path + i))
            {
                Debug.Log("Loading processor " + i + 1);
                FileStream stream = new FileStream(path + i, FileMode.Open);
                ProcessorData data = formatter.Deserialize(stream) as ProcessorData;
                stream.Close();

                Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
                string objectname = data.name;

                Processor processor = Instantiate(processorPrefab, position, Quaternion.identity);

                processor.name = objectname;
                processor.title = data.title;
                processor.output = data.output;
                processor.input_required = data.input_required;
                if (data.inputSourceName != null)
                {
                    GameObject InBlock = GameObject.Find(data.inputSourceName);
                    processor.inputSource = InBlock;
                }
            }
            else
            {
                Debug.LogError("Processor Path not found in " + path + i);
            }
        }
    }

    [ContextMenu("LoadSources")]
    void LoadSources()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + source_sub + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + sourceCount_sub + SceneManager.GetActiveScene().buildIndex;
        int sourceCount = 0;

        if (File.Exists(countpath))
        {
            FileStream countStream = new FileStream(countpath, FileMode.Open);
            sourceCount = (int)formatter.Deserialize(countStream);
            Debug.Log("load count: " + sourceCount);
            countStream.Close();
        }
        else
        {
            Debug.LogError("Source countPath not found " + countpath);
        }


        for (int i = 0; i < sourceCount; i++)
        {
            if (File.Exists(path + i))
            {
                Debug.Log("Loading source " + i + 1);
                FileStream stream = new FileStream(path + i, FileMode.Open);
                SourceData data = formatter.Deserialize(stream) as SourceData;
                stream.Close();

                Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
                string objectname = data.name;

                Source source = Instantiate(sourcePrefab, position, Quaternion.identity);

                source.name = objectname;
                source.title = data.title;
                source.output = data.output;
            }
            else
            {
                Debug.LogError("Source Path not found in " + path + i);
            }
        }
    }

    [ContextMenu("LoadDestinations")]
    void LoadDestinations()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + destination_sub + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + destinationCount_sub + SceneManager.GetActiveScene().buildIndex;
        int destinationCount = 0;

        if (File.Exists(countpath))
        {
            FileStream countStream = new FileStream(countpath, FileMode.Open);
            destinationCount = (int)formatter.Deserialize(countStream);
            Debug.Log("load count: " + destinationCount);
            countStream.Close();
        }
        else
        {
            Debug.LogError("Destination countPath not found " + countpath);
        }


        for (int i = 0; i < destinationCount; i++)
        {
            if (File.Exists(path + i))
            {
                Debug.Log("Loading destination " + i + 1);
                FileStream stream = new FileStream(path + i, FileMode.Open);
                DestinationData data = formatter.Deserialize(stream) as DestinationData;
                stream.Close();

                Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
                string objectname = data.name;

                Destination destination = Instantiate(destinationPrefab, position, Quaternion.identity);

                destination.name = objectname;
                destination.title = data.title;
                destination.input_required = data.input_required;
                if (data.inputSourceName != null)
                {
                    GameObject InBlock = GameObject.Find(data.inputSourceName);
                    destination.inputSource = InBlock;
                }
            }
            else
            {
                Debug.LogError("Destination Path not found in " + path + i);
            }
        }
    }

    [ContextMenu("LoadAssemblers")]
    void LoadAssemblers()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + assembler_sub + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + assemblerCount_sub + SceneManager.GetActiveScene().buildIndex;
        int assemblerCount = 0;

        if (File.Exists(countpath))
        {
            FileStream countStream = new FileStream(countpath, FileMode.Open);
            assemblerCount = (int)formatter.Deserialize(countStream);
            Debug.Log("load count: " + assemblerCount);
            countStream.Close();
        }
        else
        {
            Debug.LogError("Assembler countPath not found " + countpath);
        }


        for (int i = 0; i < assemblerCount; i++)
        {
            if (File.Exists(path + i))
            {
                Debug.Log("Loading assembler " + i + 1);
                FileStream stream = new FileStream(path + i, FileMode.Open);
                AssemblerData data = formatter.Deserialize(stream) as AssemblerData;
                stream.Close();

                Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
                string objectname = data.name;

                Assembler assembler = Instantiate(assemblerPrefab, position, Quaternion.identity);

                assembler.name = objectname;
                assembler.title = data.title;
                assembler.output = data.output;
                assembler.input_required1 = data.input_required1;
                assembler.input_required2 = data.input_required2;
                if (data.inputSourceName1 != null)
                {
                    GameObject InBlock = GameObject.Find(data.inputSourceName1);
                    assembler.inputSource1 = InBlock;
                }
                if (data.inputSourceName2 != null)
                {
                    GameObject InBlock = GameObject.Find(data.inputSourceName2);
                    assembler.inputSource2 = InBlock;
                }
            }
            else
            {
                Debug.LogError("Assembler Path not found in " + path + i);
            }
        }
    }

    [ContextMenu("LoadDisassemblers")]
    void LoadDisassemblers()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + disassembler_sub + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + disassemblerCount_sub + SceneManager.GetActiveScene().buildIndex;
        int disassemblerCount = 0;

        if (File.Exists(countpath))
        {
            FileStream countStream = new FileStream(countpath, FileMode.Open);
            disassemblerCount = (int)formatter.Deserialize(countStream);
            Debug.Log("load count: " + disassemblerCount);
            countStream.Close();
        }
        else
        {
            Debug.LogError("Disassembler countPath not found " + countpath);
        }


        for (int i = 0; i < disassemblerCount; i++)
        {
            if (File.Exists(path + i))
            {
                Debug.Log("Loading disassembler " + i + 1);
                FileStream stream = new FileStream(path + i, FileMode.Open);
                DisassemblerData data = formatter.Deserialize(stream) as DisassemblerData;
                stream.Close();

                Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
                string objectname = data.name;

                Disassembler disassembler = Instantiate(disassemblerPrefab, position, Quaternion.identity);

                disassembler.name = objectname;
                disassembler.title = data.title;
                disassembler.output1 = data.output1;
                disassembler.output2 = data.output2;
                disassembler.input_required = data.input_required;
                if (data.inputSourceName != null)
                {
                    GameObject InBlock = GameObject.Find(data.inputSourceName);
                    disassembler.inputSource = InBlock;
                }
                if (data.outputBlockName1 != null)
                {
                    GameObject OutBlock = GameObject.Find(data.outputBlockName1);
                    disassembler.outputBlock1 = OutBlock;
                }
                if (data.outputBlockName2 != null)
                {
                    GameObject OutBlock = GameObject.Find(data.outputBlockName2);
                    disassembler.outputBlock2 = OutBlock;
                }

            }
            else
            {
                Debug.LogError("Disassembler Path not found in " + path + i);
            }
        }
    }

    [ContextMenu("LoadMultiprocessors")]
    void LoadMultiprocessors()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + multiprocessor_sub + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + multiprocessorCount_sub + SceneManager.GetActiveScene().buildIndex;
        int multiprocessorCount = 0;

        if (File.Exists(countpath))
        {
            FileStream countStream = new FileStream(countpath, FileMode.Open);
            multiprocessorCount = (int)formatter.Deserialize(countStream);
            Debug.Log("load count: " + multiprocessorCount);
            countStream.Close();
        }
        else
        {
            Debug.LogError("Multiprocessor countPath not found " + countpath);
        }


        for (int i = 0; i < multiprocessorCount; i++)
        {
            if (File.Exists(path + i))
            {
                Debug.Log("Loading disassembler " + i + 1);
                FileStream stream = new FileStream(path + i, FileMode.Open);
                MultiprocessorData data = formatter.Deserialize(stream) as MultiprocessorData;
                stream.Close();

                Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
                string objectname = data.name;

                Multiprocessor multiprocessor = Instantiate(multiprocessorPrefab, position, Quaternion.identity);

                multiprocessor.name = objectname;
                multiprocessor.title = data.title;
                multiprocessor.output1 = data.output1;
                multiprocessor.output2 = data.output2;
                multiprocessor.input_required1 = data.input_required1;
                multiprocessor.input_required2 = data.input_required2;
                if (data.inputSourceName1 != null)
                {
                    GameObject InBlock = GameObject.Find(data.inputSourceName1);
                    multiprocessor.inputSource1 = InBlock;
                }
                if (data.inputSourceName2 != null)
                {
                    GameObject InBlock = GameObject.Find(data.inputSourceName2);
                    multiprocessor.inputSource2 = InBlock;
                }
                if (data.outputBlockName1 != null)
                {
                    GameObject OutBlock = GameObject.Find(data.outputBlockName1);
                    multiprocessor.outputBlock1 = OutBlock;
                }
                if (data.outputBlockName2 != null)
                {
                    GameObject OutBlock = GameObject.Find(data.outputBlockName2);
                    multiprocessor.outputBlock2 = OutBlock;
                }

            }
            else
            {
                Debug.LogError("Multiprocessor Path not found in " + path + i);
            }
        }
    }

}
