using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    //[SerializeField]
    //private PlayerProgress _playerProgress = null;

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
        //if (!_playerProgress.MuatProgress())
        //{
        //    _playerProgress.SimpanProgress();
        //}

        _soalSoal = _inisialData.levelPack;
        _indexSoal = _inisialData.levelIndex - 1;

        NextLevel();
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
        _pertanyaan.SetPertanyaan($"Level A-{_indexSoal + 1}", soal.pertanyaan, soal.petunjukJawaban);

        for (int i = 0; i < _pilihanJawaban.Length; i++)
        {
            UI_PoinJawaban poin = _pilihanJawaban[i];
            LevelSoalKuis.OpsiJawaban opsi = soal.opsiJawaban[i];
            poin.SetJawaban(opsi.jawabanTeks, opsi.adalahBenar);
        }
    }
}
