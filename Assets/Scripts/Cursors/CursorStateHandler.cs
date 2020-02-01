using System.Collections.Generic;
using UnityEngine;

namespace DapperDino.GGJ2020.Cursors
{
    public class CursorStateHandler : MonoBehaviour
    {
        private static readonly List<CursorBlockerBehaviour> blockers = new List<CursorBlockerBehaviour>();

        private void Start() => UpdateCursorState();

        public static void AddBlocker(CursorBlockerBehaviour blocker)
        {
            blockers.Add(blocker);

            UpdateCursorState();
        }

        public static void RemoveBlocker(CursorBlockerBehaviour blocker)
        {
            blockers.Remove(blocker);

            UpdateCursorState();
        }

        private static void UpdateCursorState()
        {
            if (blockers.Count > 0)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                return;
            }

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
