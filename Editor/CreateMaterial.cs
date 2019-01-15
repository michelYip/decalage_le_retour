using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;
using System.IO;

public class CreateMaterial : Editor
{
    [MenuItem("Créateur de Monde/Charger les textures")]
    static void CreateMaterials ()
    {
        try
        {
            AssetDatabase.StartAssetEditing();
            var textures = Selection.GetFiltered(typeof(Texture), SelectionMode.Assets).Cast<Texture>();
            foreach(var tex in textures)
            {
                string path = AssetDatabase.GetAssetPath(tex);
                string name = Path.GetFileName(path);
                path = path.Substring(0,path.LastIndexOf("."))+".mat";
                string savePath = path.Substring(0,path.LastIndexOf("Textures/"))+"Materials/"+name.Substring(0, name.LastIndexOf("."))+".mat";
                if (AssetDatabase.LoadAssetAtPath(savePath,typeof(Material)) != null)
                {
                    Debug.LogWarning("Can't create material, it already exists: " + path);
                    continue;
                }
                var mat = new Material(Shader.Find("Transparent/Diffuse"));
                mat.mainTexture = tex;
                AssetDatabase.CreateAsset(mat,savePath);
                Debug.Log("material created! "+ savePath);
            }
        }
        finally
        {
            AssetDatabase.StopAssetEditing();
            AssetDatabase.SaveAssets();
        }
    }
}
