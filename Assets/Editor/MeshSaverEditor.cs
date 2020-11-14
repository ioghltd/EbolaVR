using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MeshSaverEditor {

    [MenuItem("CONTEXT/SkinnedMeshRenderer/Save Mesh...")]
    public static void SaveSkinnedMeshInPlace(MenuCommand menuCommand)
    {
        SkinnedMeshRenderer smr = menuCommand.context as SkinnedMeshRenderer;
        Mesh msh = new Mesh();
        smr.BakeMesh(msh);

        AssetDatabase.CreateAsset(msh, "Assets/CreatedMesh/" + System.DateTime.Now.ToString("MMddhhmmss") + ".asset");
    }

    [MenuItem("CONTEXT/MeshFilter/Save Mesh...")]
	public static void SaveMeshInPlace (MenuCommand menuCommand) {
		MeshFilter mf = menuCommand.context as MeshFilter;
		Mesh m = mf.sharedMesh;
        AssetDatabase.CreateAsset(m, "Assets/CreatedMesh/" + System.DateTime.Now.ToString("MMddhhmmss") + ".asset");
        //SaveMesh(m, m.name, false, true);
	}

	[MenuItem("CONTEXT/MeshFilter/Save Mesh As New Instance...")]
	public static void SaveMeshNewInstanceItem (MenuCommand menuCommand) {
		MeshFilter mf = menuCommand.context as MeshFilter;
		Mesh m = mf.sharedMesh;
		SaveMesh(m, m.name, true, true);
	}

	public static void SaveMesh (Mesh mesh, string name, bool makeNewInstance, bool optimizeMesh) {
		string path = EditorUtility.SaveFilePanel("Save Separate Mesh Asset", "Assets/", name, "asset");
		if (string.IsNullOrEmpty(path)) return;
        
		path = FileUtil.GetProjectRelativePath(path);

		Mesh meshToSave = (makeNewInstance) ? Object.Instantiate(mesh) as Mesh : mesh;
		
		if (optimizeMesh)
		     MeshUtility.Optimize(meshToSave);
        
		AssetDatabase.CreateAsset(meshToSave, path);
		AssetDatabase.SaveAssets();
	}
	
}
