using System.ComponentModel.DataAnnotations;

namespace Adaptive.Intelligence.Shared.Test.Bases;

public class DataEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Data { get; set; } = "ABCDEF";

}

public class BusinessBaseTTests
{
    [Fact]
    public void CreateTest_AsNew()
    {
        // Arrange.
        DataEntity entity = new DataEntity();

        // Act
        MockBusinessBase<DataEntity> businessObject = new MockBusinessBase<DataEntity>();

        // Assert
        Assert.NotNull(businessObject);
        var item = businessObject.GetEntity();
        Assert.NotNull(item);

        // Alleviate.
        businessObject.Dispose();

    }
    [Fact]
    public void CreateTest_WithData()
    {
        // Arrange.
        DataEntity entity = new DataEntity();

        // Act
        MockBusinessBase<DataEntity> businessObject = new MockBusinessBase<DataEntity>(entity);

        // Assert
        Assert.NotNull(businessObject);
        var item = businessObject.GetEntity();
        Assert.Equal(entity, item);

        // Alleviate.
        businessObject.Dispose();

    }
    [Fact]
    public void Delete_HandlesException_ReturnsFalse()
    {
        var businessObject = new MockBusinessBase<DataEntity>();
        businessObject.SaveFailureFlagForTest = true;

        // Override PerformDeleteAsync to throw an exception
        Assert.False(businessObject.Delete());
    }

    [Fact]
    public void Delete_ReturnsTrue()
    {
        MockBusinessBase<DataEntity> businessObject = new MockBusinessBase<DataEntity>();
        Assert.True(businessObject.Delete());
    }

    [Fact]
    public async Task DeleteAsync_HandlesException_ReturnsFalse()
    {
        MockBusinessBase<DataEntity> businessObject = new MockBusinessBase<DataEntity>();
        businessObject.SaveFailureFlagForTest = true;

        // Override PerformDeleteAsync to throw an exception
        Assert.False(await businessObject.DeleteAsync());
    }

    [Fact]
    public async Task DeleteAsync_ReturnsTrue()
    {
        MockBusinessBase<DataEntity> businessObject = new MockBusinessBase<DataEntity>();
        Assert.True(await businessObject.DeleteAsync());
    }

    [Fact]
    public void IsValid_ReturnsTrue_WhenValidationPasses()
    {
        var businessObject = new MockBusinessBase<DataEntity>();
        // Assuming Validate method is overridden to return true for this test
        Assert.True(businessObject.IsValid);
    }
    [Fact]
    public void Load_HandlesException_ReturnsFalse()
    {
        MockBusinessBase<DataEntity> businessObject = new MockBusinessBase<DataEntity>();
        businessObject.SaveFailureFlagForTest = true;

        // Override PerformDeleteAsync to throw an exception
        Assert.Null(businessObject.Load(string.Empty));
    }

    [Fact]
    public async Task LoadAsync_HandlesException_ReturnsFalse()
    {
        MockBusinessBase<DataEntity> businessObject = new MockBusinessBase<DataEntity>();
        businessObject.SaveFailureFlagForTest = true;

        // Override PerformDeleteAsync to throw an exception
        Assert.Null(await businessObject.LoadAsync<int>(32));
    }

    [Fact]
    public void Load_Boolean_ReturnsFalseAsDefault()
    {
        MockBusinessBase<DataEntity> businessObject = new MockBusinessBase<DataEntity>();
        Assert.Null(businessObject.Load<int>(32));
    }

    [Fact]
    public async Task LoadAsync_Boolean_ReturnsFalseAsDefault()
    {
        MockBusinessBase<DataEntity> businessObject = new MockBusinessBase<DataEntity>();
        Assert.Null(await businessObject.LoadAsync<int>(32));
    }

    [Fact]
    public void Read_Property_Works()
    {
        MockBusinessBase<DataEntity> businessObject = new MockBusinessBase<DataEntity>();

        string data = businessObject.ReadPropertyCall();
        Assert.Equal(data, "ABCDEF");

        businessObject.Dispose();
    }

    [Fact]
    public void Set_Property_Works()
    {
               MockBusinessBase<DataEntity> businessObject = new MockBusinessBase<DataEntity>();
        string data = "NewData";
        businessObject.SetPropertyCall("Data", data);
        Assert.Equal(data, businessObject.ReadPropertyCall());
        businessObject.Dispose();
    }

    [Fact]
    public void Set_Property_Does_Nothing_If_Value_Is_Same()
    {
        MockBusinessBase<DataEntity> businessObject = new MockBusinessBase<DataEntity>();
        string data = "ABCDEF";
        businessObject.SetPropertyCall("Data", data);
        Assert.Equal(data, businessObject.ReadPropertyCall());
        businessObject.Dispose();
    }

