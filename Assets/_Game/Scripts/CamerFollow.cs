using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour
{
    [SerializeField] Transform Target;
    float distance;
    void Start()
    {
        distance = transform.position.z - Target.position.z;
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y , Target.position.z + distance);
    }
}
