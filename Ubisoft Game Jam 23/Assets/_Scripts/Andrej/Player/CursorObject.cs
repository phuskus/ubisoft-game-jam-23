using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zmijoguz;

public class CursorObject : SingletonMono<CursorObject>
{
    private void Start()
    {
        transform.SetParent(null);
    }
}
