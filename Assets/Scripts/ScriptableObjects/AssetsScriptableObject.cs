using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Assets", menuName = "ScriptableObjects/Assets", order = 5)]
public class AssetsScriptableObject : ScriptableObject {
    public Character[] characters = { };
    public Foundation[] foundations = { };
}
