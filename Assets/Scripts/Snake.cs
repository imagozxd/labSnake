using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public static Snake Instance { get; private set; }

    [SerializeField] private GameObject headPrefab;
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private GameObject tailPrefab;
    [SerializeField] private float moveSpeed = 5f;

    private List<GameObject> bodyParts = new List<GameObject>();
    private Vector2 direction = Vector2.right;
    private float moveTimer;
    private float moveDelay = .3f;

    private void Awake()
    {
        // Implementación del Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        CreateSnake();
    }

    private void Update()
    {
        HandleInput();
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveDelay)
        {
            moveTimer = 0;
            Move();
        }
    }

    void CreateSnake()
    {
        //crear cabeza
        GameObject head = Instantiate(headPrefab, Vector3.zero, Quaternion.identity);
        head.transform.parent = this.transform;
        bodyParts.Add(head);
        //crear cuerpo
        GameObject body = Instantiate(bodyPrefab, new Vector3(-1, 0, 0), Quaternion.identity);
        body.transform.parent = this.transform;
        bodyParts.Add(body);
        //crear cola
        GameObject tail = Instantiate(tailPrefab, new Vector3(-2, 0, 0), Quaternion.identity);
        tail.transform.parent = this.transform;
        bodyParts.Add(tail);
    }

    void HandleInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (vertical > 0 && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (vertical < 0 && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (horizontal < 0 && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
        else if (horizontal > 0 && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
    }

    void Move()
    {
        // mover las partes del cuerpo siguiendo a la cabeza
        Vector3 previousPosition = bodyParts[0].transform.position;
        bodyParts[0].transform.position += (Vector3)direction;

        for (int i = 1; i < bodyParts.Count; i++)
        {
            Vector3 temp = bodyParts[i].transform.position;
            bodyParts[i].transform.position = previousPosition;
            previousPosition = temp;
        }
    }

    public void Grow()
    {
        // crecimiento
        GameObject newBodyPart = Instantiate(bodyPrefab, bodyParts[bodyParts.Count - 1].transform.position, Quaternion.identity);
        newBodyPart.transform.parent = this.transform;
        bodyParts.Insert(bodyParts.Count - 1, newBodyPart);
    }
}
