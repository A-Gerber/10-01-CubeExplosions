using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    private float _baseExplosionForce = 3000;
    private float _baseExplosionRadius = 10;
    private float _explosionMultiplier;
    private Vector3 _pointExplode;

    public float ExplosionForce => _baseExplosionForce * _explosionMultiplier;
    public float ExplosionRadius => _baseExplosionRadius * _explosionMultiplier;

    public void Explode(List<Cube> targets)
    {
        foreach (Cube target in targets)
        {
            Rigidbody explodableObject = target.Rigidbody;

            explodableObject.AddExplosionForce(ExplosionForce, _pointExplode, ExplosionRadius);
        }
    }

    public void Init(Cube explosionCube)
    {
        _explosionMultiplier = 1 / explosionCube.transform.localScale.magnitude;
        _pointExplode = explosionCube.transform.position;
    }
}