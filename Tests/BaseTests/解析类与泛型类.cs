using CZGL.CodeAnalysis;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace BaseTests
{
    public class Model_泛型1<T1, T2, T3>
    {

    }

    public class Model_泛型2<T1, T2, T3> : Model_泛型1<T1, T2, T3> where T1 : struct
    {

    }

    public class Model_泛型3 : Model_泛型1<int, double, int>
    {

    }


    public class Model_泛型类4 { }
    public class Model_泛型类5<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
        where T1 : struct
        where T2 : class
        where T3 : notnull
        where T4 : unmanaged
        where T5 : new()
        where T6 : Model_泛型类4
        where T7 : IEnumerable<int>
        where T8 : T2
        // 组合条件
        where T9 : class, new()
        where T10 : Model_泛型类4, IEnumerable<int>, new()
    {
    }

    public interface IModel1泛型类定义
    {

    }
    public class Model1_5 : Model_泛型1<int, double, int>, IEnumerable<object>, IModel1泛型类定义
    {
        public IEnumerator<object> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class 解析泛型类
    {
        ITestOutputHelper _tempOutput;
        public 解析泛型类(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }
        [Fact]
        public void 泛型类_未定义参数类型()
        {
            Type type = typeof(Model_泛型1<,,>);
            GenericeAnalysis genericeAnalysis = new GenericeAnalysis();
            var z = genericeAnalysis.Analysis(type);
            Assert.Equal("<, , >", genericeAnalysis.Analysis(type));
            Assert.Equal("<T1, T2, T3>", genericeAnalysis.Analysis(type, true));
        }

        [Fact]
        public void 泛型类_已定义参数类型()
        {
            Type type = typeof(Model_泛型1<int, double, int>);
            GenericeAnalysis genericeAnalysis = new GenericeAnalysis();
            Assert.Equal("<int, double, int>", genericeAnalysis.Analysis(type));
            Assert.Equal("<System.Int32, System.Double, System.Int32>", genericeAnalysis.Analysis(type, true));
        }

        //[Fact]
        //public void 泛型类_未定义参数_泛型父类解析()
        //{
        //    Type type = typeof(Model_泛型2<,,>);
        //    GenericeAnalysis genericeAnalysis = new GenericeAnalysis(type);
        //    Assert.Equal("<,,>", genericeAnalysis.BaseTypeAnalysis());
        //    Assert.Equal("<T1,T2,T3>", genericeAnalysis.Analysis(true));
        //}

        [Fact]
        public void 泛型类_泛型父类解析()
        {
            Model_泛型2<int, double, int> model_ = new Model_泛型2<int, double, int>();
            Type type = model_.GetType();
            GenericeAnalysis genericeAnalysis = new GenericeAnalysis();
            Assert.Equal("<int, double, int>", genericeAnalysis.Analysis(type.BaseType));
            Assert.Equal("<System.Int32, System.Double, System.Int32>", genericeAnalysis.Analysis(type.BaseType, true));
        }

        [Fact]
        public void 子类非泛型_父类泛型已定义解析()
        {
            Type type = new Model_泛型3().GetType();
            GenericeAnalysis genericeAnalysis = new GenericeAnalysis();
            Assert.Equal("<int, double, int>", genericeAnalysis.Analysis(type.BaseType));
            Assert.Equal("<System.Int32, System.Double, System.Int32>", genericeAnalysis.Analysis(type.BaseType, true));
        }

        [Fact]
        public void 长嵌套泛型参数解析()
        {
            Type type = typeof(Model_泛型1<int, List<int>, Dictionary<List<int>, Dictionary<int, List<int>>>>);
            GenericeAnalysis genericeAnalysis = new GenericeAnalysis();
            Assert.Equal("<int, List<int>, Dictionary<List<int>, Dictionary<int, List<int>>>>", genericeAnalysis.Analysis(type));
            Assert.Equal(@"<System.Int32, System.Collections.Generic.List<System.Int32>, System.Collections.Generic.Dictionary<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<System.Int32>>>>", genericeAnalysis.Analysis(type, true));
        }

        [Fact]
        public void 超复杂的泛型约束()
        {
            Type type = typeof(Model_泛型类5<,,,,,,,,,,>);
            GenericeAnalysis genericeAnalysis = new GenericeAnalysis();
            _tempOutput.WriteLine(genericeAnalysis.GetGenericConstraintString(type));
            Assert.Equal(@"where T1 : struct 
where T2 : class 
where T3 : notnull 
where T4 : struct 
where T5 : new() 
where T6 : Model_泛型类4 
where T7 : IEnumerable<int> 
where T8 : T2 
where T9 : class,notnull,new() 
where T10 : Model_泛型类4,IEnumerable<int>,new() 
", genericeAnalysis.GetGenericConstraintString(type));
        }
    }
}
