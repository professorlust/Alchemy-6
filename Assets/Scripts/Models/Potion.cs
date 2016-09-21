﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Models
{
    [Serializable]
    public class Potion
    {
        [SerializeField]string _name;
        [SerializeField]Flask _flask;
        [SerializeField]Solvent _solvent;
        [SerializeField]Herb[] _herbs;
        [SerializeField]Effect[] _effects;
        [SerializeField]float _value;

        public string Name
        {
            get { return _name; }
        }

        public Flask Flask
        {
            get { return _flask; }
        }

        public Solvent Solvent
        {
            get { return _solvent; }
        }

        public Herb[] Herbs
        {
            get { return _herbs; }
        }

        public int IngredientCount
        {
            get { return _herbs.Length; }
        }

        public Effect[] Effects
        {
            get { return _effects; }
        }

        public float Value
        {
            get { return _value; }
        }

        public Potion(Flask flask, Solvent solvent, Ingredient[] ingredients)
        {
            _flask = (Flask)flask.Clone();
            _solvent = /*(Solvent)*/solvent/*.Clone()*/;
            var herbs = new List<Herb>();
            for (int i = 0; i < ingredients.Length; i++)
            {
                if (ingredients[i] is Herb)
                {
                    herbs.Add((Herb)ingredients[i].Clone());
                }
            }
            _herbs = herbs.ToArray();

            var effectsToCheck = new Queue<Effect>();
            for (int i = 0; i < ingredients.Length; i++)
            {
                for (int j = 0; j < ingredients[i].Effects.Length; j++)
                {
                    effectsToCheck.Enqueue(ingredients[i].Effects[j]);
                }
            }

            var effects = new List<Effect>();
            while (effectsToCheck.Count > 0)
            {
                var currentEffect = effectsToCheck.Dequeue();

                foreach (var effect in effectsToCheck)
                {
                    var combinedEffect = currentEffect.Combine(effect);

                    if (combinedEffect != null)
                    {
                        effects.Add(combinedEffect);
                    }
                }
            }

            _effects = new Effect[effects.Count];
            for (int i = 0; i < effects.Count; i++)
            {
                _effects[i] = (Effect)effects[i].Clone();
            }

            var prefix = Flask.Quality.ToString();
            var name = "Potion of ";
            for (int i = 0; i < Effects.Length; i++)
            {
                name += Effects[i].Name + " ";
            }
            _name = prefix + " " + name;

            _value = Flask.Value * IngredientCount * World.Instance.Random.Next(1, 5);
        }
    }
}