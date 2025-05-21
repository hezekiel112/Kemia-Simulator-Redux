namespace KemiaSimulatorCore.Script.Statics{
    public static class Enums{
        public enum ESceneScope{
            TITLE_SCREEN = 0,
            LOADING_SCREEN = 2,
            GAME_SCREEN = 1,
        }

        public enum EWindowCallback{
            OK_BTN_REDIRECT_TO_LOGIN_MODAL,
            EXIT_BTN_REDIRECT_TO_LOGIN_MODAL,
            
            OK_BTN_REDIRECT_TO_EXIT_GAME,
            EXIT_BTN_REDIRECT_TO_EXIT_GAME,
            
            LOADING_SCREEN_REDIRECT_TO_GAME,
            LOADING_SCREEN_REDIRECT_TO_TITLE_SCREEN,

        }
        
        public enum EWindowType{
            GENERIC_MODAL,
            LOGIN_MODAL,
            NETWORK_ERROR_MODAL,
        }
        
        public enum EWindowFlag{
            NONE,
            LOADING_SCREEN,
        }
    }
}