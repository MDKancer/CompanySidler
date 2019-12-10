using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class ConvertToPrefab : MonoBehaviour
{
    [Required("FBX Object Missing"), ReorderableList]
    public List<GameObject> FBX_GameObjects = new List<GameObject>();

    public string target_Folder = "Assets/Generated_Prefabs/";

    public List<GameObject> final_Prefab = new List<GameObject>();

    public List<GameObject> gameObjects = new List<GameObject>();
    
    [Button]
    public void Convert()
    {
        for (int i = 0; i < FBX_GameObjects.Count; i++)
        {
            final_Prefab.Add(ConvertToGameObject(FBX_GameObjects[i]));
            if (FBX_GameObjects[i].transform.childCount > 0)
            {
                    for (int j = 0; j < FBX_GameObjects[i].transform.childCount; j++)
                    {
                       var child =  ConvertToGameObject(FBX_GameObjects[i].transform.GetChild(j).gameObject);
                       child.transform.SetParent(final_Prefab[i].transform);
                    }
            }
            Apply(final_Prefab[i]);
        }
    }

    [Button]
    public void Reset()
    {
        target_Folder = "Assets/Generated_Prefabs/";
        //FBX_GameObjects.Clear();
        final_Prefab.Clear();
        gameObjects.Clear();
    }

    private GameObject ConvertToGameObject(GameObject source)
    {
        GameObject gameObject_Copy = new GameObject(source.name);
        var components = source.GetComponents(typeof(Component));
        
        foreach (var component in components)
        {
                Type type = component.GetType();
                Component copy;
            if (type == typeof(MeshFilter))
            {
                MeshFilterCopy(gameObject_Copy,source);
            }
            if (type == typeof(MeshRenderer))
            {
                MeshRenderCopy(gameObject_Copy, source);
            }
            
            if (type != typeof(Transform))
            {
                copy = gameObject_Copy.AddComponent(type);
            }
            else
            {
                copy = gameObject_Copy.GetComponent(typeof(Transform));
                
            }
                System.Reflection.FieldInfo[] fields = type.GetFields();
            
                foreach (System.Reflection.FieldInfo field in fields)
                {
                    field.SetValue(copy, field.GetValue(source));
                }
            
            
        }
        gameObjects.Add(gameObject_Copy);
        return gameObject_Copy;
    }

    private void Apply(GameObject prefab)
    {
        string localPath = target_Folder + prefab.name + ".prefab";

        // Make sure the file name is unique, in case an existing Prefab has the same name.
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        // Create the new Prefab.
        PrefabUtility.SaveAsPrefabAssetAndConnect(prefab, localPath, InteractionMode.UserAction);
    }

    private void MeshFilterCopy( GameObject copy,  GameObject source)
    {
        var meshFilter = copy.AddComponent<MeshFilter>();
        meshFilter.mesh = source.GetComponent<MeshFilter>().sharedMesh;
    }
    private void MeshRenderCopy( GameObject copy,  GameObject source)
    {
        var meshRenderer = copy.AddComponent<MeshRenderer>();
        var originalMeshRenderer = source.GetComponent<MeshRenderer>();
        meshRenderer.materials = originalMeshRenderer.sharedMaterials;
        meshRenderer.probeAnchor = originalMeshRenderer.probeAnchor;
        meshRenderer.reflectionProbeUsage = originalMeshRenderer.reflectionProbeUsage;
        meshRenderer.lightProbeUsage = originalMeshRenderer.lightProbeUsage;
        meshRenderer.shadowCastingMode = originalMeshRenderer.shadowCastingMode;
    }
}
