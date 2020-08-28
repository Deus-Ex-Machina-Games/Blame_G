using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Transactions;
using UnityEngine;

namespace LevelGenerator {
    public class Foundations {
        public static Foundation[] FOUNDATIONS = {};

        public static void LoadFoundations() {
            FOUNDATIONS = Resources.Load<FoundationsScriptable>("Data/FOUNDATIONS").FOUNDATIONS;
        }
    }

    [System.Serializable]
    public class Level {
        public int x = 0, y = 0, height = 3, width = 8;
        public List<List<Chunk>> chunks = new List<List<Chunk>> { };
        
        public Level(int getHeight, int getWidth) {
            height = getHeight; width = getWidth;
        }

        public void GenerateLevel() {
            for (int y = 0; y < height; y++) {
                chunks.Add(new List<Chunk> { });
                for (int x = 0; x < width; x++) {
                    Chunk chunk = new Chunk(16, 16, x, y);
                    chunks[y].Add(chunk);
                }
            }
        }

        public string GetLevelString() {
            string t_string = "";

            for (int y = 0; y < chunks.Count; y++) {
                for (int x = 0; x < chunks[y].Count; x++) {
                    t_string += $"{chunks[y][x].GetChunkString()}";
                }

                t_string += "\n";
            }

            return t_string;
        }
    }

    [System.Serializable]
    public class Room {
    	public int x, y;
    	public Foundation type;
    	public string status;
    	public int enemies;
    	
    	public Room(int getX, int getY, Foundation getType) {
    		x = getX; y = getY;
    		type = getType;
    	}
    }
    

    [System.Serializable]
    public class Chunk {
    	public System.Random random = new System.Random();
    	public int width = 4, height = 3;
    	public List<List<Room>> rooms = new List<List<Room>> {};
        public int x = 0, y = 0;
    	
    	public Chunk(int getWidth, int getHeight, int getX, int getY) {
    		width = getWidth; height = getHeight;
            x = getX; y = getY;
    		// rooms = objects;
    		
    		GenerateRooms();
    		GenerateDamage();
    		GenerateEnemies();
    	}
  
    	public void GenerateRooms() {
    		for (int y = 0; y < height; y++) {
    			rooms.Add(new List<Room> {});
        		for (int x = 0; x < width; x++) {
        			Foundation foundation = Foundations.FOUNDATIONS[random.Next(Foundations.FOUNDATIONS.Length)];
        			int chance = random.Next(100);
        			
        			if (chance >= foundation.chance)
        				rooms[y].Add(new Room(x, y, Foundations.FOUNDATIONS[0]));
        			else
        				rooms[y].Add(new Room(x, y, foundation));
        		}
        	}
        }
        
        public void GenerateDamage() {
    		for (int y = 0; y < height; y++) {
        		for (int x = 0; x < width; x++) {
        			int chance = random.Next(100);
        			
        			if (chance < rooms[y][x].type.brokenChance)
        				rooms[y][x].status = "b";
        			else
        				rooms[y][x].status = "n";
        		}
        	}
        }
        
        public void GenerateEnemies() {
    		for (int y = 0; y < height; y++) {
        		for (int x = 0; x < width; x++) {
        			int count = random.Next(rooms[y][x].type.minEnemies, rooms[y][x].type.maxEnemies + 1);
        			if (rooms[y][x].status == "b")	rooms[y][x].enemies = count;
        		}
        	}
        }
        
        public string GetJson() {
        	return JsonConvert.SerializeObject(rooms);
        }
        
        public void SetRooms(string json) {
        	rooms = JsonConvert.DeserializeObject<List<List<Room>>>(json);
        }
        
        public string GetChunkString() {
        	string level = "";
        	
        	for (int y = 0; y < rooms.Count; y++) {
        		for (int x = 0; x < rooms[y].Count; x++) {
        			level += $"| {rooms[y][x].type.symbol} {rooms[y][x].status} {rooms[y][x].enemies} |";
        		}
        		
        		level += "\n";
        	}
        	
        	return level;
        }
    }
}