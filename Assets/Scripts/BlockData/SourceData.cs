using UnityEngine;

[System.Serializable]

public class SourceData
{
	public string name;
	public string title;
	public string output;
	public float[] position = new float[3];
	public float[] scale = new float[3];

	public SourceData(Source source)
	{
		name = source.name;
		title = source.title;
		output = source.output;
		Vector3 sourcePos = source.transform.position;
		position[0] = sourcePos.x;
		position[1] = sourcePos.y;
		position[2] = sourcePos.z;

		Vector3 blockScale = source.transform.localScale;
		scale[0] = blockScale.x;
		scale[1] = blockScale.y;
		scale[2] = blockScale.z;

	}
}