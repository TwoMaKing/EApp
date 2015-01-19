# EApp
<p>EApp 是一款基于Microsoft .NET的面向领域驱动的轻量级应用程序开发框架, 遵循 DDD (Domain Driven Design) 和 CQRS 的设计思想, <br />支持 windows application 和 web application 的快速开发. 在DDD的基础上, 我又添加了如下 Windows application 开发框架和 Web 开发框架. 这些框架已经在我目前公司的项目中成功的使用. </p>
<p><span style="color: #008000;"><strong>Windows application 开发框架包括 </strong></span></p>
<p><strong>插件式(Plugin)模块框架 </strong></p>
<p>该框架的作用是将业务逻辑和用户界面的调用进行解耦.&nbsp;</p>
<p><strong>EApp.Core:</strong> 各种框架的核心工程. EApp.Core.Plugin 是插件式模块框架的核心.<br /><strong>EApp.Plugin.Generic:</strong> 插件式模块框架的具体实现. <br /><strong>EApp.UI.Controls:</strong> 自定义Winform UI 控件库. 比如Ribbon Menu 和 Toolbar 用于显示新增的功能模块按钮新. 模块可以是界面也可以是无界面的业务功能. <br /><strong>Xpress.UI:</strong> Demo 示例工程, 程序主界面入口.<br /><strong>Xpress.UI.Plugins:</strong> 插件式模块工程. 主程序启动时, 可根据配置条件自动load 或lazy load插件模块并将相关按钮展现到Ribbon Menu 或者 Toolbar 上.<br /><strong>Xpress.Core:</strong> Demo 工程用的实体, 通用功能和模块插件配置元数据.</p>
<p><strong>Windows MVC 框架</strong></p>
<p>仿照Asp.net MVC 框架实现的Winform Mvc 开发框架, 包含了Controller, Action 和 View 三大核心思想. 支持同步和异步的Action 和 &nbsp;Action Filter.</p>
<p><strong>EApp.Core:</strong>&nbsp;各种框架的核心工程. EApp.Core.WindowsMvc 是Windows Mvc框架的核心.<br /><strong>EApp.Windows.Mvc:</strong>&nbsp;Windows Mvc 框架的具体实现.&nbsp;<br /><strong>Xpress.Mvc:</strong>&nbsp;Demo 示例工程, 程序主界面入口.<strong><br /></strong></p>
<p><strong>Windows MVVM 框架</strong></p>
<p><strong><strong>EApp.Mvvm:</strong>&nbsp;</strong>Mvvm框架的具体实现.&nbsp;<strong><br /><strong><br /></strong></strong></p>
<p><strong><span style="color: #008000;">Web Application 开发框架包括</span></strong></p>
<p><strong>插件式 ASP.NET MVC 框架</strong></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
