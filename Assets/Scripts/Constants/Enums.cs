namespace Constatns
{
    public enum GameState 
    {
        NULL = -1,
        PAUSE = 0,
        LOADING = 1,
        MAIN_MENU = 2,
        PLAY = 3,
        EXIT = 4
    }
    public enum RuntimeState 
    {
        NULL = -1,
        SELECTED_BUILDING = 0,
        BUILDING_INFO = 1,
        LOOK = 2
        //etc.
    }

    public enum Scenes
    {
        NULL = -1,
        INTRO = 0,
        LOADING = 1,
        MAIN_MENU = 2,
        Game = 3,
        EXIT = 4
    }

    public enum BuildingState
    {
        EMPTY = 0,
        HOUSE = 1
    }

    public enum Actions
    {
        NULL = -1,
        ROTATE_RIGHT = 0,
        ROTATE_LEFT = 1,
        FOCUS_ON = 2,
        MENU = 3,
        RETURN = 4,
    }
}