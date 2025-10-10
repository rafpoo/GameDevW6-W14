using UnityEngine;
using UnityEditor;

public class ExtractAnimationClips : MonoBehaviour
{
    [MenuItem("Tools/Extract Animation Clips (Fixed)")]
    public static void Extract()
    {
        Object obj = Selection.activeObject;
        if (obj == null)
        {
            Debug.LogWarning("⚠️ Pilih file FBX di Project Window dulu!");
            return;
        }

        string path = AssetDatabase.GetAssetPath(obj);

        // Ambil semua sub-asset di FBX
        Object[] assets = AssetDatabase.LoadAllAssetsAtPath(path);

        string targetFolder = "Assets/Animations/";

        int count = 0;
        foreach (Object asset in assets)
        {
            if (asset is AnimationClip clip)
            {
                // Skip animasi '__preview__' bawaan Unity
                if (clip.name.Contains("__preview__"))
                    continue;

                // Duplikat clip jadi file .anim baru
                AnimationClip newClip = Object.Instantiate(clip);
                AssetDatabase.CreateAsset(newClip, targetFolder + clip.name + ".anim");
                Debug.Log("✅ Clip berhasil diekstrak: " + clip.name);
                count++;
            }
        }

        if (count == 0)
            Debug.LogWarning("❌ Tidak ditemukan AnimationClip dalam file FBX.");
        else
            Debug.Log($"🎉 Berhasil mengekstrak {count} clip ke folder {targetFolder}");

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
