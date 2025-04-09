using System;
using System.Collections;
using System.Linq;
using DaggerfallConnect;
using DaggerfallWorkshop.Game;
using DaggerfallWorkshop.Utility;
using DaggerfallWorkshop.Game.Utility.ModSupport;
using UnityEngine;

namespace BestiaryDEXCompat
{

    internal class TextFields
    {
        internal string MONTITLE  { get; private set; }
        internal string MONTITLE2 { get; private set; }
        internal string MONSUM    { get; private set; }
        internal string MONSUM2   { get; private set; }
        internal string ANTITLE   { get; private set; }
        internal string ANSUM     { get; private set; }
        internal string UNDTITLE  { get; private set; }
        internal string UNDSUM    { get; private set; }
        internal string DAETITLE  { get; private set; }
        internal string DAESUM    { get; private set; }
        internal string GOLTITLE  { get; private set; }
        internal string GOLSUM    { get; private set; }

        internal TextFields(Mod mod)
        {
            MONTITLE  = mod.Localize("MONTITLE");
            MONTITLE2 = mod.Localize("MONTITLE2");
            MONSUM    = mod.Localize("MONSUM");
            MONSUM2   = mod.Localize("MONSUM2");
            ANSUM     = mod.Localize("ANSUM");
            ANTITLE   = mod.Localize("ANTITLE");
            UNDSUM    = mod.Localize("UNDSUM");
            UNDTITLE  = mod.Localize("UNDTITLE");
            DAESUM    = mod.Localize("DAESUM");
            DAETITLE  = mod.Localize("DAETITLE");
            GOLSUM    = mod.Localize("GOLSUM");
            GOLTITLE  = mod.Localize("GOLTITLE");
        }
    }

    public class BestiaryDEXCompat : MonoBehaviour
    {
        internal TextFields TextFields { get; private set; }
        static Mod mod;

        [Invoke(StateManager.StateTypes.Start, 0)]
        public static void Init(InitParams initParams)
        {
            mod = initParams.Mod;
            var go = new GameObject(mod.Title);
            go.AddComponent<BestiaryDEXCompat>();
        }

        void Awake()
        {
            TextFields = new TextFields(mod);
            // Animal page
            // Looks kinda dumb, but I tried registering all the entities at once but it didn't work until I split it
            ModManager.Instance.SendModMessage("Bestiary", "REGISTER_CUSTOM_ENTITY", new object[] { "Wolf", "SnowWolf", "BloodSpider", "MountainLion", "Boar", "Mudcrab", "Dog" });
            ModManager.Instance.SendModMessage("Bestiary", "REGISTER_CUSTOM_PAGE", new object[] { TextFields.ANTITLE, TextFields.ANSUM, new string[] { "Wolf",
            "SnowWolf", "BloodSpider", "MountainLion", "Boar", "Mudcrab", "Dog" } });

            // Monster pages
            ModManager.Instance.SendModMessage("Bestiary", "REGISTER_CUSTOM_ENTITY", new object[] { "Goblin", "Homunculus", "Lizardman", "LizardmanWarrior", "Grotesque",
            "Minotaur", "Medusa", "Troll", "Ogre" });
            ModManager.Instance.SendModMessage("Bestiary", "REGISTER_CUSTOM_PAGE", new object[] { TextFields.MONTITLE, TextFields.MONSUM, new string[] { "Goblin", "Homunculus", "Lizardman", "LizardmanWarrior", "Grotesque", "Minotaur", "Medusa", "Troll", "Ogre" } });

            ModManager.Instance.SendModMessage("Bestiary", "REGISTER_CUSTOM_ENTITY", new object[] { "LandDreugh", "Wisp" });
            ModManager.Instance.SendModMessage("Bestiary", "REGISTER_CUSTOM_PAGE", new object[] { TextFields.MONTITLE2, TextFields.MONSUM2, new string[] { "LandDreugh", "Wisp" } });

            // Undead pages
            ModManager.Instance.SendModMessage("Bestiary", "REGISTER_CUSTOM_ENTITY", new object[] { "SkeletalSoldier", "GloomWraith", "FadedGhost", "Ghoul", "DireGhoul" });
            ModManager.Instance.SendModMessage("Bestiary", "REGISTER_CUSTOM_PAGE", new object[] { TextFields.UNDTITLE, TextFields.UNDSUM, new string[] { "SkeletalSoldier",
            "GloomWraith", "FadedGhost", "Ghoul", "DireGhoul" } });

            // Daedra pages
            ModManager.Instance.SendModMessage("Bestiary", "REGISTER_CUSTOM_ENTITY", new object[] { "HellHound", "FireDaemon", "Dremora", "Scamp" });
            ModManager.Instance.SendModMessage("Bestiary", "REGISTER_CUSTOM_PAGE", new object[] { TextFields.DAETITLE, TextFields.DAESUM, new string[] { "HellHound", "FireDaemon", "Dremora", "Scamp" } });

            // Golem pages
            ModManager.Instance.SendModMessage("Bestiary", "REGISTER_CUSTOM_ENTITY", new object[] { "IronGolem", "IceGolem", "StoneGolem", "DwarvenSphere", "DwarvenSteam" });
            ModManager.Instance.SendModMessage("Bestiary", "REGISTER_CUSTOM_PAGE", new object[] { TextFields.GOLTITLE, TextFields.GOLSUM, new string[] { "IronGolem", "IceGolem", "StoneGolem", "DwarvenSphere", "DwarvenSteam" } });

            mod.IsReady = true;
        }
    }
}
