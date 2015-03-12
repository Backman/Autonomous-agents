package com.rocketmandevelopment.steering {
	import flash.display.Sprite;
	import com.rocketmandevelopment.math.Vector2D;
	
	public class Vehicle extends Sprite {
		private var _mass:Number;
		
		public function get mass():Number {
			return _mass;
		}
		
		/**
		 * Gets and sets the vehicle's mass
		 */
		public function set mass(value:Number):void {
			_mass = value;
		}
		private var _maxSpeed:Number;
		
		public function get maxSpeed():Number {
			return _maxSpeed;
		}
		
		/**
		 * Gets and sets the max speed of the vehicle
		 */
		public function set maxSpeed(value:Number):void {
			_maxSpeed = value;
		}
		private var _position:Vector2D;
		
		public function get position():Vector2D {
			return _position;
		}
		
		/**
		 *Gets and sets the position of the vehicle
		 */
		public function set position(value:Vector2D):void {
			_position = value;
			x = _position.x;
			y = _position.y;
		}
		private var _velocity:Vector2D;
		
		public function get velocity():Vector2D {
			return _velocity;
		}
		
		/**
		 * Gets and sets the velocity of the vehicle
		 */
		public function set velocity(value:Vector2D):void {
			_velocity = value;
		}
		
		/**
		 * Override of the sprite set x function
		 */
		override public function set x(value:Number):void {
			super.x = value;
			_position.x = x;
		}
		
		/**
		 * Override of the sprite set y function
		 */
		override public function set y(value:Number):void {
			super.y = value;
			_position.y = y;
		}
		
		private var _inSightDist:int = 25;
		private var _maxForce:Number;
		
		private var checkLength:Number = 100; //the distance to look ahead for circles
		
		private var circleRadius:int = 6; //the radius of the circle
		
		
		private var index:int = 0; //the current waypoint index in the path array
		
		private var slowingDistance:Number = 20; //slowing distance, you can adjust this
		private var wanderAngle:Number = 0; //the change to the current direction. Produces sustained turned, keeps it from being jerky. Makes it smooth
		private var wanderChange:Number = 1; //the amount to change the angle each frame.
		
		//sprite has rotation
		
		public function Vehicle() {
			_mass = 2;
			_position = new Vector2D(x, y);
			_velocity = new Vector2D();
			_maxForce = 2;
			_maxSpeed = 2;
		}
		
		public function arrive(target:Vector2D):void {
			var desiredVelocity:Vector2D = target.cloneVector().subtract(position).normalize(); //find the straight path and normalize it
			var distance:Number = position.distance(target); //find the distance
			if(distance > slowingDistance) { //if its still too far away
				desiredVelocity.multiply(_maxSpeed); //go at full speed
			} else {
				desiredVelocity.multiply(_maxSpeed * distance / slowingDistance); //if not, slow down
			}
			
			var force:Vector2D = desiredVelocity.subtract(velocity).truncate(_maxForce); //keep the force within the max
			velocity.add(force); //apply the force
		}
		
		public function avoidObstacles(circles:Array):void {
			for(var i:int = 0; i < circles.length; i++) { //loop through the array of obstacles
				var forward:Vector2D = velocity.cloneVector().normalize(); //get the forward vector
				var diff:Vector2D = circles[i].position.cloneVector().subtract(position); //get the difference between the circle and the vehicle
				var dotProd:Number = diff.dotProduct(forward); //get the dot product
				//this will be used for projection
				
				if(dotProd > 0) { //if this object is in front of the vehicle
					var ray:Vector2D = forward.cloneVector().multiply(checkLength); //get the ray
					var projection:Vector2D = forward.cloneVector().multiply(dotProd); //project the forward vector
					var dist:Number = projection.cloneVector().subtract(diff).length; //get the distance between the circle and vehicle
					
					if(dist < circles[i].radius + width && projection.length < ray.length) {
						//if the circle is in your path (radius+width to check the full size of the vehicle)
						//projection.length and ray.length make sure you are within the max distance
						var force:Vector2D = forward.cloneVector().multiply(maxSpeed); //get the max force
						force.angle += diff.sign(velocity) * Math.PI / 2; //rotate it away from the cirlce
						//PI / 2 is 90 degrees, vector's angles are in radians
						//sign returns whether the vector is to the right or left of the other vector
						force.multiply(1 - projection.length / ray.length); //scale the force so that a far off object
						//doesn't drastically change the velocity
						velocity.add(force); //change the velocity
						velocity.multiply(projection.length / ray.length); //and scale again
					}
				}
			}
		}
		
		
		public function evade(target:Vehicle):void {
			var distance:Number = target.position.distance(position);
			var T:Number = distance / target._maxSpeed;
			var targetPosition:Vector2D = target.position.cloneVector().add(target.velocity.cloneVector().multiply(T));
			flee(targetPosition);
		}
		
		public function flee(target:Vector2D):void {
			var desiredVelocity:Vector2D = target.cloneVector().subtract(position).normalize().multiply(maxSpeed);
			var steeringForce:Vector2D = desiredVelocity.subtract(velocity);
			velocity.add(steeringForce.divide(mass).multiply(-1).truncate(_maxForce));
		}
		
		public function flock(vehicles:Array):void {
			var averageVelocity:Vector2D = velocity.cloneVector(); // used for alignment.
			//starting with the current vehicles velocity keeps the vehicles from stopping
			var averagePosition:Vector2D = new Vector2D(); //used for cohesion
			var counter:int = 0; //used for cohesion
			for(var i:int = 0; i < vehicles.length; i++) { //for each vehicle
				var vehicle:Vehicle = vehicles[i] as Vehicle;
				if(vehicle != this && isInSight(vehicle)) { //if it is not the current vehicle
					//and it is in sight
					averageVelocity.add(vehicle.velocity); //add its velocity to the average velocity
					averagePosition.add(vehicle.position); //add its position to the average position
					if(isTooClose(vehicle)) { //if it is too close
						flee(vehicle.position); //flee it, this is separation
					}
					counter++; //increase the counter to use for finding the average
				}
			}
			if(counter > 0) { //if there are vehicles around
				averageVelocity.divide(counter); //divide to find average
				averagePosition.divide(counter); //divide to find average
				seek(averagePosition); //seek the average, this is cohesion
				velocity.add(averageVelocity.subtract(velocity).divide(mass).truncate(_maxForce));
					//add the average velocity to the velocity after adjusting the force
					//this is alignment
			}
		}
		
		
		
		
		public function flowField(field:Array):void {
			var fieldX:int = Math.floor(position.x / 50); //find the vehicles location in the field
			var fieldY:int = Math.floor(position.y / 50);
			if(fieldX < 0 || fieldY < 0 || fieldX > field.length || fieldY > field[0].length) {
				//this is the important part. If the vehicle is not in the field, we do nothing
				//if the x or y is less than 0, it isn't on the field
				//if the x or y is greater than the length of the array(size of the field), you are not in the field
				return;
			}
			var force:Vector2D = field[fieldX][fieldY].cloneVector(); //clone the vector so we don't change it
			velocity.add(force.divide(mass).truncate(_maxForce)); //apply the vector based on mass and within the max force.
		}
		
		
		public function followLeader(leader:Vehicle, vehicles:Array):void {
			//each follower is given an array of all the other vehicles and the leader
			var leaderPos:Vector2D = leader.position.cloneVector(); //get the leaders position
			//clone it so you can modify it
			if(leader.inSight(this)) { //if the leader can see you
				var dist:Number = leaderPos.distance(position); //check the distance
				if(dist < 50) { //if you are within 50px
					seek(leaderPos.add(leader.velocity.perpendicular)); //move away from where the leader is going
					return; //stop following
				}
			}
			//if you are not in the leaders way
			leaderPos.subtract(leader.velocity.cloneVector().normalize().multiply(10)); //get a point behind the leader
			arrive(leaderPos); //arrive at it
			//separation for all other vehicles
			for(var i:int = 0; i < vehicles.length; i++) {
				var vehicle:Vehicle = vehicles[i] as Vehicle;
				if(tooClose(vehicle)) {
					flee(vehicle.position);
				}
			}
		}
		
		public function followPath(path:Array):void {
			if(index >= path.length) { //if you have finished with the path
				velocity.multiply(0.9); //slow down
				return; //quit
			} //otherwise
			var waypoint:Vector2D = path[index]; //get the current waypoint
			var dist:Number = waypoint.distance(position); //get the distance from the waypoint
			if(dist < 10) { //if you are within 10 pixels of the waypoint(can be adjusted based on your needs)
				index++; //go to the next waypoint
				return; //quit for now
			}
			seek(waypoint); //otherwise, seek the current waypoint
		}
		
		public function inSight(vehicle:Vehicle):Boolean {
			if(position.distance(vehicle.position) > _inSightDist) {
				return false;
			}
			var heading:Vector2D = velocity.cloneVector().normalize();
			var difference:Vector2D = vehicle.position.cloneVector().subtract(position);
			var dotProd:Number = difference.dotProduct(heading);
			
			if(dotProd < 0) {
				return false;
			}
			return true;
		}
		
		public function isInSight(vehicle:Vehicle):Boolean {
			if(position.distance(vehicle.position) > 120) { //this is changed based on the desired flocking style
				return false; //you are too far away, do nothing
			}
			var direction:Vector2D = velocity.cloneVector().normalize(); //get the direction
			var difference:Vector2D = vehicle.position.cloneVector().subtract(position); //find the difference of the two positions
			var dotProd:Number = difference.dotProduct(direction); //dotProduct
			
			if(dotProd < 0) {
				return false; //you are too far away and not facing the right direction
			}
			return true; //you are in sight.
		}
		
		public function isTooClose(vehicle:Vehicle):Boolean {
			return position.distance(vehicle.position) < 80;
		}
		
		public function pursuit(target:Vehicle):void {
			var distance:Number = target.position.distance(position);
			var T:Number = distance / target._maxSpeed;
			var targetPosition:Vector2D = target.position.cloneVector().add(target.velocity.cloneVector().multiply(T));
			seek(targetPosition);
		}
		
		
		public function seek(target:Vector2D):void {
			var desiredVelocity:Vector2D = target.cloneVector().subtract(position).normalize().multiply(maxSpeed);
			var steeringForce:Vector2D = desiredVelocity.subtract(velocity);
			velocity.add(steeringForce.divide(mass).truncate(_maxForce));
		}
		
		public function tooClose(vehicle:Vehicle):Boolean {
			return position.distance(vehicle.position) < 50;
		}
		
		public function unalignedAvoidance(vehicles:Array):void {
			for(var i:int = 0; i < vehicles.length; i++) { //check each vehicle
				var forward:Vector2D = velocity.cloneVector().normalize(); //get the forward vector
				var diff:Vector2D = vehicles[i].position.cloneVector().add(vehicles[i].velocity.cloneVector().normalize().multiply(checkLength)).subtract(position);
				var dotProd:Number = diff.dotProduct(forward); //dot product between forward and difference
				if(dotProd > 0) { //they may meet in the future
					var ray:Vector2D = forward.cloneVector().multiply(checkLength); //cast a ray in the forward direction
					var projection:Vector2D = forward.cloneVector().multiply(dotProd); //project the forward vector
					var dist:Number = projection.cloneVector().subtract(diff).length; //get the distance
					
					if(dist < vehicles[i].radius + width && projection.length < ray.length) { //they will meet in the future, we need to fix it
						var force:Vector2D = forward.cloneVector().multiply(maxSpeed); //get the maximum change
						force.angle += diff.sign(velocity) * Math.PI / 4; //rotate it away from the site of the collision
						force.multiply(1 - projection.length / ray.length); //scale it based on the distance between the position and collision site
						velocity.add(force); //adjust the velocity
						velocity.multiply(projection.length / ray.length); //scale the velocity
					}
				}
			}
		}
		
		/**
		 * Updates the vehicle based on velocity
		 */
		public function update():void {
			// keep it witin its max speed
			_velocity.truncate(_maxSpeed);
			
			// move it
			_position = _position.add(_velocity);
			
			// keep it on screen
			if(stage) {
				if(position.x > stage.stageWidth) {
					position.x = 0;
				}
				if(position.x < 0) {
					position.x = stage.stageWidth;
				}
				if(position.y > stage.stageHeight) {
					position.y = 0;
				}
				if(position.y < 0) {
					position.y = stage.stageHeight;
				}
			}
			
			
			// set the x and y, using the super call to avoid this class's implementation
			super.x = position.x;
			super.y = position.y;
			
			// rotation = the velocity's angle converted to degrees
			rotation = _velocity.angle * 180 / Math.PI;
		}
		
		//Those numbers can be changed to get other movement patterns. Those are the defaults used in the demo
		public function wander():void {
			var circleMidde:Vector2D = velocity.cloneVector().normalize().multiply(circleRadius); //circle middle is the the velocity pushed out to the radius.
			var wanderForce:Vector2D = new Vector2D();
			wanderForce.length = 3; //force length, can be changed to get a different motion
			wanderForce.angle = wanderAngle; //set the angle to move
			wanderAngle += Math.random() * wanderChange - wanderChange * .5; //change the angle randomly to make it wander
			var force:Vector2D = circleMidde.add(wanderForce); //apply the force
			velocity.add(force); //then update
		}
	}
}