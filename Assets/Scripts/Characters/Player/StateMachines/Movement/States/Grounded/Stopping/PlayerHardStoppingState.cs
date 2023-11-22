namespace Basic3rdPersonMovementAndCamera
{
    public class PlayerHardStoppingState : PlayerStoppingState
    {
        public PlayerHardStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {

        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.Player.animationData.HardStopParameterHash);

            stateMachine.ReusableData.MovementDecelerationForce = movementData.StopData.HardDecelerationForce;

            stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StrongForce;

        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.animationData.HardStopParameterHash);

        }
        #endregion

        #region Reusable Methods
        protected override void OnMove()
        {
            if (stateMachine.ReusableData.ShouldWalk)
            {
                return;
            }

            stateMachine.ChangeState(stateMachine.RunningState);
        }
        #endregion
    }
}
