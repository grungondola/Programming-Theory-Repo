using UnityEngine;

public class SquirmyFish : Fish
{
    [SerializeField]
    private float rotationRateRange = 30;
    private float rotationRate = 30;
    private float rotationUpdateDelay = 1;

    void Start()
    {
        InvokeRepeating(nameof(UpdateRotation), rotationUpdateDelay, rotationUpdateDelay);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        transform.Rotate(Vector3.up, rotationRate * Time.deltaTime);
    }

    void UpdateRotation()
    {
        rotationRate = Random.Range(-rotationRateRange, rotationRateRange);
    }
}
