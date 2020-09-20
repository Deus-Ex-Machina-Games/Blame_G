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
                    case "damage":
                        string _parameter = _strings[1];
                        float _damage = float.Parse(_strings[2]);
                        CommandsFunctions.PlayerDamage(_parameter, _damage);
                        DLog("DAMAGE COMPETE", "RESULT");
                        break;
                    case "position":
                        DLog($"POSITION: {{ X: {PlayerController.Internal.transform.position.x}, Y: {PlayerController.Internal.transform.position.y} }}", "RESULT");
                        break;
                    case "spawn":
                        string _type = _strings[1];
                        _parameter = _strings[2];
                        float _x = float.Parse(_strings[3]);
                        float _y = float.Parse(_strings[4]);
                        CommandsFunctions.Spawn(_type, _parameter, new Vector2(_x, _y));
                        DLog("SPAWN COMPETE", "RESULT");
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

        public static void PlayerDamage(string parameter, float damage) {
            Game.Player.Damage(parameter, damage);
        }

        public static void Spawn(string type, string name, Vector2 position) {
            switch (type) {
                case "enemy":
                    SceneGenerator.Internal.GenerateSpawnEnemy(name, position);
                    break;
                default: break;
            }
            
        }
    }
}
