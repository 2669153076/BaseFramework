using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace BaseFramework.Runtime{
    /// <summary>
    /// 用于跳过Unity6以下版本 打包后 的启动动画<br/>
    /// Unity6已经可以在设置中关闭启动动画
    /// </summary>
    [Preserve]
    public class SkipUnityStartAnimation {
        //此特性用于在启动画面显示之前执行这个方法
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void Skip()
        {
            Task.Run(() =>
            {
                SplashScreen.Stop(SplashScreen.StopBehavior.StopImmediate);
            });
        }

    }
}