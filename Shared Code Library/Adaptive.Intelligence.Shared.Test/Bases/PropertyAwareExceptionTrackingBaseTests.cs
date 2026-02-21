namespace Adaptive.Intelligence.Shared.Test.Bases
{
    public class PropertyAwareExceptionTrackingBaseTests
    {
        // Test to ensure the PropertyChanged event is raised correctly
        [Fact]
        public void TestPropertyChangedEventIsRaised()
        {
            var mock = new MockPropertyAwareExceptionTrackingBase();
            var eventRaised = false;
            string propertyName = "TestProperty";

            mock.PropertyChanged += (sender, args) =>
            {
                eventRaised = true;
                Assert.Equal(propertyName, args.PropertyName);
            };

            mock.InvokeOnPropertyChanged(propertyName);

            Assert.True(eventRaised, "PropertyChanged event was not raised.");
        }

        // Test to ensure the class handles null property names without throwing exceptions
        [Fact]
        public void TestPropertyChangedEventHandlesNullPropertyName()
        {
            var mock = new MockPropertyAwareExceptionTrackingBase();
            var eventRaised = false;

            mock.PropertyChanged += (sender, args) =>
            {
                eventRaised = true;
                Assert.True(string.IsNullOrEmpty(args.PropertyName));
            };

            mock.InvokeOnPropertyChanged(string.Empty);

            Assert.True(eventRaised, "PropertyChanged event should handle null property names.");
        }

        // Mock class to test PropertyAwareExceptionTrackingBase functionality
        private class MockPropertyAwareExceptionTrackingBase : PropertyAwareExceptionTrackingBase
        {
            public void InvokeOnPropertyChanged(string propertyName)
            {
                OnPropertyChanged(propertyName);
            }
        }
    }
}
