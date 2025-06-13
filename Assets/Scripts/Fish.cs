using System;
using TMPro;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private float baseSpeed = 5;
    private float speed = 5;
    // ENCAPSULATION
    public float Size { get; private set; }
    [SerializeField]
    private TMP_Text sizeDisplay;

    protected MainManager mainManager;

    protected virtual void Start()
    {
        mainManager = GameObject.Find("Main Manager").GetComponent<MainManager>();
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (mainManager.IsGameOver)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
        }
    }

    public void SetSize(float size)
    {
        Size = size;
        speed = baseSpeed * size;
        sizeDisplay.text = Math.Round(size, 1).ToString("0.0");
    }
}
