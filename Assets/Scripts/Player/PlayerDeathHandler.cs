using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    [SerializeField]
    private Canvas _gameOverCanvas;

    private void Start()
    {
        _gameOverCanvas.enabled = false;
    }

    public void Die()
    {
        _gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
