using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System;

namespace Game.Console {
    public class DebugConsole {
        public static string Log = "[INITIALIZATION]\n";

        public static void DLog(string log, string pref = "LOG") {
            Log += $"[{pref}] {log}\n";
        }

        public static void EnterCommand(string command) {
            string[] _strings = command.Split(' ');
            string name = _strings[0];

            DLog(command, "ENTER");

            try {
                switch (name) {
                    case "additem":
                        string _id = _strings[1];
                        int _count = int.Parse(_strings[2]);
                        CommandsFunctions.AddItem(_id, _count);
                        DLog("ADDITEM COMPETE", "RESULT");
                        break;
                    default: break;
                }
            } catch (Exception exception) {
                DLog($"{exception.Message}", "ERROR");
            }
        }
    }


    public class CommandsFunctions {
        public static void AddItem(string id, int count) {
            Game.Player.inventory.AppendItem(id, count);
        }
    }
}
