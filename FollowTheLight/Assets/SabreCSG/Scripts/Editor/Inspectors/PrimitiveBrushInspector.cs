using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace Sabresaurus.SabreCSG
{
	[CanEditMultipleObjects]
    [CustomEditor(typeof(PrimitiveBrush))]
    public class PrimitiveBrushInspector : Editor
    {
		float rescaleValue = 1f;

		Mesh sourceMesh = null;

		SerializedProperty brushTypeProp;

		void OnEnable () 
		{
			// Setup the SerializedProperties.
			brushTypeProp = serializedObject.FindProperty ("brushType");
		}

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

//			string[] values = Enum.GetNames(typeof(PrimitiveBrushType));
//
//			int existingIndex = (int)((PrimitiveBrush)target).BrushType;
//
//			foreach (var thisBrush in targets) 
//			{
//				PrimitiveBrush brush = (PrimitiveBrush)thisBrush;
//				int brushTypeIndex = (int)brush.BrushType;
//
//				if(brushTypeIndex != existingIndex)
//				{
//					existingIndex = -1;
//				}
//			}
//
//			int newIndex = EditorGUILayout.Popup(existingIndex, values);
//			if(newIndex != existingIndex)
//			{
//				brushType = (PrimitiveBrushType)newIndex;
//			}

			EditorGUILayout.PropertyField(brushTypeProp);

			if (GUILayout.Button("Reset Polygons"))
			{
				foreach (var thisBrush in targets) 
				{
					((PrimitiveBrush)thisBrush).ResetPolygons();
					((PrimitiveBrush)thisBrush).Invalidate();
				}
			}

			if (GUILayout.Button("Recalculate Normals"))
			{
				foreach (var thisBrush in targets) 
				{
					((PrimitiveBrush)thisBrush).RecalculateNormals();
				}
			}

			GUILayout.BeginHorizontal();
			rescaleValue = EditorGUILayout.FloatField(rescaleValue);
			
			if(GUILayout.Button("Rescale"))
			{
				if(rescaleValue != 0)
				{
					foreach (var thisBrush in targets) 
					{
						((PrimitiveBrush)thisBrush).Rescale(rescaleValue);
					}
				}
			}

			
			if (GUILayout.Button("Reset Pivot"))
			{
				foreach (var thisBrush in targets) 
				{
					((PrimitiveBrush)thisBrush).ResetPivot();
				}
			}

			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			sourceMesh = EditorGUILayout.ObjectField(sourceMesh, typeof(Mesh), false) as Mesh;

			if (GUILayout.Button("Import"))
			{
				if(sourceMesh != null)
				{
					Polygon[] polygons = PolygonFactory.GeneratePolygonsFromMesh(sourceMesh).ToArray();
					bool convex = PolygonFactory.IsMeshConvex(polygons);
					if(!convex)
					{
						Debug.LogError("Concavities detected in imported mesh. This may result in issues during CSG, please change the source geometry so that it is convex");
					}
					foreach (var thisBrush in targets) 
					{
						((PrimitiveBrush)thisBrush).SetPolygons(polygons, true);
					}
				}
			}

			GUILayout.EndHorizontal();
//
//			if(sourceMesh != null)
//			{
//				ModelImporter modelImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(sourceMesh)) as ModelImporter;
//				modelImporter.keepQuads
//			}

			if(targets.Length == 1)
			{
	            PrimitiveBrush thisBrush = ((PrimitiveBrush)target);
//	            CSGModel csgModel = thisBrush.GetCSGModel();

				if (GUILayout.Button("Send To Back"))
				{
					thisBrush.transform.SetAsFirstSibling();
				}

				if (GUILayout.Button("Send To Front"))
				{
					thisBrush.transform.SetAsLastSibling();
				}
				
				if (GUILayout.Button("Send Backward"))
				{
					int siblingIndex = thisBrush.transform.GetSiblingIndex();
					siblingIndex--;
					thisBrush.transform.SetSiblingIndex(siblingIndex);
				}

				if (GUILayout.Button("Send Forward"))
				{
					int siblingIndex = thisBrush.transform.GetSiblingIndex();
					siblingIndex++;
					thisBrush.transform.SetSiblingIndex(siblingIndex);
				}
			}
			else
			{
				GUILayout.Box("Additional options not supported in multi-select");
			}

			serializedObject.ApplyModifiedProperties ();

//            GUILayout.Label("UVs", EditorStyles.boldLabel);
//
//            if (GUILayout.Button("Flip XY"))
//            {
//                UVUtility.FlipUVsXY(thisBrush.Polygons);
//            }
//
//            GUILayout.BeginHorizontal();
//            if (GUILayout.Button("Flip X"))
//            {
//                UVUtility.FlipUVsX(thisBrush.Polygons);
//            }
//            if (GUILayout.Button("Flip Y"))
//            {
//                UVUtility.FlipUVsY(thisBrush.Polygons);
//            }
//            GUILayout.EndHorizontal();
//
//            GUILayout.BeginHorizontal();
//            if (GUILayout.Button("UVs x 2"))
//            {
//                UVUtility.ScaleUVs(thisBrush.Polygons, 2f);
//            }
//            if (GUILayout.Button("UVs / 2"))
//            {
//                UVUtility.ScaleUVs(thisBrush.Polygons, .5f);
//            }
//            GUILayout.EndHorizontal();
            // Ensure Edit Mode is on
//            csgModel.EditMode = true;
        }
    }
}