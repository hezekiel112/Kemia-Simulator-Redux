using System;
using System.Collections;
using System.Collections.Generic;
using KemiaSimulatorCore.Script.Statics;
using UnityEngine;

namespace KemiaSimulatorCore.Script.Networking.Authentication
{
    /// <summary>
    ///     Ce script gère les fonctionnalités sign-in et sign-up
    /// </summary>
    public class KSAuthentificationHandler : MonoBehaviour
    {
        private void Awake(){
            this.kslog(SystemInfo.deviceUniqueIdentifier);
        }
    }
}
