using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Pertanyaan : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _tempatTeks = null;

    [SerializeField]
    private Image _tempatGambar = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetPertanyaan(string teksPertanyaan, Sprite gambarHint)
    {
        _tempatTeks.text = teksPertanyaan;
        _tempatGambar.sprite = gambarHint;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
