using UnityEngine;

[System.Serializable]

public class DestinationData
{
	public string name;
	public string title;
	public string inputSourceName;
	public string input_required;
	public float[] position = new float[3];
	public float[] scale = new float[3]; 

	public DestinationData(Destination destination)
	{
		name = destination.name;
		title = destination.title;
		input_required = destination.input_required;
		if (destination.inputSource != null)
		{
			inputSourceName = destination.inputSource.name;
		}


		Vector3 destinationPos = destination.transform.position;
		position[0] = destinationPos.x;
		position[1] = destinationPos.y;
		position[2] = destinationPos.z;

		Vector3 blockScale = destination.transform.localScale;
		scale[0] = blockScale.x;
		scale[1] = blockScale.y;
		scale[2] = blockScale.z;

	}
}
