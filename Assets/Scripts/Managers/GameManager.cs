using Managers;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] characterPrefabs; // Prefab của các nhân vật

    private bool _isInstantiate = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // int selectedCharacterIndex = PlayerPrefs.GetInt(Constants.IndexCharacter, 0);
        // if (!_isInstantiate)
        // {
        //     Instantiate(characterPrefabs[selectedCharacterIndex], new Vector3(-10, 3, 0), Quaternion.identity);
        // }
    }
}