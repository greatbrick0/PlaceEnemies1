using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private Image emberImage;
    float fadeOutTime;

    private void Start()
    {
        InvokeRepeating("SpawnParticles", 0f, 0.05f);
    }

    void SpawnParticles()
    {
        Image emberInstance = Instantiate(emberImage, transform);
        emberInstance.GetComponent<Transform>().position = new Vector3(Random.Range(0, 1920), -15f, 0);
        
    }
}
