using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomObstcl : MonoBehaviour
{
    //object list
    public GameObject o1;
    public GameObject o2;
    public GameObject o3;

    public GameObject player;

    public GameObject spawned;

    public bool manualSpawn = false;
    public int spawnInt;

    public List<GameObject> oList;

    public float distTo;

    // Start is called before the first frame update
    void Start()
    {
        //set oList
        oList.Add(o1);
        oList.Add(o2);
        oList.Add(o3);
    }

    // Update is called once per frame
    void Update()
    {
        distTo = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if(distTo <= 100.0f && spawned == null)
        {
            Spawn();
        }
        else if(distTo > 100.0f && spawned != null)
        {
            Destroy(spawned);
        }
    }

    void Spawn()
    {
        if (manualSpawn)
        {
            spawned = Instantiate(oList[spawnInt], this.transform);
        }
        else
        {
            spawned = Instantiate(oList[Random.Range(0, 3)], this.transform);
        }
    }
}
