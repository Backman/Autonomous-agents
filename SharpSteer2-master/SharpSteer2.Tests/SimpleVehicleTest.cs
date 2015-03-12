using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace SharpSteer2.Tests
{
    [TestClass]
    public class SimpleVehicleTest
    {
        private readonly TestVehicle _vehicle = new TestVehicle();

        [TestMethod]
        public void Construct()
        {
            Assert.AreEqual(Vector3.Zero, _vehicle.Acceleration);
            Assert.AreEqual(Vector3.Forward, _vehicle.Forward);
            Assert.AreEqual(Vector3.Zero, _vehicle.Velocity);
            Assert.AreEqual(0, _vehicle.Speed);
            Assert.AreEqual(Vector3.Zero, _vehicle.SmoothedPosition);
        }

        [TestMethod]
        public void ApplyForce()
        {
            _vehicle.ApplySteeringForce(Vector3.Forward, 1);

            Assert.AreEqual(Vector3.Forward * _vehicle.Speed, _vehicle.Velocity);
        }

        private class TestVehicle
            : SimpleVehicle
        {
            public new void ApplySteeringForce(Vector3 steer, float dt)
            {
                base.ApplySteeringForce(steer, dt);
            }
        }
    }
}
