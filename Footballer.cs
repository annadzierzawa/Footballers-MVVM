﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piłkarze
{
    class Footballer
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public uint Age { get; set; }
        public uint Weight { get; set; }

        public Footballer(string firstName, string surname, uint age, uint weight)
        {
            FirstName = firstName;
            Surname = surname;
            Age = age;
            Weight = weight;
        }

        public bool isTheSame(Footballer footballer)
        {
            if (footballer.Surname != Surname) return false;
            if (footballer.FirstName != FirstName) return false;
            if (footballer.Age != Age) return false;
            if (footballer.Weight != Weight) return false;
            return true;
        }
        public override string ToString()
        {
            return $"Surname: {Surname}  Name: {FirstName} Age: {Age} Weight: {Weight} kg";
        }

    }
}
