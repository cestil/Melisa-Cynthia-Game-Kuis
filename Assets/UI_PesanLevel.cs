using TMPro;
using UnityEngine;

public class UI_PesanLevel : MonoBehaviour
{
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
    }
}
