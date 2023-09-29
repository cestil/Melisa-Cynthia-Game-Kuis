using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_OpsiLevelPack : MonoBehaviour
{
    // Static Modifier/modifier statik: apabila data dimilikii oleh kelas, bukan dimiliki oleh object
    public static event System.Action<LevelPackKuis> EventSaatKlik;

    [SerializeField]
    private Button _tombol = null;

    [SerializeField]
    private TextMeshProUGUI _packName = null;

    [SerializeField]
    private LevelPackKuis _levelPack = null;

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
        EventSaatKlik?.Invoke(_levelPack);
    }

    // 3. Unsubscribe Event
    private void OnDestroy()
    {
        _tombol.onClick.RemoveListener(SaatKlik);
        // 2. Tidak butuh invoke karena sudah otomatis saat button diklik player (sementara)
    }
}