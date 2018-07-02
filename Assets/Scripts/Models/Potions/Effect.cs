﻿using System;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public class Effect : ICloneable {
        [SerializeField]
        string name;
        [SerializeField]
        int magnitude;

        public Effect(string name, int magnitude) {
            this.name = name;
            this.magnitude = magnitude;
        }

        public string Name {
            get { return name; }
        }

        public int Magnitude {
            get { return magnitude; }
        }

        public virtual object Clone() {
            var clone = (Effect)MemberwiseClone();

            return clone;
        }

        public Effect Combine(Effect other) {
            if (Name == other.Name) {
                var clone = (Effect)Clone();

                clone.magnitude = magnitude + other.magnitude;

                return clone;
            }

            return null;
        }
    }

    public class EffectEventArgs : EventArgs {
        public Effect effect { get; set; }
        public Ingredient ingredient { get; set; }
    }
}