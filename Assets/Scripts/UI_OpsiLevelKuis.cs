using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_OpsiLevelKuis : MonoBehaviour
{
    public static event System.Action<int> EventSaatKlik;

    [SerializeField]
    private Button _tombolLevel = null;

    [SerializeField]
    private TextMeshProUGUI _levelName = null;

    [SerializeField]
    private LevelSoalKuis _levelSoal = null;

    private void Start()
    {
        if (_levelSoal != null)
            SetLevelSoal(_levelSoal, _levelSoal.levelPackIndex);

        // 1. Subscribe Event
        _tombolLevel.onClick.AddListener(SaatKlik);
    }

    // 2. Method SaatKlik() untuk menjalankan event (Invoke) saat tombol diklik
    private void SaatKlik()
    {
        EventSaatKlik?.Invoke(_levelSoal.levelPackIndex);
    }

    public void SetLevelSoal(LevelSoalKuis levelSoal, int index)
    {
        //_levelName.text = levelSoal.name;
        _levelName.text = $"Level {index + 1}";
        _levelSoal = levelSoal;

        _levelSoal.levelPackIndex = index;
    }

    // 3. Unsubscribe Event
    private void OnDestroy()
    {
        _tombolLevel.onClick.RemoveListener(SaatKlik);
    }
}
