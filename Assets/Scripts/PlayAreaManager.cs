using UnityEngine;

public class PlayAreaManager : MonoBehaviour
{
    [SerializeField]

    private float baseXScale = 4;
    [SerializeField]

    private float baseZScale = 4;
    [SerializeField]
    private float baseCameraSize = 15;
    [SerializeField]
    private Camera mainCamera;

    public void UpdatePlayAreaSize(float playerSize)
    {
        var playerScale = 0.5f + playerSize / 2;
        gameObject.transform.localScale = new Vector3(baseXScale * playerScale, 1, baseZScale * playerScale);
        mainCamera.orthographicSize = baseCameraSize * playerScale;
    }
}
