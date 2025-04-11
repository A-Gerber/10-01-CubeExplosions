using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private float _explosionRadius = 30;
    private float _explosionForce = 2000;

    public void Explode(Cube target)
    {
        List<Cube> targets = DefineTargetCubes(target);

        foreach (Cube targetCube in targets)
        {
            Rigidbody explodableObject = targetCube.Rigidbody;

            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    private List<Cube> DefineTargetCubes(Cube target)
    {
        List<Cube> targets = new();
        List<Cube> cubes = FindCubesInRadius(target);

        foreach (var cube in cubes)
        {
            if (cube.IdGroup == target.IdGroup)
            {
                targets.Add(cube);
            }
        }

        return targets;
    }

    private List<Cube> FindCubesInRadius(Cube target)
    {
        Collider[] colliders = Physics.OverlapSphere(target.transform.position, _explosionRadius);
        List<Cube> cubes = new();

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Cube>(out Cube cube))
            {
                cubes.Add(cube);
            }
        }

        return cubes;
    }
}