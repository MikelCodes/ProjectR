using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpObject : MonoBehaviour
{
    [SerializeField]
    private playerBehaviour pb;

    private void OnTriggerEnter(Collider other)
    {
        pb.makeJump(true);
    }
    private void OnTriggerExit(Collider other)
    {
        pb.makeJump(false);
    }
}
