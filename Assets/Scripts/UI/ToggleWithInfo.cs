using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
    /// <summary>
    /// A standard ToggleWithInfo that has an on / off state.
    /// </summary>
    /// <remarks>
    /// The ToggleWithInfo component is a Selectable that controls a child graphic which displays the on / off state.
    /// When a ToggleWithInfo event occurs a callback is sent to any registered listeners of UI.ToggleWithInfo._onValueChanged.
    /// </remarks>
    [AddComponentMenu("UI/ToggleWithInfo", 31)]
    [RequireComponent(typeof(RectTransform))]
    public class ToggleWithInfo : Selectable, IPointerClickHandler, ISubmitHandler, ICanvasElement
    {

        public bool hovering;

        /// <summary>
        /// Display settings for when a ToggleWithInfo is activated or deactivated.
        /// </summary>
        public enum ToggleWithInfoTransition
        {
            /// <summary>
            /// Show / hide the ToggleWithInfo instantly
            /// </summary>
            None,

            /// <summary>
            /// Fade the ToggleWithInfo in / out smoothly.
            /// </summary>
            Fade
        }

        [Serializable]
        /// <summary>
        /// UnityEvent callback for when a ToggleWithInfo is ToggleWithInfod.
        /// </summary>
        public class ToggleWithInfoEvent : UnityEvent<bool>
        { }

        /// <summary>
        /// Transition mode for the ToggleWithInfo.
        /// </summary>
        public ToggleWithInfoTransition toggleWithInfoTransition = ToggleWithInfoTransition.Fade;

        /// <summary>
        /// Graphic the ToggleWithInfo should be working with.
        /// </summary>
        public Graphic graphic;


        /// <summary>
        /// Group the ToggleWithInfo belongs to.
        /// </summary>


        /// <summary>
        /// Allow for delegate-based subscriptions for faster events than 'eventReceiver', and allowing for multiple receivers.
        /// </summary>
        /// <example>
        /// <code>
        /// //Attach this script to a ToggleWithInfo GameObject. To do this, go to Create>UI>ToggleWithInfo.
        /// //Set your own Text in the Inspector window
        ///
        /// using UnityEngine;
        /// using UnityEngine.UI;
        ///
        /// public class Example : MonoBehaviour
        /// {
        ///     ToggleWithInfo m_ToggleWithInfo;
        ///     public Text m_Text;
        ///
        ///     void Start()
        ///     {
        ///         //Fetch the ToggleWithInfo GameObject
        ///         m_ToggleWithInfo = GetComponent<ToggleWithInfo>();
        ///         //Add listener for when the state of the ToggleWithInfo changes, to take action
        ///         m_ToggleWithInfo.onValueChanged.AddListener(delegate {
        ///                 ToggleWithInfoValueChanged(m_ToggleWithInfo);
        ///             });
        ///
        ///         //Initialise the Text to say the first state of the ToggleWithInfo
        ///         m_Text.text = "First Value : " + m_ToggleWithInfo.isOn;
        ///     }
        ///
        ///     //Output the new state of the ToggleWithInfo into Text
        ///     void ToggleWithInfoValueChanged(ToggleWithInfo change)
        ///     {
        ///         m_Text.text =  "New Value : " + m_ToggleWithInfo.isOn;
        ///     }
        /// }
        /// </code>
        /// </example>
        public ToggleWithInfoEvent onValueChanged = new ToggleWithInfoEvent();

        // Whether the ToggleWithInfo is on
        [Tooltip("Is the ToggleWithInfo currently on or off?")]
        [SerializeField]
        private bool m_IsOn;

        protected ToggleWithInfo()
        { }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();

            if (!UnityEditor.PrefabUtility.IsPartOfPrefabAsset(this) && !Application.isPlaying)
                CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
        }

