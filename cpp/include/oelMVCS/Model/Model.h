#ifndef oel_MVCS_MODEL_H
#define oel_MVCS_MODEL_H

#include <string>

namespace XTC
{
namespace oelMVCS
{

//前置声明
class Board;
class Config;
class Logger;


/// <summary>
/// 数据层
/// </summary>
class Model
{
public:
    // 内部类，用于接口隔离,隐藏Model无需暴露给外部的公有方法
    class Inner
    {
    public:
        Inner(const Model* _model);

        void Setup(const Board* _board);

        void Dismantle();
    private:
        Model* model_;
    }; //class

    /// <summary> 状态 </summary>
    class Status
    {
    public:
        // 内部工厂，用于赋值私有成员
        class Factory
        {
        public:
            T New<T>(Board _board, string _uuid);
        };//class

        /// 错误码
        int getCode();

        /// 错误信息
        std::string getMessage();

        /// 数据层中心中的唯一识别码
        std::string getUuid();

        Status();

        /// <summary> 访问数据中心中的已注册状态 </summary>
        /// <param name="_uuid"> 状态的唯一识别码 </param>
        Status Access(std::string _uuid);
    private:
        Board* board_;
    };//class

    /// <summary>向视图层广播消息</summary>
    /// <param name="_action">行为</param>
    /// <param name="_data">数据</param>
    void Broadcast(std::string _action, object _data);

protected:
    Logger* getLogger();
    Config* getConfig();


    Status getStatus();


    /// <summary>
    /// 查找一个数据层
    /// </summary>
    /// <param name="_uuid"> 数据层唯一识别码</param>
    /// <returns>找到的数据层</returns>
    Model* findModel(std::string _uuid);

    /// <summary>
    /// 查找一个控制层
    /// </summary>
    /// <param name="_uuid"> 控制层唯一识别码</param>
    /// <returns>找到的控制层</returns>
    Controller* findController(std::string _uuid);

    /// <summary>
    /// 从数据层中创建一个已经注册的状态
    /// </summary>
    /// <param name="_uuid">状态唯一识别码</param>
    /// <param name="_err">错误</param>
    /// <returns>状态<returns>
    Status* spawnStatus<T>(std::string _uuid, out Error _err);

    /// <summary>
    /// 从数据中心中销毁一个已经注册的状态
    /// </summary>
    /// <param name="_uuid">状态唯一识别码</param>
    /// <param name="_err">错误</param>
    void killStatus(std::string _uuid, Error& _err);

    /// <summary>
    /// 控制层的安装
    /// </summary>
    virtual void setup();

    /// <summary>
    /// 控制层的拆卸
    /// </summary>
    virtual void dismantle();

private:
    Board* board_;
};//class

}//namespace
}//namespace
