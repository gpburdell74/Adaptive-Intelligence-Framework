namespace Adaptive.Intelligence.Shared.Test.Utility;

public class BufferBuilderTests
{
    [Fact]
    public void Append_AppendsDataCorrectly()
    {
        // Arrange
        var builder = new BufferBuilder();
        byte[] data1 = { 1, 2, 3 };
        byte[] data2 = { 4, 5 };

        // Act
        builder.Append(data1);
        builder.Append(data2);
        var result = builder.ToArray();

        // Assert
        Assert.Equal(new byte[] { 1, 2, 3, 4, 5 }, result);
        builder.Dispose();
    }

    [Fact]
    public void Append_WithOffsetAndLength_AppendsCorrectSlice()
    {
        // Arrange
        var builder = new BufferBuilder();
        byte[] data = { 10, 20, 30, 40, 50 };

        // Act
        builder.Append(data, 1, 3); // Should append 20, 30, 40
        var result = builder.ToArray();

        // Assert
        Assert.Equal(new byte[] { 20, 30, 40 }, result);
        builder.Dispose();
    }

    [Fact]
    public void Append_NullOrEmpty_DoesNothing()
    {
        // Arrange
        var builder = new BufferBuilder();

        // Act
        builder.Append(null);
        builder.Append(Array.Empty<byte>());
        var result = builder.ToArray();

        // Assert
        Assert.Empty(result);
        builder.Dispose();
    }

    [Fact]
    public void Append_WithInvalidOffsetOrLength_DoesNothing()
    {
        // Arrange
        var builder = new BufferBuilder();
        byte[] data = { 1, 2, 3 };

        // Act
        builder.Append(data, -1, 2); // Invalid offset
        builder.Append(data, 0, 0);  // Invalid length
        var result = builder.ToArray();

        // Assert
        Assert.Empty(result);
        builder.Dispose();
    }

    [Fact]
    public void ToArray_ReturnsNull_AfterClose()
    {
        // Arrange
        var builder = new BufferBuilder();
        builder.Append(new byte[] { 1, 2, 3 });

        // Act
        builder.Close();
        var result = builder.ToArray();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Dispose_MakesFurtherUseSafe()
    {
        // Arrange
        var builder = new BufferBuilder();
        builder.Append(new byte[] { 1, 2, 3 });

        // Act
        builder.Dispose();
        var result = builder.ToArray();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void MultipleDispose_DoesNotThrow()
    {
        // Arrange
        var builder = new BufferBuilder();

        // Act & Assert
        builder.Dispose();
        builder.Dispose();
    }
}
