using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constans 
{
    public const int maleNamesLength = 135;
    public const int femaleNamesLength = 196;
    public const int surnamesLength = 154;

    public const int maleModels = 14;
    public const int femaleModels = 12;

    public const int basePayPerPoint = 15;

    public const int maxWorkers = 50;

    public static readonly int[] DaysInMonth =  { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    public const int maxAvailableWorkers = 30;
    public const float AvailableFindWorkChance = 0.015f;
    public const float AvailableAppearChance = 0.5f;
    public const int minSkill = 5;
    public const int minSalary = 1000;

    public static readonly Color GreenColor = new Color(0.58f, 0.86f, 0.58f);
    public static readonly Color GrayBgInactive = new Color(0.44f, 0.44f, 0.44f);
    public static readonly Color GrayBgActive = new Color(0.49f, 0.49f, 0.49f);
    public static readonly Color WhiteTextInactive = new Color(0.83f, 0.83f, 0.83f);
    public static readonly Color WhiteTextActive = new Color(1, 1, 1);
    public static readonly Color BlackTextInactive = new Color(0.2f, 0.2f, 0.2f);
    public static readonly Color BlackTextActive = new Color(0, 0 ,0);

    public static readonly string[] gameSizes = { "Indie", "C", "B", "A", "AA", "AAA"};
    public static readonly int[] sizesScale = { 1, 5, 10, 20, 50, 100};
    public static readonly int[] sizesPrices = { 7, 10, 20, 30, 50, 75};

    public static readonly string[] gameNames = { " King Survival", " Bill the Billnes", " Light System", " Special Rage", " Knockout of Betrayal", " Raid of Liberty", " Extinction and Year", " Hell and Dragons", " Bladegene", " Datacraft", " Farcraft", " Battlerush", " Combat Operation", " Alien Curse", " Crash of Liberty", " Raid of Rescue", " Contract and Paradise", " Salvation and Conflict", " Demonside", " Grimcraft", " Battlelight", " Dynaplan", " Mass Panic", " Human Guardians", " Plan of Rule", " Plan of Heroes", " Shadow and Hunt", " Fate and Disorder", " Dreadnite", " Dragonland", " Deadmind", " Warcore", " Covert Warfare", " Lifeless Guardians", " Run of Atonement", " Protect of Execution", " House and Command", " Victory and Hell", " Farbot", " Backwind", " Farcry", " Airreign", " Crimson Voyage", " Final Bounty", " Kill of Heroes", " Besiege of Stipulation", " Knights and Galaxy", " Duty and Vampires", " Masterflight", " Biolight", " Biosky", " Bordermind", " Broken Haven", " Saint Asylum", " Retaliate of Combat", " Kill of Autonomy", " Day and Destruction", " Stealth and Victory", " Antiwind", " Astrolife", " Archereign", " Cybercraze", " Forbidden Secrets", " Phantom Shadows", " Vikings of Revelations", " Knights of Nightmares", " Shadows and Monsters", " Evidence and Blood", " Heavendrift", " Everrite", " Madkin", " Questflight", " War Memory", " Project Venture", " War of Sins", " Codes of Hauntings", " Angel and Age", " Infinity and Death", " Heroheart", " Dynaland", " Alterblaze", " Mortalrealm", " Blood Blade", " Banished Memory", " Monsters of Danger", " Spells of Rituals", " Search and Sword", " Mystery and Hope", " Dreammind", " Magicbot", " Deadmare", " Borderway", " Sacred Inferno", " Rune Universe", " Swords of Oracles", " Werewolves of Sins", " Dominion and Century", " Autumn and Stars", " Everborne", " Nighthunt", " Nightfire", " Stronghunt", " Eternal Throne", " Bloodline Desciple", " Shields of Runes", " Kings of Darkness", " Voice and Blade", " Stars and Guilds", " Spellheart", " Shadowfire", " Warrite", " Runekin", " Nuclear Kingdom", " Chaos Control", " Helicopters of Realms", " Creatures of Guns", " Gods and Adventures", " Worlds and Androids", " Miningstrike", " Battlefront", " Trainclad", " Tankville", " Drive Sim", " Police Sim", " Zombies of Explorations", " Stars of Victory", " Village and Champions", " Empires and Destiny", " Knightraid", " Archecraft", " Miningstrike", " Mechaville", " Car Creator", " Combat Force", " Zombies of Programs", " Gods of Formations", " Monsters and Days", " Power and Freedom", " Jettech", " Knightpath", " Fightfront", " Foodfront", " Snow Sports Games", " Bow Top Shots", " Professionals of Games", " Victors of Championships", " Shots and Coach", " Grand Slams and World Class", " Rugbyace", " Rugbymax", " Basketstar", " Basketballstar", " Big Game Heroes", " Angler Unleashed", " Legends of Playoffs", " Rookies of Major Leagues", " Rush and World Cup", " Winter Sports and Street Racing", " Freestylepros", " Punchpro", " Fieldmaster", " Fieldpros", " Immortal Ascension", " Mortal Liberation", " Legends of Dominion", " Gates of Turmoil", " Castles and Angels", " Fortune and Carnage", " Datarealm", " Clanlife", " Deadage", " Clanbot", " Galactic Prophecy", " Civil Laws", " Bullets of Dominion", " Dungeons of Defeat", " Angels and Legions", " War and Life", " Jetfight", " Skytide", " Starflight", " Netherspace", " Naval Retaliation", " Clan Vengeance", " Armies of Ruins", " Band of Fate", " Spirits and Titans", " Retaliation and Chaos", " Bulletrise", " Skyslayer", " Fleetfire", " Ironway", " Enemy Vengeance", " Castle Nemesis", " Rise of Carnage", " Lord of Liberation", " Soldiers and Guardians", " Mutiny and Liberation", " Clanrage", " Witchstate", " Magecommand", " Gearguild" };

    public const int contractPaymentPerScore = 250000;

    public const int PointsPool = 100;

    public const int EndYear = 2024;

    public const int publisherCount = 10;
    public const int maxPublisherAuditory = 25000;

    public const float hypeDecrease = 0.0035f;
    public const float interestDecreaseSpeed = 0.05f;
}