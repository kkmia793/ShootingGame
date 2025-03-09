using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameMode CurrentGameMode { get; private set; }
    
    public event Action<CharacterData> On1PCharacterDataReady;
    public event Action<CharacterData> On2PCharacterDataReady;
    
    private CharacterData _selected1PCharacter;
    private CharacterData _selected2PCharacter;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SetGameMode(GameMode mode)
    {
        CurrentGameMode = mode;
        Debug.Log($"ゲームモード設定: {mode}");
    }
    
    public void Set1PCharacterData(CharacterData characterData)
    {
        _selected1PCharacter = characterData;
        Debug.Log($"1Pキャラクター選択: {characterData.characterId}");
    }
    
    public void Set2PCharacterData(CharacterData characterData)
    {
        _selected2PCharacter = characterData;
        Debug.Log($"2Pキャラクター選択: {characterData.characterId}");
    }
    
    public void SetEnemyCharacterData(CharacterData characterData)
    {
        _selected2PCharacter = characterData; // 2Pキャラと同じ扱いに統一
        Debug.Log($"敵キャラクター選択: {characterData.characterId}");
    }

    public CharacterData GetSelected1PCharacter()
    {
        return _selected1PCharacter;
    }
    
    public CharacterData GetSelected2PCharacter()
    {
        return _selected2PCharacter;
    }

    // 重複選択を防ぐためのメソッドを追加
    public bool IsCharacterAlreadySelected(CharacterData characterData)
    {
        return _selected1PCharacter == characterData || _selected2PCharacter == characterData;
    }

    private void InitializeGame()
    {
        On1PCharacterDataReady?.Invoke(_selected1PCharacter);
        On2PCharacterDataReady?.Invoke(_selected2PCharacter);
        Debug.Log("a");
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")
        {
            InitializeGame();
        }
    }
}