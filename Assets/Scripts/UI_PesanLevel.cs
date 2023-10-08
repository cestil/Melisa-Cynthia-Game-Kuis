using TMPro;
using UnityEngine;

public class UI_PesanLevel : MonoBehaviour
{
    //--- Animations
    [SerializeField]
    private Animator _animator = null;
    //---

    [SerializeField]
    private GameObject _opsiMenang = null;

    [SerializeField]
    private GameObject _opsiKalah = null;

    [SerializeField]
    private TextMeshProUGUI _tempatPesan = null;

    // Setter untuk mengubah informasi, Getter untuk mengambil informasi
    public string Pesan
    {
        get => _tempatPesan.text;
        set => _tempatPesan.text = value;

    }
    // Awake dijalankan sebelum method Start(), saat applikasi baru dijalankan
    private void Awake()
    {
        // Untuk mematikan halaman pesan level
        if (gameObject.activeSelf)
            gameObject.SetActive(false);

        // 1. Subscribe Events:
        UI_Timer.EventWaktuHabis += UI_Timer_EventWaktuHabis;
        UI_PoinJawaban.EventJawabSoal += UI_PoinJawaban_EventJawabSoal;
    }

    // 2. Method yang akan dijalankan jika waktu habis
    private void UI_Timer_EventWaktuHabis()
    {
        Pesan = "Waktu Habis!!!";
        gameObject.SetActive(true);

        _opsiMenang.SetActive(false);
        _opsiKalah.SetActive(true);
    }

    private void UI_PoinJawaban_EventJawabSoal(string jawabanTeks, bool adalahBenar)
    {
        Pesan = $"Jawaban Anda {adalahBenar} (Jawab: {jawabanTeks})";
        gameObject.SetActive(true);

        if (adalahBenar)
        {
            _opsiMenang.SetActive(true);
            _opsiKalah.SetActive(false);
        }
        else
        {
            _opsiMenang.SetActive(false);
            _opsiKalah.SetActive(true);
        }

        //--- Animations
        _animator.SetBool("Menang", adalahBenar);
    }


    // 3. Unsubscribe Event
    private void OnDestroy()
    {
        UI_Timer.EventWaktuHabis -= UI_Timer_EventWaktuHabis;
        UI_PoinJawaban.EventJawabSoal -= UI_PoinJawaban_EventJawabSoal;
    }
}
