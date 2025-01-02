namespace Adaptive.Intelligence.Shared.Test.Data_Structures
{
    public class TimeTests
    {
        [Fact]
        public void ConstructorTests()
        {
            Time? t = new Time();
            Assert.NotNull(t);
            Assert.Equal(0, t.Value.TotalSeconds);
            Assert.Equal(0, t.Value.Second);
            Assert.Equal(0, t.Value.Minute);
            Assert.Equal(0, t.Value.Hour);

            t = new Time(50);
            Assert.NotNull(t);
            Assert.Equal(50, t.Value.TotalSeconds);
            Assert.Equal(50, t.Value.Second);
            Assert.Equal(0, t.Value.Minute);
            Assert.Equal(0, t.Value.Hour);

            t = new Time(-9700);
            Assert.NotNull(t);
            Assert.Equal(0, t.Value.TotalSeconds);
            Assert.Equal(0, t.Value.Second);
            Assert.Equal(0, t.Value.Minute);
            Assert.Equal(0, t.Value.Hour);

            t = new Time(Int32.MaxValue);
            Assert.NotNull(t);
            Assert.Equal(86399, t.Value.TotalSeconds);
            Assert.Equal(59, t.Value.Second);
            Assert.Equal(59, t.Value.Minute);
            Assert.Equal(23, t.Value.Hour);

        }

        [Fact]

        public void Constructor_TotalSeconds_Valid()
        {
            var time = new Time(3600);
            Assert.Equal(1, time.Hour);
            Assert.Equal(0, time.Minute);
            Assert.Equal(0, time.Second);
        }

        [Fact]
        public void Constructor_HourMinute_Valid()
        {
            var time = new Time(1, 30);
            Assert.Equal(1, time.Hour);
            Assert.Equal(30, time.Minute);
            Assert.Equal(0, time.Second);
        }

        [Fact]
        public void Constructor_HourMinuteSecond_Valid()
        {
            var time = new Time(1, 30, 45);
            Assert.Equal(1, time.Hour);
            Assert.Equal(30, time.Minute);
            Assert.Equal(45, time.Second);
        }

        [Fact]
        public void Property_Hour_SetValid()
        {
            var time = new Time(0);
            time.Hour = 5;
            Assert.Equal(5, time.Hour);
        }

        [Fact]
        public void Property_Minute_SetValid()
        {
            var time = new Time(0);
            time.Minute = 45;
            Assert.Equal(45, time.Minute);
        }

        [Fact]
        public void Property_Second_SetValid()
        {
            var time = new Time(0);
            time.Second = 30;
            Assert.Equal(30, time.Second);
        }

        [Fact]
        public void Method_AddHours_Valid()
        {
            var time = new Time(1, 0, 0);
            var newTime = time.AddHours(2);
            Assert.Equal(3, newTime.Hour);
        }

        [Fact]
        public void Method_AddMinutes_Valid()
        {
            var time = new Time(1, 0, 0);
            var newTime = time.AddMinutes(30);
            Assert.Equal(1, newTime.Hour);
            Assert.Equal(30, newTime.Minute);
        }

        [Fact]
        public void Method_AddSeconds_Valid()
        {
            var time = new Time(1, 0, 0);
            var newTime = time.AddSeconds(45);
            Assert.Equal(1, newTime.Hour);
            Assert.Equal(0, newTime.Minute);
            Assert.Equal(45, newTime.Second);
        }

        [Fact]
        public void Method_ToString_Valid()
        {
            var time = new Time(13, 30, 0);
            Assert.Equal("1:30 PM", time.ToString());
        }

        [Fact]
        public void Method_ToString_WithAmPm()
        {
            var time = new Time(1, 30, 0);
            Assert.Equal("1:30:00", time.ToString(false));
        }

        [Fact]
        public void Method_Parse_Valid()
        {
            var time = Time.Parse("13:30:00");
            Assert.Equal(13, time.Hour);
            Assert.Equal(30, time.Minute);
            Assert.Equal(0, time.Second);
        }

        [Fact]
        public void Method_TryParse_Valid()
        {
            bool success = Time.TryParse("13:30:00", out Time time);
            Assert.True(success);
            Assert.Equal(13, time.Hour);
            Assert.Equal(30, time.Minute);
            Assert.Equal(0, time.Second);
        }

        [Fact]
        public void Method_Equals_Valid()
        {
            var time1 = new Time(1, 30, 0);
            var time2 = new Time(1, 30, 0);
            Assert.True(time1.Equals(time2));
        }

        [Fact]
        public void Method_CompareTo_Valid()
        {
            var time1 = new Time(1, 30, 0);
            var time2 = new Time(2, 0, 0);
            Assert.True(time1.CompareTo(time2) < 0);
        }

        [Fact]
        public void MinValueTest()
        {
            Time? instance = Time.MinValue;

            Assert.NotNull(instance);
            Assert.Equal(0, instance.Value.Hour);
            Assert.Equal(0, instance.Value.Minute);
            Assert.Equal(0, instance.Value.Second);
            Assert.Equal(0, instance.Value.TotalSeconds);
        }
        [Fact]
        public void MaxValueTest()
        {
            Time? instance = Time.MaxValue;

            Assert.NotNull(instance);
            Assert.Equal(23, instance.Value.Hour);
            Assert.Equal(59, instance.Value.Minute);
            Assert.Equal(59, instance.Value.Second);
            Assert.Equal(86399, instance.Value.TotalSeconds);
        }
        [Fact]
        public void NowTest()
        {
            Time? instance = Time.Now;
            DateTime dt = DateTime.Now;

            Assert.NotNull(instance);
            Assert.Equal(dt.Hour, instance.Value.Hour);
            Assert.Equal(dt.Minute, instance.Value.Minute);
            Assert.Equal(dt.Second, instance.Value.Second);
        }

        [Fact]
        public void CompareToInvalidObjectTest()
        {
            Time t = Time.Now;
            DateTime dt = DateTime.Now;

            bool isGood = false;
            try
            {
                t.CompareTo(dt);
            }
            catch (ArgumentException ex)
            {
                isGood = true;
            }

            Assert.True(isGood);
        }
    }
}