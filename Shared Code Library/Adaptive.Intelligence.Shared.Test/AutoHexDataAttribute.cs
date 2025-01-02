using AutoFixture.Xunit2;
using System.Reflection;

namespace Adaptive.Intelligence.Shared.Test
{
    public class AutoHexDataAttribute : AutoDataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            IEnumerable<object[]> returnData = base.GetData(testMethod);

            foreach (var paramList in returnData)
            {
                int length = paramList.Count();
                for (int count = 0; count < length; count++)
                {
                    Random rnd = new Random();
                    int v = rnd.Next(0, 255);
                    paramList[count] = v.ToString("X2");
                }
            }
            return returnData;
        }
    }
}
