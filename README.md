# EApp
EApp 是一款基于Microsoft .NET的面向领域驱动的轻量级应用程序开发框架, 遵循 DDD (Domain Driven Design) 和 CQRS 的设计思想, 
支持 windows application 和 web application 的快速开发. 在DDD的基础上, 我又添加了如下 Windows application 开发框架和 Web 开发框架. 这些框架已经在我目前公司的项目中成功的使用. 

Windows application 开发框架包括 

插件式(Plugin)模块框架, 项目如下:

EApp.Core: 各种框架的核心工程. EApp.Core.Plugin 是插件式模块框架的核心.

EApp.Plugin.Generic: 插件式模块框架的具体实现. 

EApp.UI.Controls: 自定义的 Winform UI 控件库. 比如Ribbon Menu 和 Toolbar 用于显示新增的功能模块按钮新功能可以是界面也可以是无界面的业务功能. 


Windows MVC 框架


Web 开发框架包含了插件式的MVC扩展功能.
