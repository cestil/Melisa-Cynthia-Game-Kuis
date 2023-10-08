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

    //// Dipindah ke LevelMenuDataManager
    //[SerializeField]
    //private LevelPackKuis[] _levelPacks = new LevelPackKuis[0];

    private void Start()
    {
        //LoadLevelPack();

        if (_inisialData.SaatKalah)
        {
            UI_OpsiLevelPack_EventSaatKlik(null, _inisialData.levelPack, false);
            // ^ false agar level pack tetap terbuka walaupun player telah kalah?
            // ^ set null untuk bagian tombolnya karena memang pada bagian/saat ini tidak ada tombolnya.
        }

        // 1. Subscribe Event (pakai "+=", kemudian pencet Tab untuk otomatis membuat Method)
        UI_OpsiLevelPack.EventSaatKlik += UI_OpsiLevelPack_EventSaatKlik;
    }

    // Ubah dari private ke public..
    public void LoadLevelPack(LevelPackKuis[] levelPacks, PlayerProgress.MainData playerData)
    {
        foreach (var lp in levelPacks)
        {
            // ^ lp dalam konteks ini merupakan setiap level pack yang ada di dalam list LevelPackKuis

            // Membuat t = salinan obyek dari prefab Pack Button
            var t = Instantiate(_tombolPack);

            t.SetLevelPack(lp);

            // Memasukan salinan tersebut sebagai child dari obyek "content"
            t.transform.SetParent(_content);
            // Mengatur scale salinan tersebut secara procedural agar tidak terlalu besar
            t.transform.localScale = Vector3.one;

            // Mengecek apakah level pack terdaftar di Dictionary progress player:
            if (!playerData.progressLevel.ContainsKey(lp.name))
            {
                // Jika tidak terdaftar, maka level pack terkunci
                t.KunciLevelPack();
            }
        }
    }

    // 2. Method yang akan dijalankan saat button diklik
    private void UI_OpsiLevelPack_EventSaatKlik(UI_OpsiLevelPack tombolLevelPack, LevelPackKuis levelPack, bool terkunci)
    {
        if (terkunci)
            return;
        // ^ Jika level pack terkunci, code di bawah tidak akan dijalankan

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
