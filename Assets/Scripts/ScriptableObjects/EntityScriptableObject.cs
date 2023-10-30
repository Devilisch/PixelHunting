using UnityEngine;

[CreateAssetMenu(fileName = "Entity", menuName = "ScriptableObjects/Entity", order = 2)]
public class EntityScriptableObject : ScriptableObject
{
    [SerializeField] private EntityType type;
    [SerializeField] private Sprite sprite;
    [SerializeField] private RuntimeAnimatorController animatorController;
    [SerializeField] private float hitStunTime;
    [SerializeField] private float speed;

    public EntityType Type => type;
    public Sprite Sprite => sprite;
    public RuntimeAnimatorController AnimatorController => animatorController;
    public float HitStunTime => hitStunTime;
    public float Speed => speed;
}
