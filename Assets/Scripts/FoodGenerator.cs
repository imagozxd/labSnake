using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    public int sideX = 0;  
    public int sideY = 0;
    public float cellSize = 1f;  // Tamaño de la celda 
    public Color gizmoColor = Color.white;

    //food
    public GameObject foodPrefab;
    private GameObject currentFood;

    private HeadSnake _headSnake;

    private void Start()
    {
        GenFood(); 
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenFood();  // Les solo prueba BORRAR
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        // tamaño
        Vector2 areaSize = new Vector2(sideX, sideY);

        Gizmos.DrawWireCube(transform.position, areaSize);
    }

    public void GenFood()
    {
        if (currentFood != null)
        {
            Destroy(currentFood);
        }

        // Calcular una posición aleatoria dentro del área, alineada a la cuadrícula
        float randomX = Mathf.Round(Random.Range(-sideX / 2f, sideX / 2f) / cellSize) * cellSize;
        float randomY = Mathf.Round(Random.Range(-sideY / 2f, sideY / 2f) / cellSize) * cellSize;
        Vector2 randomPosition = new Vector2(randomX, randomY) + (Vector2)transform.position;

        
        currentFood = Instantiate(foodPrefab, randomPosition, Quaternion.identity);
        currentFood.transform.SetParent(transform);
    }

    public void CreateFood()
    {
        GenFood();
    }
}
