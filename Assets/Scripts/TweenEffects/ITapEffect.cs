using DG.Tweening;

public interface ITapEffect
{
    // Return the tween so callers can chain/wait; return null if none
    Tween Play();
}
