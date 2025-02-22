# Erigeron.WPF.SpinePlayer.Mono

## What is this?
  This project aims to play specified edition of Spine animations on Windows.
  
## 这是什么?
  此项目为在 Windows 上播放特定版本的 Spine 动画而生(主要是播放某些铸币小人😂)。
  
## Prerequisite
  - (dot NET 8.x Desktop Runtime)[https://dotnet.microsoft.com/en-us/download/dotnet/8.0]
  - Spine 3.8 atlas, texture, skel files

## 先决条件
  - (dot NET 8.x 桌面运行时)[https://dotnet.microsoft.com/en-us/download/dotnet/8.0]
  - Spine 3.8 生成的 altas, 纹理, skel 文件

## How to use?
   - First, download or export the specified Spine 3.8 files(*.atlas, *.skel, *.png included)
   - Second, modifiy the config file (auto-generated if not exisit, default at .\\data\\Default)
      - AtlasPath/SkelPath: for floating and topmost spine window
      - DesktopInsert: enable another window inside the desktop
      - StaticDesktop: set static image from PicPathD for the window inside the desktop
      - Start/Idle/Touch/DieAnimationPool: anmations for the floating window when started/after starting/touched/closing(Alt+F4)
      - Start/Idle/Touch/DieAnimationPoolD: anmations for the window inside the desktop when started/after starting/touched(actually no use)/closing
      - FullscreenHide: Hide when the foreground application is in fullscreen
      - HideAppHost: Hide this application in the application switcher view
   - You can also launch the application using startArgs like ".\app.exe SPECIFIED_CONFIG_LOCATION" to load/save the specified config

## 如何使用
   - 下载/导出对应的 Spine 3.8 文件(包含 *.atlas, *.skel, *.png，可以从 [Prts](https://prts.wiki/) 或者其它网站下载, 注意: *.png 一般不可以更名, 因为它与结构文件相绑定，其它两个可以更名)
   - 修改配置文件(默认在 .\\data\\Default, 启动程序后如果指定位置没有配置文件，则会自动生成)
      - AtlasPath/SkelPath: 前台窗口的 Spine 文件配置
      - DesktopInsert: 开启嵌入桌面功能（一个额外的 Spine 窗口/静态图像窗口)
      - StaticDesktop: 设置嵌入的为静态图像(PicPathD, 必须为全路径)
      - Start/Idle/Touch/DieAnimationPool: 当启动程序/启动后/点击(触摸)/关闭程序(Alt+F4) 时前台窗口播放的动画
      - Start/Idle/Touch/DieAnimationPoolD: 同上, 但针对于嵌入窗口
      - FullscreenHide: 前台程序最大化时，自动隐藏窗口
      - HideAppHost: 不在 Alt+Tab 中显示
   - 自定义配置文件目录: ".\app.exe 自定义位置"

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


