using UnityEngine;

public static class BallsController 
{
     




    public static bool TryMerge(Ball firstBall, Ball secondBall)
    {
        if(firstBall.Amount == secondBall.Amount)
        {
            firstBall.Upgrade();
           
            LevelController.Instance.RemoveBall(secondBall);
            secondBall.Remove();
            return true;
        }

        return false;
    }

}
