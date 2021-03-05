using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class lightCycle : MonoBehaviour
{
    public bool red = true;

    //timer
    const float CYCLE_TIME = 5.0f;

    private void Awake()
    {
        StartCoroutine(timer());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator timer()
    {
        while(true)
        {
            yield return new WaitForSeconds(CYCLE_TIME);
            switchLights();
        }
        
    }

    void switchLights()
    {
        red = !red;
    }
}
