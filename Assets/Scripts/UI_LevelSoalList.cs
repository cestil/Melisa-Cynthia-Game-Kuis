using UnityEngine;

public class UI_LevelSoalList : MonoBehaviour
{
    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    [SerializeField]
    private UI_OpsiLevelKuis _tombolLevel = null;

    [SerializeField]
    private RectTransform _content = null;

    [Space, SerializeField]
    private LevelPackKuis _levelPack = null;

    [SerializeField]
    private GameSceneManager _gameSceneManager = null;

    [SerializeField]
    private string _gameplayScene = null;

    private void Start()
    {
        //if (_levelPack != null)
        //{
        //    UnloadLevelPack(_levelPack);
        //}

        // 1. Subscribe Event
        UI_OpsiLevelKuis.EventSaatKlik += UI_OpsiLevelKuis_EventSaatKlik;
    }

    // 2. Method yang akan dijalankan saat button diklik
    private void UI_OpsiLevelKuis_EventSaatKlik(int index)
    {
        // Masukan inisialData dulu biar aman/agar pada saat scene nya dibuka, soal yang akan dibuka sudah sesuai.
        _inisialData.levelIndex = index;
        _gameSceneManager.BukaScene(_gameplayScene);
    }

    // 3. Unsubscribe Event
    private void OnDestroy()
    {
        UI_OpsiLevelKuis.EventSaatKlik -= UI_OpsiLevelKuis_EventSaatKlik;
    }

    // Membuka, me-load, menampilkan level-level dari isi level pack
    public void UnloadLevelPack(LevelPackKuis levelPack)
    {
        HapusIsiKonten();

        _levelPack = levelPack;
        for (int i = 0; i < levelPack.BanyakLevel; i++)
        {
            // Membuat salinan obyek dari prefab Pack Button
            var t = Instantiate(_tombolLevel);

            t.SetLevelSoal(levelPack.AmbilLevelKe(i), i);

            // Memasukan salinan tersebut sebagai child dari obyek "content"
            t.transform.SetParent(_content);

            // Mengatur scale salinan tersebut secara procedural agar tidak terlalu besar
            t.transform.localScale = Vector3.one;
        }
    }

    private void HapusIsiKonten()
    {
        var cc = _content.childCount;

        for (int i = 0; i < cc; i++)
        {
            Destroy(_content.GetChild(i).gameObject);
        }
    }
}
