using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ExiteMouse : MonoBehaviour
{
   [SerializeField] private MenuStart _menuStart;
   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         _menuStart.SartScene(0);
      }
   }
}
