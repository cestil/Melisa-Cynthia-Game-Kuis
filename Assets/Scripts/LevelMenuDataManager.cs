using TMPro;
using UnityEngine;

public class LevelMenuDataManager : MonoBehaviour
{
    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private TextMeshProUGUI _tempatScore = null;

    // Start is called before the first frame update
    void Start()
    {
        _tempatScore.text = $"{_playerProgress.progressData.koin}";
    }
}
