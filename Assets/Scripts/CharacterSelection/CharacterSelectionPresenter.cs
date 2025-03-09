using UnityEngine;
using System.Linq;

public class CharacterSelectionPresenter : MonoBehaviour
{
    [SerializeField] private CharacterSelectionView view;
    
    private bool _isSelectingEnemy = false;
    private CharacterSelectionModel model;

    private void Start()
    {
		CharacterData[] availableCharacters = Resources.LoadAll<CharacterData>("CharacterData");

        availableCharacters = availableCharacters
            .OrderBy(character => character.characterId)
            .ToArray();
        
        model = new CharacterSelectionModel(availableCharacters);
        
        view.UpdateCharacterDisplay(availableCharacters);
        
        model.OnCharacterSelected += OnCharacterSelected;
        
        view.OnCharacterButtonClicked += OnCharacterButtonClicked;
        view.OnBackButtonClicked += OnBackButtonClicked;
        view.OnNextButtonClicked += OnNextButtonClicked;
        
        OnCharacterSelected(model.GetSelectedCharacter());
    }

    private void OnCharacterButtonClicked(int characterIndex)
    {
        var characterData = model.AvailableCharacters[characterIndex];
        model.SelectCharacter(characterData);
    }

    private void OnCharacterSelected(CharacterData characterData)
    {
        int index = System.Array.IndexOf(model.AvailableCharacters, characterData);
        view.HighlightSelectedCharacter(index);
    }

    private void OnBackButtonClicked()
    {
        CharacterData selectedCharacter = model.GetSelectedCharacter();
        
        if (!_isSelectingEnemy)
        {
            SceneController.Instance.LoadScene("ModeSelection");
        }
        else
        {
            Debug.Log("自キャラクター選択フェーズに移行");
            view.HighlightSelectedCharacter(selectedCharacter.characterId);
            _isSelectingEnemy = false;
        }
    }

    private void OnNextButtonClicked()
    {
        CharacterData selectedCharacter = model.GetSelectedCharacter();

        if (GameManager.Instance.CurrentGameMode == GameMode.OfflineMode)
        {
            if (!_isSelectingEnemy)
            {
                // まず自キャラを選択してから敵キャラ選択へ
                GameManager.Instance.Set1PCharacterData(selectedCharacter);
                _isSelectingEnemy = true;
                
                view.ResetCharacterSelection();
                
                Debug.Log("敵キャラクター選択フェーズに移行");
            }
            else
            {
                // 1Pキャラ（自キャラ）と2Pキャラ（敵キャラ）が選択状態ならゲームシーンへ
                // 敵キャラが選択されたらゲームシーンへ
                GameManager.Instance.SetEnemyCharacterData(selectedCharacter);
                SceneController.Instance.LoadScene("Main");
            }
        }
        else if(GameManager.Instance.CurrentGameMode == GameMode.OnlineMatchingMode)
        {
            // オンラインモードの場合は直接ゲームシーンへ遷移
            GameManager.Instance.Set1PCharacterData(selectedCharacter);
            //SceneController.Instance.LoadScene("BattleScene");
        }
        else if (GameManager.Instance.CurrentGameMode == GameMode.OnlineRoomMode)
        {
            // ルームマッチの1P側のキャラクター
        }
    }

    private void OnDestroy()
    {
        model.OnCharacterSelected -= OnCharacterSelected;
        view.OnCharacterButtonClicked -= OnCharacterButtonClicked;
        view.OnBackButtonClicked -= OnBackButtonClicked;
        view.OnNextButtonClicked -= OnNextButtonClicked;
    }
}