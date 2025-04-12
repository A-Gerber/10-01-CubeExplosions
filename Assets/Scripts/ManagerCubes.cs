using System.Collections.Generic;
using UnityEngine;

public class ManagerCubes : MonoBehaviour
{
    private const int ExplodeButton = 0;

    [SerializeField] private CubesSpawner _cubesSpawner;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private Camera _camera;
    [SerializeField] private Ray _ray;

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        if (Input.GetMouseButtonDown(ExplodeButton))
        {
            SelectTargetWithRaycast();
        }
    }

    private void SelectTargetWithRaycast()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity) && hit.transform.TryGetComponent(out Cube cube))
        {
            SelectAnAction(cube);
        }
    }

    private void SelectAnAction(Cube cube)
    {
        if (cube.IsDivide())
        {
            cube.Change();

            _cubesSpawner.DivideCubes(cube);
        }
        else
        {
            _exploder.Explode(DefineTargetCubes(cube));
        }

        cube.Destroy();
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
        Collider[] colliders = Physics.OverlapSphere(target.transform.position, _exploder.ExplosionRadius);
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