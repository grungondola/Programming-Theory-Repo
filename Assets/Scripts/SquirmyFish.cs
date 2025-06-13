using UnityEngine;

public class SquirmyFish : Fish
{
    [SerializeField]
    private float rotationRateRange = 45;
    private float rotationRate = 0;
    private float rotationUpdateDelay = 1;

    protected override void Start()
    {
        base.Start();
        InvokeRepeating(nameof(UpdateRotation), rotationUpdateDelay, rotationUpdateDelay);
    }

    protected override void FixedUpdate()
    {
        if (!mainManager.IsGameOver)
        {
            transform.Rotate(Vector3.up, rotationRate * Time.deltaTime);
        }
        base.FixedUpdate();
    }

    void UpdateRotation()
    {
        rotationRate = Random.Range(-rotationRateRange, rotationRateRange);
    }
}
