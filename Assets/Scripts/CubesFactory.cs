using System.Collections.Generic;
using UnityEngine;

public class CubesFactory : MonoBehaviour
{
    [SerializeField] private Cube _prefabCube;
    [SerializeField] private CubesFactory _cubesFactory;
    [SerializeField] private Destroyer _destroyer;

    private Dictionary<int, List<Cube>> _cubes;
    private Cube _currentCube;
    private int _minCountCubes = 2;
    private int _maxCountStartCubes = 4;
    private int _maxCountCubes = 6;
    private int _idGroup = 1000;

    private void Awake()
    {
        _cubes = new();

        CreateCubes(_minCountCubes, _maxCountStartCubes, _prefabCube);
    }

    private void OnEnable()
    {
        _destroyer.Divided += DivideCubes;
        _destroyer.Exploded += Define—ubes;
    }

    private void OnDisable()
    {
        _destroyer.Divided -= DivideCubes;
        _destroyer.Exploded -= Define—ubes;
    }

    private void Define—ubes(Cube tatgetCube)
    {
        List<Cube> cubes = _cubes[tatgetCube.IdGroup];
        List<Rigidbody> targets = new();

        foreach (var cube in cubes)
        {
            if (cube != null)
            {
                targets.Add(cube.GetComponent<Rigidbody>());
            }
        }

        _destroyer.Explode(targets, tatgetCube);
    }

    private void DivideCubes(Cube cube)
    {
        CreateCubes(_minCountCubes, _maxCountCubes, cube);
    }

    private void CreateCubes(int min, int max, Cube cube)
    {
        int value = UnityEngine.Random.Range (min, max);
        int idGroup = AssignId();

        List<Cube> cubes = new ();

        for (int i = 0; i < value; i++)
        {
            _currentCube = Instantiate(cube, CalculateCoordinates(), Quaternion.identity);
            _currentCube.SetIdGroup(idGroup);
            cubes.Add(_currentCube);
        }

        _cubes.Add(idGroup, cubes);
    }

    private int AssignId()
    {
        _idGroup++;

        return _idGroup;
    }

    private Vector3 CalculateCoordinates()
    {
        int min = 1;
        int max = 19;
        int[] coordinates = new int[3];

        for (int i = 0; i < coordinates.Length; i++)
        {
            coordinates[i] = UnityEngine.Random.Range(min, max);
        }

        return new Vector3(coordinates[0], coordinates[1], coordinates[2]);
    }
}