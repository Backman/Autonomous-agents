using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using SharpSteer2.Helpers;

namespace SharpSteer2.Tests
{
    [TestClass]
    public class LocalSpaceBasisHelpersTest
    {
        private ILocalSpaceBasis Basis(Matrix m)
        {
            return new LocalSpace(m);
        }

        [TestMethod]
        public void GlobalizeDirectionTest()
        {
            Matrix m = Matrix.CreateRotationX(-MathHelper.PiOver2);
            ILocalSpaceBasis basis = Basis(m);

            var v = Vector3.Normalize(new Vector3(1, 2, 3));
            Assert.AreEqual(Vector3.TransformNormal(v, m), basis.GlobalizeDirection(v));
        }

        [TestMethod]
        public void GlobalizePositionTest()
        {
            Matrix m = Matrix.CreateRotationX(-MathHelper.PiOver2) * Matrix.CreateTranslation(10, 20, 30);
            ILocalSpaceBasis basis = Basis(m);

            var v = Vector3.Normalize(new Vector3(1, 2, 3));
            Assert.AreEqual(Vector3.Transform(v, m), basis.GlobalizePosition(v));
        }

        [TestMethod]
        public void LocalizeDirectionTest()
        {
            Matrix m = Matrix.CreateRotationX(-MathHelper.PiOver2);
            ILocalSpaceBasis basis = Basis(m);

            var v = Vector3.Normalize(new Vector3(1, 2, 3));
            Assert.AreEqual(Vector3.TransformNormal(v, Matrix.Invert(m)), basis.LocalizeDirection(v));
        }

        [TestMethod]
        public void LocalizePositionTest()
        {
            Matrix m = Matrix.CreateRotationX(-MathHelper.PiOver2) * Matrix.CreateTranslation(10, 20, 30);
            ILocalSpaceBasis basis = Basis(m);

            var v = Vector3.Normalize(new Vector3(1, 2, 3));
            Assert.AreEqual(Vector3.Transform(v, Matrix.Invert(m)), basis.LocalizePosition(v));
        }

        [TestMethod]
        public void LocalRotateForwardToSideTest()
        {
            var f = new Vector3(0, 0, 1);
            var s = new Vector3(-1, 0, 0);

            Assert.AreEqual(s, LocalSpaceBasisHelpers.LocalRotateForwardToSide(null, f));
        }

        [TestMethod]
        public void RegenerateOrthonormalBasisTest()
        {
            var f = Vector3.UnitZ * 2;
            var u = Vector3.UnitY;

            Vector3 s;
            LocalSpaceBasisHelpers.RegenerateOrthonormalBasis(f, u, out f, out s, out u);

            Assert.AreEqual(Vector3.UnitZ, f);
            Assert.AreEqual(u, Vector3.UnitY);
            Assert.AreEqual(Vector3.Cross(f, u), s);
        }
    }
}
