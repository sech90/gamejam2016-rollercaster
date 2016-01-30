using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Destroy : MonoBehaviour {
	public float x=0;
	public float y=0;
	public float z=0;

	public Spell spell = new Spell(SpellType.Balance,1);

	// Use this for initialization
	float Distance(Vector3 V1,float x,float y,float z)
	{
		return ((V1.z - z) * (V1.z - z) + (V1.x - x) * (V1.x - x)+ (V1.y-y)*(V1.y-y) );
	}
	MeshFilter HoleInTheWall(MeshFilter filter,float x,float y,float z,Spell spell)
	{
		float pow = spell.damage;
		Vector3[] vertices = filter.mesh.vertices;
		List<int> indices = new List<int>(filter.mesh.triangles);
		int count = indices.Count / 3;
	
		for (int i = count - 1; i >= 0; i--)
		{
			Vector3 V1 = transform.InverseTransformPoint(vertices[indices[i * 3 + 0]]);
			Vector3 V2 = transform.InverseTransformPoint(vertices[indices[i * 3 + 1]]);
			Vector3 V3 = transform.InverseTransformPoint(vertices[indices[i * 3 + 2]]);

			if (Distance(V1,x,y,z)< pow)
			{
				indices.RemoveRange(i * 3, 3);
			}
		}
		filter.mesh.triangles = indices.ToArray();
		return filter;
	}
	void Start () {
		
		spell.damage=1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		MeshFilter filter = GetComponent<MeshFilter>();
		HoleInTheWall (filter, x, y, z,spell);

	}
}
