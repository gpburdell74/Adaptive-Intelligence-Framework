namespace Adaptive.Intelligence.Shared.Tests.Bases
{
    public sealed class MockBusinessBase<T> : BusinessBase<T>
        where T: class
    {
        public MockBusinessBase() : base()
        {
            
        }
        public MockBusinessBase(T entity) :base(entity)
        {

        }
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

        public override bool PerformDelete(T entity)
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
        public override async Task<bool> PerformDeleteAsync(T entity)
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
        protected override async Task<T> PerformLoadAsync<IdType, T>(IdType? id) where IdType : default
        {
            await Task.Yield();
            if (!SaveFailureFlagForTest)
                return default(T);
            else
                throw new Exception("Test Load Exception");
        }

        protected override async Task<T?> PerformLoadAsync<IdType>(IdType? id) where IdType : default
        {
            await Task.Yield();
            if (!SaveFailureFlagForTest)
                return default(T);
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

        protected override bool PerformSave(T entity)
        {
            if (!SaveFailureFlagForTest)
                return true;
            else
                throw new Exception("Test Save Exception");

        }
        protected override async Task<bool> PerformSaveAsync(T entity)
        {
            await Task.Yield();
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

        public string? ReadPropertyCall()
        {
            return base.ReadProperty<string>("Data");
        }

        public void SetPropertyCall(string propertyName, string value)
        {
            base.SetProperty(propertyName, value);  
        }
        public void SetPropertyCall<T>(string propertyName, T value)
        {
            base.SetProperty(propertyName, value);
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
}