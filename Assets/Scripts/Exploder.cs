using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    private float _explosionForce = 2000;

    public float ExplosionRadius { get; private set; } = 15;

    public void Explode(List<Cube> targets, Vector3 position)
    {
        foreach (Cube targetCube in targets)
        {
            Rigidbody explodableObject = targetCube.Rigidbody;

            explodableObject.AddExplosionForce(_explosionForce, position, ExplosionRadius);
        }
    }
}