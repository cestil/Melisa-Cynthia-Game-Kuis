using UnityEngine;

[CreateAssetMenu(
    fileName = "Inisial Data Gameplay",
    menuName = "Game Kuis/Inisial Data Gameplay")]
public class InisialDataGameplay : ScriptableObject
{
    public LevelPackKuis levelPack = null;
    public int levelIndex = 0;
}
