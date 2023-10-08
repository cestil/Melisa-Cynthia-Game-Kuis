using TMPro;
using UnityEngine;

public class UI_MenuConfirmMessage : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _tempatKoin = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private GameObject _pesanCukupKoin = null;

    [SerializeField]
    private GameObject _pesanTakCukupKoin = null;

    private UI_OpsiLevelPack _tombolLevelPack = null;
    private LevelPackKuis _levelPack = null;

    void Start()
    {

        if (gameObject.activeSelf)
            gameObject.SetActive(false);

        // Confirm Message akan muncul jika tombol pack diklik dan cukup koin
        // 1. Subscribe Event
        UI_OpsiLevelPack.EventSaatKlik += UI_OpsiLevelPack_EventSaatKlik;
    }

    // 2.
    private void UI_OpsiLevelPack_EventSaatKlik(UI_OpsiLevelPack tombolLevelPack, LevelPackKuis levelPack, bool terkunci)
    {
        // Jika lavel pack tidak terkunci, return
        if (!terkunci) return;

        // Jika level pack terkunci:
        gameObject.SetActive(true);

        // Cek kecukupan koin u/ men-unlock/membeli level pack
        if (_playerProgress.progressData.koin < levelPack.Harga)
        {
            // Jika tidak cukup
            _pesanCukupKoin.SetActive(false);
            _pesanTakCukupKoin.SetActive(true);
            return;
            // ^ langsung return lagi, agar code di bawah tidak dijalankan
        }

        // Jika cukup
        _pesanTakCukupKoin.SetActive(false);
        _pesanCukupKoin.SetActive(true);

        _tombolLevelPack = tombolLevelPack;
        _levelPack = levelPack;
    }

    public void BukaLevel()
    {
        // Proses pembayaran yang sah:
        _playerProgress.progressData.koin -= _levelPack.Harga;
        _playerProgress.progressData.progressLevel[_levelPack.name] = 1;

        _tempatKoin.text = $"{_playerProgress.progressData.koin}";

        _playerProgress.SimpanProgress();

        _tombolLevelPack.BukaLevelPack();
    }

    // 3.
    private void OnDestroy()
    {
        UI_OpsiLevelPack.EventSaatKlik -= UI_OpsiLevelPack_EventSaatKlik;
    }
}
