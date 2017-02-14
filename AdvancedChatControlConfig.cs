using Rocket.API;
using System.Collections.Generic;

namespace AdvancedChatControl
{
    public class AdvancedChatControlConfig : IRocketPluginConfiguration
    {
        #region Vars
        public AdvancedChatControlConfig Instance;

        public bool EnableChat;
        public bool EnableAntiCaps;
        public bool EnableAntiCurse;
        public bool EnableAntiIP;
        public bool EnableAntiURL;
        public bool EnableChatRegex;
        public bool KickIfCaps;
        public bool KickIfCurse;
        public bool KickIfIP;
        public bool KickIfURL;
        public bool CapsResponse;
        public bool CurseResponse;
        public bool IPResponse;
        public bool URLResponse;
        public string ResponseLanguage;
        public string CapsResponseColor;
        public string CurseResponseColor;
        public string IPResponseColor;
        public string URLResponseColor;
        public List<string> AdditionalCurseWordsToBlock;
        public string ChatRegex;
        #endregion

        #region Defaults
        public void LoadDefaults()
        {
            EnableChat = true;
            EnableAntiCaps = true;
            EnableAntiCurse = true;
            EnableAntiIP = true;
            EnableAntiURL = true;
            EnableChatRegex = true;
            KickIfCaps = false;
            KickIfCurse = false;
            KickIfIP = false;
            KickIfURL = false;
            CapsResponse = true;
            CurseResponse = true;
            IPResponse = true;
            URLResponse = true;
            ResponseLanguage = "en";
            CapsResponseColor = "red";
            CurseResponseColor = "red";
            IPResponseColor = "red";
            URLResponseColor = "red";
            AdditionalCurseWordsToBlock = new List<string> { @"\b(d+(\W|\d|_)*a+(\W|\d|_)*m+(\W|\d|_)*n+(\W|\d|_)*)\b", @"\b(h+(\W|\d|_)*e+(\W|\d|_)*l+(\W|\d|_)*l+(\W|\d|_)*)\b" };
            ChatRegex = "^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9.!@#$%^&*()-+='? _]*$";
        }
        #endregion
    }
}