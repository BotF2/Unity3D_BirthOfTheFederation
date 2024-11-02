
using UnityEngine;
using Assets.Core;

[CreateAssetMenu(menuName = "Galaxy/PlayerTargetSO")]
public class PlayerDefinedTargetSO : ScriptableObject
{

    public int CivIndex;
    public Sprite Insignia;
    public CivEnum CivOwnerEnum;
    public string Name;
    public string Description;
}
