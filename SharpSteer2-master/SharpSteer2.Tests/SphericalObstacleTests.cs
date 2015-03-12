using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using SharpSteer2.Obstacles;

namespace SharpSteer2.Tests
{
    [TestClass]
    public class SphericalObstacleTests
    {
        private readonly SphericalObstacle _obstacle = new SphericalObstacle(10, Vector3.Zero);

        private readonly SimpleVehicle _vehicle = new SimpleVehicle(null);

        [TestMethod]
        public void SteerToAvoidReturnsZeroVectorIfThereIsNoIntersection()
        {
            _vehicle.Position = new Vector3(100, 100, 100);

            Assert.AreEqual(Vector3.Zero, _obstacle.SteerToAvoid(_vehicle, 1));
        }

        [TestMethod]
        public void SteerToAvoidReturnsNonZeroVectorForStationaryVehicleInsideObstacle()
        {
            _vehicle.Position = new Vector3(0, 0, 1);

            Assert.AreNotEqual(Vector3.Zero, _obstacle.SteerToAvoid(_vehicle, 1));
        }

        [TestMethod]
        public void SteerToAvoidReturnsNonZeroVectorForMovingVehicleOutsideObstacle()
        {
            _vehicle.Position = -_vehicle.Forward * 11;
            _vehicle.ApplySteeringForce(_vehicle.Forward, 3);

            var f = _obstacle.SteerToAvoid(_vehicle, 10);
            var dot = Vector3.Dot(_vehicle.Position - _obstacle.Center, f);

            Assert.IsTrue(dot >= 0);
        }
    }
}
