using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

namespace GMain.Editor.SetupTool.Window
{
    public class SetupToolWindow : EditorWindow
    {
        #region Variables
        static SetupToolWindow win;

        private string gameName = " ";
        #endregion

        #region Main Methods
        public static void InitWindow()
        {
            win = EditorWindow.GetWindow<SetupToolWindow>("Project Setup");
            win.Show();
            win.titleContent = new GUIContent("Setup folder structure tool");
        }

        void OnGUI()
        {
            //Horizontal sẽ làm cho GUI nằm ngang, nhưng khi mà có content tràn viền, không thì nó vẫn sẽ như cũ. 
            //Tương tự với Vertical.
            EditorGUILayout.BeginHorizontal();
            gameName = EditorGUILayout.TextField("Game name: ", gameName);
            EditorGUILayout.EndHorizontal();

            var button = GUILayout.Button("Create Project Structure", GUILayout.Height(35), GUILayout.ExpandWidth(true));
            if (button)
            {
                CreateFolder();
            }

            if (win != null)
                win.Repaint();
        }
        #endregion

        #region Custom Methods
        void CreateFolder()
        {
            //Create root folder
            string assetPath = Application.dataPath;
            string rootPath = assetPath + "/" + gameName;
            if (string.IsNullOrWhiteSpace(gameName))
                return;
            else
            {
                bool confirm = EditorUtility.DisplayDialog("Congratulations", "Your folder structure has been created", "Yeeehooooo");
                if (confirm)
                {
                    CloseWindow();
                }
            }

            //Create sub folder
            DirectoryInfo directoryInfo = Directory.CreateDirectory(rootPath);

            if (!directoryInfo.Exists)
            {
                return;
            }
            CreateSubFolder(rootPath);

            AssetDatabase.Refresh();
            CloseWindow();

        }

        void CreateSubFolder(string rootPath)
        {
            DirectoryInfo rootInfo = null;
            List<string> folderName = new List<string>();

            string pathResources = rootPath + "/Resources";
            rootInfo = Directory.CreateDirectory(pathResources);
            if (rootInfo.Exists)
            {
                folderName.Clear();
                folderName.Add("Sounds");
                folderName.Add("Arts");
                folderName.Add("Fonts");
                folderName.Add("Materials");
                folderName.Add("VFX");
                folderName.Add("Shader Graph");
                folderName.Add("Animation");

                CreateFolders(pathResources, folderName);
            }

            string pathScripts = rootPath + "/Scripts";
            rootInfo = Directory.CreateDirectory(pathScripts);

            string pathScenes = rootPath + "/Scenes";
            rootInfo = Directory.CreateDirectory(pathScenes);
            if (rootInfo.Exists)
                CreateScene(pathScenes, "Sample Scene");

            string pathPrefabs = rootPath + "/Prefabs";
            rootInfo = Directory.CreateDirectory(pathPrefabs);
        }

        void CreateFolders(string path, List<string> folders)
        {
            foreach (string folder in folders)
            {
                Directory.CreateDirectory(path + "/" + folder);
            }
        }

        void CreateScene(string path, string name)
        {
            UnityEngine.SceneManagement.Scene curScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            EditorSceneManager.SaveScene(curScene, path + "/" + name + ".unity", true);
        }

        private void CloseWindow()
        {
            if (win)
            {
                win.Close();
            }
        }
        #endregion
    }
}