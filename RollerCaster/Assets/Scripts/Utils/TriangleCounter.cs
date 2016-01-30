using UnityEngine;
using System.Collections;

public class TriangleCounter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MeshFilter filter = GetComponent<MeshFilter>();
		int[] tris = filter.mesh.triangles;
		Vector3[] vertices = filter.mesh.vertices;

		Debug.Log(name+ " has "+tris.Length/3+" triangles and "+vertices.Length+" vertices");
	}
}
