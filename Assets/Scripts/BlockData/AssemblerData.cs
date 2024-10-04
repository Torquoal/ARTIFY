using UnityEngine;

[System.Serializable]

public class AssemblerData
{
	public string name;
	public string title;
	public string inputSourceName1;
	public string input_required1;
	public string inputSourceName2;
	public string input_required2;
	public string output;
	public float[] position = new float[3];
	public float[] scale = new float[3]; 

	public AssemblerData(Assembler assembler)
	{
		name = assembler.name;
		title = assembler.title;
		input_required1 = assembler.input_required1;
		input_required2 = assembler.input_required2;
		output = assembler.output;

		if (assembler.inputSource1 != null)
		{
			inputSourceName1 = assembler.inputSource1.name;
		}
		if (assembler.inputSource2 != null)
		{
			inputSourceName2 = assembler.inputSource2.name;
		}


		Vector3 assemblerPos = assembler.transform.position;
		position[0] = assemblerPos.x;
		position[1] = assemblerPos.y;
		position[2] = assemblerPos.z;

		Vector3 blockScale = assembler.transform.localScale;
		scale[0] = blockScale.x;
		scale[1] = blockScale.y;
		scale[2] = blockScale.z;

	}
}