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

        CreateCubes(_minCountCubes, _maxCountCubes, _prefabCube, _startProbabilityDivide);
    }

    public void DivideCubes(Cube cube)
    {
        CreateCubes(_minCountCubes, _maxCountCubes, cube, cube.ProbabilityDivide);
    }

    private void CreateCubes(int min, int max, Cube cube, float probability)
    {
        int value = UnityEngine.Random.Range(min, max);
        int idGroup = AssignId();

        List<Cube> cubes = new();

        for (int i = 0; i < value; i++)
        {
            _currentCube = Instantiate(cube, CalculateCoordinates(), Quaternion.identity);
            _currentCube.SetIdGroup(idGroup);
            _currentCube.SetProbabilityDivide (probability);

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