using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    [SerializeField] private int xSize, ySize;

    private Vector3[] vertices;

    private WaitForSeconds waitTime = new WaitForSeconds(.1f);

    private void Awake()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];

        for (int verticeIndex = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, verticeIndex++)
            {
                vertices[verticeIndex] = new Vector3(x, y);
                yield return waitTime;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        foreach(Vector3 vertice in vertices)
        {
            Gizmos.DrawSphere(vertice, .1f);
        }
    }
}
