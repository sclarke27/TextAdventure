using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour {

    public Text textField;

    private enum StatesEnum {cellLook, mirrorLook, mirrorTake, mirrorDrop, lockLook, sheetsLook, sheetsTake, sheetsDrop, freedom};
    private StatesEnum currState;
    private bool hasMirror = false;
    private bool hasSheets = false;
    private bool hasViewedCell = false;

	// Use this for initialization
	void Start () {
        StartGame();
	}
	
	// Update is called once per frame
	void Update () {
        switch (currState)
        {
            case StatesEnum.cellLook:
                state_cell_look();           
                break;
            case StatesEnum.mirrorLook:
                state_mirror_look();
                break;
            case StatesEnum.sheetsLook:
                state_sheets_look();
                break;
            case StatesEnum.lockLook:
                state_lock_look();
                break;
            case StatesEnum.mirrorTake:
                state_mirror_take();
                break;
            case StatesEnum.sheetsTake:
                state_sheets_take();
                break;
            case StatesEnum.mirrorDrop:
                state_mirror_drop();
                break;
            case StatesEnum.sheetsDrop:
                state_sheets_drop();
                break;
            case StatesEnum.freedom:
                state_freedom();
                break;

        }


	}
    void StartGame()
    {
        hasMirror = false;
        hasSheets = false;
        hasViewedCell = false;
        currState = StatesEnum.cellLook;
    }

    #region "Look" states
    //looking at cell (starting state)
    void state_cell_look()
    {
        textField.text = ((hasViewedCell) ? "You are in a dark and dirty prison cell. " : "A load clang wakes you to a bitter hangover. You open your eyes to find yourself in a dark and dirty prison cell. ")
                       + "Something which appears to be a toilet, sink, and shower combination is rusting the corner. "
                       + ((hasSheets) ? "You are holding the dirty sheets, which is pretty gross. " : "There are some dirty sheets on the bed which you were laying in. ")
                       + ((hasMirror) ? "You are holding a mirror. " : "On the wall is a mirror.")
                       + "The cell door appears to be locked from the outside. "
                       + ((hasViewedCell) ? "You need to find a way out. " : "You have no memory of how you ended up in this cell or why. ")
                       + "\n\n"
                       + ((!hasSheets) ? "Press S to look at the Sheets\n" : "Press S to drop the sheets\n")
                       + ((!hasMirror) ? "Press M to look in Mirror\n" : "Press M to put mirror back\n")
                       + "Press L to look at the Lock\n";

        hasViewedCell = true;
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (hasMirror)
            {
                currState = StatesEnum.mirrorDrop;
            }
            else
            {
                currState = StatesEnum.mirrorLook;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (hasSheets)
            {
                currState = StatesEnum.sheetsDrop;
            }
            else
            {
                currState = StatesEnum.sheetsLook;
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            currState = StatesEnum.lockLook;
        }

    }

    //looking at mirror
    void state_mirror_look()
    {
        textField.text = "The mirror is small and so covered in dirt you can barely see how hung over you are, which is probably for the best. "
                       + "The mirror appears a bit loose like you could take it off the wall."
                       + "\n\n"
                       + "Press T to Take the mirror\n"
                       + "Press C to look at the Cell\n";

        if (Input.GetKeyDown(KeyCode.C))
        {
            currState = StatesEnum.cellLook;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            currState = StatesEnum.mirrorTake;
        }
        
    }

    //looking at lock
    void state_lock_look()
    {
        textField.text = "Its quite locked. "
                       + ((hasMirror) ? "You use the mirror to see the opposite side of the lock and notice the lock uses push buttons. Several of the buttons are cleaner then the others due to use." : "If only there was a way to unlock it.")
                       + "\n\n"
                       + ((hasMirror) ? "Press B to try mashing buttons on the lock\n" : "")
                       + "Press C to look at the Cell\n";

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (hasMirror)
            {
                currState = StatesEnum.freedom;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            currState = StatesEnum.cellLook;
        }
    }

    //looking at sheets
    void state_sheets_look()
    {
        textField.text = "Chances are good that these sheets were originally white, but now they are so stained and dirty they are far from white."
                       + "\n\n"
                       + "Press T to Take the sheets\n"
                       + "Press C to look at the Cell\n";

        if (Input.GetKeyDown(KeyCode.C))
        {
            currState = StatesEnum.cellLook;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            currState = StatesEnum.sheetsTake;
        }

    }

    // 'you escaped' message
    void state_freedom()
    {
        Application.LoadLevel("EscapedScreen");
    }
    #endregion


    #region "Take" actions
    void state_mirror_take()
    {
        hasMirror = true;
        currState = StatesEnum.cellLook;
    }
    void state_sheets_take()
    {
        hasSheets = true;
        currState = StatesEnum.cellLook;
    }
    #endregion

    #region "Drop" actions
    void state_mirror_drop()
    {
        hasMirror = false;
        currState = StatesEnum.cellLook;
    }
    void state_sheets_drop()
    {
        hasSheets = false;
        currState = StatesEnum.cellLook;
    }
    #endregion
}
