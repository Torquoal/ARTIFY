using UnityEngine;

[System.Serializable]

public class DisassemblerData
{
	public string name;
	public string title;
	public string inputSourceName;
	public string input_required;
	public string outputBlockName1;
	public string output1;
	public string outputBlockName2;
	public string output2;
	public float[] position = new float[3];
	public float[] scale = new float[3]; 

	public DisassemblerData(Disassembler disassembler)
	{
		name = disassembler.name;
		title = disassembler.title;
		input_required = disassembler.input_required;
		output1 = disassembler.output1;
		output2 = disassembler.output2;
		if (disassembler.inputSource != null)
		{
			inputSourceName = disassembler.inputSource.name;
		}
		if (disassembler.outputBlock1 != null)
		{
			outputBlockName1 = disassembler.outputBlock1.name;
		}
		if (disassembler.outputBlock2 != null)
		{
			outputBlockName2 = disassembler.outputBlock2.name;
		}


		Vector3 disassemblerPos = disassembler.transform.position;
		position[0] = disassemblerPos.x;
		position[1] = disassemblerPos.y;
		position[2] = disassemblerPos.z;

		Vector3 blockScale = disassembler.transform.localScale;
		scale[0] = blockScale.x;
		scale[1] = blockScale.y;
		scale[2] = blockScale.z;

	}
}
