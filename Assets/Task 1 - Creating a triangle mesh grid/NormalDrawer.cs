using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class reads draws the normals of a 3D model
 * It supports both Per Polygon Normals, and Per Vertex Normals
 *
 * PROD321 - Interactive Computer Graphics and Animation 
 * Copyright 2021, University of Canterbury
 * Written by Adrian Clark
 */

public class NormalDrawer : MonoBehaviour
{
    // The mesh filter to draw the normals of
    public MeshFilter meshFilter;

    // The scale that the normals should be
    public float normalScale = .1f;

    // An enum defining PerPolyNormal and PerVertexNormal values
    public enum NormalVisualisationType
    {
        PerPolyNormal,
        PerVertexNormal
    };

    // The Normal Visualisation Type to use
    public NormalVisualisationType normalVisualisationType = NormalVisualisationType.PerVertexNormal;

    void Update()
    {
        // If the mesh filter isn't set, get the mesh filter attached to the
        // GameObject this script is attached to
        if (meshFilter == null) meshFilter = GetComponent<MeshFilter>();

        // Get the mesh from the mesh filter
        Mesh m = meshFilter.sharedMesh;

        // Get the model to world transform of the mesh filter GameObject
        Transform t = meshFilter.transform;

        // Get the arrays of vertices and normals from the mesh
        Vector3[] v = m.vertices;
        Vector3[] n = m.normals;


        // If we are rendering per vertex normals
        if (normalVisualisationType == NormalVisualisationType.PerVertexNormal)
        {
            // Loop through each vertex
            for (int i = 0; i < v.Length; i++)
            {
                // Transform the vertex from model to world space
                Vector3 vT = t.TransformPoint(v[i]);
                // Transform the normal from model to world space
                Vector3 nT = t.TransformVector(n[i]);

                // Draw the Normal in white
                Debug.DrawRay(vT, nT * normalScale, Color.white, 0, true);
            }

        }
        // Otherwise if we're rendering per polygon normal
        else
        {
            // Get the array of triangle indices
            int[] triIdx = m.triangles;

            // Loop through all the triangles
            for (int i = 0; i < triIdx.Length; i += 3)
            {
                // Transform the three vertices which make up the triangle
                // from model to world space
                Vector3 vT1 = t.TransformPoint(v[triIdx[i]]);
                Vector3 vT2 = t.TransformPoint(v[triIdx[i + 1]]);
                Vector3 vT3 = t.TransformPoint(v[triIdx[i + 2]]);

                // And calculate their average (which will be the centre of
                // the triangle)
                Vector3 vT = (vT1 + vT2 + vT3) / 3.0f;

                // Transform the three normals of the vertices which make up 
                // the triangle from model to world space
                Vector3 nT1 = t.TransformVector(n[triIdx[i]]);
                Vector3 nT2 = t.TransformVector(n[triIdx[i + 1]]);
                Vector3 nT3 = t.TransformVector(n[triIdx[i + 2]]);

                // And calculate their average (which will be the average normal
                // for that polygon)
                Vector3 nT = (nT1 + nT2 + nT3) / 3.0f;

                // Draw the normal in white
                Debug.DrawRay(vT, nT * normalScale, Color.white, 0, true);
            }
        }


    }

}
