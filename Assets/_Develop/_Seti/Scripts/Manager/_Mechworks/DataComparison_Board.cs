using System;

namespace Seti
{
    [Serializable]
    public class DataComparison_Board : DataComparison
    {
        public float oldMaxSpeed, newMaxSpeed;
        public float oldTurnSpeed, newTurnSpeed;
        public float oldTiltSpeed, newTiltSpeed;
        public float oldReverseSpeed, newReverseSpeed;
        public float oldAcceleration, newAcceleration;
        public float oldMomentum, newMomentum;
        public float oldDownForce, newDownForce;
        public float oldBrakeCoefficient, newBrakeCoefficient;

        RidingGear_Board board;
        public override void SetValue(RidingGear gear, Parts parts)
        {
            board = gear as RidingGear_Board;

            CalSpec();
            switch (parts)
            {
                case Receiver:
                    CalSpec(parts as Receiver);
                    break;

                case Transducer:
                    CalSpec(parts as Transducer);
                    break;

                case Propulsor:
                    CalSpec(parts as Propulsor);
                    break;
            }
        }

        // 기존 스펙 연산
        protected override void CalSpec()
        {
            oldMaxSpeed = board.Core.receiver.Efficiency * board.Core.transducer.Efficiency * board.Core.propulsor.Performance;
            oldTurnSpeed = board.Core.propulsor.Agility;
            oldTiltSpeed = board.Core.propulsor.Agility + 1;
            oldReverseSpeed = oldMaxSpeed * 0.4f;
            oldAcceleration = board.Core.transducer.Efficiency * board.Core.propulsor.Acceleration;
            oldMomentum = board.Core.transducer.Efficiency * board.Core.propulsor.Momentum;
            oldDownForce = oldMomentum * 0.4f;
            oldBrakeCoefficient = 0.5f;
        }

        // 갱신 스펙 연산
        protected override void CalSpec(Receiver receiver)
        {
            newMaxSpeed = receiver.Efficiency * board.Core.transducer.Efficiency * board.Core.propulsor.Performance;
            newTurnSpeed = board.Core.propulsor.Agility;
            newTiltSpeed = board.Core.propulsor.Agility + 1;
            newReverseSpeed = newMaxSpeed * 0.4f;
            newAcceleration = board.Core.transducer.Efficiency * board.Core.propulsor.Acceleration;
            newMomentum = board.Core.transducer.Efficiency * board.Core.propulsor.Momentum;
            newDownForce = newMomentum * 0.4f;
            newBrakeCoefficient = 0.5f;
        }
        protected override void CalSpec(Transducer transducer)
        {
            newMaxSpeed = board.Core.receiver.Efficiency * transducer.Efficiency * board.Core.propulsor.Performance;
            newTurnSpeed = board.Core.propulsor.Agility;
            newTiltSpeed = board.Core.propulsor.Agility + 1;
            newReverseSpeed = newMaxSpeed * 0.4f;
            newAcceleration = transducer.Efficiency * board.Core.propulsor.Acceleration;
            newMomentum = transducer.Efficiency * board.Core.propulsor.Momentum;
            newDownForce = newMomentum * 0.4f;
            newBrakeCoefficient = 0.5f;
        }
        protected override void CalSpec(Propulsor propulsor)
        {
            Propulsor_Kinetic kinetic = propulsor as Propulsor_Kinetic;

            newMaxSpeed = board.Core.receiver.Efficiency * board.Core.transducer.Efficiency * propulsor.Performance;
            newTurnSpeed = kinetic.Agility;
            newTiltSpeed = kinetic.Agility + 1;
            newReverseSpeed = newMaxSpeed * 0.4f;
            newAcceleration = board.Core.transducer.Efficiency * kinetic.Acceleration;
            newMomentum = board.Core.transducer.Efficiency * kinetic.Momentum;
            newDownForce = newMomentum * 0.4f;
            newBrakeCoefficient = 0.5f;
        }
    }
}