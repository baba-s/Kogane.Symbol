using Kogane.Internal;
using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Kogane
{
	/// <summary>
	/// UniSymbolSettings の Inspector の表示を変更するエディタ拡張
	/// </summary>
	[CustomEditor( typeof( UniSymbolSettings ) )]
	public sealed class UniSymbolSettingsInspector : Editor
	{
		//==============================================================================
		// 変数
		//==============================================================================
		private SerializedProperty m_property;
		private ReorderableList    m_reorderableList;

		//==============================================================================
		// デリゲート(static)
		//==============================================================================
		public static Action<UniSymbolSettings> OnHeaderGUI { get; set; }
		public static Action<UniSymbolSettings> OnFooterGUI { get; set; }

		//==============================================================================
		// 関数
		//==============================================================================
		/// <summary>
		/// 有効になった時に呼び出されます
		/// </summary>
		private void OnEnable()
		{
			m_property = serializedObject.FindProperty( "m_list" );

			m_reorderableList = new ReorderableList( serializedObject, m_property )
			{
				onAddCallback       = OnAdd,
				onChangedCallback   = OnChanged,
				drawElementCallback = OnDrawElement,
			};
		}

		/// <summary>
		/// 無効になった時に呼び出されます
		/// </summary>
		private void OnDisable()
		{
			m_property        = null;
			m_reorderableList = null;
		}

		/// <summary>
		/// 要素を追加する時に呼び出されます
		/// </summary>
		private void OnAdd( ReorderableList list )
		{
			var index = m_property.arraySize;

			m_property.InsertArrayElementAtIndex( index );

			var property        = m_property.GetArrayElementAtIndex( index );
			var nameProperty    = property.FindPropertyRelative( "m_name" );
			var commentProperty = property.FindPropertyRelative( "m_comment" );
			var colorProperty   = property.FindPropertyRelative( "m_color" );

			nameProperty.stringValue    = string.Empty;
			commentProperty.stringValue = string.Empty;
			colorProperty.colorValue    = Color.white;
		}

		/// <summary>
		/// 要素が変更された時に呼び出されます
		/// </summary>
		private void OnChanged( ReorderableList list )
		{
			UniSymbolWindow.IsUpdate = true;
		}

		/// <summary>
		/// リストの要素を描画する時に呼び出されます
		/// </summary>
		private void OnDrawElement
		(
			Rect rect,
			int  index,
			bool isActive,
			bool isFocused
		)
		{
			var element = m_property.GetArrayElementAtIndex( index );
			rect.height -= 4;
			rect.y      += 2;
			EditorGUI.PropertyField( rect, element );
		}

		/// <summary>
		/// GUI を表示する時に呼び出されます
		/// </summary>
		public override void OnInspectorGUI()
		{
			var settings = ( UniSymbolSettings ) target;

			OnHeaderGUI?.Invoke( settings );

			if ( GUILayout.Button( "Open UniSymbol" ) )
			{
				UniSymbolWindow.Open();
			}

			serializedObject.Update();
			m_reorderableList.DoLayoutList();
			
			OnFooterGUI?.Invoke( settings );

			serializedObject.ApplyModifiedProperties();
		}
	}
}