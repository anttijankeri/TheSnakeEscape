using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fading : MonoBehaviour {

    public Texture2D fadeOutTexture;    //The texture that will overlay the screen
    public float fadeSpeed = 0.8f;      //Fade out speed

    private int drawDepth = -1000;      //Texture order in the draw hierarchy. ELI ALIN = ENSIMMÄISENÄ
    private float alpha = 1.0f;         //Tekstuurin alpha 0-1
    private int fadeDir = -1;           //Mihin suuntaan fadetaan -1 sisään tai 1 ulos

    void OnGUI ()
    {
        //Fadetaan alphaa käyttäen suuntaa, nopeutta ja Time.deltatime converttauksessa sekunneiksi.
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        //Pakotetaan (clamp) numeroiksi 0-1 koska GUI käyttää alphassa 0-1 arvoja
        alpha = Mathf.Clamp01(alpha);

        //Pidetään värit ja alpha on alpha value
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;          //Musta renderöidään viimeiseksi
        //Piirretään musta neliö koko ruudun kokoiseksi
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    /// <summary>
    /// set fadeDir to the direction parameter making the scene fade in if -1 and out if 1
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public float BeginFade (int direction)
    {
        fadeDir = direction;
        return (fadeSpeed);             //Palauttaa fadeSpeed variablen jotta Application.Loadlevel(); aika voidaan mitata
    }

    void sceneLoaded()
    {
        alpha = 1;
        BeginFade(-1);
    }
}
