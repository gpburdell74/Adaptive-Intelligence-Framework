using System;
using System.Collections.Generic;
using System.Reflection;
using Adaptive.Intelligence.Shared;
using Xunit;

namespace Adaptive.Intelligence.Shared.Test.Data_Structures
{
    public interface ITestInterface { }
    public class TestImplementation : ITestInterface { }
    public abstract class AbstractTestImplementation : ITestInterface { }
    public interface IUnrelatedInterface { }

    public class TypeResolverTests
    {
        [Fact]
        public void ResolveTypeForInterface_ReturnsNull_ForNullType()
        {
            var result = TypeResolver.ResolveTypeForInterface(null);
            Assert.Null(result);
        }

        [Fact]
        public void ResolveTypeForInterface_ReturnsNull_ForNonInterfaceType()
        {
            var result = TypeResolver.ResolveTypeForInterface(typeof(TestImplementation));
            Assert.Null(result);
        }

        [Fact]
        public void ResolveTypeForInterface_FindsConcreteType()
        {
            var result = TypeResolver.ResolveTypeForInterface(typeof(ITestInterface));
            Assert.Equal(typeof(TestImplementation), result);
        }

        [Fact]
        public void ResolveTypeForInterface_ReturnsNull_WhenNoImplementation()
        {
            var result = TypeResolver.ResolveTypeForInterface(typeof(IUnrelatedInterface));
            Assert.Null(result);
        }

        [Fact]
        public void DetermineTypeMatch_ReturnsNull_ForAbstractOrInterface()
        {
            var method = typeof(TypeResolver).GetMethod("DetermineTypeMatch", BindingFlags.NonPublic | BindingFlags.Static);
            Assert.NotNull(method);

            // Abstract class should return null
            var result = method.Invoke(null, new object[] { typeof(ITestInterface), typeof(AbstractTestImplementation) });
            Assert.Null(result);

            // Interface should return null
            result = method.Invoke(null, new object[] { typeof(ITestInterface), typeof(ITestInterface) });
            Assert.Null(result);
        }

        [Fact]
        public void DetermineTypeMatch_ReturnsType_ForValidImplementation()
        {
            var method = typeof(TypeResolver).GetMethod("DetermineTypeMatch", BindingFlags.NonPublic | BindingFlags.Static);
            Assert.NotNull(method);

            var result = method.Invoke(null, new object[] { typeof(ITestInterface), typeof(TestImplementation) });
            Assert.Equal(typeof(TestImplementation), result);
        }
    }
}