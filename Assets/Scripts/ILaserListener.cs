using UnityEngine;

public interface ILaserListener
{
    void OnLaserEnter(LaserTrap laser);
    void OnLaserStay(LaserTrap laser);
    void OnLaserExit(LaserTrap laser);
}
