# Erigeron.WPF.SpinePlayer.Mono

## What is this?
  This project aims to simplifiy play specified edition of Spine animations on Windows.
  
## 这是什么?
  此项目为在 Windows 上播放特定版本的 Spine 动画而生(主要是播放某些铸币小人😂)。
  
## Prerequisite
  (.NET 8.x Desktop Runtime)[https://dotnet.microsoft.com/en-us/download/dotnet/8.0]
  Spine 3.8 atlas, texture, skel files

## 先决条件
  (.NET 8.x 桌面运行时)[https://dotnet.microsoft.com/en-us/download/dotnet/8.0]
  Spine 3.8 生成的 altas, 纹理, skel 文件

## Extra Infomation
  Spine.Monogame.dll is built with spine-csharp & spine-xna/src in [Spine 3.8 Runtimes](https://github.com/EsotericSoftware/spine-runtimes/tree/3.8).
  You can also build it for another Spine version with the code given by [EsotericSoftware](https://github.com/EsotericSoftware/spine-runtimes/).
  Another way to play Spine Animation (using APngPlayer and apng files exported with Spine) [here](https://github.com/super-hiro69/ApngWpfPlayer).

## 附加信息
  Spine.Monogame.dll 生成方式: [Spine 3.8 Runtimes](https://github.com/EsotericSoftware/spine-runtimes/tree/3.8) 下的 spine-csharp 和 spine-xna (monogame 下只有 example, 所以需要用到 xna 下的文件来编译).
  要适配其它版本的 Spine, 请参阅 [Spine Runtimes](https://github.com/EsotericSoftware/spine-runtimes/).
  提供另外一种思路来播放 Spine 动画: [APngPlayer](https://github.com/super-hiro69/ApngWpfPlayer).

## Thanks to/参考了这些项目
  [Spine 3.8 Runtimes](https://github.com/EsotericSoftware/spine-runtimes/tree/3.8)
  [MonoGame.WpfCore](https://github.com/damian-666/MonoGame.WpfCore)

## TO-DO
  提供默认动画播放序列
  语音与立绘嵌入
  命令行参数启动
  ......
