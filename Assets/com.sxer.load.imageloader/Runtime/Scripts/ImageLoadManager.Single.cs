using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sxer.Load.ImageLoader
{
    //mono单例，场景中只出现一个；调用实例时，场景不存在则生成
    //包含一些基于mono的事件
    public partial class ImageLoadManager : MonoBehaviour
    {
        private static ImageLoadManager instance;
        public static ImageLoadManager Instance {
            get{
                if (!instance)
                {
                    new GameObject("_ImageLoadManager_", typeof(ImageLoadManager));
                }
                return instance;
            }
        }
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }
        }

        private void Update()
        {
            OnStartDownloadType(_loadTimeType);
            OnDestroyHelper();
            if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.X))
            {
                    isGUIView = !isGUIView;
            }
            //keyTime += Time.deltaTime;
            //if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.L))
            //{
            //    Debug.Log(isGUIView);
            //    isGUIView = !isGUIView;
            //}
        }

        public bool isGUIView = false;
        private void OnGUI()
        {
            if (isGUIView)
            {
                GUILayout.BeginVertical();
                //资源工具绘制
                if (_loadHelperMap != null)
                {
                    GUILayout.BeginVertical();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("tag");
                    GUILayout.Space(50);
                    GUILayout.Label("state");
                    GUILayout.Space(50);
                    GUILayout.Label("progress");
                    GUILayout.EndHorizontal();
                    foreach (var x in _loadHelperMap)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label(x.Key);
                        GUILayout.Space(50);
                        GUILayout.Label(x.Value._helperState.ToString());
                        GUILayout.Space(50);
                        GUILayout.Label(x.Value._loadProcess.ToString());
                        GUILayout.Space(50);
                        if (x.Value._helperState == HelperState.Ready)
                        {
                            if (GUILayout.Button("StartLoad"))
                            {
                                x.Value.DoResourceLoad();
                            }
                        }

                        GUILayout.EndHorizontal();
                    }

                    GUILayout.EndVertical();
                }
                GUILayout.EndVertical();
            }


            //if (_resMap != null)
            //{
            //    int allcount = _resMap.Count;
            //    int loadedCount = 0;
            //    int loadingCOunt = 0;

            //    int line = 0;
            //    int row = 0;
            //    foreach (var x in _loadHelperMap.Values)
            //    {
            //        foreach (var t in x._imagePaths)
            //        {
            //            GUI.DrawTexture(new Rect(0 + 50 * row, 0 + 50 * line, 50, 50), GetImageRes(t.realPath));
            //            row++;
            //        }
            //        row = 0;
            //        line++;
            //    }
            //    //foreach (var x in _resMap.Values)
            //    //{
            //    //    GUI.DrawTexture(new Rect(0+ 50* loadedCount, 0, 50, 50), x.resTexture2D);
            //    //    if (x.m_State == ImageResLoadState.Loading)
            //    //        loadingCOunt++;
            //    //    if (x.m_State == ImageResLoadState.Loaded)
            //    //        loadedCount++;
            //    //}

            //    GUI.Label(new Rect(100, 100, 200, 200), "总数:" + allcount);

            //    GUI.Label(new Rect(100, 300, 200, 200), "正在加载:" + loadingCOunt);
            //    GUI.Label(new Rect(100, 500, 200, 200), "加载完成:" + loadedCount);
            //    foreach (var x in _resMap.Values)
            //    {

            //    }

            //}
          
        }

    }

}

