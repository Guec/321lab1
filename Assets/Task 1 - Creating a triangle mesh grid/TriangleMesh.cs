using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class generates a mesh of a defined size and resolution using triangles
 *
 * You are required to fill in the code to generate the vertices at the right
 * location, as well as calculate the appropriate triangle indicies for the mesh
 *
 * PROD321 - Interactive Computer Graphics and Animation 
 * Copyright 2023, University of Canterbury
 * Written by Adrian Clark
 */

public class TriangleMesh : MonoBehaviour
{
    // Defines the resolution of the mesh (number of vertices in width and length)
    public int meshWidth = 100;
    public int meshLength = 100;

    // Start is called before the first frame update
    void Start()
    {
        // Create a list to store our vertices
        List<Vector3> vertices = new List<Vector3>();

        // Create a list to store our triangles
        List<int> triangles = new List<int>();

        // Generate our Vertices
        // Loop through the meshes length and width
        for (int z=0; z< meshLength; z++)
        {
            for (int x=0; x< meshWidth; x++)
            {
                float y = 0;
                vertices.Add(new Vector3(x, y, z));
            }
        }

        // Generate our triangle Indicies
        // Loop through the meshes length-1 and width-1
        for (int z=0; z< meshLength - 1; z++)
        {
            for (int x=0; x<meshWidth-1; x++)
            {
                //TODO: Change the variable initialisation code below to calculate 
                //the Top Left, Top Right, Bottom Right and Bottom Left vertex indices 
                //for the current triangles
                int vTL = ((z + 1) * meshWidth) + (x);
                int vBL = ((z + 1) * meshWidth) + (x + 1);
                int vTR = (z * meshWidth) + (x);
                int vBR = (z * meshWidth) + (x + 1);

                // Create the two triangles which make each element in the quad
                // Triangle Top Left->Bottom Left->Bottom Right
                triangles.Add(vTL);
                triangles.Add(vBL);
                triangles.Add(vBR);

                // Triangle Top Left->Bottom Right->Top Right
                triangles.Add(vTL);
                triangles.Add(vBR);
                triangles.Add(vTR);
            }
        }

        // Create our mesh object
        Mesh mesh = new Mesh();

        // Change the mesh index format to use 32 bit unsigned ints, this increases
        // The number of vertices we can have in one mesh from ~65,000 to ~4,000,000,000
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        // Assign the vertices and triangle indicies
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        // Use recalculate normals to calculate the vertex normals for our mesh
        mesh.RecalculateNormals();

        // Create a new mesh filter, and assign the mesh from before to it
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        // Create a new renderer for our mesh, and use the standard shader
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));
    }

}
