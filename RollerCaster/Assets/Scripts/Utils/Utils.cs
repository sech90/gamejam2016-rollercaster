using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utils {


	// Use this for initialization
	private static float Distance(Vector3 V1,float x,float y,float z)
	{
		return ((V1.z - z) * (V1.z - z) + (V1.x - x) * (V1.x - x)+ (V1.y-y)*(V1.y-y) );
	}

	public static void HoleInTheWall(MeshFilter filter,float x,float y,float z,float radius)
	{
		Vector3[] vertices = filter.mesh.vertices;
		List<int> indices = new List<int>(filter.mesh.triangles);
		int count = indices.Count / 3;

		for (int i = count - 1; i >= 0; i--)
		{
			Vector3 V1 = filter.transform.TransformPoint(vertices[indices[i * 3 + 0]]);

			if (Distance(V1,x,y,z)< radius)
			{
				indices.RemoveRange(i * 3, 3);
			}
		}
		filter.mesh.triangles = indices.ToArray();
	}

}
