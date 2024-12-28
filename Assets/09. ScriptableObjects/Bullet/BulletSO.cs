using UnityEngine;
using EnumTypes;

[CreateAssetMenu(fileName = "BulletSO", menuName = "Scriptable Objects/BulletSO")]
public class BulletSO : ScriptableObject
{
    public BulletType bulletType;
    public int damege;
}
