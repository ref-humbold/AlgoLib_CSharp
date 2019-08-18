// Disjoint sets structure (union-find).
using System;
using System.Linq;
using System.Collections.Generic;

namespace Algolib.Structures
{
    public class DisjointSets<E>
    {
        /// <summary>Mapa reprezentantów elementów</summary>
        private readonly Dictionary<E, E> represents;

        public DisjointSets(IEnumerable<E> universe)
        {
            represents = new Dictionary<E, E>();

            foreach(E e in universe)
                represents[e] = e;
        }

        /// <summary>Liczba zbiorów.</summary>
        public int Count
        {
            get
            {
                return represents.Keys.Select(e => FindSet(e)).Distinct().ToList().Count;
            }
        }

        /// <summary>Sprawdzanie należenia do dowolnego zbioru.</summary>
        /// <param name="element">element</param>
        /// <returns>czy element w jednym ze zbiorów</returns>
        public bool Contains(E element)
        {
            return represents.ContainsKey(element);
        }

        /// <summary>Dodawanie nowego elementu jako singleton.</summary>
        /// <param name="element">nowy element</param>
        public void AddElem(E element)
        {
            if(Contains(element))
                throw new ArgumentException($"Value {element} already present.");

            represents[element] = element;
        }

        /// <summary>Ustalanie reprezentanta zbioru.</summary>
        /// <param name="element">element ze zbioru</param>
        /// <returns>reprezentant elementu</returns>
        public E FindSet(E element)
        {
            if(!represents[element].Equals(element))
                represents[element] = FindSet(represents[element]);

            return represents[element];
        }

        /// <summary>Scalanie dwóch zbiorów.</summary>
        /// <param name="element1">element pierwszego zbioru</param>
        /// <param name="element2">element drugiego zbioru</param>
        public void UnionSet(E element1, E element2)
        {
            if(!IsSameSet(element1, element2))
                represents[FindSet(element1)] = FindSet(element2);
        }

        /// <summary>Sprawdzanie, czy elementy należą do tego samego zbioru.</summary>
        /// <param name="element1">element pierwszego zbioru</param>
        /// <param name="element2">element drugiego zbioru</param>
        /// <returns>czy elementy znajdują się w jednym zbiorze</returns>
        public bool IsSameSet(E element1, E element2)
        {
            return FindSet(element1).Equals(FindSet(element2));
        }
    } 
}
