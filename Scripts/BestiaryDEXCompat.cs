using System;
using System.Collections;
using System.Collections.Generic;
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
        internal string MMONTITLE { get; private set; }
        internal string MONSUM    { get; private set; }
        internal string MMONSUM   { get; private set; }
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
            MMONTITLE = mod.Localize("MMONTITLE");
            MONSUM    = mod.Localize("MONSUM");
            MMONSUM   = mod.Localize("MMONSUM");
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

    internal class BestiaryGroup
    {
        public string[] Entities;
        public string Title;
        public string Summary;

        public BestiaryGroup(string[] entities, string title, string summary)
        {
            Entities = entities;
            Title = title;
            Summary = summary;
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

            var bestiaryGroups = new BestiaryGroup[]
            {
                new BestiaryGroup(new[] { "NormalBat", "Boar", "BloodSpider", "Dog", "MountainLion", "Mudcrab", "SnowWolf", "Wolf" }, TextFields.ANTITLE, TextFields.ANSUM),
                new BestiaryGroup(new[] { "Goblin", "Homunculus", "Lizardman", "LizardmanWarrior", "Grotesque", "Minotaur", "Medusa", "Troll", "Ogre" }, TextFields.MONTITLE, TextFields.MONSUM),
                new BestiaryGroup(new[] { "LandDreugh", "Wisp" }, TextFields.MMONTITLE, TextFields.MMONSUM),
                new BestiaryGroup(new[] { "SkeletalSoldier", "GloomWraith", "FadedGhost", "Ghoul", "DireGhoul" }, TextFields.UNDTITLE, TextFields.UNDSUM),
                new BestiaryGroup(new[] { "HellHound", "FireDaemon", "Dremora", "Scamp" }, TextFields.DAETITLE, TextFields.DAESUM),
                new BestiaryGroup(new[] { "IronGolem", "IceGolem", "StoneGolem", "DwarvenSphere", "DwarvenSteam" }, TextFields.GOLTITLE, TextFields.GOLSUM),
            };

            foreach (var group in bestiaryGroups)
            {
                string[] entities = group.Entities;
                string title = group.Title;
                string summary = group.Summary;
                ModManager.Instance.SendModMessage("Bestiary", "REGISTER_CUSTOM_ENTITY", entities);
                ModManager.Instance.SendModMessage("Bestiary", "REGISTER_CUSTOM_PAGE", new object[] { title, summary, entities });
            }

            mod.IsReady = true;
        }
    }
}
