using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Namespace used for constants
/// </summary>
namespace Constants
{
    /// <summary>
    /// Tag names
    /// </summary>
    public static class Tags
    {
        public const string PLAYER = "Player";
        public const string ENEMY = "Enemy";

        public const string UICOINS = "CoinsUI";
        public const string PLAYER_REFERENCES = "PlayerReferences";
    }

    public static class SavePrefs
    {
        public const string ID_SELECTED_CHARACTER = "CURRENT_CHARACTER";
        public const string CHARACTERS = "CHARACTERS";
        public const string COINS = "COINS";
        public const string TOPSCORE = "TOPSCORE";
        public const string AUDIO_ENABLED = "ISAUDIOON";
    }
}