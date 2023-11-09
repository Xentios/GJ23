//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Controls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""a9b938d3-1b87-42b8-87eb-df6e8cff27f3"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""8ff85575-7af6-4f6a-908f-37b2c88afc17"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""12c1ebc9-cb4b-4910-9deb-60c216caf2e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseAim"",
                    ""type"": ""Value"",
                    ""id"": ""69e59642-b340-4ded-b4be-bcc31c2bd848"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseLeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""1b1018f4-c268-46f5-8e92-ca32af4cfff9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseRightClick"",
                    ""type"": ""Button"",
                    ""id"": ""e70032b9-416f-43d9-b5d2-cdfccf933b29"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseScrollWithCTRL"",
                    ""type"": ""Value"",
                    ""id"": ""f16f8b85-2158-4a46-b7d1-31b2f6ec00d5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseScrollWithALT"",
                    ""type"": ""Value"",
                    ""id"": ""7dd26571-3d68-4ce2-96fd-07e5ed3d7253"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseScroll"",
                    ""type"": ""Value"",
                    ""id"": ""a3c5d377-b534-477d-9cae-98fd462019b9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseScroll With SHIFT"",
                    ""type"": ""Value"",
                    ""id"": ""9f225901-2a45-41b0-8993-f7c40c41c0ac"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""0f1cea72-8100-4f3e-b482-72211c1d3a60"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c1785da2-0c4b-48f2-9dd9-9a70aef7b06c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a43e6381-553a-47d5-871b-0cdee6628b0e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d3232bf6-b63b-4578-bbc8-a16394229a28"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""62588bda-cecb-4a92-b896-4985c9d8e47e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD Old School"",
                    ""id"": ""43ec233b-0c04-4b0e-b540-87212243cd10"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ba271965-2e34-4c43-8763-3036579a9e9a"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""da484aea-7b0c-4238-b080-f1597b0db4b7"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""74d5b649-dcb1-47b0-833a-01b673e04678"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""cbc88a58-477f-4f6d-8ec0-d9b5ecdaaf8c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c0db8466-d2ec-42e3-a541-d56d100f355b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da878ffb-cb61-41c7-96a9-556b99aaaec0"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b016500-195f-4378-8e20-85fd7c97c47d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb330a26-5ea3-4ddd-a763-bc6deca53994"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseRightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Axis Modifier"",
                    ""id"": ""9f9b748d-2f0a-4c4b-8af0-082486f5d59a"",
                    ""path"": ""AxisModifier(minValue=0,maxValue=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScrollWithCTRL"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c8ddaaa4-001f-4e87-9107-b45b6ca41d81"",
                    ""path"": ""<Mouse>/scroll/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScrollWithCTRL"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""09df0e9d-a969-479d-9b4c-e5f7ddf8fd4e"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScrollWithCTRL"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Axis Modifier"",
                    ""id"": ""acbd3108-64fd-4672-b668-5426cfe00f8f"",
                    ""path"": ""AxisModifier(minValue=0,maxValue=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScrollWithCTRL"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""91104c13-1d2a-4cc1-81a9-ce33a50bbbe4"",
                    ""path"": ""<Mouse>/scroll/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScrollWithCTRL"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Axis Modifier"",
                    ""id"": ""a59e2c52-1f5f-41ed-8569-1a2b2a833838"",
                    ""path"": ""AxisModifier(minValue=0,maxValue=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScrollWithALT"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""34988d25-e808-4364-9f63-b645c04edb05"",
                    ""path"": ""<Mouse>/scroll/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScrollWithALT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""cbbd76cf-7fe4-4d30-b910-e1b92c544e1c"",
                    ""path"": ""<Keyboard>/alt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScrollWithALT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Axis Modifier"",
                    ""id"": ""2cd0cbfa-a51b-4a7f-819c-610dc99cfa30"",
                    ""path"": ""AxisModifier(minValue=0,maxValue=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScrollWithALT"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""940c854f-7687-4294-9e90-2065b3060725"",
                    ""path"": ""<Mouse>/scroll/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScrollWithALT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""b06e70fa-665f-4729-a7d9-f2cd47a2194a"",
                    ""path"": ""<Keyboard>/alt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScrollWithALT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""435ac0f2-2240-4015-8427-293b688b1832"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScroll"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""64367462-25f5-4440-9a35-c98299e85dd6"",
                    ""path"": ""<Mouse>/scroll/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""fc52c309-2c40-4c51-8258-5c2fa7ef1fb7"",
                    ""path"": ""<Mouse>/scroll/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Axis Modifier"",
                    ""id"": ""13f0fe5a-e6a1-4d1c-bcac-2fa708ff7046"",
                    ""path"": ""AxisModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScroll With SHIFT"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""46ccd3cc-aaf3-4a4b-98d4-0f9226aa10d3"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScroll With SHIFT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""23ff2c39-e15b-421f-acd5-2b217ae93325"",
                    ""path"": ""<Mouse>/scroll/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScroll With SHIFT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ad68c018-8d8b-409f-a9a5-3a85c11bc2cc"",
                    ""path"": ""<Mouse>/scroll/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScroll With SHIFT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_MouseAim = m_Player.FindAction("MouseAim", throwIfNotFound: true);
        m_Player_MouseLeftClick = m_Player.FindAction("MouseLeftClick", throwIfNotFound: true);
        m_Player_MouseRightClick = m_Player.FindAction("MouseRightClick", throwIfNotFound: true);
        m_Player_MouseScrollWithCTRL = m_Player.FindAction("MouseScrollWithCTRL", throwIfNotFound: true);
        m_Player_MouseScrollWithALT = m_Player.FindAction("MouseScrollWithALT", throwIfNotFound: true);
        m_Player_MouseScroll = m_Player.FindAction("MouseScroll", throwIfNotFound: true);
        m_Player_MouseScrollWithSHIFT = m_Player.FindAction("MouseScroll With SHIFT", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_MouseAim;
    private readonly InputAction m_Player_MouseLeftClick;
    private readonly InputAction m_Player_MouseRightClick;
    private readonly InputAction m_Player_MouseScrollWithCTRL;
    private readonly InputAction m_Player_MouseScrollWithALT;
    private readonly InputAction m_Player_MouseScroll;
    private readonly InputAction m_Player_MouseScrollWithSHIFT;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @MouseAim => m_Wrapper.m_Player_MouseAim;
        public InputAction @MouseLeftClick => m_Wrapper.m_Player_MouseLeftClick;
        public InputAction @MouseRightClick => m_Wrapper.m_Player_MouseRightClick;
        public InputAction @MouseScrollWithCTRL => m_Wrapper.m_Player_MouseScrollWithCTRL;
        public InputAction @MouseScrollWithALT => m_Wrapper.m_Player_MouseScrollWithALT;
        public InputAction @MouseScroll => m_Wrapper.m_Player_MouseScroll;
        public InputAction @MouseScrollWithSHIFT => m_Wrapper.m_Player_MouseScrollWithSHIFT;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @MouseAim.started += instance.OnMouseAim;
            @MouseAim.performed += instance.OnMouseAim;
            @MouseAim.canceled += instance.OnMouseAim;
            @MouseLeftClick.started += instance.OnMouseLeftClick;
            @MouseLeftClick.performed += instance.OnMouseLeftClick;
            @MouseLeftClick.canceled += instance.OnMouseLeftClick;
            @MouseRightClick.started += instance.OnMouseRightClick;
            @MouseRightClick.performed += instance.OnMouseRightClick;
            @MouseRightClick.canceled += instance.OnMouseRightClick;
            @MouseScrollWithCTRL.started += instance.OnMouseScrollWithCTRL;
            @MouseScrollWithCTRL.performed += instance.OnMouseScrollWithCTRL;
            @MouseScrollWithCTRL.canceled += instance.OnMouseScrollWithCTRL;
            @MouseScrollWithALT.started += instance.OnMouseScrollWithALT;
            @MouseScrollWithALT.performed += instance.OnMouseScrollWithALT;
            @MouseScrollWithALT.canceled += instance.OnMouseScrollWithALT;
            @MouseScroll.started += instance.OnMouseScroll;
            @MouseScroll.performed += instance.OnMouseScroll;
            @MouseScroll.canceled += instance.OnMouseScroll;
            @MouseScrollWithSHIFT.started += instance.OnMouseScrollWithSHIFT;
            @MouseScrollWithSHIFT.performed += instance.OnMouseScrollWithSHIFT;
            @MouseScrollWithSHIFT.canceled += instance.OnMouseScrollWithSHIFT;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @MouseAim.started -= instance.OnMouseAim;
            @MouseAim.performed -= instance.OnMouseAim;
            @MouseAim.canceled -= instance.OnMouseAim;
            @MouseLeftClick.started -= instance.OnMouseLeftClick;
            @MouseLeftClick.performed -= instance.OnMouseLeftClick;
            @MouseLeftClick.canceled -= instance.OnMouseLeftClick;
            @MouseRightClick.started -= instance.OnMouseRightClick;
            @MouseRightClick.performed -= instance.OnMouseRightClick;
            @MouseRightClick.canceled -= instance.OnMouseRightClick;
            @MouseScrollWithCTRL.started -= instance.OnMouseScrollWithCTRL;
            @MouseScrollWithCTRL.performed -= instance.OnMouseScrollWithCTRL;
            @MouseScrollWithCTRL.canceled -= instance.OnMouseScrollWithCTRL;
            @MouseScrollWithALT.started -= instance.OnMouseScrollWithALT;
            @MouseScrollWithALT.performed -= instance.OnMouseScrollWithALT;
            @MouseScrollWithALT.canceled -= instance.OnMouseScrollWithALT;
            @MouseScroll.started -= instance.OnMouseScroll;
            @MouseScroll.performed -= instance.OnMouseScroll;
            @MouseScroll.canceled -= instance.OnMouseScroll;
            @MouseScrollWithSHIFT.started -= instance.OnMouseScrollWithSHIFT;
            @MouseScrollWithSHIFT.performed -= instance.OnMouseScrollWithSHIFT;
            @MouseScrollWithSHIFT.canceled -= instance.OnMouseScrollWithSHIFT;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMouseAim(InputAction.CallbackContext context);
        void OnMouseLeftClick(InputAction.CallbackContext context);
        void OnMouseRightClick(InputAction.CallbackContext context);
        void OnMouseScrollWithCTRL(InputAction.CallbackContext context);
        void OnMouseScrollWithALT(InputAction.CallbackContext context);
        void OnMouseScroll(InputAction.CallbackContext context);
        void OnMouseScrollWithSHIFT(InputAction.CallbackContext context);
    }
}
