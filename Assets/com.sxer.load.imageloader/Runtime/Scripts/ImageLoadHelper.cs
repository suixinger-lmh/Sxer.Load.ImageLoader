using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Sxer.Load.ImageLoader
{
    public enum HelperState
    {
        None,
        Ready,
        Loading,
        Finish
    }
    /// <summary>
    /// 图片加载批量管理工具
    /// 批量图片路径
    /// 【默认状态下，helper仅协助下载，和完成回调；完成后自动销毁】
    /// 【标记状态下，helper可提供额外功能】
    /// 准备完成的批量图片
    /// </summary>
    public class ImageLoadHelper :MonoBehaviour
    {
        //内部：协程管理
        //外部：判断加载状态
        //当前路径数量，无效路径，已加载路径，需要加载路径

        //批量管理
        //协程管理
        //
        //
        public string _tag;

        public List<string> _imageStrPaths;
        public List<ImagePath> _imagePaths;

        public HelperState _helperState = HelperState.None;
        public float _loadProcess = 0;

        public bool _autoDestroy = true;

        Texture2D[] _imageTexs;

        public Action<Texture2D[]> preparedCallBack;
        
        
        /// <summary>
        /// 初始准备，拿到路径，和回调事件
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="imagePaths"></param>
        /// <param name="afterload"></param>
        public void InitHelper(string tagStr, List<string> imagePaths, Action<Texture2D[]> afterload)
        {
            _tag = tagStr;
            _imageStrPaths = imagePaths;
            preparedCallBack = afterload;

            //初始化
            DataReset();

            //基础信息打印
            DebugLoadInfo();

            //初始化完成
            _helperState = HelperState.Ready;
        }

        /// <summary>
        /// 进行资源准备
        /// </summary>
        public void DoResourceLoad()
        {
            Debug.Log(_tag+"Helper开始下载...");

            _helperState = HelperState.Loading;
            //检测所有路径状态
            BeforeLoadCheck();
            
            //准备资源
            StartCoroutine(PrepareAllRes());
            //等待加载
            StartCoroutine(WaitPrepareImage());
        }

        void DataReset()
        {
            _helperState = HelperState.None;
            preparedCount = 0;//当前资源准备完成数量  //当preparedCount = needwaitcount+ needloadcount时，本次资源准备完成
            _loadProcess = 0;//加载进度
        }

        void DebugLoadInfo()
        {
            int allPath = _imageStrPaths.Count;
            int nullPath = _imageStrPaths.FindAll(p => string.IsNullOrEmpty(p)).Count;
            Debug.Log( string.Format("当前批次Tag:{0}\r\n当前路径总量：{1}\r\n空路径数：{2}\r\n", _tag,allPath, nullPath));
        }

        /// <summary>
        /// 检测所有路径状态，
        /// 放在初始化位置：路径状态被提前确定，完成受路径检测顺序影响
        /// 放在加载前：不受加载顺序影响
        /// 线程不安全
        /// </summary>
        void BeforeLoadCheck()
        {
            _imagePaths = new List<ImagePath>();
            foreach(var x in _imageStrPaths)
            {
                _imagePaths.Add(ImageLoadManager.CheckPath(x));
            }

            //本次需要提取数量
            needWaitCount = _imagePaths.FindAll(p => p.pathType == ImageLoadManager.PathType.DoWaitPath).Count + _imagePaths.FindAll(p => p.pathType == ImageLoadManager.PathType.DoGetPath).Count;
            //本次需要新加载数量
            needLoadCount = _imagePaths.FindAll(p => p.pathType == ImageLoadManager.PathType.NewPath).Count;

            Debug.Log(string.Format("提取数量：{0}\r\n加载数量:{1}\r\n",needWaitCount,needLoadCount));
        }


        public int preparedCount;// = needloadcount+needwaitcount;
        public int needLoadCount;
        public int needWaitCount;

        /// <summary>
        /// 协程加载图片，一张一张加载
        /// </summary>
        /// <returns></returns>
        IEnumerator PrepareAllRes()
        {
            for(int i = 0; i < _imagePaths.Count; i++)
            {
                int tempInt = i;
                switch (_imagePaths[tempInt].pathType)
                {
                    case ImageLoadManager.PathType.NewPath://需要新加载的
                        yield return StartCoroutine(ImageLoadManager.WWWLoadImage_Texture2D(_imagePaths[tempInt].realPath,()=> { preparedCount++; }));
                        break;
                    case ImageLoadManager.PathType.DoWaitPath://需要等待下载的
                        yield return StartCoroutine(ImageLoadManager.CheckFinish(_imagePaths[tempInt].realPath, () => { preparedCount++; }));
                        break;
                    case ImageLoadManager.PathType.DoGetPath://已经有的
                        preparedCount++;
                        break;
                }
            }
        }
        /// <summary>
        /// 开启协程 监听加载
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitPrepareImage()
        {
            while (needLoadCount + needWaitCount > preparedCount)
            {
                _loadProcess = preparedCount * 1.0f / (needLoadCount + needWaitCount);
                yield return null;
            }
            //下载完成
            _loadProcess = preparedCount * 1.0f / (needLoadCount + needWaitCount);
           
            //完成回调
            if (preparedCallBack != null)
            {
                //整理图片资源
                GetAllImages();
                preparedCallBack(_imageTexs);
            }


            _helperState = HelperState.Finish;
        }
        void GetAllImages()
        {
            _imageTexs = new Texture2D[_imagePaths.Count];
            for(int i = 0; i < _imagePaths.Count; i++)
            {
                if (_imagePaths[i].pathType == ImageLoadManager.PathType.NullPath)
                    _imageTexs[i] = null;
                else
                    _imageTexs[i] = ImageLoadManager.Instance.GetImageRes(_imagePaths[i].realPath);
            }
        }



    }
}

