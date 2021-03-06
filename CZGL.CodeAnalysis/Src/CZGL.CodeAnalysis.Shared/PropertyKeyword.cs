﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    /// <summary>
    /// 属性可用的关键字
    /// </summary>
    [Flags]
    public enum PropertyKeyword
    {
        [MemberDefineName(Name = "")]
        Default = 0,

        [MemberDefineName(Name = "const")]
        Const = 1,

        [MemberDefineName(Name = "static")]
        Static = 1 << 1,

        [MemberDefineName(Name = "readonly")]
        Readonly = 1 << 2,

        [MemberDefineName(Name = "abstract")]
        Abstract = 1 << 3,

        [MemberDefineName(Name = "static readonly")]
        StaticReadonly = Static | Readonly
    }
}
