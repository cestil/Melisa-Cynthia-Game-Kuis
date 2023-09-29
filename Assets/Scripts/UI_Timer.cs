using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    [SerializeField]
    private UI_PesanLevel _tempatPesan = null;

    [SerializeField]
    private Slider _timeBar = null;

    [SerializeField]
    private float _waktuJawab = 30f;

    private float _sisaWaktu = 0f; // Data Sementara
    private bool _waktuBerjalan = true;

    public bool WaktuBerjalan
    {
        get => _waktuBerjalan;
        set => _waktuBerjalan = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        UlangWaktu();
    }

    public void UlangWaktu()
    {
        _sisaWaktu = _waktuJawab;
    }
    // Update is called once per frame
    void Update()
    {
        if (!_waktuBerjalan)
            return;

        // deltaTime itu 0 koma sekian detik yang dijalankan oleh tiap frame per detik?
        // di mana nilainya bisa berbeda-beda tergantung dari kecepatan komputer memproses setiap frame tersebut pada saat itu juga?
        _sisaWaktu -= Time.deltaTime;
        _timeBar.value = _sisaWaktu / _waktuJawab;

        if (_sisaWaktu <= 0f)
        {
            _tempatPesan.Pesan = "Waktu Habis!!!";
            _tempatPesan.gameObject.SetActive(true);
            // Debug.Log("Waktu Habis");
            _waktuBerjalan = false;
            return;
        }

        // Debug.Log(_sisaWaktu);
    }
}
