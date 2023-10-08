using UnityEngine;

[CreateAssetMenu(
    fileName = "Level Pack Baru",
    menuName = "Game Kuis/Level Pack Kuis")]
public class LevelPackKuis : ScriptableObject
{
    [SerializeField]
    private LevelSoalKuis[] _isiLevel = new LevelSoalKuis[0];

    public int BanyakLevel => _isiLevel.Length;

    public LevelSoalKuis AmbilLevelKe(int index)
    {
        return _isiLevel[index];
    }

    [SerializeField]
    private int _harga = 0;

    // Property Getter saja agar obyek di luar hanya dapet membaca informasi saja.
    public int Harga => _harga;
}
