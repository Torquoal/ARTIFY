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

    void Awake()
    {

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
        Debug.Log(path);
        Debug.Log(countpath);

        for (int i = 0; i < processors.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            ProcessorData data = new ProcessorData(processors[i]);
            Debug.Log(data);
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    [ContextMenu("ClearBlocks")]
    void RemoveAllBlocks()
    { 

        string[] allTags = { "Processor", "Source", "Destination", "Assembler", "Disassembler", "Multiprocessor" };

        foreach (string Tag in allTags){
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag(Tag)){
                Destroy(obj);
            }
        }
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

      
        for (int i=0; i<processorCount; i++)
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

}
