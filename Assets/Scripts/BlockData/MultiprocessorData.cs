using UnityEngine;

[System.Serializable]

public class MultiprocessorData
{
	public string name;
	public string title;
	public string inputSourceName1;
	public string input_required1;
	public string inputSourceName2;
	public string input_required2;
	public string outputBlockName1;
	public string output1;
	public string outputBlockName2;
	public string output2;
	public float[] position = new float[3];

	public MultiprocessorData(Multiprocessor multiprocessor)
	{
		name = multiprocessor.name;
		title = multiprocessor.title;
		input_required1 = multiprocessor.input_required1;
		input_required2 = multiprocessor.input_required2;
		output1 = multiprocessor.output1;
		output2 = multiprocessor.output2;
		if (multiprocessor.inputSource1 != null)
		{
			inputSourceName1 = multiprocessor.inputSource1.name;
		}
		if (multiprocessor.inputSource2 != null)
		{
			inputSourceName2 = multiprocessor.inputSource2.name;
		}
		if (multiprocessor.outputBlock1 != null)
		{
			outputBlockName1 = multiprocessor.outputBlock1.name;
		}
		if (multiprocessor.outputBlock2 != null)
		{
			outputBlockName2 = multiprocessor.outputBlock2.name;
		}


		Vector3 multiprocessorPos = multiprocessor.transform.position;
		position[0] = multiprocessorPos.x;
		position[1] = multiprocessorPos.y;
		position[2] = multiprocessorPos.z;

	}
}
