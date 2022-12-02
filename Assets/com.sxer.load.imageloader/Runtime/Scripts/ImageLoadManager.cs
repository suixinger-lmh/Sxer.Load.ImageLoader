using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sxer.Load.ImageLoader
{
    /// <summary>
    /// 图片资源加载管理
    /// </summary>
    public partial class ImageLoadManager
    {

        //checkpath放在生成工具之前，每个批次的加载顺序就写死了，等待的开启加载只能等加载完的批次完成。
        //避免加载顺序写死，只能在启动加载时，再进行路径check
        #region 对外接口
        public void LoadImage(string path, Action<Texture2D[]> loadAction = null)
        {
            //创建加载工具
            CreateLoadHelper(new List<string>() { path}, loadAction);
        }
        public void LoadImage(string[] path, Action<Texture2D[]> loadAction = null)
        {
            //创建加载工具
            CreateLoadHelper(new List<string>(path), loadAction);
        }
        //图片路径
        public void LoadImage(List<string> path, Action<Texture2D[]> loadAction = null)
        {
            //创建加载工具
            CreateLoadHelper(path, loadAction);
        }

        /// <summary>
        /// 提供通过tag获取图片加载工具，可用工具处理资源
        /// </summary>
        public void LoadImageWithTag()
        {

        }



        /*
         * 1.卸载一张资源
         * 2.卸载一批资源
         * 3.卸载所有资源
         * 
         * 判断
         */
        //路径卸载
        public void UnloadImage(string imagePath)
        {
            //检测当前路径是否在正在下载的helper中
            foreach (var x in _loadHelperMap.Values)
            {
                if(x._helperState == HelperState.Loading)
                {
                    if (x._imageStrPaths.Exists(p => p.Equals(imagePath)))
                    {
                        Debug.LogError(imagePath+"正在下载中，请等待下载完成后进行卸载！");
                        return;
                    }
                }
            }
            if (_resMap.ContainsKey(imagePath))
            {
                Debug.Log(imagePath + "已卸载！");
                _resMap[imagePath].ReleaseSource();
                _resMap.Remove(imagePath);
                return;
            }
            Debug.LogError(imagePath + "资源不存在！");
            return;
        }
        public void UnloadImage(string[] path)
        {
            foreach(var x in path)
            {
                UnloadImage(x);
            }
        }
        public void UnloadImage(List<string> path)
        {
            foreach (var x in path)
            {
                UnloadImage(x);
            }
        }
        //批次卸载
        public void UnloadHelper(string tag)
        {

        }
        

        //全部卸载
        public void UnloadAllImage()
        {
            foreach (var x in _loadHelperMap.Values)
            {
                if (x._helperState == HelperState.Loading)
                {
                    Debug.LogError("还在下载中，请等待下载完成后进行卸载！");
                    return;
                }
            }

            foreach (var x in _resMap.Values)
            {
                x.ReleaseSource();
            }
            _resMap.Clear();
        }


        #endregion


      
        //void DestroyLoadHelper(ImageLoadHelper helper)
        //{
        //    if (_loadHelperMap.ContainsValue(helper))
        //    {
        //        GameObject.Destroy(_loadHelperMap.Values.);
        //        _loadHelperMap.Remove(tagName);
        //    }
        //}





        public Texture2D GetImageRes(string path)
        {
            if (_resMap.ContainsKey(path))
            {
                if(_resMap[path].m_State == ImageResLoadState.Loaded)
                    return _resMap[path].resTexture2D;
                else
                {
                    Debug.LogError("资源尚未加载完成！" + path);
                    return null;
                }
            }
            else
            {
                Debug.LogError("需要先加载图片！"+path);
                return null;
            }
        }




        //void StopHelper




        //生成对应资源类

        //开始下载

        //记录到资源类 //



        //按资源表加载
        //按绝对路径加载

        //网络加载
        //IO加载


        ImageLoadHelper nowLoadingHelper;
        /// <summary>
        /// 管理下载的方式
        /// </summary>
        /// <param name="loadType"></param>
        void OnStartDownloadType(LoadTimeType loadType)
        {
            if (_loadHelperMap != null)
            {
                //同步进行，所有的同时加载（协程多会卡顿）
                if(loadType == LoadTimeType.UpdateLoad)
                {
                    //
                    foreach (var x in _loadHelperMap.Values)
                    {
                        if (x._helperState == HelperState.Ready)
                            x.DoResourceLoad();
                    }

                }

                if(loadType == LoadTimeType.OnlyOneLoad)
                {

                    if (nowLoadingHelper == null)
                    {
                        foreach (var x in _loadHelperMap.Values)
                        {
                            if (x._helperState == HelperState.Ready)
                            {
                                nowLoadingHelper = x;
                                nowLoadingHelper.DoResourceLoad();
                                break;
                            }
                        }

                    }
                    else
                    {
                        if (nowLoadingHelper._helperState == HelperState.Finish)
                        {
                            nowLoadingHelper = null;
                        }
                    }
                }
            }
               

        }


        List<string> destroyTagName = new List<string>();
        /// <summary>
        /// 自动销毁Helper
        /// </summary>
        void OnDestroyHelper()
        {
            foreach(var x in _loadHelperMap)
            {
                if(x.Value._helperState == HelperState.Finish && x.Value._autoDestroy)
                {
                    destroyTagName.Add(x.Key);
                }
            }


            //删除
            for(int i = 0; i < destroyTagName.Count; i++)
            {
                DestroyLoadHelper(destroyTagName[i]);
                _loadHelperMap.Remove(destroyTagName[i]);
            }
            destroyTagName.Clear();
        }


        void DestroyLoadHelper(string tagName)
        {
            if (_loadHelperMap.ContainsKey(tagName))
            {
                Debug.Log("销毁" + tagName);
                GameObject.Destroy(_loadHelperMap[tagName].gameObject);
            }
        }



        //单张加载
        //批量加载


        //IO加载
        //WWW加载

        //通用方式：加载字节数据，(判断图片类型)，转对应图片
        //特殊方式:WWW直接加载Image 对异常图片没有判断
    }
}