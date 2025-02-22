# Erigeron.WPF.SpinePlayer.Mono

## What is this?
  This project aims to play specified edition of Spine animations on Windows.
  
## 这是什么?
  此项目为在 Windows 上播放特定版本的 Spine 动画而生(主要是播放某些铸币小人😂)。

## Preview/效果查看
![2](https://github.com/user-attachments/assets/9381090b-95f8-4969-9b41-21da60079e91)
  
## Prerequisite
  - (dot NET 8.x Desktop Runtime)[https://dotnet.microsoft.com/en-us/download/dotnet/8.0]
  - Spine 3.8 atlas, texture, skel files

## 先决条件
  - (dot NET 8.x 桌面运行时)[https://dotnet.microsoft.com/en-us/download/dotnet/8.0]
  - Spine 3.8 生成的 atlas, 纹理, skel 文件

## Description of the configuration file
  - ForeSpine: Spine configuration of the foreground window (distinct from the one embedded)
      - CharName: displayed on the window title
      - Atlas/SkelPath: Atlas/Skel file location
      - WindowLeft/Top/Width/Height: Left margin/top margin/width/height of the window
      - SkeletonLeft/Top/Width/Height: Left margin/top margin/width/height of the Spine Skeleton container
      - SkeletonX/Y: Coordinates of the Spine Skeleton base point relative to the container
      - SkeletonMix: Transition time between two adjacent animations
      - SkeletonScale/X/Y: Spine Skeleton scaling (set to -1 to flip the Skeleton)
      - EnableMove: Enable the function of moving the window following the specified animation
      - MoveClock: Single move interval
      - MoveMin: Minimum value of the left margin when the window moves
      - MoveMax: Maximum value of the left margin when the window moves
      - AutoRevserse: Random trigger Skeleton reverse (make ScaleX opposite)
      - ReversePossibility: The trigger probability of AutoRevserse, 100 means it will definitely trigger
      - StartAnimationPool: Animation pool when the program starts
      - IdleAnimationPool: Animation pool after the program starts
      - TouchAnimationPool: Animation pool for left click (valid only when the window is not locked, invalid for window embedded)
      - DieAnimationPool: Animation pool for when the program is about to close (Alt+F4)
      - MoveAnimationPool: Animation list that supports window follow-up and their movement rate
  - DesktopInsert: Embed an additional window into the desktop
  - StaticDesktop: The window embedded only displays static images
  - PicPathD: Static image path (non-relative path)
  - DesktopSpine: Similar to ForeSpine, for the window embedded
  - AudioEnabled: Enable playing voice
  - AudioIntervalMin: Minimum interval for playing voice (The timer will not start until the playback ends)
  - AudioIntervalMax: The maximum interval for playing voice
  - AudioPath: Voice folder
  - On startup: \AuidoPath\Start\ (Not affected by the interval, but suspend the timer until it ends)
  - After startup: \AudioPath\Idle\
  - When left-clicking: \AudioPath\Touch\ (Not affected by the interval, but suspend the timer until it ends)
  - When closing: \AudioPath\Die\ (Not affected by the interval, but suspend the timer until it ends)
  - FullscreenHide: Automatically hide the foreground Spine window when the system's foreground window is maximized
  - HideAppHost: Hide this window in the application switcher (Alt+Tab)

## 配置文件说明
  - ForeSpine: 前台(与嵌入到桌面相区别)窗口的 Spine 配置
      - CharName: 显示在窗口标题上
      - Atlas/SkelPath: Atlas/Skel 文件位置
      - WindowLeft/Top/Width/Height: 窗口的左边界/上边界/宽度/高度
      - SkeletonLeft/Top/Width/Height: Spine Skeleton 容器的左边界/上边界/宽度/高度
      - SkeletonX/Y: Spine Skeleton 基点相对于容器的坐标
      - SkeletonMix: 相邻两个动画的过渡时间
      - SkeletonScale/X/Y: Spine Skeleton 缩放(设置成 -1 可翻转 Skeleton)
      - EnableMove: 开启窗口跟随指定动画移动的功能
      - MoveClock: 单次移动间隔
      - MoveMin: 窗口移动时左边界的最小值
      - MoveMax: 窗口移动时左边界的最大值
      - AutoRevserse: 随机触发 Skeleton 反转 (X 方向缩放取相反数)
      - ReversePossibility: 上一条的触发概率, 100 为必定触发
      - StartAnimationPool: 程序启动时的动画池
      - IdleAnimationPool: 程序启动后的动画池
      - TouchAnimationPool: 左键单击的动画池 (仅窗口未锁定时有效, 嵌入桌面后不会触发)
      - DieAnimationPool: 程序即将关闭的动画池 (Alt+F4)
      - MoveAnimationPool: 支持窗口跟随移动的动画列表及移动速率
  - DesktopInsert: 将一个额外的窗口嵌入桌面
  - StaticDesktop: 嵌入桌面的窗口仅显示静态图片
  - PicPathD: 静态图片路径 (非相对路径)
  - DesktopSpine: 与 ForeSpine 类似, 只是这个配置的是嵌入到桌面的 Spine
  - AudioEnabled: 开启播放语音的选项
  - AudioIntervalMin: 播放语音的最小间隔 (在播放结束后才会开始计时)
  - AudioIntervalMax: 播放语音的最大间隔
  - AudioPath: 语音文件夹
      - 启动时: \AuidoPath\Start\ (此项不受间隔所影响, 但播放时仍会暂停计时器)
      - 启动后: \AudioPath\Idle\
      - 左键单击时: \AudioPath\Touch\ (此项不受间隔所影响, 但播放时仍会暂停计时器)
      - 关闭时: \AudioPath\Die\ (此项不受间隔所影响, 但播放时仍会暂停计时器)
  - FullscreenHide: 当系统的前台窗口最大化时, 自动隐藏前台 Spine 窗口
  - HideAppHost: 在程序切换器 (Alt+Tab) 中隐藏此窗口

## Extra Infomation
  - Spine.Monogame.dll is built with spine-csharp & spine-xna/src in [Spine 3.8 Runtimes](https://github.com/EsotericSoftware/spine-runtimes/tree/3.8).
  - You can also build it for another Spine version with the code given by [EsotericSoftware](https://github.com/EsotericSoftware/spine-runtimes/).
  - Another way to play Spine Animation (using APngPlayer and apng files exported with Spine) [here](https://github.com/super-hiro69/ApngWpfPlayer).

## 附加信息
  - Spine.Monogame.dll 生成方式: [Spine 3.8 Runtimes](https://github.com/EsotericSoftware/spine-runtimes/tree/3.8) 下的 spine-csharp 和 spine-xna (monogame 下只有 example, 所以需要用到 xna 下的文件来编译).
  - 要适配其它版本的 Spine, 请参阅 [Spine Runtimes](https://github.com/EsotericSoftware/spine-runtimes/).
  - 提供另外一种思路来播放 Spine 动画: [APngPlayer](https://github.com/super-hiro69/ApngWpfPlayer).

## Thanks to/参考了这些项目
  - [Spine 3.8 Runtimes](https://github.com/EsotericSoftware/spine-runtimes/tree/3.8)
  - [MonoGame.WpfCore](https://github.com/damian-666/MonoGame.WpfCore)


