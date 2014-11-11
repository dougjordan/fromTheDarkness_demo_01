using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Up and running");
        BeginGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Restart");
            RestartGame();
        }
    }

    public Maze mazePrefab;

    private Maze mazeInstance;

    private void BeginGame()
    {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        StartCoroutine(mazeInstance.Generate());
    }

    private void RestartGame()
    {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        BeginGame();
    }
}