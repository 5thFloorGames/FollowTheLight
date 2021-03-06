#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Sabresaurus.SabreCSG
{
	public static class SabreGraphics
	{
		// Screen position right at the front (note can't use 1, because even though OSX accepts it Windows doesn't)
		public const float FRONT_Z_DEPTH = 0.99f;

	    private static Material marqueeBorderMaterial = null;
	    private static Material marqueeFillMaterial = null;
	    private static Material selectedBrushMaterial = null;
		private static Material selectedBrushDashedMaterial = null;
	    private static Material gizmoMaterial = null;
//	    private static Material gizmoSelectedMaterial = null;
	    private static Material vertexMaterial = null;
	    private static Material planeMaterial = null;
	    private static Material previewMaterial = null;

		private static Texture2D addIconTexture = null;
		private static Texture2D subtractIconTexture = null;
		private static Texture2D dialogOverlayTexture = null;

		private static Texture2D clearTexture = null;
		private static Texture2D halfWhiteTexture = null;
		private static Texture2D halfBlackTexture = null;

		public static Texture2D AddIconTexture 
		{
			get 
			{
				if(addIconTexture == null)
				{
					addIconTexture = UnityEditor.AssetDatabase.LoadMainAssetAtPath(CSGModel.GetSabreCSGPath() + "Gizmos/Add.png") as Texture2D;
				}
				return addIconTexture;
			}
		}

		public static Texture2D SubtractIconTexture 
		{
			get 
			{
				if(subtractIconTexture == null)
				{
					subtractIconTexture = UnityEditor.AssetDatabase.LoadMainAssetAtPath(CSGModel.GetSabreCSGPath() + "Gizmos/Subtract.png") as Texture2D;
				}
				return subtractIconTexture;
			}
		}

		public static Texture2D DialogOverlayTexture 
		{
			get 
			{
				if(dialogOverlayTexture == null)
				{
					dialogOverlayTexture = UnityEditor.AssetDatabase.LoadMainAssetAtPath(CSGModel.GetSabreCSGPath() + "Gizmos/DialogOverlay75.png") as Texture2D;
				}
				return dialogOverlayTexture;
			}
		}

		public static Texture2D ClearTexture
		{
			get
			{
				if(clearTexture == null)
				{
					clearTexture = new Texture2D(2,2, TextureFormat.RGBA32, false);
					for (int x = 0; x < clearTexture.width; x++) 
					{
						for (int y = 0; y < clearTexture.height; y++) 
						{
							clearTexture.SetPixel(x,y,Color.clear);
						}	
					}
					clearTexture.Apply();
				}
				return clearTexture;
			}
		}

		public static Texture2D HalfWhiteTexture
		{
			get
			{
				if(halfWhiteTexture == null)
				{
					halfWhiteTexture = new Texture2D(2,2, TextureFormat.RGBA32, false);
					for (int x = 0; x < halfWhiteTexture.width; x++) 
					{
						for (int y = 0; y < halfWhiteTexture.height; y++) 
						{
							halfWhiteTexture.SetPixel(x,y,new Color(1,1,1,0.5f));
						}	
					}
					halfWhiteTexture.Apply();
				}
				return halfWhiteTexture;
			}
		}

		public static Texture2D HalfBlackTexture
		{
			get
			{
				if(halfBlackTexture == null)
				{
					halfBlackTexture = new Texture2D(2,2, TextureFormat.RGBA32, false);
					for (int x = 0; x < halfBlackTexture.width; x++) 
					{
						for (int y = 0; y < halfBlackTexture.height; y++) 
						{
							halfBlackTexture.SetPixel(x,y,new Color(0,0,0,0.5f));
						}	
					}
					halfBlackTexture.Apply();
				}
				return halfBlackTexture;
			}
		}

	    public static Material GetMarqueeBorderMaterial()
	    {
	        if (marqueeBorderMaterial == null)
	        {
	            marqueeBorderMaterial = new Material(Shader.Find("Transparent/Diffuse"));
	        }
	        return marqueeBorderMaterial;
	    }

	    public static Material GetMarqueeFillMaterial()
	    {
	        if (marqueeFillMaterial == null)
	        {
	            marqueeFillMaterial = new Material(Shader.Find("Transparent/Diffuse"));
	        }
	        return marqueeFillMaterial;
	    }

	    public static Material GetSelectedBrushMaterial()
	    {
	        if (selectedBrushMaterial == null)
	        {
	            selectedBrushMaterial = new Material(Shader.Find("SabreCSG/Line"));
                selectedBrushMaterial.hideFlags = HideFlags.HideAndDontSave;
	            selectedBrushMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
	        }
	        return selectedBrushMaterial;
	    }

		public static Material GetSelectedBrushDashedMaterial()
		{
			if (selectedBrushDashedMaterial == null)
			{
				selectedBrushDashedMaterial = new Material(Shader.Find("SabreCSG/Line Dashed"));
				selectedBrushDashedMaterial.hideFlags = HideFlags.HideAndDontSave;
				selectedBrushDashedMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
			}
			return selectedBrushDashedMaterial;
		}

	    public static Material GetGizmoMaterial()
	    {
	        if (gizmoMaterial == null)
	        {
				Shader shader = Shader.Find("Particles/Alpha Blended");
				gizmoMaterial = new Material(shader);
	            gizmoMaterial.hideFlags = HideFlags.HideAndDontSave;
	            gizmoMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
				gizmoMaterial.mainTexture = UnityEditor.AssetDatabase.LoadMainAssetAtPath(CSGModel.GetSabreCSGPath() + "Gizmos/SquareGizmo8x8.png") as Texture;
	        }
	        return gizmoMaterial;
	    }

	    public static Material GetVertexMaterial()
	    {
	        if (vertexMaterial == null)
	        {
				Shader shader = Shader.Find("Particles/Alpha Blended");
				vertexMaterial = new Material(shader);
	            vertexMaterial.hideFlags = HideFlags.HideAndDontSave;
	            vertexMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
				vertexMaterial.mainTexture = UnityEditor.AssetDatabase.LoadMainAssetAtPath(CSGModel.GetSabreCSGPath() + "Gizmos/CircleGizmo8x8.png") as Texture;
	        }
	        return vertexMaterial;
	    }

	    public static Material GetPreviewMaterial()
	    {
	        //		if(previewMaterial == null)
	        {
	//			Shader shader = Shader.Find("Particles/Alpha Blended");
				Shader shader = Shader.Find("Unlit/Texture");
				previewMaterial = new Material(shader);
	//            previewMaterial = new Material("Shader \"Preview/Texture Colored Blended\" {\n" +
	//                                          " Properties {\n" +
	//                                          "_MainTex (\"Base (RGB)\", 2D) = \"white\" {}\n" +
	//                                          "\n}" +
	//                                           "SubShader { Tags { \"RenderType\" = \"Opaque\" } Pass { \n" +
	//                                          "    ZWrite Off Cull Off Fog { Mode Off } \n" +
	//                                          "    BindChannels {\n" +
	//                                          "      Bind \"vertex\", vertex Bind \"color\", color }\n" +
	//                                          "     SetTexture [_MainTex] { combine previous * texture } \n" +
	//                                          "} } }"
	//                                          );
	            previewMaterial.hideFlags = HideFlags.HideAndDontSave;
	            previewMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
	        }

	        return previewMaterial;
	    }

	    public static Material GetPlaneMaterial()
	    {
	        if (planeMaterial == null)
	        {
	            planeMaterial = new Material(Shader.Find("SabreCSG/Plane"));
	            planeMaterial.hideFlags = HideFlags.HideAndDontSave;
	            planeMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
	        }
	        return planeMaterial;
	    }

	    public static void DrawBillboardQuad(Vector3 screenPosition, int width, int height)
	    {
			screenPosition.z = FRONT_Z_DEPTH;

	        GL.TexCoord2(0, 0);
			GL.Vertex(screenPosition + new Vector3(-width / 2, -height / 2, 0));
	        GL.TexCoord2(1, 0);
			GL.Vertex(screenPosition + new Vector3(width / 2, -height / 2, 0));
	        GL.TexCoord2(1, 1);
			GL.Vertex(screenPosition + new Vector3(width / 2, height / 2, 0));
	        GL.TexCoord2(0, 1);
			GL.Vertex(screenPosition + new Vector3(-width / 2, height / 2, 0));
	    }

		public static void DrawScreenLine (Vector3 screenPosition1, Vector3 screenPosition2)
		{
			screenPosition1.z = FRONT_Z_DEPTH;
			GL.Vertex(screenPosition1);

			screenPosition2.z = FRONT_Z_DEPTH;
			GL.Vertex(screenPosition2);
		}

		public static void DrawScreenRectFill (Rect rect)
		{
			Vector3 topLeft = new Vector3(rect.xMin,rect.yMin, FRONT_Z_DEPTH);
			Vector3 topRight = new Vector3(rect.xMax, rect.yMin, FRONT_Z_DEPTH);
			Vector3 bottomLeft = new Vector3(rect.xMin, rect.yMax, FRONT_Z_DEPTH);
			Vector3 bottomRight = new Vector3(rect.xMax, rect.yMax, FRONT_Z_DEPTH);

			GL.Vertex(topLeft);
			GL.Vertex(topRight);
			GL.Vertex(bottomRight);
			GL.Vertex(bottomLeft);

			GL.Vertex(bottomLeft);
			GL.Vertex(bottomRight);
			GL.Vertex(topRight);
			GL.Vertex(topLeft);
		}

		public static void DrawScreenRectOuter (Rect rect)
		{
			Vector3 topLeft = new Vector3(rect.xMin,rect.yMin, FRONT_Z_DEPTH);
			Vector3 topRight = new Vector3(rect.xMax, rect.yMin, FRONT_Z_DEPTH);
			Vector3 bottomLeft = new Vector3(rect.xMin, rect.yMax, FRONT_Z_DEPTH);
			Vector3 bottomRight = new Vector3(rect.xMax, rect.yMax, FRONT_Z_DEPTH);
			
			GL.Vertex(topLeft);
			GL.Vertex(topRight);

			GL.Vertex(bottomLeft);
			GL.Vertex(bottomRight);

			GL.Vertex(topLeft);
			GL.Vertex(bottomLeft);

			GL.Vertex(bottomRight);
			GL.Vertex(topRight);
		}

		public static void DrawBox(Bounds bounds, Transform transform = null)
		{
			Vector3 center = bounds.center;

			// Calculate each of the transformed axis with their corresponding length
			Vector3 up = Vector3.up * bounds.extents.y;
			Vector3 right = Vector3.right * bounds.extents.x;
			Vector3 forward = Vector3.forward * bounds.extents.z;

			if(transform != null)
			{
				center = transform.TransformPoint(bounds.center);

				// Calculate each of the transformed axis with their corresponding length
				up = transform.TransformVector(Vector3.up) * bounds.extents.y;
			 	right = transform.TransformVector(Vector3.right) * bounds.extents.x;
				forward = transform.TransformVector(Vector3.forward) * bounds.extents.z;
			}

			// Verticals
			GL.Vertex(center - right - forward + up);
			GL.Vertex(center - right - forward - up);
			GL.Vertex(center - right + forward + up);
			GL.Vertex(center - right + forward - up);
			GL.Vertex(center + right - forward + up);
			GL.Vertex(center + right - forward - up);
			GL.Vertex(center + right + forward + up);
			GL.Vertex(center + right + forward - up);

			// Horizontal - forward/back
			GL.Vertex(center - right + forward - up);
			GL.Vertex(center - right - forward - up);
			GL.Vertex(center + right + forward - up);
			GL.Vertex(center + right - forward - up);
			GL.Vertex(center - right + forward + up);
			GL.Vertex(center - right - forward + up);
			GL.Vertex(center + right + forward + up);
			GL.Vertex(center + right - forward + up);

			// Horizontal - right/left
			GL.Vertex(center + right - forward - up);
			GL.Vertex(center - right - forward - up);
			GL.Vertex(center + right + forward - up);
			GL.Vertex(center - right + forward - up);
			GL.Vertex(center + right - forward + up);
			GL.Vertex(center - right - forward + up);
			GL.Vertex(center + right + forward + up);
			GL.Vertex(center - right + forward + up);
		}

	    public static void DrawPlane(UnityEngine.Plane plane, Vector3 center, Color colorFront, Color colorBack, float size)
	    {
	        SabreGraphics.GetPlaneMaterial().SetPass(0);

	        GL.Begin(GL.QUADS);

	        Vector3 normal = plane.normal.normalized;
	        Vector3 tangent;

	        if (normal == Vector3.up || normal == Vector3.down)
	        {
	            tangent = Vector3.Cross(normal, Vector3.forward).normalized;
	        }
	        else
	        {
	            tangent = Vector3.Cross(normal, Vector3.up).normalized;
	        }

	        Vector3 binormal = Quaternion.AngleAxis(90, normal) * tangent;

	        //		GL.Color(colorFront);
	        //		GL.Vertex(center + (normal * -plane.distance) - tangent * size - binormal * size);
	        //		GL.Vertex(center + (normal * -plane.distance) + tangent * size - binormal * size);
	        //		GL.Vertex(center + (normal * -plane.distance) + tangent * size + binormal * size);
	        //		GL.Vertex(center + (normal * -plane.distance) - tangent * size + binormal * size);
	        //
	        //		GL.Color(colorBack);
	        //		GL.Vertex(center + (normal * -plane.distance) - tangent * size + binormal * size);
	        //		GL.Vertex(center + (normal * -plane.distance) + tangent * size + binormal * size);
	        //		GL.Vertex(center + (normal * -plane.distance) + tangent * size - binormal * size);
	        //		GL.Vertex(center + (normal * -plane.distance) - tangent * size - binormal * size);

	        GL.Color(colorFront);
	        GL.Vertex(center - tangent * size - binormal * size);
	        GL.Vertex(center + tangent * size - binormal * size);
	        GL.Vertex(center + tangent * size + binormal * size);
	        GL.Vertex(center - tangent * size + binormal * size);

	        GL.Color(colorBack);
	        GL.Vertex(center - tangent * size + binormal * size);
	        GL.Vertex(center + tangent * size + binormal * size);
	        GL.Vertex(center + tangent * size - binormal * size);
	        GL.Vertex(center - tangent * size - binormal * size);

	        GL.End();


			GL.Begin(GL.LINES);

			GL.Color(Color.white);

			GL.Vertex(center - tangent * size + binormal * size);
			GL.Vertex(center + tangent * size + binormal * size);

			GL.Vertex(center + tangent * size + binormal * size);
			GL.Vertex(center + tangent * size - binormal * size);

			GL.Vertex(center + tangent * size - binormal * size);
			GL.Vertex(center - tangent * size - binormal * size);

			GL.Vertex(center - tangent * size - binormal * size);
			GL.Vertex(center - tangent * size + binormal * size);

			GL.Color(Color.green);

			Vector3 normalOffset = -normal * 0.01f;

			GL.Vertex(center + normalOffset - tangent * size + binormal * size);
			GL.Vertex(center + normalOffset + tangent * size + binormal * size);
			
			GL.Vertex(center + normalOffset + tangent * size + binormal * size);
			GL.Vertex(center + normalOffset + tangent * size - binormal * size);
			
			GL.Vertex(center + normalOffset + tangent * size - binormal * size);
			GL.Vertex(center + normalOffset - tangent * size - binormal * size);
			
			GL.Vertex(center + normalOffset - tangent * size - binormal * size);
			GL.Vertex(center + normalOffset - tangent * size + binormal * size);

			GL.Color(Color.red);

			normalOffset = normal * 0.01f;
			
			GL.Vertex(center + normalOffset - tangent * size + binormal * size);
			GL.Vertex(center + normalOffset + tangent * size + binormal * size);
			
			GL.Vertex(center + normalOffset + tangent * size + binormal * size);
			GL.Vertex(center + normalOffset + tangent * size - binormal * size);
			
			GL.Vertex(center + normalOffset + tangent * size - binormal * size);
			GL.Vertex(center + normalOffset - tangent * size - binormal * size);
			
			GL.Vertex(center + normalOffset - tangent * size - binormal * size);
			GL.Vertex(center + normalOffset - tangent * size + binormal * size);
			
			GL.End();
	    }

		public static void DrawRotationCircle(Vector3 worldCenter, Vector3 normal, float radius, Vector3 initialRotationDirection)
		{
			Vector3 tangent;
			
			if (normal == Vector3.up || normal == Vector3.down)
			{
				tangent = Vector3.Cross(normal, Vector3.forward).normalized;
			}
			else
			{
				tangent = Vector3.Cross(normal, Vector3.up).normalized;
			}
			
			Vector3 binormal = Quaternion.AngleAxis(90, normal) * tangent;

			// Scale the tangent and binormal by the radius
			binormal *= radius;
			tangent *= radius;

			int count = 30;
			float deltaAngle = (2f * Mathf.PI) / count;
			
			GL.Begin(GL.TRIANGLES);
			GL.Color(new Color(1,0,1,0.3f));
			         
	        for (int i = 0; i < count; i++) 
	        {
				GL.Vertex(worldCenter);
				GL.Vertex(worldCenter + tangent * Mathf.Sin(i * deltaAngle) + binormal * Mathf.Cos(i * deltaAngle));
				GL.Vertex(worldCenter + tangent * Mathf.Sin((i+1) * deltaAngle) + binormal * Mathf.Cos((i+1) * deltaAngle));
			}
			GL.End();


			GL.Begin(GL.LINES);
			GL.Color(Color.magenta);

			for (int i = 0; i < count; i++) 
			{
				GL.Vertex(worldCenter + tangent * Mathf.Sin(i * deltaAngle) + binormal * Mathf.Cos(i * deltaAngle));
				GL.Vertex(worldCenter + tangent * Mathf.Sin((i+1) * deltaAngle) + binormal * Mathf.Cos((i+1) * deltaAngle));
			}
			GL.End();

			if(CurrentSettings.AngleSnappingEnabled)
			{
				Quaternion cancellingRotation = Quaternion.Inverse(Quaternion.LookRotation(normal));
				Vector3 planarInitialDirection = cancellingRotation * initialRotationDirection;
				float angleOffset = Mathf.Atan2(planarInitialDirection.x,planarInitialDirection.y);

				float angleSnapDistance = CurrentSettings.AngleSnapDistance;

				count = (int)(360f / angleSnapDistance);
				deltaAngle = (2f * Mathf.PI) / count;

				bool divisorOf90 = ((90 % angleSnapDistance) == 0);

				GL.Begin(GL.LINES);
				GL.Color(Color.white);
				
				for (int i = 0; i < count; i++) 
				{
					float totalDeltaAngleDeg = i * angleSnapDistance;
					GL.Vertex(worldCenter + tangent * Mathf.Sin(angleOffset + i * deltaAngle) + binormal * Mathf.Cos(angleOffset + i * deltaAngle));
					if(divisorOf90 && (totalDeltaAngleDeg % 90) == 0)
					{
						GL.Vertex(worldCenter + 0.7f * tangent * Mathf.Sin(angleOffset + i * deltaAngle) + 0.7f * binormal * Mathf.Cos(angleOffset + i * deltaAngle));
					}
					else
					{
						GL.Vertex(worldCenter + 0.9f * tangent * Mathf.Sin(angleOffset + i * deltaAngle) + 0.9f * binormal * Mathf.Cos(angleOffset + i * deltaAngle));
					}
				}
				GL.End();
				
				// Draw a line showing the initial rotation angle, so the user can compare their current angle to it
				GL.Begin(GL.LINES);
				GL.Color(Color.yellow);
				
				GL.Vertex(worldCenter);
				GL.Vertex(worldCenter + initialRotationDirection);
				
				GL.End();
			}
		}

		public static void DrawPolygons(Color color, Transform transform, params Polygon[] polygons)
		{
			GL.Begin(GL.LINES);
			GL.Color(color);
			
			for (int j = 0; j < polygons.Length; j++) 
			{
				Polygon polygon = polygons[j];
				for (int i = 0; i < polygon.Vertices.Length - 1; i++)
				{
					GL.Vertex(transform.TransformPoint(polygon.Vertices[i].Position));
					GL.Vertex(transform.TransformPoint(polygon.Vertices[i + 1].Position));
				}
				GL.Vertex(transform.TransformPoint(polygon.Vertices[polygon.Vertices.Length - 1].Position));
				GL.Vertex(transform.TransformPoint(polygon.Vertices[0].Position));
			}
			
			GL.End();
			
			GL.Begin(GL.TRIANGLES);
			color.a = 0.3f;
			GL.Color(color);
			
			for (int j = 0; j < polygons.Length; j++) 
			{
				Polygon polygon = polygons[j];
				Vector3 position1 = polygon.Vertices[0].Position;
				
				for (int i = 1; i < polygon.Vertices.Length - 1; i++)
				{
					GL.Vertex(transform.TransformPoint(position1));
					GL.Vertex(transform.TransformPoint(polygon.Vertices[i].Position));
					GL.Vertex(transform.TransformPoint(polygon.Vertices[i + 1].Position));
				}
			}
			GL.End();
		}

		public static void DrawPolygons(Color color, params Polygon[] polygons)
		{
			GL.Begin(GL.LINES);
			GL.Color(color);
			
			for (int j = 0; j < polygons.Length; j++) 
			{
				Polygon polygon = polygons[j];
				for (int i = 0; i < polygon.Vertices.Length - 1; i++)
				{
					GL.Vertex(polygon.Vertices[i].Position);
					GL.Vertex(polygon.Vertices[i + 1].Position);
				}
				GL.Vertex(polygon.Vertices[polygon.Vertices.Length - 1].Position);
				GL.Vertex(polygon.Vertices[0].Position);
			}
			
			GL.End();
			
			GL.Begin(GL.TRIANGLES);
			color.a *= 0.3f;
			GL.Color(color);
			
			for (int j = 0; j < polygons.Length; j++) 
			{
				Polygon polygon = polygons[j];
				Vector3 position1 = polygon.Vertices[0].Position;
				
				for (int i = 1; i < polygon.Vertices.Length - 1; i++)
				{
					GL.Vertex(position1);
					GL.Vertex(polygon.Vertices[i].Position);
					GL.Vertex(polygon.Vertices[i + 1].Position);
				}
			}
			GL.End();
		}

		public static void DrawPolygonsOutline(Color color, params Polygon[] polygons)
		{
			Vector3 depthAdjust = -0.01f * SceneView.currentDrawingSceneView.camera.transform.forward;
			GL.Begin(GL.LINES);

			for (int j = 0; j < polygons.Length; j++) 
			{
				Polygon polygon = polygons[j];

				GL.Color(color);

				for (int i = 0; i < polygon.Vertices.Length; i++)
				{
					Vector3 currentPosition = polygon.Vertices[i].Position + depthAdjust;
					Vector3 nextPosition = polygon.Vertices[(i + 1)%polygon.Vertices.Length].Position + depthAdjust;

					GL.Vertex(currentPosition);
					GL.Vertex(nextPosition);
				}
			}

			GL.End();
		}

		public static void DrawPolygonsOutlineDashed(Color color, params Polygon[] polygons)
		{
			Vector3 depthAdjust = -0.01f * SceneView.currentDrawingSceneView.camera.transform.forward;
			GL.Begin(GL.LINES);

			for (int j = 0; j < polygons.Length; j++) 
			{
				Polygon polygon = polygons[j];

				GL.Color(color);

				for (int i = 0; i < polygon.Vertices.Length; i++)
				{
					Vector3 currentPosition = polygon.Vertices[i].Position + depthAdjust;
					Vector3 nextPosition = polygon.Vertices[(i + 1)%polygon.Vertices.Length].Position + depthAdjust;

					GL.TexCoord2(0,0);
					GL.Vertex(currentPosition);
					GL.TexCoord2(Vector3.Distance(nextPosition,currentPosition),0);
					GL.Vertex(nextPosition);
				}
			}

			GL.End();
		}

//		public static void DrawThickLineTest(Vector3 testPoint1, Vector3 testPoint2, float width)
//	    {
//			Camera sceneViewCamera = UnityEditor.SceneView.currentDrawingSceneView.camera;
//    		
////    		Vector3 screenPoint1 = sceneViewCamera.WorldToScreenPoint(testPoint1);
////    		Vector3 screenPoint2 = sceneViewCamera.WorldToScreenPoint(testPoint2);
//    		
////    		Vector3 perpendicular = new Vector3(screenPoint2.x - screenPoint1.x, screenPoint1.y - screenPoint2.y, screenPoint1.z).normalized;
//    		
////			Vector3 up = sceneViewCamera.transform.up;
//			Vector3 forward = sceneViewCamera.transform.forward * 0.01f;
//
//			Vector3 cameraVector = sceneViewCamera.transform.position - (testPoint1 + testPoint2)/2f;
//			Vector3 lineVector = testPoint2 - testPoint1;
//
//			Vector3 up = Vector3.Cross(cameraVector.normalized, lineVector.normalized);
//
////			Vector3 up = Quaternion.LookRotation(sceneViewCamera.transform.forward, sceneViewCamera.transform.up) * Vector3.up;
//
//			GL.Color(Color.black);
//			GL.Vertex(testPoint1 + up * width - forward);
//			GL.Vertex(testPoint1 - up * width - forward);
//			GL.Vertex(testPoint2 - up * width - forward);
//			GL.Vertex(testPoint2 + up * width - forward);
//
//			GL.Color(Color.green);
//			GL.Vertex(testPoint1 + up * width/2 - forward);
//			GL.Vertex(testPoint1 - up * width/2 - forward);
//			GL.Vertex(testPoint2 - up * width/2 - forward);
//			GL.Vertex(testPoint2 + up * width/2 - forward);
//	    }
	}
}
#endif