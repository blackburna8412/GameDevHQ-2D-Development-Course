using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Text _scoreText;
    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private Image _livesImage;
    [SerializeField] private Text _gameOverText;
    [SerializeField] GameObject _gameOverTextObj;

    [SerializeField] private Text _shieldText;
    [SerializeField] private Text _ammoText;

    private bool _isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        _ammoText.text = "Ammo: " + 15 + "/15";
        _shieldText.text = "Shields: " + 0 + "/3";
        _scoreText.text = "Score: " + 0;
        _gameOverText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver != false)
        {
                SceneManager.LoadScene(0);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && _isGameOver != false)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void UpdateAmmoText(int ammoCount)
    {
        _ammoText.text = "Ammo: " + ammoCount + "/15";
    }

    public void UpdateShieldText(int shieldCount)
    {
        _shieldText.text = "Shields: " + shieldCount + "/3";
    }

    public void UpdateScoreText(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _livesSprites[currentLives];

        if(currentLives == 0)
        {
            _gameOverTextObj.SetActive(true);
            StartCoroutine(GameOverRoutine());
        }
    }

    IEnumerator GameOverRoutine()
    {
        while (true)
        {
            _isGameOver = true;
            _gameOverText.enabled = true;
            yield return new WaitForSeconds(.5f);
            _gameOverText.enabled = false;
            yield return new WaitForSeconds(.5f);
        }
    }

}
