namespace Adaptive.Intelligence.Shared.Test.Bases;

public sealed class MockBusinessBase : BusinessBase
{
    public bool SaveFailureFlagForTest { get; set; }

    public bool ThrowOnValidate { get; set; }

    // Flags for event handler invocation (for testing event wiring)
    public bool ChildPropertyChangedCalled { get; set; }
    public bool ChildValidationChangedCalled { get; set; }

    protected override void RegisterEvents(BusinessBase? instance)
    {
        base.RegisterEvents(instance);
    }

    public override bool PerformDelete()
    {
        bool success = false;

        if (!SaveFailureFlagForTest)
            success = true;
        else
            throw new Exception("Test Delete Exception");

        return success;
    }

    public override async Task<bool> PerformDeleteAsync()
    {
        await Task.Yield();
        bool success = false;

        if (!SaveFailureFlagForTest)
            success = true;
        else
            throw new Exception("Test Delete Exception");

        return success;

    }

    protected override ResultType PerformLoad<IdType, ResultType>(IdType? id) where IdType : default
    {

        if (!SaveFailureFlagForTest)
            return default(ResultType);
        else
            throw new Exception("Test Load Exception");

    }

    protected override async Task<ResultType> PerformLoadAsync<IdType, ResultType>(IdType? id) where IdType : default
    {
        await Task.Yield();
        if (!SaveFailureFlagForTest)
            return default(ResultType);
        else
            throw new Exception("Test Load Exception");
    }

    protected override bool PerformSave()
    {
        if (!SaveFailureFlagForTest)
            return true;
        else
            throw new Exception("Test Save Exception");

    }

    protected override async Task<bool> PerformSaveAsync()
    {
        await Task.Yield();
        if (!SaveFailureFlagForTest)
            return true;
        else
            throw new Exception("Test Save Exception");

    }


    // Helper to add a validation message
    public void AddValidationMessage(string message, bool isValid)
    {
        ValidationMessages.Add(new ValidationMessage
        {
            IsValid = isValid,
            Level = isValid ? ValidationLevel.Informational : ValidationLevel.Error,
            Message = message
        });
    }

    // Helper to trigger PropertyValidationChanged event
    public void TriggerValidationChanged(string propertyName)
    {
        OnPropertyValidationChanged(propertyName);
    }

    // Helper to trigger PropertyChanged event
    public void TriggerPropertyChanged(string propertyName)
    {
        OnPropertyChanged(propertyName);
    }

    // Override PerformValidation to simulate validation and exception
    protected override ValidationMessageList PerformValidation()
    {
        if (ThrowOnValidate)
            throw new Exception("Validation exception for test.");
        // If no error messages, return valid
        return new ValidationMessageList();
    }

    // Override event handlers to set flags for testing event wiring
    protected override void OnPropertyChanged(string propertyName)
    {
        ChildPropertyChangedCalled = true;
        base.OnPropertyChanged(propertyName);
    }

    protected override void OnPropertyValidationChanged(string propertyName)
    {
        ChildValidationChangedCalled = true;
        base.OnPropertyValidationChanged(propertyName);
    }

    public void RegisterEventsTestCall(BusinessBase? instance)
    {
        base.RegisterEvents(instance);
    }

    public void UnregisterEventsTestCall(BusinessBase? instance)
    {
        base.UnRegisterEvents(instance);
    }
}
