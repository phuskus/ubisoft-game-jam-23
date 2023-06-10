using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zmijoguz
{
    //Example: public class GameSettings : SingletonSO<GameSettings>
    //Create "Recourses" folder in the root folder and place the ScriptableObject inside
    public class SingletonSO<T> : ScriptableObject where T : SingletonSO<T>
    {
        private static T instance;
        private static T[] foundAssets;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    foundAssets = Resources.LoadAll<T>("");

                    if (foundAssets == null || foundAssets.Length < 1) Debug.LogError(nameof(T) + " not found!");
                    else
                    {
                        if (foundAssets.Length > 1) Debug.LogWarning("Multiple " + nameof(T) + " found! \n" + foundAssets[0].name + " has been selected.");

                        instance = foundAssets[0];
                    }
                }
                return instance;
            }
        }
    } 
} //namespace end


