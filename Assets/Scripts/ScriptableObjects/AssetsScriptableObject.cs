using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Assets", menuName = "ScriptableObjects/Assets", order = 5)]
public class AssetsScriptableObject : ScriptableObject {
    public CCharacter[] characters = { };
    public CFoundation[] foundations = { };
    public CItem[] items = { };
}
