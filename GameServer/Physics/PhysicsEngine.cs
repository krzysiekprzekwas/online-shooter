using System;
using System.Collections.Generic;
using System.Numerics;
using GameServer.Game;
using GameServer.MapObjects;
using GameServer.Models;
using GameServer.States;

namespace GameServer.Physics
{
    public class PhysicsEngine
    {
        private GameEngine _gameEngine;
        public PhysicsEngine(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }

        public void ApplyPhysics()
        {
            foreach (Player player in GameState.Instance.Players)
            {
                player.Speed *= (1 - Config.PLAYER_DECCELERATION);
                Vector2 speedVector = player.Speed + GetSpeedFromPlayerInput(player);

                speedVector = CalculatePossibleMovementVector(player, speedVector);

                //player.Speed = speedVector;
                //player.Position += speedVector;
            }
        }

        public static Vector2 GetSpeedFromPlayerInput(Player player)
        {
            Vector2 upVector = new Vector2(0, 1);
            Vector2 rightVector = new Vector2(1, 0);
            Vector2 calculatedSpeedVector = new Vector2(0, 0);

            // First get direction
            if (player.Keys.Contains("w"))
                calculatedSpeedVector += upVector;
            if (player.Keys.Contains("s"))
                calculatedSpeedVector -= upVector;
            if (player.Keys.Contains("a"))
                calculatedSpeedVector -= rightVector;
            if (player.Keys.Contains("d"))
                calculatedSpeedVector += rightVector;

            // Scale vector to be speed length
            if (calculatedSpeedVector.LengthSquared() > 0)
            {
                Vector2 normalizedSpeed = Vector2.Normalize(calculatedSpeedVector);
                float vectorLength = Config.PLAYER_SPEED / (float)Config.SERVER_TICK;
                calculatedSpeedVector = normalizedSpeed * vectorLength;
            }

            return calculatedSpeedVector;
        }

        public Vector2 CalculatePossibleMovementVector(Player player, Vector2 speedvector)
        {
            // Variables used to calculate speed vector
            var offset = 0f;
            var speedVectorLength = speedvector.Length();
            var speedVectorNormalized = Vector2.Normalize(speedvector);
            float currentPrecision = speedVectorLength / 2f;
            MapObject intersectionObject = null;

            do
            {
                // Create moved sphere
                Vector2 checkPosition = player.Position + (speedVectorNormalized * (offset + currentPrecision));
                MapCircle s = new MapCircle(checkPosition, player.Diameter);

                // Check for intersection
                intersectionObject = CheckAnyIntersectionWithWorld(s);

                // Update new position and offset
                currentPrecision /= 2f;
                if (intersectionObject == null) //  No object found, increase offset
                    offset += currentPrecision;

            } // Do this as long as we reach desired precision
            while (currentPrecision >= Config.INTERSECTION_INTERVAL);

            return player.Position + (speedVectorNormalized * offset);
            //return new Vector2(0, 0);
        }

        //public MapObject GetIntersectionObjectTowardsDirection(out float offset, Player player, Vector2 speedVectorNormalized, float speedVectorLength)
        //{
        //    // Variables used to calculate speed vector
        //    offset = 0f;
        //    float currentPrecision = speedVectorLength / 2f;
        //    MapObject intersectionObject = null;

        //    do
        //    {
        //        // Create moved sphere
        //        Vector2 checkPosition = player.Position + (speedVectorNormalized * (offset + currentPrecision));
        //        MapCircle s = new MapCircle(checkPosition, player.Diameter);

        //        // Check for intersection
        //        intersectionObject = CheckAnyIntersectionWithWorld(s);

        //        // Update new position and offset
        //        if (intersectionObject != null) // Object found try a bit closer
        //            currentPrecision /= 2f;
        //        else // No object found, increase offset
        //        {
        //            offset += currentPrecision;
        //            currentPrecision /= 2f;
        //        }


        //    } // Do this as long as we reach desired precision
        //    while (currentPrecision >= Config.INTERSECTION_INTERVAL);

        //    return intersectionObject;
        //}

        //public Vector2 GetNewPlayerPosition(Player player, Vector2 speedVector)
        //{
        //    // Calculate length
        //    float speedVectorLength = speedVector.Length();

        //    // Almost standing still
        //    if (speedVectorLength == 0)
        //        return new Vector2(0, 0);

        //    Vector2 speedVectorNormalized = Vector2.Normalize(speedVector);

        //    // Get intersection object
        //    MapObject intersectionObject = GetIntersectionObjectTowardsDirection(out float offset, player, speedVectorNormalized, speedVectorLength);

        //    Vector2 parralelVector = GetParralelMovementVector(player, speedVector, speedVectorLength, intersectionObject, offset);

        //    // Update speed vector
        //    return (speedVectorNormalized * offset) + parralelVector;
        //}

        public MapObject CheckAnyIntersectionWithWorld(MapCircle s)
        {
            // Check intersection with all map objects
            foreach (MapObject obj in MapState.Instance.MapObjects)
            {
                bool intersects = false;
                if (obj is MapRect)
                    intersects = Intersection.CheckIntersection((MapRect)obj, s);
                else if (obj is MapCircle)
                    intersects = Intersection.CheckIntersection((MapCircle)obj, s);

                if (intersects)
                    return obj;
            }

            return null;
        }
    }
}
