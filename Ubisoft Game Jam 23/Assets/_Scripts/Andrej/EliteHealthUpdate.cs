using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

public class EliteHealthUpdate : MonoBehaviour
{
    public List<MeshRenderer> Cubes;
    public Material DamagedMaterial;
    public int Index;

    private void Start()
    {
        Index = 0;
    }

    public void DamageNextCube()
    {
        if(Index < Cubes.Count)
        {
            Cubes[Index].material = DamagedMaterial;
            Index++;
        }
    }
}
