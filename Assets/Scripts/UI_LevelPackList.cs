using UnityEngine;

public class UI_LevelPackList : MonoBehaviour
{
    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    // Yang akan menerima panggilan dari EventSaatKlik pada UI_OpsiLevelPack tadi
    [SerializeField]
    private UI_LevelSoalList _levelList = null;

    [SerializeField]
    private UI_OpsiLevelPack _tombolPack = null;

    [SerializeField]
    private RectTransform _content = null;

    [Space, SerializeField]
    private LevelPackKuis[] _levelPacks = new LevelPackKuis[0];

    private void Start()
    {
        LoadLevelPack();

        // 1. Subscribe Event (pakai "+=", kemudian pencet Tab untuk otomatis membuat Method)
        UI_OpsiLevelPack.EventSaatKlik += UI_OpsiLevelPack_EventSaatKlik;
    }

    private void LoadLevelPack()
    {
        foreach (var lp in _levelPacks)
        {
            // Membuat salinan obyek dari prefab Pack Button
            var t = Instantiate(_tombolPack);

            t.SetLevelPack(lp);

            // Memasukan salinan tersebut sebagai child dari obyek "content"
            t.transform.SetParent(_content);

            // Mengatur scale salinan tersebut secara procedural agar tidak terlalu besar
            t.transform.localScale = Vector3.one;
        }
    }

    // 2. Method yang akan dijalankan saat button diklik
    private void UI_OpsiLevelPack_EventSaatKlik(LevelPackKuis levelPack)
    {
        //Buka object menu Levels (menampilkan isi (Level-Level) dari menu Level Pack)
        _levelList.gameObject.SetActive(true);
        _levelList.UnloadLevelPack(levelPack);

        // Menutup object menu Level Packs
        gameObject.SetActive(false);

        _inisialData.levelPack = levelPack;
    }

    // 3. Unsubscribe Event (pakai "-=") (wajib untuk menghindari Memory Leak)
    private void OnDestroy()
    {
        UI_OpsiLevelPack.EventSaatKlik -= UI_OpsiLevelPack_EventSaatKlik;
    }
}
