using UnityEngine;

[CreateAssetMenu(fileName = "FruitInfo", menuName = "ScriptableObjects/FruitInfo", order = 1)]
public class FruitInfo : ScriptableObject
{
    [SerializeField] private FruitType type;
    [SerializeField] private Sprite sprite;
    [SerializeField] private RuntimeAnimatorController animatorController;
    [SerializeField] private int points;
    [SerializeField] private int countOnStage;

    public FruitType Type => type;
    public int Points => points;
    public int CountOnStage => countOnStage;
    public Sprite Sprite => sprite;
    public RuntimeAnimatorController AnimatorController => animatorController;
}
