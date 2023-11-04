using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ADV
{
    public class CardMovement : MonoBehaviour {
        public Card Source;
        public Vector2 CurrentDirection;
        public float Speed;
        [Space]
        public bool Collision;
        public bool ChangeOnContact;
        public float CollisionSpeed;

        public void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void MovementUpdate(float Delay)
        {
            if (!Source.CombatActive())
                return;
            if (Collision)
                CollisionUpdate(Delay);

            if (Source.GetKey("FollowCursor") == 1)
                CurrentDirection = (Cursor.Main.GetPosition() - Source.GetPosition()).normalized;

            NextMovement(Delay);
        }

        public void ChangeDirection()
        {
            CurrentDirection = new Vector2(Random.Range(-9.9f, 9.9f), Random.Range(-9.9f, 9.9f));
        }

        public void SetDirection(float X, float Y)
        {
            CurrentDirection = new Vector2(X, Y);
        }

        public void NextMovement(float Delay)
        {
            Vector2 Next = CurrentDirection.normalized * Delay * Speed;
            float Try = 100;
            while (((Next.x < 0 && Source.GetPosition().x + Next.x - Source.Radius < CombatControl.CombatRangeLeft)
                || (Next.x > 0 && Source.GetPosition().x + Next.x + Source.Radius > CombatControl.CombatRangeRight)
                || (Next.y < 0 && Source.GetPosition().y + Next.y - Source.Radius < CombatControl.CombatRangeDown)
                || (Next.y > 0 && Source.GetPosition().y + Next.y + Source.Radius > CombatControl.CombatRangeUp)) && Try > 0)
            {
                Try--;
                ChangeDirection();
                Next = CurrentDirection.normalized * Delay * Speed;
            }
            if (Source.GetKey("FollowCursor") == 1 && (Cursor.Main.GetPosition() - Source.GetPosition()).magnitude < Next.magnitude)
                Next = Cursor.Main.GetPosition() - Source.GetPosition();
            Source.ChangePosition(Next);

            if (Next.x != 0 || Next.y != 0)
                Source.Anim.Animator.SetBool("Moving", true);
            else
                Source.Anim.Animator.SetBool("Moving", false);
        }

        public void CollisionUpdate(float Delay)
        {
            List<Card> Collisions = Targeting.TargetInRange(Source.GetPosition(), Source.Radius - 0.1f);
            if (Collisions.Count <= 1)
                return;
            Vector2 CollisionDirection = new Vector2();
            bool Contact = false;
            foreach (Card C in Collisions)
            {
                if (C == Source || !C.Movement || !C.Movement.Collision || !C.CombatActive())
                    continue;
                Contact = true;
                CollisionDirection += (Source.GetPosition() - C.GetPosition()).normalized;
            }
            if (!Contact)
                return;
            if (ChangeOnContact)
                CurrentDirection = CollisionDirection;
            Source.ChangePosition(CollisionDirection * Delay * CollisionSpeed);
        }
    }
}