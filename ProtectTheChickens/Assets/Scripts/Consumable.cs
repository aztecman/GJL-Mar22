using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Collections;

//This is a basic interface with a single required
//method.
public interface Consumable
{
    int GetEaten();
}

//This is a generic interface where T is a placeholder
//for a data type that will be provided by the 
//implementing class.
//public interface Consumable<T>
//{
//    void Damage(T damageTaken);
//}

