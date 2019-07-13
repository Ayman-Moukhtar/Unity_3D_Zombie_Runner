using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    [SerializeField]
    private Canvas _gameOverCanvas;

    private void Start()
    {
        _gameOverCanvas.gameObject.SetActive(false);
        _gameOverCanvas.enabled = false;
    }

    public void Die()
    {
        _gameOverCanvas.gameObject.SetActive(true);
        _gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