#endif // if UNITY_EDITOR

        public virtual void Rebuild(CanvasUpdate executing)
        {
#if UNITY_EDITOR
            if (executing == CanvasUpdate.Prelayout)
                onValueChanged.Invoke(m_IsOn);
#endif
        }

        public virtual void LayoutComplete()
        { }

        public virtual void GraphicUpdateComplete()
        { }

        protected override void OnDestroy()
        {
            //if (m_Group != null)
            //    m_Group.EnsureValidState();
            base.OnDestroy();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            //SetToggleWithInfoGroup(m_Group, false);
            PlayEffect(true);
        }

        protected override void OnDisable()
        {
            //SetToggleWithInfoGroup(null, false);
            base.OnDisable();
        }

        protected override void OnDidApplyAnimationProperties()
        {
            // Check if isOn has been changed by the animation.
            // Unfortunately there is no way to check if we don?t have a graphic.
            if (graphic != null)
            {
                bool oldValue = !Mathf.Approximately(graphic.canvasRenderer.GetColor().a, 0);
                if (m_IsOn != oldValue)
                {
                    m_IsOn = oldValue;
                    Set(!oldValue);
                }
            }

            base.OnDidApplyAnimationProperties();
        }



        /// <summary>
        /// Whether the ToggleWithInfo is currently active.
        /// </summary>
        /// <example>
        /// <code>
        /// /Attach this script to a ToggleWithInfo GameObject. To do this, go to Create>UI>ToggleWithInfo.
        /// //Set your own Text in the Inspector window
        ///
        /// using UnityEngine;
        /// using UnityEngine.UI;
        ///
        /// public class Example : MonoBehaviour
        /// {
        ///     ToggleWithInfo m_ToggleWithInfo;
        ///     public Text m_Text;
        ///
        ///     void Start()
        ///     {
        ///         //Fetch the ToggleWithInfo GameObject
        ///         m_ToggleWithInfo = GetComponent<ToggleWithInfo>();
        ///         //Add listener for when the state of the ToggleWithInfo changes, and output the state
        ///         m_ToggleWithInfo.onValueChanged.AddListener(delegate {
        ///                 ToggleWithInfoValueChanged(m_ToggleWithInfo);
        ///             });
        ///
        ///         //Initialize the Text to say whether the ToggleWithInfo is in a positive or negative state
        ///         m_Text.text = "ToggleWithInfo is : " + m_ToggleWithInfo.isOn;
        ///     }
        ///
        ///     //Output the new state of the ToggleWithInfo into Text when the user uses the ToggleWithInfo
        ///     void ToggleWithInfoValueChanged(ToggleWithInfo change)
        ///     {
        ///         m_Text.text =  "ToggleWithInfo is : " + m_ToggleWithInfo.isOn;
        ///     }
        /// }
        /// </code>
        /// </example>

        public bool isOn
        {
            get { return m_IsOn; }

            set
            {
                Set(value);
            }
        }

        /// <summary>
        /// Set isOn without invoking onValueChanged callback.
        /// </summary>
        /// <param name="value">New Value for isOn.</param>
        public void SetIsOnWithoutNotify(bool value)
        {
            Set(value, false);
        }

        void Set(bool value, bool sendCallback = true)
        {
            if (m_IsOn == value)
                return;

            // if we are in a group and set to true, do group logic
            m_IsOn = value;
            /*if (m_Group != null && m_Group.isActiveAndEnabled && IsActive())
            {
                if (m_IsOn || (!m_Group.AnyToggleWithInfosOn() && !m_Group.allowSwitchOff))
                {
                    m_IsOn = true;
                    m_Group.NotifyToggleWithInfoOn(this, sendCallback);
                }
            }*/

            // Always send event when ToggleWithInfo is clicked, even if value didn't change
            // due to already active ToggleWithInfo in a ToggleWithInfo group being clicked.
            // Controls like Dropdown rely on this.
            // It's up to the user to ignore a selection being set to the same value it already was, if desired.
            PlayEffect(toggleWithInfoTransition == ToggleWithInfoTransition.None);
            if (sendCallback)
            {
                UISystemProfilerApi.AddMarker("ToggleWithInfo.value", this);
                onValueChanged.Invoke(m_IsOn);
            }
        }

        /// <summary>
        /// Play the appropriate effect.
        /// </summary>
        private void PlayEffect(bool instant)
        {
            if (graphic == null)
                return;

#if UNITY_EDITOR
            if (!Application.isPlaying)
                graphic.canvasRenderer.SetAlpha(m_IsOn ? 1f : 0f);
            else
#endif
            graphic.CrossFadeAlpha(m_IsOn ? 1f : 0f, instant ? 0f : 0.1f, true);
        }

        /// <summary>
        /// Assume the correct visual state.
        /// </summary>
        protected override void Start()
        {
            PlayEffect(true);
        }

        private void InternalToggleWithInfo()
        {
            if (!IsActive() || !IsInteractable())
                return;

            isOn = !isOn;
        }

        /// <summary>
        /// React to clicks.
        /// </summary>
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            InternalToggleWithInfo();
        }

        public virtual void OnSubmit(BaseEventData eventData)
        {
            InternalToggleWithInfo();
        }

        private void Update()
        {
            if (IsHighlighted() == true)
            {
                hovering = true;
            }
            else
            {
                hovering = false;
            }
        }
    }
}