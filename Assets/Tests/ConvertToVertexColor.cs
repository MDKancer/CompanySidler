using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToVertexColor : MonoBehaviour
{
    public Material vertexColorMaterial;
    public Vector3[] vertices;
    public Color[] colors;
    
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Mesh mesh;
    
    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        mesh = meshFilter.sharedMesh;
        vertices = mesh.vertices;
        
        colors = mesh.colors;
        

        MakeCopy();
    }

    private GameObject MakeCopy()
    {
        GameObject copy = new GameObject(this.name+" (Copy)");
        MeshRenderer mr = copy.AddComponent<MeshRenderer>();
        MeshFilter mf = copy.AddComponent<MeshFilter>();
        
        mf.mesh = mesh;
        mr.material = vertexColorMaterial;
        
        var newColors = new Color[colors.Length];
        
        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            int[] triangles = mf.mesh.GetTriangles(i);

            for (int j = 0; j < triangles.Length; j++)
            {
                if(newColors.Length>0)
                {
                    newColors[triangles[j]] = colors[i];
                }
            }
        }

        mf.mesh.colors = newColors;
        
        
        return copy;
    }

}
