using BepInEx;
using BepInEx.Configuration;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using UnityEngine;
using Utilla;


namespace Capybara
{ 
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [ModdedGamemode]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        /*Assetloading*/
        public static readonly string assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        static GameObject Capybaraa; // the Capybaraa asset
        static GameObject CapybaraSpawn; // the capybara location

        void OnEnable()
        {

            Utilla.Events.GameInitialized += OnGameInitialized;
            Capybaraa.gameObject.SetActive(this.enabled);
        }

        void OnDisable()
        {
            Utilla.Events.GameInitialized -= OnGameInitialized;
            Capybaraa.gameObject.SetActive(this.enabled);
        }



        void OnGameInitialized(object sender, EventArgs e)
        {
            Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("Capybara.Resources.capybara");
            AssetBundle bundle = AssetBundle.LoadFromStream(str);
            GameObject CapybaraaGameObject = bundle.LoadAsset<GameObject>("CAPYBARA");
            Capybaraa = Instantiate(CapybaraaGameObject);
            CapybaraSpawn = GameObject.Find("Level/canyon/Canyon/pillow");

            {
                Capybaraa.transform.SetParent(CapybaraSpawn.transform, false);
                Capybaraa.transform.localPosition = new Vector3(0f, -0.1f, 0.1f);
                Capybaraa.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                Capybaraa.transform.localScale = new Vector3(10f, 10f, 10f);
                Capybaraa.gameObject.SetActive(false);

            }
        }
        /* This attribute tells Utilla to call this method when a modded room is joined */
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            /* Activate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = true;
            Capybaraa.gameObject.SetActive(this.enabled);
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            /* Deactivate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = false;
            Capybaraa.gameObject.SetActive(false);
        }
    }
}