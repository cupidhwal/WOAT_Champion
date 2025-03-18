using System;

namespace Seti
{
    /// <summary>
    /// ControlType.Input Controller
    /// </summary>
    public class Controller_Input : Controller_Base
    {
        // 필드
        #region Variables
        private InputSystem_Actions control;
        #endregion

        // 인터페이스
        #region Interface
        public override Type GetControlType() => typeof(Control_Input);
        #endregion

        // 라이프 사이클
        #region Life Cycle
        protected override void Awake()
        {
            base.Awake();

            // 초기화
            control = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            // 입력 이벤트 연결
            BindInputEvents();
            control.Enable();
        }

        private void OnDisable()
        {
            // 입력 이벤트 해제
            UnbindInputEvents();
            control.Disable();
        }
        #endregion

        // 메서드
        #region Methods
        private void BindInputEvents()
        {
            // Look 행동 이벤트 바인딩
            if (behaviourMap.TryGetValue(typeof(Look), out var lookBehaviour))
            {
                Look look = lookBehaviour as Look;
                if (look.HasStrategy<Look_Normal>())
                {
                    control.Player.Look.performed += look.OnLookPerformed;
                    control.Player.Look.canceled += look.OnLookCanceled;
                }
                if (look.HasStrategy<Look_KeepGoing>())
                {
                    control.Player.KeepGoing.started += look.OnKeepGoingStarted;
                    control.Player.KeepGoing.canceled += look.OnKeepGoingCanceled;
                }
            }

            // Move 행동 이벤트 바인딩
            if (behaviourMap.TryGetValue(typeof(Move), out var moveBehaviour))
            {
                Move move = moveBehaviour as Move;
                if (move.HasStrategy<Move_Normal>() || move.HasStrategy<Move_Walk>())
                {
                    control.Player.Move.performed += move.OnMovePerformed;
                    control.Player.Move.canceled += move.OnMoveCanceled;
                }
                if (move.HasStrategy<Move_Run>())
                {
                    control.Player.Sprint.started += move.OnRunStarted;
                }
            }

            // Dash 행동 이벤트 바인딩
            if (behaviourMap.TryGetValue(typeof(Dash), out var dashBehaviour))
            {
                Dash dash = dashBehaviour as Dash;
                control.Player.Dash.started += dash.OnDashStarted;
            }

            // Jump 행동 이벤트 바인딩
            //if (behaviourMap.TryGetValue(typeof(Jump), out var jumpBehaviour))
            //{
            //    Jump jump = jumpBehaviour as Jump;
            //    control.Player.Jump.started += jump.OnJumpStarted;
            //}

            // Interact 행동 이벤트 바인딩
            if (behaviourMap.TryGetValue(typeof(Interact), out var interactBehaviour))
            {
                Interact interact = interactBehaviour as Interact;
                control.Player.Interact.started += interact.OnInteractStarted;
            }
        }

        private void UnbindInputEvents()
        {
            // Look 행동 이벤트 해제
            if (behaviourMap.TryGetValue(typeof(Look), out var lookBehaviour))
            {
                Look look = lookBehaviour as Look;
                if (look.HasStrategy<Look_Normal>())
                {
                    control.Player.Look.performed -= look.OnLookPerformed;
                    control.Player.Look.canceled -= look.OnLookCanceled;
                }
                if (look.HasStrategy<Look_KeepGoing>())
                {
                    control.Player.KeepGoing.started -= look.OnKeepGoingStarted;
                    control.Player.KeepGoing.canceled -= look.OnKeepGoingCanceled;
                }
            }

            // Move 행동 이벤트 해제
            if (behaviourMap.TryGetValue(typeof(Move), out var moveBehaviour))
            {
                Move move = moveBehaviour as Move;
                if (move.HasStrategy<Move_Normal>() || move.HasStrategy<Move_Walk>())
                {
                    control.Player.Move.performed -= move.OnMovePerformed;
                    control.Player.Move.canceled -= move.OnMoveCanceled;
                }
                if (move.HasStrategy<Move_Run>())
                    control.Player.Sprint.started -= move.OnRunStarted;
            }

            // Dash 행동 이벤트 해제
            if (behaviourMap.TryGetValue(typeof(Dash), out var dashBehaviour))
            {
                Dash dash = dashBehaviour as Dash;
                control.Player.Dash.started -= dash.OnDashStarted;
            }

            // Jump 행동 이벤트 해제
            //if (behaviourMap.TryGetValue(typeof(Jump), out var jumpBehaviour))
            //{
            //    Jump jump = jumpBehaviour as Jump;
            //    control.Player.Jump.started -= jump.OnJumpStarted;
            //}

            // Interact 행동 이벤트 해제
            if (behaviourMap.TryGetValue(typeof(Interact), out var interactBehaviour))
            {
                Interact interact = interactBehaviour as Interact;
                control.Player.Interact.started -= interact.OnInteractStarted;
            }
        }
        #endregion
    }
}