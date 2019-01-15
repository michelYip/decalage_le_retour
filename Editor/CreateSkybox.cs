using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;
using System.IO;

public class CreateSkybox : Editor
{
    [MenuItem("Créateur de Monde/Charger les skybox")]
    static void CreateSkyboxes ()
    {
        try
        {
            AssetDatabase.StartAssetEditing();
            var textures = Selection.GetFiltered(typeof(Texture), SelectionMode.Assets).Cast<Texture>();
            foreach(var tex in textures)
            {
                string path = AssetDatabase.GetAssetPath(tex);
                path = path.Substring(0,path.LastIndexOf("."))+".mat";
                string name = Path.GetFileName(path);
                string savePath = path.Substring(0,path.LastIndexOf("Textures/"))+"Materials/"+name.Substring(0, name.LastIndexOf("."))+".mat";
                if (AssetDatabase.LoadAssetAtPath(savePath,typeof(Material)) != null)
                {
                    Debug.LogWarning("Can't create material, it already exists: " + savePath);
                    continue;
                }
                var skybox = new Material(Shader.Find("RenderFX/Skybox"));
                skybox.SetTexture("_FrontTex", tex);
                skybox.SetTexture("_BackTex", tex);
                skybox.SetTexture("_LeftTex", tex);
                skybox.SetTexture("_RightTex", tex);
                skybox.SetTexture("_UpTex", tex);
                skybox.SetTexture("_DownTex", tex);
                AssetDatabase.CreateAsset(skybox,savePath);
                Debug.Log("skybox created!"+ savePath);
            }
        }
        finally
        {
            AssetDatabase.StopAssetEditing();
            AssetDatabase.SaveAssets();
        }
    }
}
