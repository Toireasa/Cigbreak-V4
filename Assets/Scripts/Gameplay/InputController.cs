using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CigBreak
{
	/// <summary>
	/// In game input controller
	/// Responsible for classifying input and showing on screen trail
	/// Support for touch and mouse
	/// </summary>
	[RequireComponent (typeof(LineRenderer))]
	public class InputController : MonoBehaviour
	{
		// References to other objects and components
		private Camera inputCamera = null;
		private LineRenderer lineRenderer = null;
		private InputMessanger inputMessanger = null;

		// Points in the trail
		private List<LineVertex> lineVertices = new List<LineVertex> ();

		// Editor customisable settings
		[SerializeField]
		private float tapErrorLimit = 0.1f;
		[SerializeField]
		private float trackedDistance = 4f;
		[SerializeField]
		private float trackedTime = 1f;

		// Input classification
		public enum InputType
		{
			None,
			Swipe,
			Tap

		}

		void Start ()
		{
			// Cache references to other objects and components
			inputCamera = GetComponentInParent<Camera> ();
			lineRenderer = GetComponent<LineRenderer> ();
			inputMessanger = GetComponentInChildren<InputMessanger> ();
			inputMessanger.Initialise (QualifyInput);
			inputMessanger.Disable ();
		}


		void Update ()
		{
			if (Input.touchCount > 0) {
				// Only process input from the first finger touching the screen
				Touch touch = Input.GetTouch (0);

				switch (touch.phase) {
				case TouchPhase.Began:
					// Set cuttent finger position as the input start point
					//lineRenderer.SetVertexCount(1);
					lineRenderer.positionCount = 1;
					AddNewLineVertex (touch.position);
                    // Enable input messanger component and set it's position
					inputMessanger.Enable ();
					inputMessanger.SetPosition (lineVertices [lineVertices.Count - 1].Position);
					break;

				case TouchPhase.Moved:
                    // Add new poins during movement
					//lineRenderer.SetVertexCount (lineVertices.Count + 1);
					lineRenderer.positionCount = lineVertices.Count + 1;
					AddNewLineVertex (touch.position);
					TrunkateLine ();
					inputMessanger.SetPosition (lineVertices [lineVertices.Count - 1].Position);
					break;

				case TouchPhase.Stationary:
                        // Fade out line
					TrunkateLine ();
					break;

				case TouchPhase.Ended:
                        // Clean up when input ends
					inputMessanger.Disable ();
					//lineRenderer.SetVertexCount (0);
					lineRenderer.positionCount = 0;
					lineVertices.Clear ();
					break;
				}
			}
#if UNITY_EDITOR
            // In editor process mouse input
            else {
				if (Input.GetMouseButtonDown (0)) {
					//lineRenderer.SetVertexCount (1);
					lineRenderer.positionCount = 1;
					AddNewLineVertex (Input.mousePosition);
					inputMessanger.Enable ();
					inputMessanger.SetPosition (lineVertices [lineVertices.Count - 1].Position);
				} else if (Input.GetMouseButton (0)) {
					//lineRenderer.SetVertexCount (lineVertices.Count + 1);
					lineRenderer.positionCount = lineVertices.Count + 1;
					AddNewLineVertex (Input.mousePosition);
					TrunkateLine ();
					inputMessanger.SetPosition (lineVertices [lineVertices.Count - 1].Position);
				} else if (Input.GetMouseButtonUp (0)) {
					inputMessanger.Disable ();
					//lineRenderer.SetVertexCount (0);
					lineRenderer.positionCount = 0;
					lineVertices.Clear ();
				}
			}
#endif

		}

		/// <summary>
		/// Adds new point to the input trail 
		/// </summary>
		/// <param name="inputPosition">input position (screen space)</param>
		private void AddNewLineVertex (Vector3 inputPosition)
		{
			Vector3 position = inputCamera.ScreenToWorldPoint (new Vector3 (inputPosition.x, inputPosition.y, CigBreakConstants.Values.SceneDistance - 1f));
			lineRenderer.SetPosition (lineVertices.Count, position);
			lineVertices.Add (new LineVertex (position));
		}

		private float CalculateLineLength ()
		{
			float distance = 0f;
			for (int i = 1; i < lineVertices.Count; i++) {
				distance += Vector3.Distance (lineVertices [i - 1].Position, lineVertices [i].Position);
			}

			return distance;
		}

		/// <summary>
		/// "Fades out" the line based on the specifiec length and time restrictions
		/// </summary>
		private void TrunkateLine ()
		{
			// Cannot truncate the line if it only has 2 points
			if (lineVertices.Count == 2) {
				return;
			}

			// Truncate the line by replacing the old positions with more recent ones and cutting off the excess
			if (CalculateLineLength () >= trackedDistance || lineVertices [0].LifeTime >= trackedTime) {
				List<LineVertex> remainingVertices = new List<LineVertex> ();
				remainingVertices.Add (lineVertices [lineVertices.Count - 1]);
				remainingVertices.Add (lineVertices [lineVertices.Count - 2]);

				float currentDistance = 0f;
				for (int i = lineVertices.Count - 3; i >= 0; i--) {
					currentDistance += Vector3.Distance (lineVertices [i].Position, lineVertices [i + 1].Position);
					if (currentDistance <= trackedDistance && lineVertices [i].LifeTime <= trackedTime) {
						remainingVertices.Add (lineVertices [i]);
					} else {
						break;
					}
				}

				//lineRenderer.SetVertexCount (remainingVertices.Count);
				lineRenderer.positionCount = remainingVertices.Count;
				lineVertices.Clear ();
				for (int i = remainingVertices.Count - 1; i >= 0; i--) {
					lineRenderer.SetPosition (lineVertices.Count, remainingVertices [i].Position);
					lineVertices.Add (remainingVertices [i]);
				}
			}
		}

		/// <summary>
		/// Based on the input displacement classify input as swipe or tap
		/// </summary>
		/// <returns>type of input</returns>
		private InputType QualifyInput ()
		{
			float length = CalculateLineLength ();
			if (length <= tapErrorLimit) {
				return InputType.Tap;
			} else {
				return InputType.Swipe;
			}
		}

		/// <summary>
		/// Defines a point in the trail line, which is defined by position and its creation time
		/// </summary>
		private class LineVertex
		{
			private Vector3 position;

			public Vector3 Position { get { return position; } }

			private float creationTime;

			public float CreationTime { get { return creationTime; } }

			public float LifeTime { get { return Time.time - creationTime; } }

			public LineVertex ()
			{
				position = Vector3.zero;
				creationTime = Time.time;
			}

			public LineVertex (Vector3 position)
			{
				this.position = position;
				creationTime = Time.time;
			}
		}
	}
}
