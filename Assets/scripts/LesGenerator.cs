using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesGenerator : MonoBehaviour
{

    public KeyCode triggerButton = KeyCode.Space;
    public GameObject[] strom;


    // Start is called before the first frame update
    void Start()
    {
        GenerateForest();
    }

    void GenerateForest(){
        for (int i = 0; i < 100; i++)
        {
            GenetareTree();
        }
    }

    void GenetareTree(){

        GameObject treeSpawn = Instantiate(strom[Random.Range(0, strom.Length)]);

        float x = Random.Range(-30, 30);
        float y = 0f;
        float z = Random.Range(-30, 30);
        float rotation = Random.Range(0, 360);

        treeSpawn.transform.position = new Vector3(x, y, z);
        treeSpawn.transform.eulerAngles = new Vector3(0, rotation, 0);

        float sx = Random.Range(0.9f, 1.1f);
        float sy = Random.Range(0.9f, 1.1f);
        float sz = Random.Range(0.9f, 1.1f);

        treeSpawn.transform.localScale = new Vector3(sx, sy , sz);

    }
}
