using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    private void Start()
    {
        CreateMesh();
    }

    public void CreateMesh()
    {
        Vector3[] vertices = CreateVertices();

        Vector3 center = GetCenter(vertices);

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] -= center;
        }

        int[] triangles = {
            0, 2, 1, 0, 3, 2, //face front			
            2, 3, 4, 2, 4, 5, //face top			
            1, 2, 5, 1, 5, 6, //face right			
            0, 7, 4, 0, 4, 3, //face left			
            5, 4, 7, 5, 7, 6, //face back			
            0, 6, 7, 0, 1, 6  //face bottom			
        };

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.Optimize();
        mesh.RecalculateNormals();
    }

    private static Vector3[] CreateVertices()
    {
        return new Vector3[] {
            new Vector3(0, 0, 0),
            new Vector3(Random.Range(0.5f, 1f), 0, 0),
            new Vector3(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), 0),
            new Vector3(0, Random.Range(0.5f, 1f), 0),
            new Vector3(0, Random.Range(0.5f, 1f), Random.Range(0.5f, 1f)),
            new Vector3(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f)),
            new Vector3(Random.Range(0.5f, 1f), 0, Random.Range(0.5f, 1f)),
            new Vector3(0, 0, Random.Range(0.5f, 1f)),
        };
    }

    private Vector3 GetCenter(Vector3[] points)
    {
        Vector3 center = Vector3.zero;
        for (int i = 0; i < points.Length; i++)
        {
            center += points[i];
        }
        center /= points.Length;
        return center;
    }
}
