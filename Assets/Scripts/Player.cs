using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction;
    private SpriteRenderer spriteRenderer;
    private int spriteIndex;
    private GameManager gameManager;
    private AudioSource audioSource;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpForce = 5f;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);

        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            direction = Vector3.up * jumpForce;
            audioSource.PlayOneShot(audioSource.clip);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * jumpForce;
                audioSource.PlayOneShot(audioSource.clip);
            }

        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;


        Quaternion rot = transform.rotation;
        if (direction.y > 0)
        {
            rot.z = direction.y * 0.05f;
            transform.rotation = rot;
        }
        else
        {
            rot.z = direction.y * 0.05f;
            transform.rotation = rot;
        }

    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if(spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameManager.GameOver();
        }

        if (collision.gameObject.CompareTag("Scoring"))
        {
            gameManager.IncreaseScore();
        }
    }
}
