using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_OpsiLevelPack : MonoBehaviour
{
    // Static Modifier/modifier statik: apabila data dimilikii oleh kelas, bukan dimiliki oleh object
    public static event System.Action<UI_OpsiLevelPack, LevelPackKuis, bool> EventSaatKlik;
    // ^ Ditambahkan bool juga
    // ^ Ditambah lagi UI_OpsiLevelPack untuk "mengirim" langsung tombolnya

    [SerializeField]
    private Button _tombol = null;

    [SerializeField]
    private TextMeshProUGUI _packName = null;

    [SerializeField]
    private LevelPackKuis _levelPack = null;

    [SerializeField]
    private TextMeshProUGUI _labelTerkunci = null;

    [SerializeField]
    private TextMeshProUGUI _labelHarga = null;

    [SerializeField]
    private bool _terkunci = false;

    private void Start()
    {
        if (_levelPack != null)
            SetLevelPack(_levelPack);

        // 1. Subscribe Event
        _tombol.onClick.AddListener(SaatKlik);
    }

    public void SetLevelPack(LevelPackKuis levelPack)
    {
        _packName.text = levelPack.name;
        _levelPack = levelPack;
    }

    // 2. Method yang akan dijalankan saat tombol diklik
    private void SaatKlik()
    {
        //Debug.Log("KLIK!!");

        // "?" sebelum memanggil Method artinya: mengecek apakah ada atau tidaknya Method yang terdaftar/men-subscribe pada suatu Event, pada konteks ini: EventSaatKlik.
        // Jika tidak ada, maka Method Invoke tidak akan dipanggil; Jika ada, Method Invoke akan dipanggil. 
        EventSaatKlik?.Invoke(this, _levelPack, _terkunci);
        // ^ tergantung nilai bool, level pack yang diklik sedang terkunci atau tidak
        // ^ this = untuk merujuk ke benda (dengan class UI_OpsiLevelPack) pemilik komponen script ini sendiri
    }

    // 3. Unsubscribe Event
    private void OnDestroy()
    {
        _tombol.onClick.RemoveListener(SaatKlik);
        // 2. Tidak butuh invoke karena sudah otomatis saat button diklik player (sementara)
    }

    public void BukaLevelPack()
    {
        _terkunci = false;
        _labelTerkunci.gameObject.SetActive(false);
        // Mengakses parrent dari suatu object...
        _labelHarga.transform.parent.gameObject.SetActive(false);
    }

    public void KunciLevelPack()
    {
        _terkunci = true;
        _labelTerkunci.gameObject.SetActive(true);
        _labelHarga.transform.parent.gameObject.SetActive(true);
        _labelHarga.text = $"{_levelPack.Harga}";
    }
}