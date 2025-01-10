using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class ChooseCharacterController : MonoBehaviour
{
    [SerializeField] private Sprite[] characterSprites;
    [SerializeField] private Image image;
    [SerializeField] private List<string> characterNames = new List<string>();
    [SerializeField] private TMP_Text characterName;

    private int _selectedCharacterIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateViewCharacter();
    }

    public void NextCharacter()
    {
        _selectedCharacterIndex++;
        if (_selectedCharacterIndex >= characterSprites.Length)
        {
            _selectedCharacterIndex = 0;
        }

        UpdateViewCharacter();
    }

    public void PreviousCharacter()
    {
        _selectedCharacterIndex--;
        if (_selectedCharacterIndex < 0)
        {
            _selectedCharacterIndex = characterSprites.Length - 1;
        }

        UpdateViewCharacter();
    }

    public void ConfirmCharacter()
    {
        PlayerPrefs.SetInt(CONSTANT.INDEX_CHARACTER, _selectedCharacterIndex);
        SceneManager.LoadScene("Level_0");
    }

    private void UpdateViewCharacter()
    {
        if (characterSprites.Length <= 0)
        {
            Debug.LogWarning("No character selected");
            return;
        }

        characterName.text = characterNames[_selectedCharacterIndex];
        image.sprite = characterSprites[_selectedCharacterIndex];
    }
}