    [Fact]
    public void Set_Property_Does_Not_Crash_On_Invalid_Type()
    {
        MockBusinessBase<DataEntity> businessObject = new MockBusinessBase<DataEntity>();
        string data = "ABCDEF";
        businessObject.SetPropertyCall<int>("Data", 27);
        Assert.Equal(data, businessObject.ReadPropertyCall());
        businessObject.Dispose();
    }

    // Additional tests for Load, Save, etc. following a similar pattern
    [Fact]
    public void Save_HandlesException_ReturnsFalse()
    {
        MockBusinessBase<DataEntity> businessObject = new MockBusinessBase<DataEntity>();
        businessObject.SaveFailureFlagForTest = true;

        // Override PerformDeleteAsync to throw an exception
        Assert.False(businessObject.Save());
    }

    // Additional tests for Load, Save, etc. following a similar pattern
    [Fact]
    public async Task SaveAsync_HandlesException_ReturnsFalse()
    {
        MockBusinessBase<DataEntity> businessObject = new MockBusinessBase<DataEntity>();
        businessObject.SaveFailureFlagForTest = true;

        // Override PerformDeleteAsync to throw an exception
        Assert.False(await businessObject.SaveAsync());
    }
    [Fact]
    public void Save_ReturnsTrue()
    {
        MockBusinessBase<DataEntity> businessObject = new MockBusinessBase<DataEntity>();
        Assert.True(businessObject.Save());
    }

    [Fact]
    public async Task SaveAsync_ReturnsTrue()
    {
        MockBusinessBase<DataEntity> businessObject = new MockBusinessBase<DataEntity>();
        Assert.True(await businessObject.SaveAsync());
    }

    // ... (existing tests) ...

    [Fact]
    public void PropertyValidationChanged_Event_IsRaised()
    {
        var businessObject = new MockBusinessBase<DataEntity>();
        string? changedProperty = null;
        businessObject.PropertyValidationChanged += (sender, e) =>
        {
            changedProperty = e.PropertyName;
        };

        businessObject.TriggerValidationChanged("TestProperty");

        Assert.Equal("TestProperty", changedProperty);
    }

    [Fact]
    public void ValidationMessages_AreCleared_OnValidate()
    {
        var businessObject = new MockBusinessBase<DataEntity>();
        businessObject.AddValidationMessage("Error", false);

        Assert.False(businessObject.ValidationMessages.AreAllValid());

        businessObject.Validate();

        Assert.True(businessObject.ValidationMessages.AreAllValid());
    }

    [Fact]
    public void Validate_HandlesException_AddsErrorMessage()
    {
        var businessObject = new MockBusinessBase<DataEntity>();
        businessObject.ThrowOnValidate = true;

        businessObject.Validate();

        Assert.False(businessObject.ValidationMessages.AreAllValid());
        Assert.Contains(businessObject.ValidationMessages, m => m.Message == "The validation process failed.");
    }

    [Fact]
    public void Validate_WithValidationContext_ReturnsValidationResults()
    {
        var businessObject = new MockBusinessBase<DataEntity>();
        var context = new ValidationContext(businessObject);

        var results = businessObject.Validate(context);

        Assert.NotNull(results);
    }

    [Fact]
    public void Dispose_ClearsValidationMessages_AndEvents()
    {
        var businessObject = new MockBusinessBase<DataEntity>();
        businessObject.AddValidationMessage("Error", false);

        businessObject.Dispose();

        Assert.Null(businessObject.ValidationMessages);
    }

    [Fact]
    public void RegisterEvents_And_UnRegisterEvents_WireAndUnwireEvents()
    {
        var parent = new MockBusinessBase<DataEntity>();
        var child = new MockBusinessBase<DataEntity>();

        parent.RegisterEventsTestCall(child);
        child.TriggerPropertyChanged("ChildProp");
        child.TriggerValidationChanged("ChildVal");

        // If events are wired, parent's handlers should be called (simulate by setting flags)
        Assert.True(parent.ChildPropertyChangedCalled);
        Assert.True(parent.ChildValidationChangedCalled);

        parent.ChildPropertyChangedCalled = false;
        parent.ChildValidationChangedCalled = false;

        parent.UnregisterEventsTestCall(child);
        child.TriggerPropertyChanged("ChildProp");
        child.TriggerValidationChanged("ChildVal");

        // After unwiring, handlers should not be called
        Assert.False(parent.ChildPropertyChangedCalled);
        Assert.False(parent.ChildValidationChangedCalled);
    }
}
