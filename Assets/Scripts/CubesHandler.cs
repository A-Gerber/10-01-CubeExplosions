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
        if (Input.GetMouseButtonDown(ExplodeButton))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(_ray, out hit, Mathf.Infinity) && hit.transform.TryGetComponent(out Cube cube))
            {
                SelectAnAction(cube);
            }
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
            _exploder.Explode(cube);
        }

        cube.Destroy();
    }
}