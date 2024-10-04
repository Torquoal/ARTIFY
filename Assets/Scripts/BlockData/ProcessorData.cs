
using UnityEngine;

[System.Serializable]

public class ProcessorData
{
	public string name;
	public string title;
	public string inputSourceName;
	public string input_required;
	public string output;
	public float[] position = new float[3];
	public float[] scale = new float[3];

	public ProcessorData(Processor processor)
    {
		name = processor.name;
		title = processor.title;
		input_required = processor.input_required;
		output = processor.output;
		if (processor.inputSource != null) {
			inputSourceName = processor.inputSource.name;
		}
		

		Vector3 processorPos = processor.transform.position;
		position[0] = processorPos.x;
		position[1] = processorPos.y;
		position[2] = processorPos.z;

		Vector3 blockScale = processor.transform.localScale;
		scale[0] = blockScale.x;
		scale[1] = blockScale.y;
		scale[2] = blockScale.z;

	}
}
