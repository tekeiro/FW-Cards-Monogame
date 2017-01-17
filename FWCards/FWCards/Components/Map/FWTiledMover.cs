using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Nez;

namespace FWCards.Components.Map
{

    public struct FWTiledCollisionResult
    {
        public Collider collider;
        public bool collides;
        public Vector2 resultantMotion;
    }


    /// <summary>
    /// Helper class to move a Tiled Map Entity across map and taking
    /// account of all Colliders.
    /// Based on code of <see cref="Mover"/>
    /// </summary>
    public class FWTiledMover : Component
    {
        private ColliderTriggerHelper _triggerHelper;

        public override void onAddedToEntity()
        {
            _triggerHelper = new ColliderTriggerHelper(entity);
        }


        public FWTiledCollisionResult move(Vector2 motion)
        {
            FWTiledCollisionResult colResult = new FWTiledCollisionResult();
            colResult.collider = null;
            colResult.collides = false;
            colResult.resultantMotion = motion;

            var colliders = entity.getComponents<Collider>();

            // No collider?
            if (colliders.Count == 0 || _triggerHelper == null)
            {
                return colResult;
            }

            // 1. Move all non-trigger Colliders and get closest Collision
            foreach (var collider in colliders)
            {
                // Skip triggers for noew. We will revisit them after.
                if (collider.isTrigger)
                    continue;

                // Fetch anything that we might collide with at our new position
                var bounds = new RectangleF(collider.bounds.location, collider.bounds.size);
                bounds.x += motion.X;
                bounds.y += motion.Y;
                var neighbors = Physics.boxcastBroadphaseExcludingSelf(collider, ref bounds, collider.collidesWithLayers);

                foreach (var neighbor in neighbors)
                {
                    // Skip triggers for now. We will revisit them after we move.
                    if (neighbor.isTrigger)
                        continue;

                    CollisionResult colResult2;
                    if (collider.collidesWith(neighbor, motion, out colResult2))
                    {
                        colResult.collider = colResult2.collider;
                        colResult.collides = colResult2.collider != null;
                        colResult.resultantMotion = motion - colResult2.minimumTranslationVector;
                    }
                }
            }

            ListPool<Collider>.free(colliders);

            // 2. do an overlap check of all Colliders that are triggers with all broadphase colliders, triggers or not.
            //    Any overlaps result in trigger events.
            _triggerHelper.update();

            return colResult;
        }
    }
}
