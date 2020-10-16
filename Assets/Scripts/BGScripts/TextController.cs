using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    // Start is called before the first frame update
    public int points = 0;
    public Text textPuntaje;

    public int vidas = 3;
    public Text textVidas;
    public void AddPoints(int point)
    {
        points += point;
        textPuntaje.text = "Puntos: " + this.points;
    }

    public void QuitarVidas(int vida)
    {
        vidas -= vida;
        textVidas.text = "Vidas: " + this.vidas;
    }
    public int GetPoints()
    {
        return points;
    }

    public int GetVidas()
    {
        return vidas;
    }
}
