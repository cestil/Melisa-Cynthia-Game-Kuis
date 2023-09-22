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
    private string _filename = "contoh.txt";

    public MainData progressData = new MainData();

    public void SimpanProgress()
    {
        // Sampel Data
        progressData.koin = 200;
        if (progressData.progressLevel == null)
            progressData.progressLevel = new();
        progressData.progressLevel.Add("Level Pack 1", 3);
        progressData.progressLevel.Add("Level Pack 3", 5);

        // Informasi penyimpanan data
        var directory = Application.dataPath + "/Temporary";
        var path = directory + "/" + _filename;

        // Membuat Directory Temporary
        // Jika directory belum ada sama sekali, maka buat directory baru.
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory has been Created: " + directory);
        }

        //Membuat file baru
        // Jika file belum ada sama sekali, maka buat file baru.
        if (File.Exists(path))
        {
            File.Create(path).Dispose();
            Debug.Log("File created: " + path);
        }

        // \n ditulis untuk menambahkan baris baru, atau sama dengan pencet "Enter" pada keyboard.
        var konten = $"{progressData.koin}\n";

        // Menyimpan data ke dalam file menggunakan binari formatter
        var fileStream = File.Open(path, FileMode.OpenOrCreate);
        //var formatter = new BinaryFormatter();

        fileStream.Flush();
        //formatter.Serialize(fileStream, progressData);

        //===== BW
        // Menyimpan data ke dalam file menggunakan binari writer
        var writer = new BinaryWriter(fileStream);

        writer.Write(progressData.koin);
        foreach (var i in progressData.progressLevel)
        {
            writer.Write(i.Key);
            writer.Write(i.Value);
        }

        foreach (var i in progressData.progressLevel)
        {
            konten += $"{i.Key} {i.Value}\n";
        }

        File.WriteAllText(path, konten);
        //=====

        // Putuskan aliran memori dengan File menggunakan Dispose, untuk menghindari memory leak.
        // writer untuk BW, fileStream untuk BF?
        writer.Dispose();
        fileStream.Dispose();

        Debug.Log($"{_filename} berhasil disimpan.");
    }


    public bool MuatProgress()
    {
        // Informasi penyimpanan data
        var directory = Application.dataPath + "/Temporary";
        var path = directory + "/" + _filename;

        var fileStream = File.Open(path, FileMode.OpenOrCreate);

        try
        {
            var reader = new BinaryReader(fileStream);

            try
            {
                progressData.koin = reader.ReadInt32();
                if (progressData.progressLevel == null)
                    // progressData.progressLevel = new Dictionary<string, int>();
                    // Tapi karena penulisan C# sekarang sudah makin canggih, jadi begini saja cukup:
                    progressData.progressLevel = new();

                // Me-read-nya memang menggunakan Method PeekChar() ini:
                // Method ini digunakan untuk memastikan Reader tidak memajukkan posisi aliran (stream) agar semua isi konten File dibaca. 
                while (reader.PeekChar() != -1)
                {
                    // Mengecek nama level
                    var namaLevelPack = reader.ReadString();
                    // Mengecek angka level (memakai tipe Int32 saja cukup)
                    var levelKe = reader.ReadInt32();
                    // Data-data yang sudah di-read dimasukan ke dalam Dictionary()
                    progressData.progressLevel.Add(namaLevelPack, levelKe);
                    // Mengecek apakah sudah berhasil dimuat ke dalam Dictionary()
                    Debug.Log($"{namaLevelPack}:{levelKe}");
                }

                // Putuskan aliran memori dengan file menggunakan dispose, kali ini memberhentikan Reader-nya.
                reader.Dispose();
            }
            catch (System.Exception e)
            {
                // Putuskan aliran memori dengan file menggunakan dispose
                reader.Dispose();
                // Sekalian fileStream-nya juga ditutupp, untuk berjaga-jaga karena yang buat tutorialnya saja khawatir.
                fileStream.Dispose();

                Debug.Log($"ERROR: Terjadi kesalahan saat memuat progress binari\n{e.Message}");

                return false;
            }

            //// Memuat data dari file menggunakan binari formatter
            //var formatter = new BinaryFormatter();

            //progressData = (MainData)formatter.Deserialize(fileStream);

            // Putuskan aliran memori dengan file menggunakan dispose
            fileStream.Dispose();

            Debug.Log($"{progressData.koin}; {progressData.progressLevel.Count}");

            return true;
        }
        catch (System.Exception e)
        {
            // Putuskan aliran memori dengan file menggunakan dispose
            fileStream.Dispose();

            Debug.Log($"ERROR: Terjadi kesalahan saat memuat progress\n{e.Message}");

            return false;
        }
    }
}
