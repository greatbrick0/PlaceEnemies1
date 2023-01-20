using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    float speed = 10.0f;
    [SerializeField]
    float lifetime = 20.0f;
    float age = 0.0f;

    protected virtual void Update()
    {
        age += 1.0f * Time.deltaTime;
    }


}
