using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private Vector3 behindPlayer;

    private Vector3 debug;
    private bool worldCam;

    private void Start()
    {
        debug = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if (worldCam == true)
        this.transform.position = debug;
    }

    public void camPos(bool field)
    {
        if (field == false)
        {
            worldCam = false;
            this.transform.localPosition = behindPlayer;
        }
        else
        {
            worldCam = true;
        }
    }

}
