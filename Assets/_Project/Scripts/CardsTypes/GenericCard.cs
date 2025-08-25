using UnityEngine;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

namespace cg
{
    [CreateAssetMenu(menuName = "CardGame/Generic Card")]
    public class GenericCard : BaseCard
    {
        /// <summary>
        /// Is necessary to do CardsBehaviours.Behaviours just to simplify custom editor
        /// </summary>
        public GenericCard_CardsBehaviours CardsBehaviours;
    }

    /// <summary>
    /// This struct is created just to simplify custom editor 
    /// (don't use base.OnInspectorGUI to avoid show double Behaviours list, because HideInInspector for some reason break the custom editor)
    /// </summary>
    [System.Serializable]
    public class GenericCard_CardsBehaviours
    {
        // Use [SerializeReference] to serialize polymorphic types
        [SerializeReference] public List<BaseCardBehaviour> Behaviours;
    }

    #region editor
#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(GenericCard_CardsBehaviours))]
    public class GenericCardCardsBehavioursDrawer : PropertyDrawer
    {
        private ReorderableList reorderableList;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // The property is the "CardsBehaviours" instance.
            // Find the list property relative to it.
            SerializedProperty behaviorsProperty = property.FindPropertyRelative("Behaviours");

            // Check if the ReorderableList has been initialized
            if (reorderableList == null)
            {
                InitializeReorderableList(behaviorsProperty);
            }

            // Draw the ReorderableList
            reorderableList.DoLayoutList();
        }

        private void InitializeReorderableList(SerializedProperty listProperty)
        {
            // Initialize the ReorderableList
            reorderableList = new ReorderableList(
                listProperty.serializedObject,
                listProperty,
                draggable: true,
                displayHeader: true,
                displayAddButton: true,
                displayRemoveButton: true
            );

            // Customize the header drawing
            reorderableList.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "Card Behaviors");
            };

            // Customize how each element is drawn in the list
            reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);

                //show sequence THEN with more indent level
                var sequenceType = (ECardBehaviourSequenceType)element.FindPropertyRelative("SequenceType").enumValueIndex;
                if (sequenceType == ECardBehaviourSequenceType.Then)
                    EditorGUI.indentLevel += 2;

                EditorGUI.PropertyField(rect, element, new GUIContent(element.managedReferenceFullTypename.Split('.').Last()), true);
            };

            // Set the height for each element
            reorderableList.elementHeightCallback = (int index) =>
            {
                var element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
                return EditorGUI.GetPropertyHeight(element, true) + EditorGUIUtility.standardVerticalSpacing;
            };

            // Customize the add button menu
            reorderableList.onAddDropdownCallback = (Rect buttonRect, ReorderableList list) =>
            {
                var menu = new GenericMenu();
                var types = System.AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => p.IsSubclassOf(typeof(BaseCardBehaviour)) && !p.IsAbstract);

                foreach (var type in types)
                {
                    menu.AddItem(new GUIContent(type.Name), false, () => {
                        var newBehavior = System.Activator.CreateInstance(type);
                        list.serializedProperty.arraySize++;
                        list.serializedProperty.GetArrayElementAtIndex(list.serializedProperty.arraySize - 1).managedReferenceValue = newBehavior;
                        list.serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                }
                menu.ShowAsContext();
            };
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // You must return the total height of the ReorderableList here.
            // Unfortunately, this is a limitation of PropertyDrawers and ReorderableList.
            // It is hard to calculate the height correctly without drawing it first.
            // This is why a CustomEditor is generally better for this kind of complex UI.
            // A common workaround is to return a hardcoded value or a rough estimate.
            // However, the best practice is to use a CustomEditor for the parent class (GenericCard).

            return 0; // Return 0 to let the ReorderableList handle its own height.
        }
    }

#endif
    #endregion
}