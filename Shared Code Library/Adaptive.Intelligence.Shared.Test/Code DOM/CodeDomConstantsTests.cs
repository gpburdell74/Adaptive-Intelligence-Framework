using Adaptive.CodeDom;

namespace Adaptive.Intelligence.Shared.Test.CodeDOM
{
    public class CodeDomConstantsTests
    {
        [Fact]
        public void TestConstants()
        {
            Assert.Equal("System.Object", CodeDomConstants.DefaultObjectName);
            Assert.Equal("IDisposable", CodeDomConstants.DisposableInterface);
            Assert.Equal("Public Events", CodeDomConstants.RegionPublicEvents);
            Assert.Equal("Private Constants", CodeDomConstants.RegionPrivateConstants);
            Assert.Equal("Private Member Declarations", CodeDomConstants.RegionPrivateMembers);
            Assert.Equal("Constructor / Dispose Methods", CodeDomConstants.RegionConstructorDispose);
            Assert.Equal("Public Properties", CodeDomConstants.RegionPublicProperties);
            Assert.Equal("Protected Properties", CodeDomConstants.RegionProtectedProperties);
            Assert.Equal("Abstract Methods / Functions", CodeDomConstants.RegionAbstractMethods);
            Assert.Equal("Protected Methods / Functions", CodeDomConstants.RegionProtectedMethods);
            Assert.Equal("Public Methods / Functions", CodeDomConstants.RegionPublicMethods);
            Assert.Equal("Private Methods / Functions", CodeDomConstants.RegionPrivateMethods);
            Assert.Equal("Private Event Handlers", CodeDomConstants.RegionEventHandlers);
            Assert.Equal("Private Event Methods", CodeDomConstants.RegionEventMethods);
            Assert.Equal("namespace ", CodeDomConstants.CsNamespace);
            Assert.Equal("using ", CodeDomConstants.CsUsing);
            Assert.Equal(";", CodeDomConstants.CsLineEnd);
            Assert.Equal("{", CodeDomConstants.CsBlockStart);
            Assert.Equal("}", CodeDomConstants.CsBlockEnd);
            Assert.Equal("/// <param name=\"{0}\">", CodeDomConstants.CsXmlParamStart);
            Assert.Equal("/// </param>", CodeDomConstants.CsXmlParamEnd);
            Assert.Equal("/// <remarks>", CodeDomConstants.CsXmlRemarksStart);
            Assert.Equal("/// </remarks>", CodeDomConstants.CsXmlRemarksEnd);
            Assert.Equal("/// <returns>", CodeDomConstants.CsXmlReturnsStart);
            Assert.Equal("/// </returns>", CodeDomConstants.CsXmlReturnsEnd);
            Assert.Equal("/// <summary>", CodeDomConstants.CsXmlSummaryStart);
            Assert.Equal("/// </summary>", CodeDomConstants.CsXmlSummaryEnd);
            Assert.Equal("/// {0}", CodeDomConstants.CsXmlComment);
            Assert.Equal("/// <seealso cref=\"{0}\"/>", CodeDomConstants.CsXmlSeeAlso);
            Assert.Equal("#region {0}", CodeDomConstants.CsRegionStart);
            Assert.Equal("#endregion", CodeDomConstants.CsRegionEnd);
            Assert.Equal("Namespace ", CodeDomConstants.VbNamespace);
            Assert.Equal("Imports ", CodeDomConstants.VbUsing);
            Assert.Equal("\r\n", CodeDomConstants.VbLineEnd);
            Assert.Equal("\t", CodeDomConstants.VbBlockStart);
            Assert.Equal("End ", CodeDomConstants.VbBlockEnd);
            Assert.Equal("''' <param name=\"{0}\">", CodeDomConstants.VbXmlParamStart);
            Assert.Equal("''' </param>", CodeDomConstants.VbXmlParamEnd);
            Assert.Equal("''' <remarks>", CodeDomConstants.VbXmlRemarksStart);
            Assert.Equal("''' </remarks>", CodeDomConstants.VbXmlRemarksEnd);
            Assert.Equal("''' <returns>", CodeDomConstants.VbXmlReturnsStart);
            Assert.Equal("''' </returns>", CodeDomConstants.VbXmlReturnsEnd);
            Assert.Equal("''' <summary>", CodeDomConstants.VbXmlSummaryStart);
            Assert.Equal("''' </summary>", CodeDomConstants.VbXmlSummaryEnd);
            Assert.Equal("''' {0}", CodeDomConstants.VbXmlComment);
            Assert.Equal("''' <seealso cref=\"{0}\"/>", CodeDomConstants.VbXmlSeeAlso);
            Assert.Equal("# Region {0}", CodeDomConstants.VbRegionStart);
            Assert.Equal("# End Region", CodeDomConstants.VbRegionEnd);
        }
    }
}