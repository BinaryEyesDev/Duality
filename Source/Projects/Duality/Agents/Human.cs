using System;
using System.Collections.Generic;
using System.Linq;
using Duality.Components;
using Duality.Data;
using Duality.Editing;
using Duality.Extensions;
using Duality.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Orca.Logging;

namespace Duality.Agents
{
    public class Human
        : IAgent
    {
        public AgentState State { get; set; }
        public Sprite Sprite { get; set; }
        public string Name => Sprite.Id.Name;
        public Transform2D Transform => Sprite.Transform;
        public DirectionInfo? TargetDirection;
        public Dictionary<string, Texture2D[]> Animations;
        public string CurrentAnimation;
        public const float TimeBetweenFrames = 0.2f;
        public float WalkSpeed;

        public Human Initialize()
        {
            _lastIndex = GridIndex.Invalid;
            _idle = Sprite.Image;
            _timeUntilNextFrame = TimeBetweenFrames;
            _currentAnimationFrame = 0;
            WalkSpeed = 20.0f;
            CurrentAnimation = "WalkUp";
            Animations = new Dictionary<string, Texture2D[]>();

            var groups = GameDriver.Instance.TextureRegistry.Groupings;
            var groupsFound = groups.TryGetValue("Animations", out var animationsGroups);
            if (!groupsFound) return this;

            var animationFound = animationsGroups.TryGetValue(Name, out var textures);
            if (!animationFound) return this;

            AddAnimationTextures("WalkDown", textures);
            AddAnimationTextures("WalkUp", textures);
            AddAnimationTextures("WalkLeft", textures);
            AddAnimationTextures("WalkRight", textures);
            return this;
        }

        private void AddAnimationTextures(string animationName, List<Texture2D> textures)
        {
            try
            {
                var animation = new List<Texture2D>(textures.Count);
                var walkAnimation = textures.Where(entry => entry.Name.Contains(animationName)).ToList();
                for (var i = 0; i < walkAnimation.Count; i++)
                {
                    var texture = walkAnimation.FirstOrDefault(entry => entry.Name.Contains($"{i+1}"));
                    if (texture == null)
                        throw new NullReferenceException($"failed to locate animation frame {i+1}");
                    
                    animation.Add(texture);
                }

                Animations.Add(animationName, animation.ToArray());
            }
            catch (Exception e)
            {
                Log.Warning($"failed to generate animation: {animationName}\n{e.Message}");
            }
        }

        public void SolveWorldActions(GameDriver driver)
        {
            TargetDirection ??= LookForTarget(Sprite.Transform);
            CurrentAnimation = DetermineCurrentAnimation();
            if (CurrentAnimation == "Idle")
                Sprite.Image = _idle;
            else
                RunAnimation();

            if (TargetDirection == null) return;
            var info = TargetDirection.Value;
            var targetPosition = CalculateGridFromWorld.GetWorldPosition(info.Index);
            var direction = Vector2.Normalize(targetPosition - Transform.Position);
            Transform.Position += direction*WalkSpeed*FrameTime.ScaledElapsed;

            var distance = Vector2.Distance(Transform.Position, targetPosition);
            if (distance > 1.0f) return;

            _lastIndex = info.Index;
            TargetDirection = null;
        }

        private string DetermineCurrentAnimation()
        {
            if (TargetDirection == null) return "Idle";
            
            var direction = TargetDirection.Value;
            if (Math.Abs(direction.Angle - 270.0f) < 0.001f) return "WalkLeft";
            if (Math.Abs(direction.Angle - 180.0f) < 0.001f) return "WalkDown";
            if (Math.Abs(direction.Angle - 90.0f) < 0.001f) return "WalkRight";
            if (Math.Abs(direction.Angle - 0.0f) < 0.001f) return "WalkUp";
            return "Idle";
        }

        public DirectionInfo? LookForTarget(Transform2D transform)
        {
            DirectionInfo? direction = null;
            DirectionInfo? lastDirection = null;

            var grassLayerIndex = GameViewManager.CalculateLayerIndex("Grass");
            var buildingLayerIndex = GameViewManager.CalculateLayerIndex("Buildings");
            var natureLayerIndex = GameViewManager.CalculateLayerIndex("Nature");

            var cellPosition = CalculateGridFromWorld.GetGridWorldPosition(transform.Position);
            var cell = CalculateGridFromWorld.GetGridIndex(cellPosition);
            var adjacentInfos = GenerateAdjacentDirectionInfos.CardinalOnly(cell);
            lastDirection = GetLastUsed(adjacentInfos);

            while (adjacentInfos.Count > 0 && direction == null)
            {
                var adjacentInfo = adjacentInfos.PopRandom();
                if (!adjacentInfo.Index.IsValid) continue;

                var adjacent = GameDriver.Instance.World[adjacentInfo.Index];
                var grassLayer = adjacent.Layers[grassLayerIndex];
                if (grassLayer is not {Id: {SubGroup: "Grass"}}) continue;

                var buildingLayer = adjacent.Layers[buildingLayerIndex];
                if (buildingLayer != null) continue;

                var natureLayer = adjacent.Layers[natureLayerIndex];
                if (natureLayer != null && natureLayer.Id.Name.Contains("Tree"))
                    continue;

                direction = adjacentInfo;
            }

            return direction ?? lastDirection;
        }

        private DirectionInfo? GetLastUsed(IList<DirectionInfo> adjacentInfos)
        {
            if (!_lastIndex.IsValid) return null;

            for (var i=adjacentInfos.Count-1; i>=0; i--)
            {
                if (adjacentInfos[i].Index != _lastIndex) continue;
                var info = adjacentInfos[i];
                
                adjacentInfos.RemoveAt(i);
                return info;
            }

            return null;
        }

        private void RunAnimation()
        {
            _timeUntilNextFrame -= FrameTime.ScaledElapsed;
            if (_timeUntilNextFrame > 0.0f) return;

            var animationFound = Animations.TryGetValue(CurrentAnimation, out var animation);
            if (!animationFound) return;

            _currentAnimationFrame = (_currentAnimationFrame + 1)%animation.Length;
            _timeUntilNextFrame = TimeBetweenFrames;
            Sprite.Image = animation[_currentAnimationFrame];
        }

        public void Kill()
        {
            
        }

        private GridIndex _lastIndex;
        private int _currentAnimationFrame;
        private float _timeUntilNextFrame;
        private Texture2D _idle;
    }
}
