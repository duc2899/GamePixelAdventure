using System;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace Player
{
    public class ChooseCharacterController : MonoBehaviour
    {
        [SerializeField] private List<Character> characters = new List<Character>();
        [SerializeField] private Button buttonNextCharacter;
        [SerializeField] private Button buttonPreviousCharacter;
        [SerializeField] private Button buttonConfirmCharacter;
        [SerializeField] private TMP_Text characterName;
        [SerializeField] private Image image;

        private int _selectedCharacterIndex = 0;


        private void Awake()
        {
            buttonPreviousCharacter.onClick.AddListener(PreviousCharacter);
            buttonNextCharacter.onClick.AddListener(NextCharacter);
            buttonConfirmCharacter.onClick.AddListener(ConfirmCharacter);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            UpdateViewCharacter();
        }

        private void NextCharacter()
        {
            _selectedCharacterIndex++;
            
            if (_selectedCharacterIndex >= characters.Count)
            {
                _selectedCharacterIndex = 0;
            }

            UpdateViewCharacter();
        }

        private void PreviousCharacter()
        {
            _selectedCharacterIndex--;
            
            if (_selectedCharacterIndex < 0)
            {
                _selectedCharacterIndex = characters.Count - 1;
            }

            UpdateViewCharacter();
        }

        private void ConfirmCharacter()
        {
            PlayerPrefs.SetInt(Constants.IndexCharacter, _selectedCharacterIndex);
            SceneManager.LoadScene("Level_0");
        }

        private void UpdateViewCharacter()
        {
            if (characters.Count <= 0)
            {
                Debug.LogWarning("No character selected");
                return;
            }

            characterName.text = characters[_selectedCharacterIndex].CharacterName;
            image.sprite = characters[_selectedCharacterIndex].CharacterSprite;
        }
    }
}