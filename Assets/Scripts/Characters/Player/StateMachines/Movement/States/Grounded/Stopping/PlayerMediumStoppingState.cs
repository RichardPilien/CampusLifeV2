namespace Basic3rdPersonMovementAndCamera
{
    public class PlayerMediumStoppingState : PlayerStoppingState
    {
        public PlayerMediumStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {

        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.Player.animationData.MediumStopParameterHash);


            stateMachine.ReusableData.MovementDecelerationForce = movementData.StopData.MediumDecelerationForce;

            stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.MediumForce;

        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.animationData.MediumStopParameterHash);
        }
        #endregion
    }
}
