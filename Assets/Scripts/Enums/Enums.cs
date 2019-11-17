namespace Enums
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
        NONE = 0,
        GAME_MENU = 1,
        PLAYING = 2,
        PAUSE = 3,
        FOCUS_ON = 4,
        BUILDING_INFO = 5
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
        EMPTY = 0,
        WORK = 1,
        PAUSE = 2,
    }

    public enum HumanState
    {
        NONE = -1,
        WORK = 0,
        WALK = 1,
        LEARN = 2,
        PAUSE = 3,
        TALK = 4,
        COMMUNICATION = 5,
        QUITED = 6
    }

    public enum EntityType
    {
        NONE = -1,
        CUSTOMER = 0,
        BUILDING = 2,
        DEVELOPER = 3,
        AZUBI = 4,
        DEVOPS = 5,
        ACCOUNTER = 6,
        TESTER = 7,
        DESIGNER = 8,
        ANALYST = 9,
        TEAM_LEADER = 10,
        PERSONAL = 11,
        ADMIN = 12,
        PRODUCT_OWNER = 13,
        HACKER = 14
        
    }

    public enum CustomerType
    {
        OTHER = 0,
        TELEKOM = 1,
        REWE = 2,
        TOM = 3,
        BOSH = 4,
        UNITY_MEDIA = 5,
        DE_POST = 6
    }

    public enum TaskType
    {
        ANALYZES_SPECIFICATION_BOOK = 0,
        WRITE_DOCUMENTATION = 1,
        CREATE_DB = 2,
        CONNECT_TO_DB = 3,
        CONNECT_TO_SERVER = 4,
        CREATE_UI = 5,
        CREATE_BACKEND = 6,
        TEST = 7
        
        
    }

    public enum Actions
    {
        NONE = 0,
        ROTATE_RIGHT = 1,
        ROTATE_LEFT = 2,
        MOVE_FORWARD = 3,
        MOVE_BACK = 4,
        MOVE_LEFT = 5,
        MOVE_RIGHT = 6,
        FOCUS_ON = 7,
        MENU = 8,
        RETURN = 9
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

    public enum BuildingType
    {
        NONE = -1,
        TARENT_TOWN = 0,
        REWE = 1,
        SERVER = 2,
        TOM = 3,
        OFFICE = 4,
        ADMIN = 5,
        ACCOUNTING = 6,
        MARKETING = 7,
        DEV_OPS = 8,
        AZUBIS = 9,
        SOCIAL_RAUM = 10,
        TELEKOM = 11
        
        //etc.
    }

    public enum ParticleType
    {
        CASH = 0,
        PROJECT = 1
    }

}