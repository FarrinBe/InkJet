using UnityEngine;

public class Oscillator1 : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] [Min(0.01f)] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;     
    }
    
    // Update is called once per frame
    void Update()
    {
        if (period == 0f) return; // stop NaN error and prevent divide by zero

        const float tau = Mathf.PI * 2; // constant value of 6.283
        float cycles = Time.time / period; // continuously growing over time
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1

        Vector3 offset = movementVector * movementFactor; // movementFactor goes from 0 to 1
        transform.position = startingPosition + offset; // position of object moves from A to B
    }
}