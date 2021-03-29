#ifndef oel_MVCS_ANY_H
#define oel_MVCS_ANY_H

#include <string>

namespace XTC
{
namespace oelMVCS
{

class Any
{
public:
    enum Tag
    {
        NULL = 0,
        StringValue = 1,
        IntValue = 2,
        LongValue = 3,
        FloatValue = 4,
        DoubleValue = 5,
        BoolValue = 6
    };


    Any();

    Any(std::string _value);

    Any(float _value);

    Any(double _value);

    Any(bool _value);

    Any(int _value);

    Any(long _value);

    bool IsNull();

    bool IsString();

    bool IsInt();

    bool IsLong();

    bool IsFloat();

    bool IsDouble();

    bool IsBool();

    std::string AsString();

    int AsInt();

    long AsLong();

    float AsFloat();

    double AsDouble();

    bool AsBool();

private:
    std::string value_;
    Tag tag_;
};//class

}//namespace
}//namespace

#endif
