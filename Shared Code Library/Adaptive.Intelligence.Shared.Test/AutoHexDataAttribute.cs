using AutoFixture.Xunit3;
using System.Reflection;
using Xunit.Sdk;

namespace Adaptive.Intelligence.Shared.Test
{
    public class AutoHexDataAttribute : AutoDataAttribute
    {
        public override async ValueTask<IReadOnlyCollection<ITheoryDataRow>> GetData(MethodInfo testMethod, DisposalTracker disposalTracker)
        {
            ParameterInfo[] parameterList = testMethod.GetParameters();

            List<TheoryDataRow> list = new List<TheoryDataRow>();
            List<string> dataList = new List<string>();
            foreach (var paramList in parameterList)
            {
                Random rnd = new Random();
                int v = rnd.Next(0, 255);
                string hexData = v.ToString("X2");
                dataList.Add(hexData);
            }
            list.Add(new TheoryDataRow(dataList.ToArray()));
            return list;
        }
    }
}
