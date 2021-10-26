using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    public static class ReorderableArrayField
    {
        public static VisualElement CreateReorderableArrayField(SerializedProperty serializedProperty)
        {
            Assert.IsTrue(serializedProperty.isArray);
            var container = new VisualElement
            {
                style =
                {
                    marginTop = new StyleLength(1),
                    marginBottom = new StyleLength(1),
                    marginLeft = new StyleLength(3)
                }
            };
            container.Add(new Label(serializedProperty.displayName)
            {
                style =
                {
                    paddingTop = new StyleLength(2),
                    paddingRight = new StyleLength(2),
                    paddingLeft = new StyleLength(0f),
                    unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft)
                }
            });
            var list = new VisualElement
            {
                style = { marginTop = new StyleLength(5) }
            };
            container.Add(list);

            var arraySizeProperty = serializedProperty.FindPropertyRelative("Array.size");
            var arraySizeField = new IntegerField
            {
                bindingPath = arraySizeProperty.propertyPath,
                style = { display = new StyleEnum<DisplayStyle>(DisplayStyle.None) }
            };
            arraySizeField.RegisterValueChangedCallback(e => { Refresh(); });
            container.Add(arraySizeField);

            void AddArrayElement()
            {
                serializedProperty.arraySize += 1;
                serializedProperty.serializedObject.ApplyModifiedProperties();
            }

            void DeleteArrayElementAt(int i)
            {
                if (serializedProperty.GetArrayElementAtIndex(i) == null)
                {
                    return;
                }
                serializedProperty.DeleteArrayElementAtIndex(i);
                serializedProperty.serializedObject.ApplyModifiedProperties();
            }

            void MoveArrayElement(int srcIndex, int dstIndex)
            {
                if (serializedProperty.GetArrayElementAtIndex(srcIndex) == null)
                {
                    return;
                }
                serializedProperty.MoveArrayElement(srcIndex, dstIndex);
                serializedProperty.serializedObject.ApplyModifiedProperties();
            }

            VisualElement CreateCell(int i)
            {
                var listItem = new VisualElement
                {
                    style =
                    {
                        flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row),
                        borderTopWidth = new StyleFloat(1),
                        borderTopColor = new StyleColor(Color.gray),
                        paddingTop = new StyleLength(5),
                        paddingBottom = new StyleLength(5)
                    }
                };

                var field = new PropertyField();
                var element = serializedProperty.GetArrayElementAtIndex(i);
                field.BindProperty(element);
                field.style.flexGrow = 1;
                listItem.Add(field);

                var listItemMenu = new VisualElement
                {
                    style =
                    {
                        borderLeftWidth = new StyleFloat(1),
                        borderLeftColor = new StyleColor(Color.gray)
                    }
                };
                listItem.Add(listItemMenu);
                var moveUpButton = new Button(() => MoveArrayElement(i, i - 1))
                {
                    text = "▲",
                    tooltip = "Move Up"
                };
                moveUpButton.SetEnabled(i > 0);
                listItemMenu.Add(moveUpButton);

                var removeButton = new Button(() => DeleteArrayElementAt(i))
                {
                    text = "-",
                    tooltip = "Delete"
                };
                listItemMenu.Add(removeButton);

                var moveDownButton = new Button(() => MoveArrayElement(i, i + 1))
                {
                    text = "▼",
                    tooltip = "Move Down"
                };
                moveDownButton.SetEnabled(i < serializedProperty.arraySize - 1);
                listItemMenu.Add(moveDownButton);

                return listItem;
            }

            void Refresh()
            {
                list.Unbind();
                list.Clear();
                for (var i = 0; i < serializedProperty.arraySize; i++)
                {
                    list.Add(CreateCell(i));
                }

                var addButton = new Button(AddArrayElement)
                {
                    text = "+",
                    tooltip = "Add"
                };
                list.Add(addButton);
            }

            Refresh();

            return container;
        }
    }
}
