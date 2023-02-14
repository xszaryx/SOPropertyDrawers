using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace DangerField.Variables
{
    [CustomPropertyDrawer(typeof(IntReference))]
    public class IntReferenceDrawer : PropertyDrawer
    {
        //*
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            var valueGroup = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;

            var useConstant = property.FindPropertyRelative("UseConstant");
            var constantValue = property.FindPropertyRelative("ConstantValue");
            var variable = property.FindPropertyRelative("Variable");
            var testDsc = property.FindPropertyRelative("testDsc");

            Debug.Log("IntReference Drawer testDec: " + testDsc.stringValue);

            var label = new Label(property.displayName);
            label.AddToClassList("unity-text-element");
            label.AddToClassList("unity-base-field");
            label.AddToClassList("unity-base-field__label");
            container.Add(label);

            // add a button
            var button = new Button();
            button.AddToClassList("pane-button");
            container.Add(button);

            //*
            button.clicked += () =>
            {
                useConstant.boolValue = false;

                // create the menu and add items to it
                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent("Use Constant"), false, () =>
                {
                    valueGroup.Clear();
                    useConstant.boolValue = true;
                    var constantValueField = new IntegerField();
                    constantValueField.BindProperty(constantValue);
                    constantValueField.AddToClassList("unity-base-text-field__input");
                    constantValueField.AddToClassList("unity-base-text-field__input--single-line");
                    constantValueField.AddToClassList("unity-base-field__input");
                    constantValueField.AddToClassList("unity-integer-field__input");
                    valueGroup.Add(constantValueField);
                    useConstant.serializedObject.ApplyModifiedProperties();

                });
                menu.AddItem(new GUIContent("Use Variable"), false, () =>
                {
                    valueGroup.Clear();
                    useConstant.boolValue = false;
                    var variableField = new ObjectField();
                    variableField.objectType = typeof(IntVariable);
                    variableField.BindProperty(variable);
                    variableField.AddToClassList("unity-base-field__input");
                    variableField.AddToClassList("unity-object-field__input");
                    valueGroup.Add(variableField);
                    useConstant.serializedObject.ApplyModifiedProperties();
                });
                
                // display the menu
                menu.DropDown(button.worldBound);
            };
            //button.AddToClassList("unity-text-element");
            //button.AddToClassList("unity-button");
           
            if (useConstant.boolValue)
            {
                var constantValueField = new IntegerField();
                constantValueField.BindProperty(constantValue);
                constantValueField.AddToClassList("unity-base-text-field__input");
                constantValueField.AddToClassList("unity-base-text-field__input--single-line");
                constantValueField.AddToClassList("unity-base-field__input");
                constantValueField.AddToClassList("unity-integer-field__input");
                valueGroup.Add(constantValueField);
            }
            else
            {
                var variableField = new ObjectField();
                variableField.objectType = typeof(IntVariable);
                variableField.AddToClassList("unity-base-field__input");
                variableField.AddToClassList("unity-object-field__input");
                variableField.BindProperty(variable);
                valueGroup.Add(variableField);
            }

            container.Add(valueGroup);/**/
            

            return container;
        }
    }
}
