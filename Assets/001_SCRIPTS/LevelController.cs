using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] sprite;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject preButton;

    private int _currentLevelIndex;
    private int _levelOffset = 1;

    private void Start()
    {
        _currentLevelIndex = SceneManager.GetActiveScene().buildIndex - _levelOffset;
        UpdateSprite();
    }

    void Update()
    {
        UpdateButtonView();
    }

    public void LoadNextLevel()
    {
        if (_currentLevelIndex + 1 < SceneManager.sceneCountInBuildSettings - _levelOffset)
        {
            _currentLevelIndex++;
            SceneManager.LoadScene(_currentLevelIndex + _levelOffset);
        }
    }

    public void LoadPreviousLevel()
    {
        Debug.Log(_currentLevelIndex);
        if (_currentLevelIndex > 0)
        {
            _currentLevelIndex--;
            SceneManager.LoadScene(_currentLevelIndex + _levelOffset);
        }
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadHome()
    {
        SceneManager.LoadScene(0);
    }

    private void UpdateSprite()
    {
        if (_currentLevelIndex >= 0)
        {
            image.sprite = sprite[_currentLevelIndex];
        }
    }

    private void UpdateButtonView()
    {
        if (_currentLevelIndex <= 0)
        {
            preButton.SetActive(false);
        }
        else
        {
            preButton.SetActive(true);
        }


        if (_currentLevelIndex >= SceneManager.sceneCountInBuildSettings - _levelOffset - 1)
        {
            nextButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
        }
    }

    private void OnEnable()
    {
        // Đăng ký callback khi scene được load
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _currentLevelIndex = SceneManager.GetActiveScene().buildIndex - _levelOffset;

        UpdateSprite();
    }
}