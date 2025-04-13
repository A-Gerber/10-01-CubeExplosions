using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    private float _baseExplosionForce = 3000;
    private float _baseExplosionRadius = 10;
    private float _explosionMultiplier;

    public float ExplosionForce => _baseExplosionForce * _explosionMultiplier;
    public float ExplosionRadius => _baseExplosionRadius * _explosionMultiplier;

    public void Explode(Cube explosionCube)
    {
        _explosionMultiplier = 1 / explosionCube.transform.localScale.magnitude;

        foreach (Cube target in DefineTargetCubes(explosionCube))
        {
            Rigidbody explodableObject = target.Rigidbody;

            explodableObject.AddExplosionForce(ExplosionForce, explosionCube.transform.position, ExplosionRadius);
        }
    }

    private List<Cube> DefineTargetCubes(Cube target)
    {
        Collider[] colliders = Physics.OverlapSphere(target.transform.position, ExplosionRadius);
        List<Cube> targets = new();

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Cube>(out Cube cube))
            {
                targets.Add(cube);
            }
        }

        return targets;
    }
}