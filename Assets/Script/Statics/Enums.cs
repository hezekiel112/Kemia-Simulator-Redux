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
        }
        
        public enum EWindowType{
            GENERIC_MODAL,
            LOGIN_MODAL,
            NETWORK_ERROR_MODAL,
        }
    }
}