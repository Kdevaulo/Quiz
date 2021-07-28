using System;
using UnityEngine;

public class ParticleSpawn : MonoBehaviour
{
    public void SpawnParticles(GameObject card)
    {
        ParticleSystem stars = card.GetComponentInChildren<ParticleSystem>();
        if (stars == null)
        {
            throw new Exception("Card Prefab does not have ParticleSystem component");
        }

        stars.Play();
    }
}
