using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]

public class Cube : MonoBehaviour
{
    private Destroyer _destroyer;
    private CubesSpawner _cubesSpawner;
    private Renderer _renderer;

    private float _multiplierProbability = 0.5f;
    private float _multiplierScale = 0.5f;
 
    public float ProbabilityDivide { get; private set; }
    public int IdGroup { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _destroyer = FindObjectOfType<Destroyer>();
        _cubesSpawner = FindObjectOfType<CubesSpawner>();
        Rigidbody = GetComponent<Rigidbody>();

        Rigidbody.useGravity = true;

        ChangeColor();
    }

    private void OnMouseDown()
    {
        SelectAnAction();
    }

    public void SetIdGroup(int id)
    {
        IdGroup = id;
    }

    public void SetProbabilityDivide(float probability)
    {
        ProbabilityDivide = probability;
    }

    private bool IsDivide()
    {
        int maxValue = 100;
        int minValue = 1;

        int randomValue = UnityEngine.Random.Range(minValue, maxValue);

        return  randomValue <= ProbabilityDivide;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void SelectAnAction()
    {
        TryGetComponent(out Cube cube);

        if (IsDivide())
        {
            Change();

            _cubesSpawner.DivideCubes(cube);
        }
        else
        {
            _destroyer.Explode(cube);
        }

        Destroy();
    }

    private void ChangeColor()
    {
        _renderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    private void Change()
    {
        Rigidbody.useGravity = true;

        transform.localScale *= _multiplierScale;
        ProbabilityDivide *= _multiplierProbability;

        ChangeColor();
    }
}