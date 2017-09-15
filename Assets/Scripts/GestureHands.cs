using UnityEngine;
using System.Collections;
using Leap;

public class GestureHands : MonoBehaviour {

    Controller ctrl;
    public GameObject CompanionCube;
    public GameObject CompanionCube2;

	// Use this for initialization
	void Start () {
        ctrl = new Controller();
        ctrl.EnableGesture(Gesture.GestureType.TYPESWIPE);
        ctrl.Config.SetFloat("Gesture.Swipe.MinLength", 150f);
        ctrl.Config.SetFloat("Gesture.Swipe.MinVelocity", 1000f);
        //ctrl.Config.SetFloat
        ctrl.Config.Save();

	}
	
	// Update is called once per frame
	void Update () {
        Frame frame = ctrl.Frame();        
        GestureList gestures = frame.Gestures();
        for (int i = 0; i < gestures.Count; i++) { 
           Gesture gesture = gestures[i];
           if (gesture.Type == Gesture.GestureType.TYPESWIPE) {
               SwipeGesture Swipe = new SwipeGesture(gesture);
               Vector swipeDirection = Swipe.Direction;
               if (swipeDirection.x < 0) {                                 
               }
               else if (swipeDirection.x > 0) {
               }

           }
        }
	}
}
