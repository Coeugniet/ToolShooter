using Shooter;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{

    [SerializeField]
    private LevelProfile level;

    public void Generate() {
        GameObject chunk = new GameObject(level.name);
        int size = LevelProfile.levelSize;

        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                GameObject o = level.layer1[Mathf.Abs(i - (size - 1)) * size + j];
                if (o != null) {
                    Vector2 objectSize = o.GetComponent<SpriteRenderer>().bounds.size;
                    GameObject.Instantiate(o, chunk.transform.position + new Vector3(j * objectSize.x, i * objectSize.y, 0), chunk.transform.rotation, chunk.transform);
                }
            }
        }

        chunk.AddComponent<ChunkBehaviour>();

        string localPath = "Assets/Prefabs/Chunks/" + chunk.name + ".prefab";

        if (AssetDatabase.LoadAssetAtPath<GameObject>(localPath) != null)
            AssetDatabase.DeleteAsset(localPath);

        PrefabUtility.SaveAsPrefabAssetAndConnect(chunk, localPath, InteractionMode.UserAction);
        DestroyImmediate(chunk);
    }
}
