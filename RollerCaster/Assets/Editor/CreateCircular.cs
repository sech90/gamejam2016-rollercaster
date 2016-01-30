using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class CreateCircular : ScriptableWizard
{

	public enum Orientation
	{
		Horizontal,
		Vertical
	}

	public enum AnchorPoint
	{
		TopLeft,
		TopHalf,
		TopRight,
		RightHalf,
		BottomRight,
		BottomHalf,
		BottomLeft,
		LeftHalf,
		Center
	}

	public int widthSegments = 1;
	public int lengthSegments = 1;
	public float width = 1.0f;
	public float length = 1.0f;
	public Orientation orientation = Orientation.Horizontal;
	public AnchorPoint anchor = AnchorPoint.Center;
	public bool addCollider = false;
	public bool createAtOrigin = true;
	public bool twoSided = false;
	public string optionalName;

	static Camera cam;
	static Camera lastUsedCam;


	[MenuItem("GameObject/Create Other/Custom Circle...")]
	static void CreateWizard()
	{
		cam = Camera.current;
		// Hack because camera.current doesn't return editor camera if scene view doesn't have focus
		if (!cam)
			cam = lastUsedCam;
		else
			lastUsedCam = cam;
		ScriptableWizard.DisplayWizard("Create Circle",typeof(CreateCircular));
	}


	void OnWizardUpdate()
	{
		widthSegments = Mathf.Clamp(widthSegments, 1, 254);
		lengthSegments = Mathf.Clamp(lengthSegments, 1, 254);
	}
	Mesh CarveCircle(Mesh mesh)
	{
		
		Vector3[] vertices = mesh.vertices;
		List<int> indices = new List<int>(mesh.triangles);
		int count = indices.Count / 3;
		float minx=+100.0f;
		float maxx=-100.0f;

		for (int i = count - 1; i >= 0; i--)
		{
			Vector3 V1 = vertices[indices[i * 3 + 0]];
			Vector3 V2 = vertices[indices[i * 3 + 1]];
			Vector3 V3 = vertices[indices[i * 3 + 2]];
			float tmp = width*width/4;
			//Debug.Log(V1.x +" "+V1.y+" "+V1.z );
			/*if (minx > V1.x)
				minx = V1.x;
			if (minx > V2.x)
				minx = V2.x;
			if (minx > V3.x)
				minx = V3.x;
			
			if (maxx < V1.x)
				maxx = V1.x;
			if (maxx < V2.x)
				maxx = V2.x;
			if (maxx < V3.x)
				maxx = V3.x;
*/
			//if ( (V1.z*V1.z+V1.x*V1.x) > tmp )

			if ( (V1.z*V1.z+V1.x*V1.x) > tmp && (V2.z*V2.z+V2.x*V2.x) >tmp  && (V3.z*V3.z+V3.x*V3.x) > tmp)
			{
			//	Debug.Log(V1.z*V1.z+V1.x*V1.x);
		//		Debug.Log(V2.z*V2.z+V2.x*V2.x);
			//	Debug.Log(V3.z*V3.z+V3.x*V3.x);
			

				indices.RemoveRange(i * 3, 3);
			}
		}
		mesh.triangles = indices.ToArray();
		vertices = mesh.vertices;
		indices = new List<int>(mesh.triangles);
		count = indices.Count / 3;
		Debug.Log(name + " has now 1232131234 " + count + " triangles and " + vertices.Length + " vertices");
		Debug.Log(minx +" "+maxx);

		return mesh;
	}

	void OnWizardCreate()
	{
		GameObject plane = new GameObject();

		if (!string.IsNullOrEmpty(optionalName))
			plane.name = optionalName;
		else
			plane.name = "Circle";

		if (!createAtOrigin && cam)
			plane.transform.position = cam.transform.position + cam.transform.forward*5.0f;
		else
			plane.transform.position = Vector3.zero;

		Vector2 anchorOffset;
		string anchorId;
		switch (anchor)
		{
		case AnchorPoint.TopLeft:
			anchorOffset = new Vector2(-width/2.0f,length/2.0f);
			anchorId = "TL";
			break;
		case AnchorPoint.TopHalf:
			anchorOffset = new Vector2(0.0f,length/2.0f);
			anchorId = "TH";
			break;
		case AnchorPoint.TopRight:
			anchorOffset = new Vector2(width/2.0f,length/2.0f);
			anchorId = "TR";
			break;
		case AnchorPoint.RightHalf:
			anchorOffset = new Vector2(width/2.0f,0.0f);
			anchorId = "RH";
			break;
		case AnchorPoint.BottomRight:
			anchorOffset = new Vector2(width/2.0f,-length/2.0f);
			anchorId = "BR";
			break;
		case AnchorPoint.BottomHalf:
			anchorOffset = new Vector2(0.0f,-length/2.0f);
			anchorId = "BH";
			break;
		case AnchorPoint.BottomLeft:
			anchorOffset = new Vector2(-width/2.0f,-length/2.0f);
			anchorId = "BL";
			break;			
		case AnchorPoint.LeftHalf:
			anchorOffset = new Vector2(-width/2.0f,0.0f);
			anchorId = "LH";
			break;			
		case AnchorPoint.Center:
		default:
			anchorOffset = Vector2.zero;
			anchorId = "C";
			break;
		}

		MeshFilter meshFilter = (MeshFilter)plane.AddComponent(typeof(MeshFilter));
		plane.AddComponent(typeof(MeshRenderer));

		string planeAssetName = plane.name + widthSegments + "x" + lengthSegments + "W" + width + "L" + length + (orientation == Orientation.Horizontal? "H" : "V") + anchorId + ".asset";
		Mesh m = (Mesh)AssetDatabase.LoadAssetAtPath("Assets/Editor/" + planeAssetName,typeof(Mesh));

		if (m == null)
		{
			m = new Mesh();
			m.name = plane.name;

			int hCount2 = widthSegments+1;
			int vCount2 = lengthSegments+1;
			int numTriangles = widthSegments * lengthSegments * 6;
			if (twoSided) {
				numTriangles *= 2;
			}
			int numVertices = hCount2 * vCount2;

			Vector3[] vertices = new Vector3[numVertices];
			Vector2[] uvs = new Vector2[numVertices];
			int[] triangles = new int[numTriangles];
			Vector4[] tangents = new Vector4[numVertices];
			Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

			int index = 0;
			float uvFactorX = 1.0f/widthSegments;
			float uvFactorY = 1.0f/lengthSegments;
			float scaleX = width/widthSegments;
			float scaleY = length/lengthSegments;
			for (float y = 0.0f; y < vCount2; y++)
			{
				for (float x = 0.0f; x < hCount2; x++)
				{
					if (orientation == Orientation.Horizontal)
					{
						vertices[index] = new Vector3(x*scaleX - width/2f - anchorOffset.x, 0.0f, y*scaleY - length/2f - anchorOffset.y);
					}
					else
					{
						vertices[index] = new Vector3(x*scaleX - width/2f - anchorOffset.x, y*scaleY - length/2f - anchorOffset.y, 0.0f);
					}
					tangents[index] = tangent;
					uvs[index++] = new Vector2(x*uvFactorX, y*uvFactorY);
				}
			}

			index = 0;
			for (int y = 0; y < lengthSegments; y++)
			{
				for (int x = 0; x < widthSegments; x++)
				{
					triangles[index]   = (y     * hCount2) + x;
					triangles[index+1] = ((y+1) * hCount2) + x;
					triangles[index+2] = (y     * hCount2) + x + 1;

					triangles[index+3] = ((y+1) * hCount2) + x;
					triangles[index+4] = ((y+1) * hCount2) + x + 1;
					triangles[index+5] = (y     * hCount2) + x + 1;
					index += 6;
				}
				if (twoSided) {
					// Same tri vertices with order reversed, so normals point in the opposite direction
					for (int x = 0; x < widthSegments; x++)
					{
						triangles[index]   = (y     * hCount2) + x;
						triangles[index+1] = (y     * hCount2) + x + 1;
						triangles[index+2] = ((y+1) * hCount2) + x;

						triangles[index+3] = ((y+1) * hCount2) + x;
						triangles[index+4] = (y     * hCount2) + x + 1;
						triangles[index+5] = ((y+1) * hCount2) + x + 1;
						index += 6;
					}
				}
			}

			m.vertices = vertices;
			m.uv = uvs;
			m.triangles = triangles;
			m.tangents = tangents;
			m.RecalculateNormals();
			m = CarveCircle (m);
			AssetDatabase.CreateAsset(m, "Assets/Editor/" + planeAssetName);
			AssetDatabase.SaveAssets();
		}
		//m = CarveCircle (m);

		meshFilter.sharedMesh = m;

		m.RecalculateBounds();

		if (addCollider)
			plane.AddComponent(typeof(BoxCollider));

		Selection.activeObject = plane;
	}
}