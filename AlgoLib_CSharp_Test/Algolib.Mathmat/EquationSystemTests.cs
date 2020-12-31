// Tests: Structure of linear equations system
using NUnit.Framework;

namespace Algolib.Mathmat
{
    [TestFixture]
    public class EquationSystemTests
    {
        [Test]
        public void Solve_WhenSingleSolution_ThenSolution()
        {
            //given
            EquationSystem testObject = new EquationSystem(new Equation[] {
                new Equation(new double [] { 2, 3, -2 }, 15),
                new Equation(new double [] { 7, -1, 0 }, 4),
                new Equation(new double[] { -1, 6, 4 }, 9)
            });
            // when
            double[] result = testObject.Solve();
            // then
            Assert.That(result, Is.EqualTo(new double[] { 1, 3, -2 }));
            Assert.That(testObject.IsSolution(result), Is.True);
            Assert.That(testObject.IsSolution(new double[] { -2, -18, -36.5 }), Is.False);
        }

        [Test]
        public void Solve_WhenNoSolution_ThenNoSolutionException()
        {
            // given
            EquationSystem testObject = new EquationSystem(new Equation[] {
                new Equation(new double [] { 2, 3, -2 }, 15),
                new Equation(new double [] { 7, -1, 0 }, 4),
                new Equation(new double[] { -1, -1.5, 1 }, -1)
            });
            // when
            TestDelegate testDelegate = () => testObject.Solve();
            // then
            Assert.That(testDelegate, Throws.TypeOf<NoSolutionException>());
            Assert.That(testObject.IsSolution(new double[] { 1, 3, -2 }), Is.False);
            Assert.That(testObject.IsSolution(new double[] { -2, -18, -36.5 }), Is.False);
        }

        [Test]
        public void Solve_WhenInfiniteSolutions_ThenInfiniteSolutionsException()
        {
            // given
            EquationSystem testObject = new EquationSystem(new Equation[] {
                new Equation(new double [] { 2, 3, -2 }, 15),
                new Equation(new double [] { 7, -1, 0 }, 4),
                new Equation(new double[] { 4, 6, -4 }, 30)
            });
            // when
            TestDelegate testDelegate = () => testObject.Solve();
            // then
            Assert.That(testDelegate, Throws.TypeOf<InfiniteSolutionsException>());
            Assert.That(testObject.IsSolution(new double[] { 1, 3, -2 }), Is.True);
            Assert.That(testObject.IsSolution(new double[] { -2, -18, -36.5 }), Is.True);
        }
    }
}
