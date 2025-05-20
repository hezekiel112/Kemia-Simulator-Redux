namespace KemiaSimulatorCore.Script.HUD{
    public interface IUILoader{
        public bool HasLoaded(int scene_id);
        public void Load(int scene_id);
        public void Unload(int scene_id);
    }
}