using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private LevelPackKuis _soalSoal = null;

    [SerializeField]
    private UI_Pertanyaan _pertanyaan = null;

    [SerializeField]
    private UI_PoinJawaban[] _pilihanJawaban = new UI_PoinJawaban[0];

    [SerializeField]
    private GameSceneManager _gameSceneManager = null;

    [SerializeField]
    private string _namaSceneMenuPilihLevel = string.Empty;

    private int _indexSoal = -1;

    private void Start()
    {
        _soalSoal = _inisialData.levelPack;
        _indexSoal = _inisialData.levelIndex - 1;

        NextLevel();

        // 1. Subscribe Event
        UI_PoinJawaban.EventJawabSoal += UI_PoinJawaban_EventJawabSoal;
    }

    // 2. Method yang akan dijalankan setiap kali tombol pilihan jawaban diklik
    private void UI_PoinJawaban_EventJawabSoal(string jawaban, bool adalahBenar)
    {
        // Jika jawaban yang dipilih salah, langsung return;
        if (!adalahBenar) return;

        // Untuk mengecek nama level pack dan soal/level terakhir yang dimainkan,
        string namaLevelPack = _inisialData.levelPack.name;
        int levelTerakhir = _playerProgress.progressData.progressLevel[namaLevelPack];

        // Cek apabila level yang dimainkan terakhir kali terselesaikan,
        if (_indexSoal + 2 > levelTerakhir)
        {
            // Jika jawaban yang dipilih benar, player akan mendapatkan 20 koin
            _playerProgress.progressData.koin += 20;

            // Menambahkan value progress level agar player sudah dapat mengakses level selanjutnya:
            _playerProgress.progressData.progressLevel[namaLevelPack] = _indexSoal + 2;

            // Kemudian, Save progress:
            _playerProgress.SimpanProgress();
        }
    }

    // 3. Unsubscribe Event
    private void OnDestroy()
    {
        UI_PoinJawaban.EventJawabSoal -= UI_PoinJawaban_EventJawabSoal;
    }

    // Method yang dijalankan setiap kali player Quit dari app nya.
    private void OnApplicationQuit()
    {
        _inisialData.SaatKalah = false;
    }

    public void NextLevel()
    {
        // Soal index selanjutnya
        _indexSoal++;

        
        // Jika index melampaui soal terakhir, 
        if (_indexSoal >= _soalSoal.BanyakLevel)
        {
            //// Old: Ulang soal dari awal / dari soal pertama
            //_indexSoal = 0;

            // New: Kembali ke scene Menu Pilih Level
            _gameSceneManager.BukaScene(_namaSceneMenuPilihLevel);
            // Agar prosedur di bawahnya tidak perlu dijalankan, karena di atas sudah pindah scene.
            return;
        }

        // Ambil data Pertanyaan
        LevelSoalKuis soal = _soalSoal.AmbilLevelKe(_indexSoal);

        // Set informasi soal
        _pertanyaan.SetPertanyaan($"Level {_indexSoal + 1}", soal.pertanyaan, soal.petunjukJawaban);

        for (int i = 0; i < _pilihanJawaban.Length; i++)
        {
            UI_PoinJawaban poin = _pilihanJawaban[i];
            LevelSoalKuis.OpsiJawaban opsi = soal.opsiJawaban[i];
            poin.SetJawaban(opsi.jawabanTeks, opsi.adalahBenar);
        }
    }
}
