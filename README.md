
---
[TOC]
---



# 简介

oelMVCS是一个轻量级的MVCS框架。

## 框架组成

oelMVCS由数据层（model）、视图层（view）、控制层（controller）、服务层（service）四层组成。
每一层均有一个管理中心(center)负责实例的注册。层之间可以通过各自的管理中心获取其它层的实例。层之间的访问权限如下所示：

```
          ______________________________
         |        |                    |
        \|/      \|/                   |
    Service -> Model -> Controller -> View
        /|\      /|\         | 
         |________|__________|
```

## 数据流

oelMVCS使用单向数据流，数据流使用状态(status)进行数据的传递。状态使用归并方式，总是将新的数据和现有状态合并为新的状态。以保证数据的有效更改均发生在数据层（model）。

### 状态

状态（status）被定义为变化时会引起视图层（view）变化的那一部分数据。

### 属性

属性（property）被定义为即使发生变化，也不引起视图层（view）变化的那一部分数据。

## 管线
为了便于进行组件的自动化操作和手动化操作，提供了管线的概念
- 静态管线（StaticPipe）
 操作在主循环外部自动进行安装和拆卸的组件
- 动态管线（DynamicPipe）
 操作在主循环内部手动安装和拆卸的组件


## 生命期

```

框架初始化（Initialize）        ：创建组件中心
 |
\|/
静态管线注册组件（Register）      ：在组件中心添加组件
 |
\|/
框架安装已注册组件（Setup）        ： 调用所有组件的setup方法
 |
\|/
主循环                          ：主循环内使用动态管线手动安装和拆卸组件
 | 
\|/
框架拆卸已注册组件（Dismantle）    ：调用所有组件的Dismantle方法
 |
\|/
静态管线注销组件（Cancel）         ：在组件中心移除组件
 |
\|/
框架销毁（Release）               ：销毁组件中心
```



# 开始使用


## Unity

- 创建一个类，继承RootMono
- 在Awake()中调用Initialize()。
- 在OnEnable()中调用Setup()。
- 在OnDisable()中调用Dismantle()。
- 在OnDestroy()中调用Release()。
- 在场景中创建空物体MVCS,挂接上继承RootMono的类的脚本。
- 创建一个类，继承UIFacade,挂接在UI对象上。

详细参看 SampleMVCS.cs 和 SampleUIFacade.cs


### Mock

service层支持mock方式，只需要简单的实现Service.MockProcessor委托。

```c sharp
Service service = new Service();
service.doamin = "http://127.0.0.1";
service.useMock = true;
service.MockProcessor = this.mockProcessor;
```

```c sharp
IEnumerator mockProcessor(string _url, string _method, Dictionary<string, Any> _params, Service.OnReplyCallback _onReply, Service.OnErrorCallback _onError, Service.Options _options)
{
    yield return new WaitForEndOfFrame();

    if (_url.EndsWith("/login"))
    {
        if (_params["username"].AsString.Equals("admin") && _params["password"].AsString.Equals("admin"))
            _onReply("ok");
        else
            _onError("errcode:1");
        yield break;
    }

    _onError("");
}

```