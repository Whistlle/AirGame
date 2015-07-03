using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sov.AVGPart
{
    public enum Opcode
    {
        // BASE.
        STRING = 5,	// load string .
        TABLE = 6,     // load table.
        FUNC = 7,		// OBSOLETE.
        MESSAGING = 8,
        RETURN = 10,
        WAIT_TOUCH = 11,	// Wait.
        TEXT = 13,      // load text.
        ATTACH = 14,	// attach object
        VAR = 17,			// Variable Literal.	
        //	nULL = 21,		// if this , pc is incremented.
        DEBUG_LOG = 22,			// Variable Literal.	
        ITWEEN = 23,			// ITWEEN.

        IF = 25,
        NODE = 26,		// ViNode Mark in  codes.
        JUMP = 27,		// Jump to Another node.

        // Message.
        PRINT = 29,
        CM = 30,
        RELINE = 31,		//[r] reline
        CURRENT = 32,
        SET_TEXT = 33,
        HIDE_MESSAGE = 34,
        PAGE = 35,		//[p] page

        // System.
        WAIT = 39,
        //	START_WAIT = 40,
        //	UPDATE_WAIT = 41,
        STOP = 42,
        SHOW_MENU = 43,
        HIDE_MENU = 44,
        SCENARIO = 45,
        //TRIGGER_EVENT_WITH_ARGS = 46,
        END = 49,			// End of the ScenarioNode.


        // Sound.
        PLAY_SOUND = 50,
        STOP_SOUND = 51,
        STOP_VOICE = 52,

        PLAY_AUDIO_FROM_RESOURCE = 53,

        // Layer.
        LAYOPT = 60,
        TRANSFORM = 61,

        // Util.
        FLAG_ON = 80,
        FLAG_OFF = 81,
        LOAD_RESOURCE = 82,
        INSTANTIATE_AS_GAMEOBJECT = 83,
        SELECTIONS = 84,
        LINK = 85,
        PLAY_SCENARIO = 86,
        DESTROY_OBJECT = 87,

        // Scene.
        LOAD_SCENE = 90,
        CLEAR_SCENE = 91,
        SCENE_NODE = 92,

        // Effets.
        PLAY_ANIMATION = 100,
        BEGIN_TRANSITION = 101,
        END_TRANSITION = 102,

        ADD = 113,      // add
        SUB = 114,      // subtract
        MUL = 115,      // multiply
        DIV = 116,      // divide
        REM = 117,      // remainder
        EQUAL = 118,    // equal
        MORE = 119,     // more than
        LESS = 120,     // less than
        ASSIGN_STRING = 121,     // operator =.
        SET_LEFT_HAND = 122,
        SET_RIGHT_HAND = 123
    }
}
