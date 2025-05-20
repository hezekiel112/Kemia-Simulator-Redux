using UnityEngine;

namespace KemiaSimulatorCore.Script.Helper{
    public record RandomPlayerName{
        public static string[] Usernames
        {
            get => new[]
            {
                "John Smith",
                "XxxChicheKebabKiller632xxX",
                "Nassim Kemia",
                "Monsieur Jouques",
                "Monsieur Cuomo",
                "Monsieur Salom",
                "Jean Kulky",
                "John John",
                "Ben Dover",
                "Emma Bite",
            };
        }

        public static string GetRandomPlayerName(){
            int random_range = Random.Range(0, Usernames.Length);
            return Usernames[random_range];
        }
    }
}