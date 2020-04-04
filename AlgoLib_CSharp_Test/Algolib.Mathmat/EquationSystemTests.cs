using NUnit.Framework;

namespace Algolib.Mathmat
{
    [TestFixture]
    public class EquationSystemTests
    {
        protected EquationSystem TestObject;

        [SetUp]
        public void SetUp()
        {
            TestObject = new EquationSystem(new Equation[] {
                new Equation(new double [] { 2, 3, -2 }, 15),
                new Equation(new double [] { 7, -1, 0 }, 4),
                new Equation(new double[] { -1, 6, 4 }, 9)
            });
        }

        [TearDown]
        public void TearDown()
        {
            TestObject = null;
        }

        [Test]
        public void Solve_WhenSingleSolution_ThenSolution()
        {
            // when
            double[] result = TestObject.Solve();
            // then
            Assert.AreEqual(new double[] { 1, 3, -2 }, result);
            Assert.IsTrue(TestObject.IsSolution(result));
            Assert.IsFalse(TestObject.IsSolution(new double[] { -2, -18, -36.5 }));
        }

        [Test]
        public void Solve_WhenNoSolution_ThenNoSolutionException()
        {
            // given
            TestObject = new EquationSystem(new Equation[] {
                new Equation(new double [] { 2, 3, -2 }, 15),
                new Equation(new double [] { 7, -1, 0 }, 4),
                new Equation(new double[] { -1, -1.5, 1 }, -1)
            });
            // when - then
            Assert.Throws<NoSolutionException>(() => TestObject.Solve());
            Assert.IsFalse(TestObject.IsSolution(new double[] { 1, 3, -2 }));
            Assert.IsFalse(TestObject.IsSolution(new double[] { -2, -18, -36.5 }));
        }

        [Test]
        public void Solve_WhenInfiniteSolutions_ThenInfiniteSolutionsException()
        {
            // given
            TestObject = new EquationSystem(new Equation[] {
                new Equation(new double [] { 2, 3, -2 }, 15),
                new Equation(new double [] { 7, -1, 0 }, 4),
                new Equation(new double[] { 4, 6, -4 }, 30)
            });
            // when - then
            Assert.Throws<InfiniteSolutionsException>(() => TestObject.Solve());
            Assert.IsTrue(TestObject.IsSolution(new double[] { 1, 3, -2 }));
            Assert.IsTrue(TestObject.IsSolution(new double[] { -2, -18, -36.5 }));
        }
    }
}
