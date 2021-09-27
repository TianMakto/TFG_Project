// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""23ba313e-ffa6-42ba-a17c-c7b80ad2680c"",
            ""actions"": [
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Value"",
                    ""id"": ""7c8fbaae-279c-477b-a18c-437747f03712"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""b60f7e6a-361a-46bd-830f-8e1283069d07"",
                    ""expectedControlType"": ""Dpad"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GamepadAim"",
                    ""type"": ""Value"",
                    ""id"": ""7047fc3e-abd8-4ded-9b95-c8201954d6ee"",
                    ""expectedControlType"": ""Dpad"",
                    ""processors"": ""StickDeadzone"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""54929365-fddf-491a-91ea-959de0e08594"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""eeee7dae-f212-464f-bef8-265937c2e081"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PauseGame"",
                    ""type"": ""Button"",
                    ""id"": ""1d73b401-2c89-48f2-950e-0a31e7539994"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7f71c3b8-97a8-4ccc-850b-fea14cb9d1f7"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse And Keyboard"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a787a2d-71ed-4422-b249-c0d82cfcabad"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8e7ebe9-6d65-4f21-a2a1-6d2494cb60a6"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""e01ce8e3-f048-4e0e-9e3a-f7c0843adca3"",
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
                    ""id"": ""ee24c229-98b5-419e-ac4b-917dcc4a3a06"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse And Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5a3604f9-a33b-4e8a-932c-84f209066c16"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse And Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ffc6b498-df44-475d-b872-9137d8be35e5"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse And Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a07d977d-ce89-42d9-a2fe-67bad30ea31e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse And Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left Stick Gamepad"",
                    ""id"": ""6b7c31ca-6379-4ab6-bb23-f8fefb33b9c1"",
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
                    ""id"": ""be096962-45d0-4240-a393-206aafdd5d57"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3237e2c4-b676-4af0-ad5c-d654e28ee7f6"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""08c4e8a5-0827-4121-858b-ed957b4b02e6"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a3f4cc2d-4505-4d5f-b22c-762fa06428f8"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""398cf1dd-0cd2-471b-9e6a-2758b1345ef3"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse And Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b6d8f14-b198-4249-b5c1-60faeb90df7f"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41980f71-ce2e-41cf-b3a0-ad348f03fbb5"",
                    ""path"": ""<DualShockGamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1bc3e592-54a8-43fe-91a3-cdc1ae587361"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse And Keyboard"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee2d3925-4027-4f17-ac62-af8b978605e6"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e8f422cd-1b17-4de4-8967-7d7de77304d2"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b2df8f5-0b5e-4b12-8382-87abefaa48b0"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse And Keyboard"",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89b89a11-ede9-4aa9-887b-b33a1e7291af"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c79a198a-89c8-4b54-adb6-7ef466474e75"",
                    ""path"": ""<DualShockGamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Aim"",
                    ""id"": ""0a3d9d25-216c-4fe1-aac6-ebd5093e5952"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GamepadAim"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7d682b1a-2bda-4548-8cf1-98ecad875027"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GamepadAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a4dc4e35-54a7-4d36-8e3e-1aa27986a94c"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GamepadAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7c3cd9c8-f675-438e-a148-c277a286f97f"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GamepadAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5310b9d8-50da-4b2d-9318-9703a913e586"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GamepadAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse And Keyboard"",
            ""bindingGroup"": ""Mouse And Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Shoot = m_Player.FindAction("Shoot", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_GamepadAim = m_Player.FindAction("GamepadAim", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_Reload = m_Player.FindAction("Reload", throwIfNotFound: true);
        m_Player_PauseGame = m_Player.FindAction("PauseGame", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Shoot;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_GamepadAim;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_Reload;
    private readonly InputAction m_Player_PauseGame;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shoot => m_Wrapper.m_Player_Shoot;
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @GamepadAim => m_Wrapper.m_Player_GamepadAim;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Reload => m_Wrapper.m_Player_Reload;
        public InputAction @PauseGame => m_Wrapper.m_Player_PauseGame;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Shoot.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @GamepadAim.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGamepadAim;
                @GamepadAim.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGamepadAim;
                @GamepadAim.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGamepadAim;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Reload.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @PauseGame.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPauseGame;
                @PauseGame.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPauseGame;
                @PauseGame.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPauseGame;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @GamepadAim.started += instance.OnGamepadAim;
                @GamepadAim.performed += instance.OnGamepadAim;
                @GamepadAim.canceled += instance.OnGamepadAim;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @PauseGame.started += instance.OnPauseGame;
                @PauseGame.performed += instance.OnPauseGame;
                @PauseGame.canceled += instance.OnPauseGame;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_MouseAndKeyboardSchemeIndex = -1;
    public InputControlScheme MouseAndKeyboardScheme
    {
        get
        {
            if (m_MouseAndKeyboardSchemeIndex == -1) m_MouseAndKeyboardSchemeIndex = asset.FindControlSchemeIndex("Mouse And Keyboard");
            return asset.controlSchemes[m_MouseAndKeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnShoot(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnGamepadAim(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnPauseGame(InputAction.CallbackContext context);
    }
}
