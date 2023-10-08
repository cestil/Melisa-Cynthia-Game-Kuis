using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Agar AudioManager dapat diakses di script mana pun:
    // Menggunakan pola desain Singleton = 1 Class, 1 Object
    public static AudioManager instance = null;

    [SerializeField]
    private AudioSource _bgmPrefab = null;

    [SerializeField]
    private AudioSource _sfxPrefab = null;

    [SerializeField]
    private AudioClip[] _bgmClips = new AudioClip[0];

    // BGM & SFX sementara
    private AudioSource _bgm = null;
    private AudioSource _sfx = null;

    private void Awake()
    {
        // Apabila dalam instance sudah ada obyek, maka hapus Audio Manager:
        if (instance != null)
        {
            Debug.Log("Obyek \"Audio Manager\" sudah ada.\n" +
                "Hapus Obyek serupa.", instance);
            Destroy(this.gameObject);
            return;
        }

        instance = this;

        // Agar object BGM juga tidak hancur begitu saja jika pindah scene:
        DontDestroyOnLoad(this.gameObject);

        //BGM
        // Create BGM Object (instantiate dari Prefab yang sudah ada):
        _bgm = Instantiate(_bgmPrefab);
        // Agar BGM yang dipasang tidak hancur begitu saja jika pindah scene:
        DontDestroyOnLoad(_bgm);

        //SFX
        // Create SFX Object (instantiate dari Prefab yang sudah ada):
        _sfx = Instantiate(_sfxPrefab);
        // Agar SFX yang dipasang tidak hancur begitu saja jika pindah scene:
        DontDestroyOnLoad(_sfx);
    }

    private void OnDestroy()
    {
        if (this == instance)
        {
            // Mengosongkan instance setiap kali obyek Audio Manager dihapus.
            instance = null;

            // Memastikan agar BGM dan SFX ikut dihapus setiap obyek Audio Manager yang dipasangkan script AudioManager ini dihapus,
            if (_bgm != null)
                Destroy(_bgm.gameObject);

            if (_sfx != null)
                Destroy(_sfx.gameObject);
        }
    }

    // Untuk memilih Audio Clip BGM:
    public void PlayBGM(int index)
    {
        // Abaikan jika audio clip yang dipilih sama dengan yang sudah aktif:
        if (_bgm.clip == _bgmClips[index])
            return;

        // Sisanya, jika audio clip yang dipilih berbeda, replace BGM dengan yang baru dipilih, kemudian Play BGM baru tersebut:
        _bgm.clip = _bgmClips[index];
        _bgm.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        // Method untuk menjalankan suara 1x saja:
        _sfx.PlayOneShot(clip);
    }
}
