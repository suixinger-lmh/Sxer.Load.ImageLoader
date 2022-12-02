using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sxer.Load.ImageLoader
{
    /// <summary>
    /// 图片资源加载管理
    /// 接收路径，进行加载，加载完成回调；【内部处理资源重复，资源空问题】
    /// </summary>
    public partial class ImageLoadManager
    {




        public static IEnumerator CheckFinish(string path,Action finishDo = null)
        {
            while(_resMap[path].m_State!= ImageResLoadState.Loaded)
            {
                yield return null;
            }
            if(finishDo!=null)
                finishDo();
        }


        ImageLoadHelper CreateLoadHelper(List<string> imagePaths, Action<Texture2D[]> loadAction = null, string tag = "default")
        {
            //路径无意义时不生成工具
            if(imagePaths.Find(p=>!string.IsNullOrEmpty(p)) == null)
            {
                Debug.LogError("当前批次路径全部为空！");
                return null;
            }
            
            //标签自增保证不重复
            if (_loadHelperMap.ContainsKey(tag))
                tag += _loadHelperMap.Count;

            GameObject obj = new GameObject(tag, typeof(ImageLoadHelper));
            obj.transform.SetParent(this.transform);
            ImageLoadHelper imageLoadHelper = obj.GetComponent<ImageLoadHelper>();
            imageLoadHelper.InitHelper(tag, imagePaths, loadAction);

            _loadHelperMap.Add(tag, imageLoadHelper);

            return imageLoadHelper;
        }


        /// <summary>
        /// 对传入路径进行识别，空，已经存在列表中，新路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ImagePath CheckPath(string path)
        {
            if(string.IsNullOrEmpty(path))//空路径
            {
                return new ImagePath(path, PathType.NullPath);//空标识
            }

            if (_resMap.ContainsKey(path))
            {
                //不需要加载的分为 加载完成和正在加载
                if(_resMap[path].m_State == ImageResLoadState.Loaded)//已加载完成
                {
                    return new ImagePath(path, PathType.DoGetPath);//直接获取标识
                }
                else
                {
                    return new ImagePath(path, PathType.DoWaitPath);//等待加载完成标识
                }
            }
            else//路径不存在：记录
            {
                _resMap.Add(path, new ImageRes());
                return new ImagePath(path, PathType.NewPath);//需要加载标识
            }


        }


    }
}