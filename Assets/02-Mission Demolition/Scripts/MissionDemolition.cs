using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode {
idle,
playing,
levelEnd
}
public class MissionDemolition : MonoBehaviour {
static public MissionDemolition S; // a private Singleton
[Header("Set in Inspector")]
public Text uitLevel; //The UIText_Level Text
public Text uitShots; //The UIText_Shots Text
public Text uitButton; //The Text on UIButton_view
public Vector3 castlePos; //The place to put castles
public GameObject[] castles; //An array of the castles

[Header("Set Dynamically")]
public int level;
public int levelMax;
public int shotsTaken;
public GameObject castle;
public GameMode mode = GameMode.idle;
public string showing = "Show Slingshot";

void Start() {
       s = this;
       level = 0;
       levelMax = castles.Length;
       StartLevel; 
    }
    void StartLevel(){
        if (castle != null) {
            Destroy(castle);
        }
// Destroy old projectiles if they exist
GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
foreach (GameObject pTemp in gos) {
Destroy( pTemp );
}

// Instantiate the new castle
castle = Instantiate( castles[level] ) as GameObject;
castle.transform.position = castlePos;
shotsTaken = 0;

// Reset the camera
SwitchView("Show Both");
ProjectileLine.S.Clear();
Goal.goalMet = false;

UpdateGUI();
mode = GameMode.playing;

}
void UpdateGUI(){
    uitLevel.text = "Level: "+(level+1)+" of "+levelMax;
    uitShots.text = "Shots Taken: "+shotsTaken;

}

void Update() {
    if ( (mode == GameMode.playing) &&Goal.goalMet ){
        mode = GameMode.levelEnd;
        SwitchView("Show Both");
        Invoke("NextLevel", 2f);
    }
    void NextLevel() {
        level++;
        if (level == levelMax) {
            level = 0;
        }
        startLevel();
    }
    
    public void SwitchView(string eView = "" ){
        if (eView == "") {
            eView = uitButton.text;
        }
        showing = eView;
        switch (showing) {
        case "Show Slingshot":
            FollowCam.POI = null;
            uitButton.text = "Show Castle";
            break;
            
        case "Show Castle":
            FollowcCam.POI = S.castle;
            uitButton.text = "Show Both";
            break;

        case "Show Both":
            FollowCam.POI = GameObject.Find("ViewBoth");
            uitButton.text = "Show Slingshot";
            break;
            
        }
    }
    public static void ShotFired() {
        S.shotsTaken++;
    }


}

