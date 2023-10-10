using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(
    fileName = "Player Progress",
    menuName = "Game Kuis/Player Progress")]
public class PlayerProgress : ScriptableObject
{
    [System.Serializable]
    public struct MainData
    {
        public int koin;
        public Dictionary<string, int> progressLevel;
    }

    [SerializeField]
    private string _filename = "Save1.txt";

    [SerializeField]
    private string _startingLevelPackName = string.Empty;

    public MainData progressData = new MainData();

    // Method untuk men-save File
    public void SimpanProgress()
    {
        //// Sampel Data
        ////progressData.koin = 200;
        //if (progressData.progressLevel == null)
        //    progressData.progressLevel = new();
        //progressData.progressLevel.Add("Level Pack 1", 3);
        //progressData.progressLevel.Add("Level Pack 3", 5);

        // Simpan Starting Data saat obyek Dictionary belum ada saat Player Progress dimuat/di-load
        // Sebagai data awal "New Game" jika player belum mempunyai save file sama sekali:
        if (progressData.progressLevel == null)
        {
            progressData.progressLevel = new();
            progressData.koin = 0;
            progressData.progressLevel.Add(_startingLevelPackName, 1);
        }

// Informasi penyimpanan data, untuk menentukan path penyimpanan data di berbagai platform:
// elif = singkatann dari else if
// endif untuk menutup blok kodingan pre-proses, yang dijalankan 1x saat build game,
    // untuk memangkas script coding yang perlu dibuang dan dikompilasikan
    // contoh: Saat sudah jadi software APK di Andro, baris directory = dataPath dihapus
#if UNITY_EDITOR
        string directory = Application.dataPath + "/Progress/"; 
#elif (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        string directory = Application.persistentDataPath + "/Progress/";
#endif
        var path = directory + "/" + _filename;

        //Membuat file baru
        // Jika file belum ada sama sekali, maka buat file baru.
        if (File.Exists(path))
        {
            File.Create(path).Dispose();
            Debug.Log("a new Save File has been created: " + path);
        }

        // Menyimpan data ke dalam file menggunakan binari formatter
        var fileStream = File.Open(path, FileMode.OpenOrCreate);
        // BF
        var formatter = new BinaryFormatter();

        fileStream.Flush();
        // BF
        formatter.Serialize(fileStream, progressData);

        ////===== BW
        //// \n ditulis untuk menambahkan baris baru, atau sama dengan pencet "Enter" pada keyboard.
        //var konten = $"{progressData.koin}\n";

        //// Menyimpan data ke dalam file menggunakan binari writer
        //var writer = new BinaryWriter(fileStream);

        //writer.Write(progressData.koin);
        //foreach (var i in progressData.progressLevel)
        //{
        //    writer.Write(i.Key);
        //    writer.Write(i.Value);
        //}

        //// Putuskan aliran memori dengan File menggunakan Dispose, untuk menghindari memory leak.
        //writer.Dispose();

        //foreach (var i in progressData.progressLevel)
        //{
        //    konten += $"{i.Key} {i.Value}\n";
        //}

        //File.WriteAllText(path, konten);
        ////===== BW

        // Putuskan aliran memori dengan File menggunakan Dispose, untuk menghindari memory leak.
        fileStream.Dispose();

        Debug.Log($"{_filename} has been created.");
    }


    // Method untuk me-load Save File yang sudah ada
    public bool MuatProgress()
    {
// Informasi penyimpanan data, untuk menentukan path penyimpanan data di berbagai platform:
#if UNITY_EDITOR
        string directory = Application.dataPath + "/Progress/"; 
#elif (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        string directory = Application.persistentDataPath + "/Progress/";
#endif
        var path = directory + "/" + _filename;

        // Membuat Directory Temporary
        // Jika directory belum ada sama sekali, maka buat directory baru.
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("a new Directory has been created: " + directory);
        }

        var fileStream = File.Open(path, FileMode.OpenOrCreate);

        try
        {
            //// ===== BR
            //var reader = new BinaryReader(fileStream);

            //try
            //{
            //    progressData.koin = reader.ReadInt32();

            //if (progressData.progressLevel == null)
            //{
            //    // progressData.progressLevel = new Dictionary<string, int>();
            //    // Tapi karena penulisan C# sekarang sudah makin canggih, jadi begini saja cukup:
            //    progressData.progressLevel = new();
            //}

            //    // Me-read-nya memang menggunakan Method PeekChar() ini:
            //    // Method ini digunakan untuk memastikan Reader tidak memajukkan posisi aliran (stream) agar semua isi konten File dibaca. 
            //    while (reader.PeekChar() != -1)
            //    {
            //        // Mengecek nama level
            //        var namaLevelPack = reader.ReadString();
            //        // Mengecek angka level (memakai tipe Int32 saja cukup)
            //        var levelKe = reader.ReadInt32();
            //        // Data-data yang sudah di-read dimasukan ke dalam Dictionary()
            //        progressData.progressLevel.Add(namaLevelPack, levelKe);
            //        // Mengecek apakah sudah berhasil dimuat ke dalam Dictionary()
            //        Debug.Log($"{namaLevelPack}:{levelKe}");
            //    }

            //    // Putuskan aliran memori dengan file menggunakan dispose, kali ini memberhentikan Reader-nya.
            //    reader.Dispose();
            //}
            //catch (System.Exception e)
            //{
            //    // Putuskan aliran memori dengan file menggunakan dispose
            //    reader.Dispose();
            //    // Sekalian fileStream-nya juga ditutup, untuk berjaga-jaga karena yang buat tutorialnya saja khawatir.
            //    fileStream.Dispose();

            //    Debug.Log($"ERROR: Terjadi kesalahan saat memuat progress binari\n{e.Message}");

            //    return false;
            //}
            //// ===== BR

            // ===== BF
            // Memuat data dari file menggunakan binari formatter
            var formatter = new BinaryFormatter();

            progressData = (MainData)formatter.Deserialize(fileStream);
            // ===== BF

            // Putuskan aliran memori dengan file menggunakan dispose
            fileStream.Dispose();

            Debug.Log($"Data Loaded: Koin: {progressData.koin}; Level Pack Unlocked: {progressData.progressLevel.Count}");

            return true;
        }
        catch (System.Exception e)
        {
            // Putuskan aliran memori dengan file menggunakan dispose
            fileStream.Dispose();

            Debug.Log($"ERROR: Cannot load progress data.\n" +
                $"No Save File detected\n" + 
                $"{e.Message}");

            return false;
        }
    }
}
