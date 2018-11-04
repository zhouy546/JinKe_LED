using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class CreateGrid : MonoBehaviour {
    public int width = 10;
    public int depth = 10;
    public GameObject cubePrefab;
    public Transform parent;
    // Use this for initialization
    void Start() {
        initialization();
    }

    public void initialization(){
        CreateObjectGrid(cubePrefab);
    }


	
	// Update is called once per frame
	void Update () {
		
	}


    public void CreateObjectGrid(GameObject gameObject) {
        var EntityManager = World.Active.GetOrCreateManager<EntityManager>();
        NativeArray<Entity> cubeArray = new NativeArray<Entity>(width * depth, Allocator.Temp);

        int i = 0;
       EntityManager.Instantiate(cubePrefab, cubeArray);

        for (int x  = -width/2; x < width/2; x++)
        {
            for (int z = -width/2; z < depth/2; z++)
            {
                EntityManager.AddComponentData(cubeArray[i], new ICD_MoveHeight { dis = Random.Range(0.1f, 0.5f) });
                EntityManager.AddComponentData(cubeArray[i], new ICD_MoveSpeed { MoveSpeed = Random.Range(0.01F, 0.09F) });
                float3 startPos = new float3(x, 0, z);
                EntityManager.SetComponentData(cubeArray[i], new Position { Value = startPos });



              //  Debug.Log(i);
                i++;
            }
        }

        cubeArray.Dispose();

      
    }
}
