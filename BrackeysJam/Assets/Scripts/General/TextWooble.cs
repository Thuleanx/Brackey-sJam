using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextWooble : MonoBehaviour
{
	const int numVerticesPerLetter = 4;

	[SerializeField] float timeScale = 2f;
	[SerializeField] float xScale = .1f;
	[SerializeField] float amplitudeScale = .5f;

	TMP_Text textComponent;

	void Awake() {
		textComponent = GetComponent<TextMeshProUGUI>();
	}

	void Update() {
		textComponent.ForceMeshUpdate();

		var textInfo = textComponent.textInfo;

		for (int i = 0; i < textInfo.characterCount; i++) {
			var charInfo = textInfo.characterInfo[i];
			
			if (charInfo.isVisible) {
				var vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

				for (int j = 0; j < numVerticesPerLetter; j++)
				{
					var origin = vertices[charInfo.vertexIndex + j];

					vertices[charInfo.vertexIndex + j] = origin + new Vector3(0, Mathf.Sin(Time.time * timeScale + origin.x * xScale) * amplitudeScale, 0);
				}
			}
		}

		for (int i = 0; i < textInfo.meshInfo.Length; i++) {
			var meshInfo = textInfo.meshInfo[i];
			meshInfo.mesh.vertices = meshInfo.vertices;

			textComponent.UpdateGeometry(meshInfo.mesh, i);
		}
	}
}
