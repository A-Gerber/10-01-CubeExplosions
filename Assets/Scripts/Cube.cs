using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(ColorChanger))]
public class Cube : MonoBehaviour
{
    private ColorChanger _colorChanger;

    private float _multiplierProbability = 0.5f;
    private float _multiplierScale = 0.5f;
 
    public float ProbabilityDivide { get; private set; }
    public int IdGroup { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        Rigidbody = GetComponent<Rigidbody>();

        Rigidbody.useGravity = true;
    }

    private void Start()
    {
        _colorChanger.ChangeColor();
    }

    public void Init(int id, float probability)
    {
        IdGroup = id;
        ProbabilityDivide = probability;
    }

    public bool IsDivide()
    {
        int maxValue = 100;
        int minValue = 1;

        int randomValue = UnityEngine.Random.Range(minValue, maxValue + 1);

        return  randomValue <= ProbabilityDivide;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Change()
    {
        transform.localScale *= _multiplierScale;
        ProbabilityDivide *= _multiplierProbability;
    }
}