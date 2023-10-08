using TMPro;
using UnityEngine;

public class LevelMenuDataManager : MonoBehaviour
{
    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private TextMeshProUGUI _tempatScore = null;

    // .....
    [SerializeField]
    private UI_LevelPackList _levelPackList = null;

    // Pindahan dari ^ UI_LevelPackList
    [SerializeField]
    private LevelPackKuis[] _levelPacks = new LevelPackKuis[0];


    void Start()
    {
        if (!_playerProgress.MuatProgress())
        {
            _playerProgress.SimpanProgress();
        }

        _levelPackList.LoadLevelPack(_levelPacks, _playerProgress.progressData);

        _tempatScore.text = $"{_playerProgress.progressData.koin}";

        // Untuk memainkan kembali BGM pertama setiap kali obyek Level Menu Data Manager (yang hanya ada pada scene Menu Pilih Level) muncul:
        AudioManager.instance.PlayBGM(0);
    }
}
