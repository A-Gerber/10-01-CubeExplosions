using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private float _probabilitySplit = 100;

    private Renderer _renderer;

    private float _dividerProbability = 2;
    private float _multiplierScale = 0.5f;

    public float ExplosionRadius { get; private set; } = 30;
    public float ExplosionForce { get; private set; } = 2000;
    public int IdGroup { get; private set; }

    public void SetIdGroup (int id)
    {
        IdGroup = id;
    }

    public void Change()
    {
        Reduce();
        ChangeColor();
    }

    public bool IsDivide()
    {
        int percent = 100;
        int minValue = 1;
        int coefficient = percent / (int)_probabilitySplit;

        int randomValue = UnityEngine.Random.Range(minValue, coefficient);

        return coefficient == randomValue;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        ChangeColor();
    }

    private void ChangeColor()
    {
        _renderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    private void Reduce()
    {
        transform.localScale *= _multiplierScale;
        _probabilitySplit /= _dividerProbability;
    }
}