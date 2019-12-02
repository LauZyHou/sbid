# sbid
内生安全建模工具，基于.Net Core 3.0的WPF桌面应用。

## 配置
作为.Net Core项目导入Visual Studio使用。

## 其它
要追加目录结构，执行：
```
tree -d -I "[a-z]*" >> README.md
```
## 目录结构
.
├── Model（MVVM的Model）
├── Resources（存放资源字典）
├── UI（MVVM的View）
├── UserControl（用户控件）
│   ├── CloseableTabItem（带删除按钮的TabItem）
│   ├── FuncPanel（存放功能面板，如全局的面板/状态机的面板/拓扑图的面板等）
│   ├── GlobalBlock（存放定制的图形，如UserType的图形）
│   ├── GraphButtonStack（暂时作废）
│   ├── GraphPanel（暂时作废）
│   ├── QuickTab（快捷工具栏）
│   └── Thumb（存放Thumb控件等相关操作的继承类）
├── Utils
└── ViewModel（MVVM的ViewModel）
