using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;

namespace AdvancedChatControl
{
    public class AdvancedChatControl : RocketPlugin<AdvancedChatControlConfig>
    {
        #region Vars
        public string pluginName = "AdvancedChatControl";
        public string pluginVersion = "1.0";
        public string pluginDev = "Teyhota";
        public string pluginSite = "Plugins.4Unturned.tk";
        public string unturnedVersion = "3.17.17.2";

        #region New Update Checker
        public static WebClient myWebClient = new WebClient();
        string nextVersion1 = "1.0.1";
        string nextVersion2 = "1.1";
        string nextVersion3 = "2.0";
        string newUpdateSite = myWebClient.DownloadString("http://plugins.4unturned.tk/updatehandler/advanced-chat-control/1.0");
        #endregion

        public static AdvancedChatControl Instance;
        public static Color CAPSResponseColor;
        public static Color CurseResponseColor;
        public static Color IPResponseColor;
        public static Color URLResponseColor;
        public List<string> DefaultCurseWordList;
        public bool AllIsUpper(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!Char.IsUpper(input[i]))
                    return false;
            }

            return true;
        }
        #endregion

        #region Load
        protected override void Load()
        {
            base.Load();
            Instance = this;
            Rocket.Core.Logging.Logger.Log(" Loading...");
            Rocket.Core.Logging.Logger.LogWarning("Plugin by: " + pluginDev);
            Rocket.Core.Logging.Logger.LogWarning("Plugin Version: " + pluginVersion);
            Rocket.Core.Logging.Logger.LogWarning("For Unturned Version: " + unturnedVersion);
            Rocket.Core.Logging.Logger.LogWarning("Support: " + pluginSite);

            CAPSResponseColor = UnturnedChat.GetColorFromName(Instance.Configuration.Instance.CapsResponseColor, Color.red);
            CurseResponseColor = UnturnedChat.GetColorFromName(Instance.Configuration.Instance.CurseResponseColor, Color.red);
            IPResponseColor = UnturnedChat.GetColorFromName(Instance.Configuration.Instance.IPResponseColor, Color.red);
            URLResponseColor = UnturnedChat.GetColorFromName(Instance.Configuration.Instance.URLResponseColor, Color.red);
            UnturnedPlayerEvents.OnPlayerChatted += UnturnedPlayerEvents_OnPlayerChatted;

            #region New Update Checker
            using (var client = new WebClient())
            {
                #region Next Version 1
                if (newUpdateSite.Contains(nextVersion1))
                {
                    Rocket.Core.Logging.Logger.LogError("------");
                    Rocket.Core.Logging.Logger.LogError("A new version is available!");
                    Rocket.Core.Logging.Logger.LogError("Please update to version " + nextVersion1);
                    Rocket.Core.Logging.Logger.LogError("now, by going to...");
                    Rocket.Core.Logging.Logger.LogError("github.com/Teyhota/" + pluginName + "/releases/");
                    Rocket.Core.Logging.Logger.LogError("------");
                }
                #endregion

                #region Next Version 2
                else if (newUpdateSite.Contains(nextVersion2))
                {
                    Rocket.Core.Logging.Logger.LogError("------");
                    Rocket.Core.Logging.Logger.LogError("A new version is available!");
                    Rocket.Core.Logging.Logger.LogError("Please update to version " + nextVersion2);
                    Rocket.Core.Logging.Logger.LogError("now, by going to...");
                    Rocket.Core.Logging.Logger.LogError("github.com/Teyhota/" + pluginName + "/releases/");
                    Rocket.Core.Logging.Logger.LogError("------");
                }
                #endregion

                #region Next Version 3
                else if (newUpdateSite.Contains(nextVersion3))
                {
                    Rocket.Core.Logging.Logger.LogError("------");
                    Rocket.Core.Logging.Logger.LogError("A new version is available!");
                    Rocket.Core.Logging.Logger.LogError("Please update to version " + nextVersion3);
                    Rocket.Core.Logging.Logger.LogError("now, by going to...");
                    Rocket.Core.Logging.Logger.LogError("github.com/Teyhota/" + pluginName + "/releases/");
                    Rocket.Core.Logging.Logger.LogError("------");
                }
                #endregion

                #region Up To Date
                else
                {
                    Rocket.Core.Logging.Logger.LogError("------");
                    Rocket.Core.Logging.Logger.LogError(pluginName + " is up to date!");
                    Rocket.Core.Logging.Logger.LogError("------");
                }
                #endregion
            }
            #endregion

            if (Instance.Configuration.Instance.ResponseLanguage == "en")
            {
                Rocket.Core.Logging.Logger.LogWarning("Response Language: English");
            }
            else if (Instance.Configuration.Instance.ResponseLanguage == "es")
            {
                Rocket.Core.Logging.Logger.LogWarning("Lenguaje de Respuesta: Español");
            }
            else if (Instance.Configuration.Instance.ResponseLanguage == "fr")
            {
                Rocket.Core.Logging.Logger.LogWarning("Langue de Réponse: Français");
            }
            Rocket.Core.Logging.Logger.Log(" Loaded!");
        }
        #endregion

        #region When The Player Chats.....
        private void UnturnedPlayerEvents_OnPlayerChatted(UnturnedPlayer player, ref Color color, string message, EChatMode chatMode, ref bool cancel)
        {
            #region if player != admin
            if (!player.IsAdmin)
            {
                #region Block Curse Words
                if (Instance.Configuration.Instance.EnableAntiCurse)
                {
                    #region Default Curse Words
                    DefaultCurseWordList = new List<string> { @"\b(s+(\W|\d|_)*h+(\W|\d|_)*i+(\W|\d|_)*t+(\W|\d|_)*)\b", @"\b(p+(\W|\d|_)*i+(\W|\d|_)*s+(\W|\d|_)*s+(\W|\d|_)*)\b", @"\b(f+(\W|\d|_)*u+(\W|\d|_)*c+(\W|\d|_)*k+(\W|\d|_)*)\b", @"\b(c+(\W|\d|_)*u+(\W|\d|_)*n+(\W|\d|_)*t+(\W|\d|_)*)\b", @"\b(c+(\W|\d|_)*o+(\W|\d|_)*c+(\W|\d|_)*k+(\W|\d|_)*)\b", @"\b(t+(\W|\d|_)*i+(\W|\d|_)*t+(\W|\d|_)*)\b", @"\b(t+(\W|\d|_)*i+(\W|\d|_)*t+(\W|\d|_)*s+(\W|\d|_)*)\b", @"\b(t+(\W|\d|_)*i+(\W|\d|_)*t+(\W|\d|_)*t+(\W|\d|_)*i+(\W|\d|_)*e+(\W|\d|_)*s+(\W|\d|_)*)\b", @"\b(b+(\W|\d|_)*i+(\W|\d|_)*t+(\W|\d|_)*c+(\W|\d|_)*h+(\W|\d|_)*)\b", @"\b(n+(\W|\d|_)*i+(\W|\d|_)*g+(\W|\d|_)*g+(\W|\d|_)*e+(\W|\d|_)*r+(\W|\d|_)*)\b", @"\b(n+(\W|\d|_)*i+(\W|\d|_)*g+(\W|\d|_)*g+(\W|\d|_)*a+(\W|\d|_)*)\b", @"\b(p+(\W|\d|_)*u+(\W|\d|_)*s+(\W|\d|_)*s+(\W|\d|_)*y+(\W|\d|_)*)\b", @"\b(d+(\W|\d|_)*i+(\W|\d|_)*c+(\W|\d|_)*k+(\W|\d|_)*)\b", @"\b(d+(\W|\d|_)*i+(\W|\d|_)*c+(\W|\d|_)*k+(\W|\d|_)*h+(\W|\d|_)*e+(\W|\d|_)*a+(\W|\d|_)*d+(\W|\d|_)*)\b", @"\b(a+(\W|\d|_)*s+(\W|\d|_)*s+(\W|\d|_)*)\b", @"\b(a+(\W|\d|_)*s+(\W|\d|_)*s+(\W|\d|_)*h+(\W|\d|_)*o+(\W|\d|_)*l+(\W|\d|_)*e+(\W|\d|_)*)\b", @"\b(t+(\W|\d|_)*w+(\W|\d|_)*a+(\W|\d|_)*t+(\W|\d|_)*)\b", @"\b(f+(\W|\d|_)*a+(\W|\d|_)*g+(\W|\d|_)*)\b", @"\b(f+(\W|\d|_)*a+(\W|\d|_)*g+(\W|\d|_)*g+(\W|\d|_)*o+(\W|\d|_)*t+(\W|\d|_)*)\b", @"\b(w+(\W|\d|_)*h+(\W|\d|_)*o+(\W|\d|_)*r+(\W|\d|_)*e+(\W|\d|_)*)\b", @"\b(s+(\W|\d|_)*l+(\W|\d|_)*u+(\W|\d|_)*t+(\W|\d|_)*)\b", @"\b(b+(\W|\d|_)*a+(\W|\d|_)*s+(\W|\d|_)*t+(\W|\d|_)*a+(\W|\d|_)*r+(\W|\d|_)*d+(\W|\d|_)*)\b", @"\b(p+(\W|\d|_)*r+(\W|\d|_)*i+(\W|\d|_)*c+(\W|\d|_)*k+(\W|\d|_)*)\b", @"\b(s+(\W|\d|_)*h+(\W|\d|_)*i+(\W|\d|_)*t+(\W|\d|_)*h+(\W|\d|_)*e+(\W|\d|_)*a+(\W|\d|_)*d+(\W|\d|_)*)\b", @"\b(a+(\W|\d|_)*r+(\W|\d|_)*s+(\W|\d|_)*e+(\W|\d|_)*)\b", @"\b(j+(\W|\d|_)*i+(\W|\d|_)*z+(\W|\d|_)*z+(\W|\d|_)*)\b", @"\b(c+(\W|\d|_)*u+(\W|\d|_)*m+(\W|\d|_)*)\b", @"\b(g+(\W|\d|_)*a+(\W|\d|_)*y+(\W|\d|_)*)\b", @"\b(d+(\W|\d|_)*i+(\W|\d|_)*l+(\W|\d|_)*d+(\W|\d|_)*o+(\W|\d|_)*)\b", @"\b(d+(\W|\d|_)*u+(\W|\d|_)*m+(\W|\d|_)*b+(\W|\d|_)*a+(\W|\d|_)*s+(\W|\d|_)*s+(\W|\d|_)*)\b", @"\b(r+(\W|\d|_)*e+(\W|\d|_)*t+(\W|\d|_)*a+(\W|\d|_)*r+(\W|\d|_)*d+(\W|\d|_)*)\b", @"\b(j+(\W|\d|_)*e+(\W|\d|_)*w+(\W|\d|_)*)\b", @"\b(j+(\W|\d|_)*e+(\W|\d|_)*w+(\W|\d|_)*s+(\W|\d|_)*)\b", @"\b(c+(\W|\d|_)*l+(\W|\d|_)*i+(\W|\d|_)*t+(\W|\d|_)*)\b", @"\b(p+(\W|\d|_)*e+(\W|\d|_)*n+(\W|\d|_)*i+(\W|\d|_)*s+(\W|\d|_)*)\b", @"\b(v+(\W|\d|_)*a+(\W|\d|_)*g+(\W|\d|_)*i+(\W|\d|_)*n+(\W|\d|_)*a+(\W|\d|_)*)\b", @"\b(a+(\W|\d|_)*n+(\W|\d|_)*u+(\W|\d|_)*s+(\W|\d|_)*)\b", @"\b(a+(\W|\d|_)*s+(\W|\d|_)*s+(\W|\d|_)*c+(\W|\d|_)*r+(\W|\d|_)*a+(\W|\d|_)*c+(\W|\d|_)*k+(\W|\d|_)*)\b", @"\b(s+(\W|\d|_)*e+(\W|\d|_)*x+(\W|\d|_)*)\b" };
                    foreach (string defaultCurseWord in DefaultCurseWordList)
                    {
                        if (Regex.IsMatch(message, defaultCurseWord))
                        {
                            if (Instance.Configuration.Instance.CurseResponse)
                            {
                                if (Instance.Configuration.Instance.ResponseLanguage == "en")
                                {
                                    UnturnedChat.Say(player, Instance.Translate("en_curse_response"), CurseResponseColor);
                                }
                                else if (Instance.Configuration.Instance.ResponseLanguage == "es")
                                {
                                    UnturnedChat.Say(player, Instance.Translate("es_repuesta_de_blasfemias"), CurseResponseColor);
                                }
                                else if (Instance.Configuration.Instance.ResponseLanguage == "fr")
                                {
                                    UnturnedChat.Say(player, Instance.Translate("fr_reponse_jurer"), CurseResponseColor);
                                }
                            }
                            if (Instance.Configuration.Instance.KickIfCurse)
                            {
                                if (Instance.Configuration.Instance.ResponseLanguage == "en")
                                {
                                    player.Kick(Instance.Translate("en_curse_response"));
                                }
                                else if (Instance.Configuration.Instance.ResponseLanguage == "es")
                                {
                                    player.Kick(Instance.Translate("es_repuesta_de_blasfemias"));
                                }
                                else if (Instance.Configuration.Instance.ResponseLanguage == "fr")
                                {
                                    player.Kick(Instance.Translate("fr_reponse_jurer"));
                                }
                            }
                            cancel = true;
                            break;
                        }
                    }
                    #endregion

                    #region Extra Curse Words
                    foreach (string extraCurseWord in Instance.Configuration.Instance.AdditionalCurseWordsToBlock)
                    {
                        if (Regex.IsMatch(message, extraCurseWord))
                        {
                            if (Instance.Configuration.Instance.CurseResponse)
                            {
                                if (Instance.Configuration.Instance.ResponseLanguage == "en")
                                {
                                    UnturnedChat.Say(player, Instance.Translate("en_curse_response"), CurseResponseColor);
                                }
                                else if (Instance.Configuration.Instance.ResponseLanguage == "es")
                                {
                                    UnturnedChat.Say(player, Instance.Translate("es_repuesta_de_blasfemias"), CurseResponseColor);
                                }
                                else if (Instance.Configuration.Instance.ResponseLanguage == "fr")
                                {
                                    UnturnedChat.Say(player, Instance.Translate("fr_reponse_jurer"), CurseResponseColor);
                                }
                            }
                            if (Instance.Configuration.Instance.KickIfCurse)
                            {
                                if (Instance.Configuration.Instance.ResponseLanguage == "en")
                                {
                                    player.Kick(Instance.Translate("en_curse_response"));
                                }
                                else if (Instance.Configuration.Instance.ResponseLanguage == "es")
                                {
                                    player.Kick(Instance.Translate("es_repuesta_de_blasfemias"));
                                }
                                else if (Instance.Configuration.Instance.ResponseLanguage == "fr")
                                {
                                    player.Kick(Instance.Translate("fr_reponse_jurer"));
                                }
                            }
                            cancel = true;
                            break;
                        }
                    }
                    #endregion
                }
                #endregion

                #region Block URLs In Chat
                if (Instance.Configuration.Instance.EnableAntiURL)
                {
                    if (!Regex.IsMatch(message, @"/^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$/"))
                    {
                        if (Instance.Configuration.Instance.URLResponse)
                        {
                            if (Instance.Configuration.Instance.ResponseLanguage == "en")
                            {
                                UnturnedChat.Say(player, Instance.Translate("en_url_response"), URLResponseColor);
                            }
                            else if (Instance.Configuration.Instance.ResponseLanguage == "es")
                            {
                                UnturnedChat.Say(player, Instance.Translate("es_repuesta_de_url"), URLResponseColor);
                            }
                            else if (Instance.Configuration.Instance.ResponseLanguage == "fr")
                            {
                                UnturnedChat.Say(player, Instance.Translate("fr_reponse_url"), URLResponseColor);
                            }
                        }
                        if (Instance.Configuration.Instance.KickIfURL)
                        {
                            if (Instance.Configuration.Instance.ResponseLanguage == "en")
                            {
                                player.Kick(Instance.Translate("en_url_response"));
                            }
                            else if (Instance.Configuration.Instance.ResponseLanguage == "es")
                            {
                                player.Kick(Instance.Translate("es_repuesta_de_url"));
                            }
                            else if (Instance.Configuration.Instance.ResponseLanguage == "fr")
                            {
                                player.Kick(Instance.Translate("fr_reponse_url"));
                            }
                        }
                        cancel = true;
                    }
                }
                #endregion

                #region Block Certain Characters
                if (Instance.Configuration.Instance.EnableChatRegex == true)
                {
                    if (!Regex.IsMatch(message, Instance.Configuration.Instance.ChatRegex.ToString()))
                    {
                        cancel = true;
                        return;
                    }
                }
                #endregion

                #region Disable Chat Completely
                if (Instance.Configuration.Instance.EnableChat == false)
                {
                    cancel = true;
                    return;
                }
                #endregion

                #region Disable Caps Lock Spam
                if (Instance.Configuration.Instance.EnableAntiCaps)
                {
                    if (AllIsUpper(message))
                    {
                        if (Instance.Configuration.Instance.CapsResponse)
                        {
                            if (Instance.Configuration.Instance.ResponseLanguage == "en")
                            {
                                UnturnedChat.Say(player, Translate("en_caps_response"), CAPSResponseColor);
                            }
                            else if (Instance.Configuration.Instance.ResponseLanguage == "es")
                            {
                                UnturnedChat.Say(player, Translate("es_repuesta_de_caps"), CAPSResponseColor);
                            }
                            else if (Instance.Configuration.Instance.ResponseLanguage == "fr")
                            {
                                UnturnedChat.Say(player, Translate("fr_reponse_caps"), CAPSResponseColor);
                            }
                        }
                        if (Instance.Configuration.Instance.KickIfCaps)
                        {
                            if (Instance.Configuration.Instance.ResponseLanguage == "en")
                            {
                                player.Kick(Translate("en_caps_response", Instance.Configuration.Instance.CapsResponseColor));
                            }
                            else if (Instance.Configuration.Instance.ResponseLanguage == "es")
                            {
                                player.Kick(Translate("es_repuesta_de_caps", Instance.Configuration.Instance.CapsResponseColor));
                            }
                            else if (Instance.Configuration.Instance.ResponseLanguage == "fr")
                            {
                                player.Kick(Translate("fr_reponse_caps", Instance.Configuration.Instance.CapsResponseColor));
                            }
                        }
                        cancel = true;
                    }
                }

                #endregion

                #region Block IP Addresses In Chat
                if (Instance.Configuration.Instance.EnableAntiIP)
                {
                    if (Regex.IsMatch(message, @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$"))
                    {
                        if (Instance.Configuration.Instance.IPResponse)
                        {
                            if (Instance.Configuration.Instance.ResponseLanguage == "en")
                            {
                                UnturnedChat.Say(player, Instance.Translate("en_ip_response"), IPResponseColor);
                            }
                            else if (Instance.Configuration.Instance.ResponseLanguage == "es")
                            {
                                UnturnedChat.Say(player, Instance.Translate("es_repuesta_de_ip"), IPResponseColor);
                            }
                            else if (Instance.Configuration.Instance.ResponseLanguage == "fr")
                            {
                                UnturnedChat.Say(player, Instance.Translate("fr_reponse_ip"), IPResponseColor);
                            }
                        }
                        if (Instance.Configuration.Instance.KickIfIP)
                        {
                            if (Instance.Configuration.Instance.ResponseLanguage == "en")
                            {
                                player.Kick(Instance.Translate("en_ip_response"));
                            }
                            else if (Instance.Configuration.Instance.ResponseLanguage == "es")
                            {
                                player.Kick(Instance.Translate("es_repuesta_de_ip"));
                            }
                            else if (Instance.Configuration.Instance.ResponseLanguage == "fr")
                            {
                                player.Kick(Instance.Translate("fr_reponse_ip"));
                            }
                        }
                        cancel = true;
                    }
                }
                #endregion
            }
            #endregion
        }
        #endregion

        #region Unload
        protected override void Unload()
        {
            UnturnedPlayerEvents.OnPlayerChatted -= UnturnedPlayerEvents_OnPlayerChatted;
            base.Unload();
            Rocket.Core.Logging.Logger.Log(" Unloaded!");
        }
        #endregion

        #region Translations
        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList()
                {
                    {"en_caps_response", "Please, don't use all caps!"},
                    {"en_curse_response", "Please, don't use profanity!"},
                    {"en_my_curse_response", "Please, don't use profanity!"},
                    {"en_ip_response", "Please, don't advertise other servers!"},
                    {"en_url_response", "Please, don't advertise other websites!"},
                    {"en_my_url_response", "Please, don't advertise other websites!"},
                    {"es_repuesta_de_caps", "Por favor, no caps el chat!"},
                    {"es_repuesta_de_blasfemias", "¡Por favor, no uses blasfemias!"},
                    {"es_mi_repuesta_de_blasfemias", "¡Por favor, no uses blasfemias!"},
                    {"es_repuesta_de_ip", "Por favor, no anunciar otros servidores!"},
                    {"es_repuesta_de_url", "Por favor, no anunciar otros sitios del web!"},
                    {"es_mi_repuesta_de_url", "Por favor, no anunciar otros sitios del web!"},
                    {"fr_reponse_caps", "S'il vous plaît, ne pas caps le chat!"},
                    {"fr_reponse_jurer", "S'il vous plaît, n'utilisez pas de blasphèmes!"},
                    {"fr_ma_reponse_jurer", "S'il vous plaît, n'utilisez pas de blasphèmes!"},
                    {"fr_reponse_ip", "Ne publiez pas d'autres serveurs!"},
                    {"fr_reponse_url", "Ne publiez pas d'autres sites Web!"},
                    {"fr_ma_reponse_url", "Ne publiez pas d'autres sites Web!"}
                };
            }
        }
        #endregion
    }
}