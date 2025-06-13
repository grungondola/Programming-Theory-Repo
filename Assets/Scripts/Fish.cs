using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    public float Size { get; private set; }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    public void SetSize(float size)
    {
        Size = size;
    }
}
