using System;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private const int DestroyButton = 0;

    [SerializeField] private Camera _camera;
    [SerializeField] private Ray _ray;

    public event Action<Cube> Divided;
    public event Action<Cube> Exploded;

    public void Explode(List<Rigidbody> targets, Cube cube)
    {
        foreach (Rigidbody explodableObject in targets)
        {
            explodableObject.AddExplosionForce(cube.ExplosionForce, transform.position, cube.ExplosionRadius);
        }
    }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        if (Input.GetMouseButtonDown(DestroyButton))
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

            Divided?.Invoke(cube);
        }
        else
        {
            Exploded?.Invoke(cube);
        }

        cube.Destroy();
    }
}