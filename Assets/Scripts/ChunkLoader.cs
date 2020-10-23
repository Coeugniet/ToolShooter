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
        GameObject spawns = new GameObject();
        spawns.transform.parent = chunk.transform;
        spawns.name = "Spawns";

        List<Transform> eSpawnList = new List<Transform>();
        List<Transform> cSpawnList = new List<Transform>();
        List<Transform> dSpawnList = new List<Transform>();

        int ennemycpt = 0;
        int coincpt = 0;
        int diamondcpt = 0;

        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                GameObject o = level.layer1[Mathf.Abs(i - (size - 1)) * size + j];
                if (o != null) {
                    Vector2 objectSize = o.GetComponent<SpriteRenderer>().bounds.size;
                    Vector3 position = chunk.transform.position + new Vector3(j * objectSize.x, i * objectSize.y, 0);
                    if (o.GetComponent<Poolable>() == null) {
                        GameObject.Instantiate(o, position, chunk.transform.rotation, chunk.transform);
                    } else {
                        if (o.name.Contains("Ennemy")) {
                            ennemycpt++;
                            GameObject ennemySpawnPoint = new GameObject();
                            ennemySpawnPoint.transform.position = position;
                            ennemySpawnPoint.transform.parent = spawns.transform;
                            ennemySpawnPoint.name = "EnnemySpawnPoint" + ennemycpt;
                            eSpawnList.Add(ennemySpawnPoint.transform);
                        }
                        if (o.name.Contains("Coin")) {
                            coincpt++;
                            GameObject coinSpawnPoint = new GameObject();
                            coinSpawnPoint.transform.position = position;
                            coinSpawnPoint.transform.parent = spawns.transform;
                            coinSpawnPoint.name = "CoinSpawnPoint" + coincpt;
                            cSpawnList.Add(coinSpawnPoint.transform);
                        }
                        if (o.name.Contains("Diamond")) {
                            diamondcpt++;
                            GameObject diamondSpawnPoint = new GameObject();
                            diamondSpawnPoint.transform.position = position;
                            diamondSpawnPoint.transform.parent = spawns.transform;
                            diamondSpawnPoint.name = "DiamondSpawnPoint" + diamondcpt;
                            dSpawnList.Add(diamondSpawnPoint.transform);
                        }
                    }
                }
            }
        }

        ChunkBehaviour cb = chunk.AddComponent<ChunkBehaviour>();
        cb.data = AssetDatabase.LoadAssetAtPath("Assets/Data/ChunkData.asset", typeof(ChunkDataProfile)) as ChunkDataProfile;
        cb.eSpawnList = eSpawnList;
        cb.cSpawnList = cSpawnList;
        cb.dSpawnList = dSpawnList;

        string localPath = "Assets/Prefabs/Chunks/" + chunk.name + ".prefab";

        if (AssetDatabase.LoadAssetAtPath<GameObject>(localPath) != null)
            AssetDatabase.DeleteAsset(localPath);

        PrefabUtility.SaveAsPrefabAssetAndConnect(chunk, localPath, InteractionMode.UserAction);
        DestroyImmediate(chunk);
    }
}
