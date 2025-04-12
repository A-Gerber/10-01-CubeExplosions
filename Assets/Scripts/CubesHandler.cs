using System.Collections.Generic;
using UnityEngine;

public class CubesHandler : MonoBehaviour
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

            _cubesSpawner.SpawnCubes(cube);
        }
        else
        {
            _exploder.Explode(DefineTargetCubes(cube), cube.transform.position);
        }

        cube.Destroy();
    }

    private List<Cube> DefineTargetCubes(Cube target)
    {
        List<Cube> targets = new();

        foreach (var cube in _cubesSpawner.GiveListCubes(target.IdGroup))
        {
            if (cube != null)
            {
                targets.Add(cube);
            }
        }

        return targets;
    }
}