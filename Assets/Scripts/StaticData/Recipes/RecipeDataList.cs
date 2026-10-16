using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeDataList", menuName = "StaticData/RecipeDataList", order = 0)]
public class RecipeDataList : ScriptableObject
{
    public List<RecipeData> Recipes;
}
