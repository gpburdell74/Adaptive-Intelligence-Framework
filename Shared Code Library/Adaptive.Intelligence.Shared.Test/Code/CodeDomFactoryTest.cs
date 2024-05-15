using Adaptive.Intelligence.Shared.Code;
using System.CodeDom;

namespace Adaptive.Intelligence.Shared.Tests
{
    /// <summary>
    /// Unit Tests for the <see cref="CodeDomFactory"/> class.
    /// </summary>
    public class CodeDomFactoryTest
    {
        [Fact]
        public void CreateBooleanValueTextCreatedCorrectly()
        {
            CodeCommentStatement[] statements = CodeDomFactory.CreateBooleanValueText(" if some condition is true");

            Assert.NotNull(statements);
            Assert.True(statements.Length == 3);
            Assert.NotNull(statements[0]);
            Assert.NotNull(statements[1]);
            Assert.NotNull(statements[2]);

        }
        [Fact]
        public void CreateBooleanValueTextContentIsCorrect()
        {
            CodeCommentStatement[] statements = CodeDomFactory.CreateBooleanValueText(" if some condition is true");
            
            Assert.True(statements[0].Comment.DocComment);
            Assert.True(statements[1].Comment.DocComment);
            Assert.True(statements[2].Comment.DocComment);
            Assert.Contains("<value>", statements[0].Comment.Text);
            Assert.Contains(" if some condition is true", statements[1].Comment.Text);
            Assert.Contains("</value>", statements[2].Comment.Text);

        }
        [Fact]
        public void CreateBooleanValueParamTextCreatedCorrectly()
        {
            CodeCommentStatement[] statements = CodeDomFactory.CreateBooleanValueText(" the item is true", " the item is false");

            Assert.NotNull(statements);
            Assert.True(statements.Length == 3);
            Assert.NotNull(statements[0]);
            Assert.NotNull(statements[1]);
            Assert.NotNull(statements[2]);
        }
        [Fact]
        public void CreateBooleanValueParamTextContentIsCorrect()
        {
            CodeCommentStatement[] statements = CodeDomFactory.CreateBooleanValueText("if the item is true", "if the item is false");
            string commentText = statements[1].Comment.Text;

            Assert.True(statements[0].Comment.DocComment);
            Assert.True(statements[1].Comment.DocComment);
            Assert.True(statements[2].Comment.DocComment);

            
            Assert.Contains("<value>", statements[0].Comment.Text);
            Assert.Contains("<b>true</b> if the item is true", commentText);
            Assert.Contains("<b>false</b> if the item is false", commentText);
            Assert.Contains("</value>", statements[2].Comment.Text);
        }
    }
}
