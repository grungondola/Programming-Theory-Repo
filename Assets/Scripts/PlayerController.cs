using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;
    InputAction moveAction;
    private float size = 1.0f;
    private float growthRatio = 0.1f;
    private SpawnManager spawnManager;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        var moveVector2 = moveAction.ReadValue<Vector2>();
        var moveVector3 = new Vector3(moveVector2.x, 0, moveVector2.y).normalized;
        gameObject.transform.Translate(speed * Time.deltaTime * moveVector3);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            var fish = other.gameObject.GetComponent<Fish>();
            if (fish.Size < size)
            {
                Destroy(other.gameObject);
                UpdateSize(size + fish.Size * growthRatio);
            }
            else
            {
                Debug.Log("You've been eaten");
            }
        }
    }

    private void UpdateSize(float size)
    {
        this.size = size;
        transform.localScale = new Vector3(size, 0.1f, size);
        spawnManager.UpdateSizeRange(size);
    }
}
