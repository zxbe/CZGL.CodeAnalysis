﻿using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace RoslynTests
{
    public class DelegateTests
    {
        ITestOutputHelper _tempOutput;
        public DelegateTests(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }

        private class TestAttribute : Attribute
        {
            public TestAttribute() { }
            public TestAttribute(string a, string b) { }

            public string A { get; set; }
            public string B { get; set; }
        }

        public delegate void T1();
        public delegate string T2();
        public delegate string T3(string a);

        [Test("1", "2", A = "3", B = "4")]
        public delegate void T4();


        [Fact]
        public void 委托_T1()
        {
            DelegateBuilder builder = new DelegateBuilder();
            var field = builder
                .WithAccess(MemberAccess.Public)
                .WithReturnType("void")
                .WithName("T1").BuildSyntax();
            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("public delegate void T1();", result);
        }

        [Fact]
        public void 委托_T2()
        {
            DelegateBuilder builder = new DelegateBuilder();
            var field = builder
                .WithAccess(MemberAccess.Public)
                .WithReturnType("string")
                .WithName("T2").BuildSyntax();
            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("public delegate string T2();", result);
        }

        [Fact]
        public void 委托_T3()
        {
            DelegateBuilder builder = new DelegateBuilder();
            var field = builder
                .WithAccess(MemberAccess.Public)
                .WithReturnType("string")
                .WithParams("string a")
                .WithName("T3").BuildSyntax();
            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal("public delegate string T3(string a);", result);
        }

        [Fact]
        public void 委托_T4()
        {
            DelegateBuilder builder = new DelegateBuilder();
            var field = builder
                .WithAttributes(new string[] { @"[Test(""1"", ""2"", A = ""3"", B = ""4"")]" })
                .WithAccess(MemberAccess.Public)
                .WithReturnType("void")
                .WithName("T4").BuildSyntax();
            var result = field.NormalizeWhitespace().ToFullString();
#if Log
            _tempOutput.WriteLine(result);
#endif
            Assert.Equal(@"[Test(""1"", ""2"", A = ""3"", B = ""4"")]
public delegate void T4();", result);
        }


    }
}
