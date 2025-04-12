using System.Collections.Generic;
using UnityEngine;

public class CubesSpawner : MonoBehaviour
{
    [SerializeField] private Cube _prefabCube;

    private Dictionary<int, List<Cube>> _cubes;
    private Cube _currentCube;

    private float _startProbabilityDivide = 100;
    private int _minCountCubes = 2;
    private int _maxCountCubes = 6;
    private int _idGroup = 1000;

    private void Awake()
    {
        _cubes = new();

        SpawmStartCubes();
    }

    public List<Cube> GiveListCubes(int id)
    {
        return _cubes[id];
    }

    public void SpawnCubes(Cube cube)
    {
        int value = UnityEngine.Random.Range(_minCountCubes, _maxCountCubes + 1);
        int idGroup = ++_idGroup;

        List<Cube> cubes = new();

        for (int i = 0; i < value; i++)
        {
            _currentCube = Instantiate(cube, cube.transform.position, Quaternion.identity);
            _currentCube.Init(idGroup, cube.ProbabilityDivide);

            cubes.Add(_currentCube);
        }

        _cubes.Add(idGroup, cubes);
    }

    private void SpawmStartCubes()
    {
        int value = UnityEngine.Random.Range(_minCountCubes, _maxCountCubes + 1);
        int idGroup = ++_idGroup;

        List<Cube> cubes = new();

        for (int i = 0; i < value; i++)
        {
            _currentCube = Instantiate(_prefabCube, CalculateCoordinates(), Quaternion.identity);
            _currentCube.Init(idGroup, _startProbabilityDivide);

            cubes.Add(_currentCube);
        }

        _cubes.Add(idGroup, cubes);
    }

    private Vector3 CalculateCoordinates()
    {
        int min = 1;
        int max = 19;
        int[] coordinates = new int[3];

        for (int i = 0; i < coordinates.Length; i++)
        {
            coordinates[i] = UnityEngine.Random.Range(min, max + 1);
        }

        return new Vector3(coordinates[0], coordinates[1], coordinates[2]);
    }
}