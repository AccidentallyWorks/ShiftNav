using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LightTestMove : MonoBehaviour
{
    //Nav Components
    public Transform dest; //current destination
    public Transform startPt; //"respawn" point
    public Transform firstDest; //the first wp to trigger chain
    public List<Transform> lastDest; //previous dest
    public List<Transform> wpList; //list of wp's from current chkpt


    NavMeshAgent nav;
    int layerMaskLights = 1 << 11;
    int layerMaskAI = 1 << 12;

    //AI Stop/Start
    public bool stop = false;
    public Collider[] lightColliders; //Access lights
    RaycastHit hit; //car ref
    bool arrived = false;

    // Start is called before the first frame update
    void Start()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
        nav.speed = Random.Range(6.5f, 8.6f);
        nav.destination = dest.position;
    }

    // Update is called once per frame
    void Update()
    {
        nav.isStopped = stop;

        if (!stop)
        {
            arrived = true;
            if (nav.remainingDistance <= 1.75 && arrived) //When in range
            {
                if (!dest.GetComponent<WPLead>().end)
                {
                    lastDest.Add(dest); //store ref to last dest
                    dest = newDestination();
                }
                else
                {
                    resetCar();
                    //OnBecameInvisible();
                }
                nav.destination = dest.position;
                arrived = false;//starts new movement
            }
        }
        
        if(lastDest.Count >= 4)
        {
            lastDest.Clear();
        }

        lightColliders = Physics.OverlapSphere(gameObject.transform.position, 35.0f, layerMaskLights);

        if (lightColliders.Length != 0)
        {
            stop = lightColliders[0].gameObject.GetComponent<lightCycle>().red;
            nav.isStopped = stop;
        }
        else if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y + 1.5f,transform.position.z), transform.forward, out hit, 30.0f, layerMaskAI)) //check for car in front, read stop state
        {
            stop = hit.collider.gameObject.GetComponent<LightTestMove>().stop;
            nav.isStopped = stop;
        }
    }

    void resetCar()
    {
        nav.Warp(startPt.position); //reset position
        lastDest.Clear();
        dest = firstDest;
    }

    Transform newDestination()
    {
        /*AI Prototype*/
        //Transform newDest = null;
        //writeWPs();
        //foreach (Transform t in wpList)//get a linked node from the imported wp connections
        //{
        //    foreach (Transform i in lastDest)//index through all previous wp's
        //    {
        //        if (t != i)//check if current import equals any previous destination
        //        {
        //            newDest = t;
        //            break;
        //        }
        //    }
        //    if (newDest != null)
        //    {
        //        break;
        //    }
        //}
        //return
        //    newDest;

        Transform newDest = dest.GetComponent<WPLead>().rollWP();
        foreach (Transform t in lastDest)//get a linked node from the imported wp connections
        {
            if (newDest == t)
            {
                newDest = dest.GetComponent<WPLead>().rollWP();
            }
        }
        return
            newDest;

    }

    void writeWPs()
    {
        wpList.Clear();
        foreach (Transform t in dest.GetComponent<WPLead>().linkedWPS)
        {
            wpList.Add(t);
        }
    }

    private void OnBecameInvisible()
    {
        resetCar();
    }




}
