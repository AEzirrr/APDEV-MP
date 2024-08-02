using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHudScreen : View {

	void Start () {
		
	}

	public void OnMainMenuClicked() {
		LoadManager.Instance.LoadScene (SceneNames.MAIN_SCENE);
	}

}
