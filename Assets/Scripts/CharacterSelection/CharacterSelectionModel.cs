using System;
using UnityEngine;

public class CharacterSelectionModel
{
    public CharacterData[] AvailableCharacters { get; private set; }
    private CharacterData selectedCharacter;

    public event Action<CharacterData> OnCharacterSelected;

    public CharacterSelectionModel(CharacterData[] availableCharacters)
    {
        AvailableCharacters = availableCharacters;
        selectedCharacter = availableCharacters.Length > 0 ? availableCharacters[0] : null;
    }

    public void SelectCharacter(CharacterData characterData)
    {
        if (characterData != null)
        {
            selectedCharacter = characterData;
            OnCharacterSelected?.Invoke(selectedCharacter);
        }
    }

    public CharacterData GetSelectedCharacter()
    {
        return selectedCharacter;
    }
}