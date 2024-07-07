using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup gameOver;
    //public GameObject gameOverPrefab;
    //public bool isGameOver;
    //public GameObject gameOverInstance;

    public Transform CanvasParent;
    void Start()
    {
        NewGame();
    }
    
    void Update()
    {
        //if (isGameOver && !gameOverInstance)
        //{
        //    gameOverInstance = Instantiate(gameOverPrefab, CanvasParent);
        //}
    }

    public void NewGame()
    {
        gameOver.alpha = 0f;
        gameOver.interactable = false;

        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();

        board.enabled = true;
    }

    public void GameOver()
    {
        board.enabled = false;
        gameOver.interactable = true;
        StartCoroutine(Fade(gameOver, 1f, 1f));

        //if (!isGameOver && !gameOverInstance)
        //{
        //    gameOverInstance = Instantiate(gameOverPrefab, CanvasParent);
        //    isGameOver = true; // 标记游戏已结束
        //}
    }
    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay); 

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while(elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = to;
    }
    public void TryAgain(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
