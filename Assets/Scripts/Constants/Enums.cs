namespace Constants
{
    public enum GameState 
    {
        NONE = -1,
        INTRO = 0,
        LOADING = 1,
        MAIN_MENU = 2,
        GAME = 3,
        EXIT = 4
    }
    public enum RunTimeState 
    {
        NONE = -1,
        GAME_MENU = 0,
        PLAYING = 1,
        PAUSE = 2
        //etc.
    }

    public enum Scenes
    {
        NONE = -1,
        INTRO = 0,
        LOADING = 1,
        MAIN_MENU = 2,
        GAME = 3,
        EXIT = 4
    }

    public enum BuildingState
    {
        NONE = 0,
        WORK = 1,
        UPGRADING = 2,
        PAUSE = 3,
        HACKED = 4
    }

    public enum EntityState
    {
        NONE = -1,
        WORK = 0,
        WALK = 1,
        LEARN = 2,
        PAUSE = 3
    }

    public enum EntityType
    {
        NONE = -1,
        CLIENT = 0,
        BUILDING = 2,
        WORKER = 3,
        AZUBI = 4
        
    }

    public enum Actions
    {
        NONE = -1,
        ROTATE_RIGHT = 0,
        ROTATE_LEFT = 1,
        FOCUS_ON = 2,
        MENU = 3,
        RETURN = 4
    }
    public enum Worker_Position
    {
        NONE = -1,
        IDEA = 0,
        LEADER = 1,
        DEVELOPER =2,
        ARTIST = 3
    }

    public enum PathProgress
    {
        NONE = -1,
        STOPPED = 0,
        MOVE = 1,
        FINISHED=2
    }
}