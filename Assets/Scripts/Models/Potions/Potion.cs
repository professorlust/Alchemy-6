﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models {
    [Serializable]
    public class Potion {
        [SerializeField]
        string name;
        [SerializeField]
        Flask flask;
        [SerializeField]
        Solvent solvent;
        [SerializeField]
        Herb[] herbs;
        [SerializeField]
        Effect[] effects;
        [SerializeField]
        float value;

        public Potion(Flask flask, Solvent solvent, Ingredient[] ingredients) {
            this.name = "";

            this.flask = flask;

            this.solvent = /*(Solvent)*/solvent/*.Clone()*/;

            var herbs = new List<Herb>();
            // var otherTypes = new List<OtherType>();

            foreach (var ingredient in ingredients) {
                if (ingredient is Herb) {
                    herbs.Add((Herb)ingredient.Clone());
                }
                // if (ingredients[i] is OtherType)
                // {
                //     otherTypes.Add((OtherType)ingredients[i].Clone());
                // }
            }

            this.herbs = herbs.ToArray();
            //this.otherTypes = otherTypes.ToArray();

            var effectsToCheck = new Queue<Effect>();

            foreach (var ingredient in ingredients) {
                foreach (var effect in ingredient.Effects) {
                    effectsToCheck.Enqueue(effect);
                }
            }

            var effects = new List<Effect>();

            while (effectsToCheck.Count > 0) {
                var currentEffect = effectsToCheck.Dequeue();

                foreach (var effect in effectsToCheck) {
                    var combinedEffect = currentEffect.Combine(effect);

                    if (combinedEffect != null) {
                        effects.Add(combinedEffect);
                    }
                }
            }

            this.effects = effects.ToArray();

            var prefix = flask.Quality.ToString();
            var name = "Potion of ";

            for (int i = 0; i < this.effects.Length; i++) {
                name += this.effects[i].Name;

                if (i < this.effects.Length - 1) {
                    name += ", ";
                }
            }

            this.name = prefix + " " + name;

            value = flask.Value * ingredients.Length;
        }

        public string Name {
            get { return name; }
        }

        public Flask Flask {
            get { return flask; }
        }

        public Solvent Solvent {
            get { return solvent; }
        }

        public Herb[] Herbs {
            get { return herbs; }
        }

        public int IngredientCount {
            get { return herbs.Length; }
        }

        public Effect[] Effects {
            get { return effects; }
        }

        public float Value {
            get { return value; }
        }
    }

    public class PotionEventArgs : EventArgs {
        public Potion potion { get; set; }
        public Employee employee { get; set; }
    }
}