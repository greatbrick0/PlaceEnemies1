using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmberParticle : MonoBehaviour
{
    [SerializeField] float speed;
    
    void OnEnable()
    {
        float launchAngle = Random.Range(-10, 10);
        transform.rotation = Quaternion.Euler(0f, 0f, launchAngle);
    }

    void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
        transform.Rotate(0f, 0f, Random.Range(-0.2f, 0.2f));
    }
}
