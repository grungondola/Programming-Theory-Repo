using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] FishPrefabs;

    [SerializeField]
    private float xRange = 19.9f;
    private float rangeScale = 1.0f;
    [SerializeField]
    private float zRange = 19.9f;
    [SerializeField]
    private float minSize = 0.01f;
    [SerializeField]
    private float maxSize = 2.0f;

    private float minSpawnDelay = 0.1f;
    private float maxSpawnDelay = 2.0f;
    private float spawnDelay = 1.0f;

    private MainManager mainmanager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(SpawnFish), spawnDelay, spawnDelay);
        mainmanager = GameObject.Find("Main Manager").GetComponent<MainManager>();
    }

    public void UpdateSizeRange(float playerSize)
    {
        maxSize = playerSize * 1.5f;
        minSize = playerSize / 2;
        rangeScale = 0.5f + playerSize / 2;
    }

    void SpawnFish()
    {
        if (!mainmanager.IsGameOver)
        {
            spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);

            var size = GetFishSize();
            var (startPosition, direction) = GenerateVectorAndRotation(size);

            var fishPrefab = SelectFishPrefab();
            fishPrefab.transform.localScale = new Vector3(size, 0.1f, size);
            var fish = Instantiate(fishPrefab, startPosition, direction);

            fish.GetComponent<Fish>().SetSize(size);
        }
    }

    // ABSTRACTION
    private GameObject SelectFishPrefab()
    {
        var fishIndex = Random.Range(0, FishPrefabs.Length);
        return FishPrefabs[fishIndex];
    }

    // ABSTRACTION
    private float GetFishSize()
    {
        return Random.Range(minSize, maxSize);
    }

    // ABSTRACTION
    private (Vector3 startPosition, Quaternion direction) GenerateVectorAndRotation(float size)
    {
        var startDeterminer = Random.Range(0, 4);
        var xRange = (this.xRange * rangeScale) - size / 2;
        var zRange = (this.zRange * rangeScale) - size / 2;

        float xStartPos, zStartPos;
        switch (startDeterminer)
        {
            case 0: // Start Top
                zStartPos = zRange;
                xStartPos = Random.Range(-xRange, xRange);
                break;
            case 1: // Start Right
                zStartPos = Random.Range(-zRange, zRange);
                xStartPos = xRange;
                break;
            case 2: // Start Bottom
                zStartPos = -zRange;
                xStartPos = Random.Range(-xRange, xRange);
                break;
            case 3: // Start Left
            default:
                zStartPos = Random.Range(-zRange, zRange);
                xStartPos = -xRange;
                break;
        }

        float rotation = Random.Range(1.0f, 89.0f);
        if (xStartPos < 0)
        {
            if (zStartPos >= 0)
            {
                // Starting in Upper Left Quadrant, rotate towards Lower Right quadrant
                rotation += 90.0f;
            }
            // Starting in Lower Left Quadrant, rotate towards Upper Right quadrant
        }
        else
        {
            if (zStartPos < 0)
            {
                // Starting in Lower Right Quadrant, rotate towards Upper Left Quadrant
                rotation += 270.0f;
            }
            else
            {
                // Starting in Upper Right Quadrant, rotate towards Lower Left Quadrant
                rotation += 180.0f;
            }
        }

        // Convert calculated values to starting position and rotation values
        var startPosition = new Vector3(xStartPos, 0.1f, zStartPos);
        var directionQuat = Quaternion.Euler(0, rotation, 0);
        Debug.Log($"Moving from {startPosition} of size {size} with direction {directionQuat.eulerAngles}");

        return (startPosition, directionQuat);
    }
}
