using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class Player {
        public static Character[] CHARACTERS = Assets.Data.Assets.characters;

        public static int currentID = -1;
        public static int currentY = 2;

        public static int currentCharacterID = 0;

        public static float Health;
        public static float Hunger;
        public static float Water;

        public static void SelectPlayer(int characterID) {
            if (characterID < CHARACTERS.Length)
                currentCharacterID = characterID;
            else return;

            Character character = GetCurrentCharacter();

            Health = character.maxHealth;
            Hunger = character.maxHunger;
            Water = character.maxWater;
        }

        public static Character GetCurrentCharacter() {
            return CHARACTERS[currentCharacterID];
        }

        public static Character GetCharacterByIndex(int index) {
            return CHARACTERS[index];
        }
    }
}
