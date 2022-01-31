using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    [SerializeField] private int xSize, ySize;

    private Mesh myMesh;
    private Vector3[] vertices;

    private WaitForSeconds waitTime = new WaitForSeconds(.1f);

    private void Awake()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        myMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = myMesh;
        myMesh.name = "Procedural Grid";

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];

        for (int verticeIndex = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, verticeIndex++)
            {
                vertices[verticeIndex] = new Vector3(x, y);
                yield return waitTime;
            }
        }
        myMesh.vertices = vertices;

        int[] triangles = new int[xSize * ySize * 6];
        for (int tempTriangles = 0, tempVertices = 0, y = 0; y < ySize; y++, tempVertices++)
        {
            for (int x = 0; x < xSize; x++, tempTriangles += 6, tempVertices++)
            {
                triangles[tempTriangles] = tempVertices;
                triangles[tempTriangles + 3] = triangles[tempTriangles + 2] = tempVertices + 1;
                triangles[tempTriangles + 4] = triangles[tempTriangles + 1] = tempVertices + xSize + 1;
                triangles[tempTriangles + 5] = tempVertices + xSize + 2;
                myMesh.triangles = triangles;
                yield return waitTime;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        Gizmos.color = Color.black;
        foreach(Vector3 vertice in vertices)
        {
            Gizmos.DrawSphere(vertice, .1f);
        }
    }
}
