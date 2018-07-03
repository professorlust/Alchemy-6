﻿using System;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public class Herb : Ingredient {
        [SerializeField]
        private Rarity rarity;
        [SerializeField]
        private Region[] regions;

        public Rarity Rarity {
            get { return rarity; }
        }

        public Region[] Regions {
            get { return regions; }
        }

        public Herb(string name, Effect[] effects, Rarity rarity, Region[] regions) : base(name, effects) {
            this.rarity = rarity;
            this.regions = regions;
        }
    }

    public enum Rarity {
        Common,
        Uncommon,
        Rare
    }

    public enum Region {
        Plains,
        Forest,
        Desert,
        Tundra
    }

    public class HerbEventArgs : EventArgs {
        private Herb herb;

        public Herb Herb {
            get { return herb; }
        }

        public HerbEventArgs(Herb herb) {
            this.herb = herb;
        }
    }
}