using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress 
{
    public event EventHandler<OnProgessChangedEventArgs> OnProgressChanged;
    public class OnProgessChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
}
