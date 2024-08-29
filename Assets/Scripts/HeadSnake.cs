using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSnake : MonoBehaviour
{
    private ScoreManager _scoreManager;
    private FoodGenerator _foodGenerator;
    public bool _existFood = false;

    private void Start()
    {
        // Acceder al Singleton Snake
        _scoreManager = FindObjectOfType<ScoreManager>();
        _foodGenerator = FindObjectOfType<FoodGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ranita"))
        {
            // Llamo a Grow de Snake
            Snake.Instance.Grow();
            Debug.Log("pobre ranita");
            Destroy(other.gameObject);
            _existFood = true;

            // Incrementar el puntaje
            if (_scoreManager != null)
            {
                _scoreManager.IncreaseScore();
            }
            if (_foodGenerator != null)
            {
                _foodGenerator.GenFood();
            }
        }
        else if (other.gameObject.CompareTag("pared"))
        {
            // Reiniciar el juego al colisionar con una pared
            RestartGame();
        }
    }

    private void RestartGame()
    {
        // Reiniciar la escena actual
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
