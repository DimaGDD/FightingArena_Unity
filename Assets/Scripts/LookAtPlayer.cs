using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public new Transform camera;
    void LateUpdate()
    {
        transform.LookAt(camera);
    }
}
