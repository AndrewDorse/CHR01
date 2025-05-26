using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController
{
    private ScreenView _view;



    public ScreenController(ScreenView view)
    {
        _view = view;
    } 

    public void Open() { }

    public void Close() 
    { 
        Dispose();
        _view.CloseScreen(); 
    } 

    public virtual void Dispose()
    {

    } 
}
