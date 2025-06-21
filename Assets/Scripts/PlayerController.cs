using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float baseSpeed = 10.0f;
    private float speed = 10.0f;
    InputAction moveAction;
    private float size = 1.0f;
    private float growthRatio = 0.1f;
    private SpawnManager spawnManager;
    private PlayAreaManager playAreaManager;
    private MainManager mainManager;

    [SerializeField]
    private GameObject animatedFish;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        mainManager = GameObject.Find("Main Manager").GetComponent<MainManager>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        playAreaManager = GameObject.Find("Play Area").GetComponent<PlayAreaManager>();
    }

    void FixedUpdate()
    {
        if (!mainManager.IsGameOver)
        {
            var moveVector2 = moveAction.ReadValue<Vector2>();
            var moveVector3 = new Vector3(moveVector2.x, 0, moveVector2.y).normalized;
            gameObject.transform.Translate(speed * Time.deltaTime * moveVector3);

            if (moveVector3 != Vector3.zero)
            {
                UpdateFishRotation(moveVector3);
            }
        }
    }

    void UpdateFishRotation(Vector3 moveDirection)
    {
        var rotation = Quaternion.LookRotation(moveDirection);
        if (moveDirection.x < 0)
        {
            rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 180);
        }

        animatedFish.transform.SetLocalPositionAndRotation(Vector3.zero, rotation);
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
                mainManager.GameOver();
            }
        }
    }

    // ABSTRACTION
    private void UpdateSize(float size)
    {
        var score = size - 1;
        this.size = size;
        speed = baseSpeed * size;
        playAreaManager.UpdatePlayAreaSize(size);
        spawnManager.UpdateSizeRange(size);
        mainManager.UpdateScore(score);
        transform.localScale = new Vector3(size, 0.1f, size);

        var gameManager = GameManager.Instance;
        if (gameManager != null && score > gameManager.HighScorePoints)
        {
            gameManager.SetHighScore(gameManager.PlayerName, score);
            mainManager.UpdateHighScoreDisplay(score);
        }
    }
}
