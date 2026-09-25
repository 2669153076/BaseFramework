using BaseFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.U2D;
using UnityEngine.Video;

namespace BaseFramework.Editor{
    /// <summary>
    /// 编辑器资源管理器
    /// 开发时使用该管理器加载资源，发布后无法使用该管理器
    /// </summary>
    public class EditorResManager : SingletonNormal<EditorResManager>
    {
        private string rootPath = "Assets/Editor/ArtRes";

        private EditorResManager() { }


        /// <summary>
        /// 加载资源
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="path">文件相对路径（相对于Assets/Editor/ArtRes）</param>
        /// <returns></returns>
        public T LoadEditorRes<T>(string path) where T : UnityEngine.Object
        {

#if UNITY_EDITOR
            //文件扩展名
            string extra = typeof(T) switch
            {
                var t when t == typeof(GameObject) => ".prefab",
                var t when t == typeof(SceneAsset) => ".unity",

                var t when t == typeof(Sprite) => ".png",
                var t when t == typeof(Texture2D) => ".png",
                var t when t == typeof(Texture) => ".png",
                var t when t == typeof(SpriteAtlas) => ".spriteatlas",

                var t when t == typeof(Material) => ".mat",
                var t when t == typeof(Shader) => ".shader",

                var t when t == typeof(AnimatorController) => ".controller",
                var t when t == typeof(AnimationClip) => ".anim",
                var t when t == typeof(AvatarMask) => ".mask",

                var t when t == typeof(AudioClip) => ".mp3",
                var t when t == typeof(AudioMixer) => ".mixer",

                var t when t == typeof(VideoClip) => ".mp4",

                var t when t == typeof(PhysicMaterial) => ".physicMaterial",
                var t when t == typeof(PhysicsMaterial2D) => ".physicsMaterial2D",

                var t when t == typeof(TextAsset) => ".txt",
                _ => ""
            };


            T res = AssetDatabase.LoadAssetAtPath<T>(rootPath + path + extra);
            return res;
#else
            return string.null;
#endif
        }

        /// <summary>
        /// 加载Sprite资源
        /// </summary>
        /// <param name="path">文件相对路径（相对于Assets/Editor/ArtRes）</param>
        /// <param name="spriteName">文件名</param>
        /// <returns></returns>
        public Sprite LoadSprite(string path, string spriteName)
        {
#if UNITY_EDITOR
            Object[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(rootPath + path + ".png");
            foreach (var item in sprites)
            {
                if (spriteName == item.name)
                {
                    return item as Sprite;
                }
            }
#endif
            return null;
        }
        /// <summary>
        /// 加载指定路径下所有Sprite资源
        /// </summary>
        /// <param name="path">文件相对路径（相对于Assets/Editor/ArtRes）</param>
        /// <returns></returns>
        public Dictionary<string, Sprite> LoadSprites(string path)
        {
#if UNITY_EDITOR
            Dictionary<string, Sprite> spriteDict = new();

            Object[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(rootPath + path + ".png");
            foreach (var item in sprites)
            {
                spriteDict.Add(item.name, item as Sprite);
            }
            return spriteDict;
#else
            return null; 
#endif
        }
    }
}
