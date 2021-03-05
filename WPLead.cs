using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPLead : MonoBehaviour
{
    public Transform[] linkedWPS;
    public bool end = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public List<Transform> getWPS()
    //{
    //    return
    //        linkedWPS;
    //}

    public Transform rollWP()
    {
        if (linkedWPS.Length == 2 || linkedWPS.Length == 3)
        {
            Debug.Log("rolled");
            return
                linkedWPS[Random.Range(0, linkedWPS.Length)];
        }
        else
        {
            return
                linkedWPS[0];
        }

    }

    public bool compareWP(Transform t)
    {
        bool output = false;
        foreach (Transform i in linkedWPS)
        {
            if (i == t)
            {
                output = true;
                break;
            }
            else
            {
                output = false;
            }
        }
        return
            output;
    }
}
