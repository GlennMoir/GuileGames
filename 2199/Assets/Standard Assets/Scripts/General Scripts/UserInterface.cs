
using UnityEngine;
using System.Collections; 

public class UserInterface : MonoBehaviour {
		
	//Used for setting the current TimeScale when starting or unpausing game.
	private float savedTimeScale;

	private double lastTime = 0; 

	//Not 100% sure what these are used for.
	private float[] fpsarray;
	private float fps;
		 
	public int lowFPS = 30;
	public int highFPS = 50;

	//Used for Options Menu	
	private int toolbarInt = 0;
	private string[]  toolbarstrings =  {"Audio","Video","Game"};

	
	//Used for the Options/Settings page.
 	private int settingsInt = 0;
	private string[]  settingsStrings={"Audio","Video","Game"};

	//An enumerator page type that is set to the individual menu pages.
	private enum Page {
	    None, Main, Play, DeckBuildSelect, DeckBuildFaction, DeckBuildUnits, DeckBuildTactics, Options
	}
	private Page currentPage; 

	public GUIStyle TextStyle = new GUIStyle();
	public GUIStyle ImageStyle = new GUIStyle();
	public GUISkin menuSkins;

	//Fonts used within the game.
	public Font NexaLight;
	public Font PacLibertas;
	public Font OldEnglish;

	//--------MENU TEXTURES AND VARIABLES---------//

	//The background textures for the menus
	public Texture2D Main_Menu_BG;
	public Texture2D Main_Menu_BG2;
	public Texture2D Deck_Build_Select;
	public Texture2D Deck_Build_Faction;
	public Texture2D Deck_Build_Units;
	public Texture2D Deck_Build_Tactics;
	public Texture2D Options_Menu_PHolder;
	public Texture2D Play_Menu_BG;

	private Texture2D currentTexture;
	private int textureCounter = 0;

	//Additional Textures
	public Texture2D Back_icon;
	public Texture2D Guile_Games_Logo;
	public Texture2D Ninety_Nine_Logo;
	//-------- PLAY TEXTURES AND VARIABLES ---------//


	public Texture2D RankedGame;
	public Texture2D CasualGame;

	//-------- DECK BUILD TEXTURES AND VARIABLES---------//

	//Deck Build Textures
	public Texture2D Empty_Deck;
	public Texture2D Faction_Russia;
	public Texture2D Faction_America;
	public Texture2D Faction_China;

	public Texture2D SUnit1;
	public Texture2D SUnit2;
	public Texture2D SUnit3;
	public Texture2D SUnit4;
	public Texture2D SUnit5;
	public Texture2D SUnit6;
	public Texture2D SUnitNonExist;
	public Texture2D NonSelected;

	private const int MAX_SELECTED_UNITS = 6;	
	private const int MAX_SELECTED_CARDS = 10;	
	private Texture2D[] SelectedUnit = new Texture2D[MAX_SELECTED_UNITS];
	private Texture2D[] SelectedCard = new Texture2D[MAX_SELECTED_CARDS];

	public Texture2D FuryCard;
	public Texture2D PushForwardCard;
	public Texture2D CardBack;

	public Texture2D EmptyTab;
	public Texture2D FuryTab;
	public Texture2D CardBackTab;
	public Texture2D PushForwardTab;


	//Variables used for fade animation
	//private float alpha = 1f;
	//private bool fading = false;
	//private float stayTime = 4f; //Time before fading starts.
	//private float fadeTime = 1f; //Time it takes to fade.
	//private float lastFadeTime = 0; 

			
	// Use this for initialization
	void Start () {
		fpsarray = new float[Screen.width];
	    Time.timeScale = 1;
		TextStyle.font = NexaLight;
		currentTexture = Main_Menu_BG;
		PauseGame();

		for (int i = 0; i < MAX_SELECTED_UNITS; i++){
			SelectedUnit[i] = NonSelected;
		}

		for (int i = 0; i < MAX_SELECTED_CARDS; i++){
			SelectedCard[i] = EmptyTab;
		}


	}

			
	// Update is called once per frame
	void Update () {
	}

	//Sets up the GUI.
	void OnGUI(){

		if (Time.realtimeSinceStartup - lastTime > 5 && currentPage == Page.Main){
			changeMainMenuTexture();
			lastTime = Time.realtimeSinceStartup;
		}

		if (menuSkins != null) {
	        GUI.skin = menuSkins;
	    }

		if (IsGamePaused()) {
			switch (currentPage) {
			case Page.Main: ShowMainMenu(); break;
			case Page.Play: ShowPlay(); break;
			case Page.DeckBuildSelect: ShowDeckSelect(); break;
			case Page.DeckBuildFaction: ShowDeckFaction(); break;
			case Page.DeckBuildUnits: ShowDeckUnits(); break;
			case Page.DeckBuildTactics: ShowDeckTactics(); break;
			case Page.Options: ShowOptionsTemp(); break;
			}
		}  

	

	}

	//Creates a new GUI area for text and textures.
	void BeginPage(int width, int height) {
		GUILayout.BeginArea(new Rect(Mathf.Round((Screen.width - width) / 2), 
		                             Mathf.Round((Screen.height - height) / 2), width, height));
	}

	//Ends the GUI area and shows the backbutton if not the main menu page.
	void EndPage() {
		GUILayout.EndArea();
		if (currentPage != Page.Main) {
			ShowBackButton();
		}
	}
	
	void changeMainMenuTexture(){
		textureCounter++;
		if (textureCounter == 1){
			currentTexture = Main_Menu_BG2;
		} else if (textureCounter == 2){
			currentTexture = Main_Menu_BG;
			textureCounter = 0;
		}
	}

	void setSelectedUnit(Texture2D texture){
		for (int i = 0; i < MAX_SELECTED_UNITS; i++){
			if (SelectedUnit[i] == NonSelected){
				SelectedUnit[i] = texture;
				break;
			} else {
			}
		}
	}

	void setSelectedCard(Texture2D texture){
		for (int i = 0; i < MAX_SELECTED_CARDS; i++){
			if (SelectedCard[i] == EmptyTab){
				SelectedCard[i] = texture;
				break;
			} else {
			}
		}
	}

	void collapseCardTab(){
		for (int i = 0; i < MAX_SELECTED_CARDS-1; i++){
			if (SelectedCard[i] == EmptyTab){
				for (int j = i+1; j < MAX_SELECTED_CARDS; j++){
					if(SelectedCard[j] != EmptyTab){
						SelectedCard[i] = SelectedCard[j];
						SelectedCard[j] = EmptyTab;
						break;
					}
				}
			}
		}
	}

//	void fadeAnimation(float fadeTime, bool fadeBool){
//		if (fading) return;
//		fading = true;
//		float t = 0f;
//		while (t < 1.0){
//			t += Time.deltaTime/fadeTime;
//			alpha = Mathf.Clamp01(fadeBool? t:1-t);
//			break;
//		}
//		fading = false;
//	}

	//Shows the Main Menu and sets up corresponding TextStyles.




	void ShowMainMenu() {

		GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), currentTexture);
		GUI.DrawTexture(new Rect((int)(Screen.width/25.6),(int)(Screen.height/37.92592), (int)(Screen.width/4.24896), (int)(Screen.height/4.89171)), Ninety_Nine_Logo);
		GUI.DrawTexture(new Rect((int)(Screen.width/1.07619),(int)(Screen.height/1.13190), (int)(Screen.width/17.0677896), (int)(Screen.height/9.97402)), Guile_Games_Logo);

		TextStyle.font = NexaLight;
		TextStyle.normal.textColor = new Color((238f/255f),(229f/255f) ,(170f/255f), 1f);
		TextStyle.fontSize = (int)(Screen.width/10.24);	
		TextStyle.padding.top = (int)(Screen.height/25.6);


		GUILayout.BeginArea(new Rect((int)(Screen.width/30.72),(int)(Screen.height/3.5),Screen.width,Screen.height));

			if (GUILayout.Button("PLAY", TextStyle)) {
				currentPage = Page.Play;
			} else if  (GUILayout.Button ("BUILD DECK", TextStyle)) {
				currentPage = Page.DeckBuildSelect;
			} else if (GUILayout.Button ("OPTIONS", TextStyle)) {
				currentPage = Page.Options;
			} else if (GUILayout.Button ("QUIT", TextStyle)) {
				Application.Quit();
			}
		EndPage();
	}

	//Shows the Play Menu
	void ShowPlay(){
		GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), Play_Menu_BG);
		GUILayout.BeginArea(new Rect(0,0,Screen.width,Screen.height));

		if (GUI.Button(new Rect((int)(Screen.width/18.395),(int)(Screen.height/4.52430),(int)(Screen.width/2.38973),(int)(Screen.height/1.79229)), CasualGame, ImageStyle)){
			Application.LoadLevel("Board Layout");
		} else if (GUI.Button (new Rect((int)(Screen.width/1.89688),(int)(Screen.height/4.52430),(int)(Screen.width/2.38973),(int)(Screen.height/1.79229)), RankedGame, ImageStyle)){
			Application.LoadLevel("BoardRotation ver2");
		} 

		EndPage();
	}

	//Shows the Deck Select Menu
	void ShowDeckSelect(){
		GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), Deck_Build_Select);
		GUILayout.BeginArea(new Rect(0,0,2048,1536));

		//IF TEXTURE ISNT EMPTY DECK, GO TO BUILDUNITS, OTHERWISE GO TO DECK BUILD
		
		if (GUI.Button(new Rect((int)(Screen.width/24.38), (int)(Screen.height/13.71), (int)(Screen.width/3.59), (int)(Screen.height/2.69)), Faction_Russia, ImageStyle)){
			currentPage = Page.DeckBuildUnits;
		} else if (GUI.Button(new Rect((int)(Screen.width/2.78), (int)(Screen.height/13.71), (int)(Screen.width/3.59), (int)(Screen.height/2.69)), Faction_America, ImageStyle)){
			currentPage = Page.DeckBuildUnits;
		} else if (GUI.Button(new Rect((int)(Screen.width/1.48), (int)(Screen.height/13.71), (int)(Screen.width/3.59), (int)(Screen.height/2.69)), Faction_China, ImageStyle)){
			currentPage = Page.DeckBuildUnits;
		} else if (GUI.Button(new Rect((int)(Screen.width/24.38),(int)(Screen.height/1.84), (int)(Screen.width/3.59), (int)(Screen.height/2.69)), Empty_Deck, ImageStyle)){
			currentPage = Page.DeckBuildFaction;
		} else if (GUI.Button(new Rect((int)(Screen.width/2.78), (int)(Screen.height/1.84), (int)(Screen.width/3.59),  (int)(Screen.height/2.69)), Empty_Deck, ImageStyle)){
			currentPage = Page.DeckBuildFaction;
		} else if (GUI.Button(new Rect((int)(Screen.width/1.48), (int)(Screen.height/1.84),  (int)(Screen.width/3.59),  (int)(Screen.height/2.69)), Empty_Deck, ImageStyle)){
			currentPage = Page.DeckBuildFaction;
		}

		EndPage();
	}

	//Shows the Deck Faction Select
	void ShowDeckFaction(){
		GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), Deck_Build_Faction);
		GUILayout.BeginArea(new Rect(0,0,2048,1536));

		if (GUI.Button(new Rect((int)(Screen.width/46.5),(int)(Screen.height/3.38),(int)(Screen.width/3.27),(int)(Screen.height/2.45)), Faction_Russia, ImageStyle)){
			currentPage = Page.DeckBuildUnits;
		} else if (GUI.Button (new Rect((int)(Screen.width/2.88),(int)(Screen.height/3.38),(int)(Screen.width/3.27),(int)(Screen.height/2.45)), Faction_America, ImageStyle)){
			currentPage = Page.DeckBuildUnits;
		} else if (GUI.Button (new Rect((int)(Screen.width/1.49),(int)(Screen.height/3.38),(int)(Screen.width/3.27),(int)(Screen.height/2.45)), Faction_China, ImageStyle)){
			currentPage = Page.DeckBuildUnits;
		}

		EndPage();
	}

	//Shows the Deck Unit Select
	void ShowDeckUnits(){
		GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), Deck_Build_Units);
		GUILayout.BeginArea(new Rect(0,0,2048,1536));

		TextStyle.font = PacLibertas;
		TextStyle.fontSize = (int)(Screen.width/20);
		TextStyle.normal.textColor = new Color(0,0,0, 1f);

		if (GUI.Button(new Rect((int)(Screen.width/9.8), (int)(Screen.height/21.33333), (int)(Screen.width/8.5333), (int)(Screen.height/24.38095)),"UNITS", TextStyle)){
			currentPage = Page.DeckBuildTactics;
		} else if (GUI.Button(new Rect((int)(Screen.width/2.90), (int)(Screen.height/21.33333), (int)(Screen.width/5.68999), (int)(Screen.height/21.94285)),"TACTICS", TextStyle)) {
			currentPage = Page.DeckBuildTactics;
		}


		//Selected Unit Card Buttons
		if (GUI.Button(new Rect((int)(Screen.width/5.81), (int)(Screen.height/7.11), (int)(Screen.width/5.30), (int)(Screen.height/3.95)), SelectedUnit[0], ImageStyle)){
			SelectedUnit[0] = NonSelected;
		} else if (GUI.Button(new Rect((int)(Screen.width/2.46), (int)(Screen.height/7.11), (int)(Screen.width/5.30), (int)(Screen.height/3.95)), SelectedUnit[1], ImageStyle)){
			SelectedUnit[1] = NonSelected;
		}else if (GUI.Button(new Rect((int)(Screen.width/1.56), (int)(Screen.height/7.11), (int)(Screen.width/5.30), (int)(Screen.height/3.95)), SelectedUnit[2], ImageStyle)){
			SelectedUnit[2] = NonSelected;
		}else if (GUI.Button(new Rect((int)(Screen.width/5.81), (int)(Screen.height/2.21), (int)(Screen.width/5.30), (int)(Screen.height/3.95)), SelectedUnit[3], ImageStyle)){
			SelectedUnit[3] = NonSelected;
		}else if (GUI.Button(new Rect((int)(Screen.width/2.46), (int)(Screen.height/2.21), (int)(Screen.width/5.30), (int)(Screen.height/3.95)), SelectedUnit[4], ImageStyle)){
			SelectedUnit[4] = NonSelected;
		}else if (GUI.Button(new Rect((int)(Screen.width/1.56), (int)(Screen.height/2.21), (int)(Screen.width/5.30), (int)(Screen.height/3.95)), SelectedUnit[5], ImageStyle)){
			SelectedUnit[5] = NonSelected;
		}

		//Unit Scrollbar Card Buttons
		if (GUI.Button(new Rect((int)(Screen.width/24.97), (int)(Screen.height/1.27), (int)(Screen.width/8.22), (int)(Screen.height/6.16)), SUnit1, ImageStyle)){
			setSelectedUnit(SUnit1);
		} else if (GUI.Button(new Rect((int)(Screen.width/5.75), (int)(Screen.height/1.27), (int)(Screen.width/8.22), (int)(Screen.height/6.16)), SUnit2, ImageStyle)){
			setSelectedUnit(SUnit2);
		} else if (GUI.Button(new Rect((int)(Screen.width/3.25), (int)(Screen.height/1.27), (int)(Screen.width/8.22), (int)(Screen.height/6.16)), SUnit3, ImageStyle)){
			setSelectedUnit(SUnit3);
		} else if (GUI.Button(new Rect((int)(Screen.width/2.27), (int)(Screen.height/1.27), (int)(Screen.width/8.22), (int)(Screen.height/6.16)), SUnit4, ImageStyle)){
			setSelectedUnit(SUnit4);
		} else if (GUI.Button(new Rect((int)(Screen.width/1.74), (int)(Screen.height/1.27), (int)(Screen.width/8.22), (int)(Screen.height/6.16)), SUnit5, ImageStyle)){
			setSelectedUnit(SUnit5);
		} else if (GUI.Button(new Rect((int)(Screen.width/1.41), (int)(Screen.height/1.27), (int)(Screen.width/8.22), (int)(Screen.height/6.16)), SUnit6, ImageStyle)){
			setSelectedUnit(SUnit6);
		} else if (GUI.Button(new Rect((int)(Screen.width/1.19), (int)(Screen.height/1.27), (int)(Screen.width/8.22), (int)(Screen.height/6.16)), SUnitNonExist, ImageStyle)){
			setSelectedUnit(SUnitNonExist);
		}

		EndPage();
	}

	//Shows the Deck Tactic Select
	void ShowDeckTactics(){
		GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), Deck_Build_Tactics);
		GUILayout.BeginArea(new Rect(0,0,2048,1536));

		TextStyle.font = PacLibertas;
		TextStyle.fontSize = (int)(Screen.width/20);
		TextStyle.normal.textColor = new Color(0,0,0,1f);

		if (GUI.Button(new Rect((int)(Screen.width/9.8), (int)(Screen.height/21.33333), (int)(Screen.width/8.5333), (int)(Screen.height/24.38095)),"UNITS",TextStyle)){
			currentPage = Page.DeckBuildUnits;
		}else if  (GUI.Button(new Rect((int)(Screen.width/2.90), (int)(Screen.height/21.33333), (int)(Screen.width/5.68999), (int)(Screen.height/21.94285)),"TACTICS", TextStyle)) {
			currentPage = Page.DeckBuildUnits;
		}

		//Shows the cards to be selected. 
		//THIS WILL BE CHANGED WHEN A DYNAMIC AREA IS ADDED
		if (GUI.Button(new Rect((int)(Screen.width/14.42), (int)(Screen.height/6.40), (int)(Screen.width/5.52), (int)(Screen.height/2.90)), FuryCard, ImageStyle)){
			setSelectedCard(FuryTab);
		} else if (GUI.Button(new Rect((int)(Screen.width/3.36), (int)(Screen.height/6.40), (int)(Screen.width/5.52), (int)(Screen.height/2.90)), PushForwardCard, ImageStyle)){
			setSelectedCard(PushForwardTab);
		} else if (GUI.Button(new Rect((int)(Screen.width/1.90), (int)(Screen.height/6.40), (int)(Screen.width/5.52), (int)(Screen.height/2.90)), CardBack, ImageStyle)){
			setSelectedCard(CardBackTab);
		} else if (GUI.Button(new Rect((int)(Screen.width/14.42), (int)(Screen.height/1.78), (int)(Screen.width/5.52), (int)(Screen.height/2.90)), CardBack, ImageStyle)){
			setSelectedCard(CardBackTab);
		} else if (GUI.Button(new Rect((int)(Screen.width/3.36), (int)(Screen.height/1.78), (int)(Screen.width/5.52), (int)(Screen.height/2.90)), CardBack, ImageStyle)){
			setSelectedCard(CardBackTab);
		} else if (GUI.Button(new Rect((int)(Screen.width/1.90), (int)(Screen.height/1.78), (int)(Screen.width/5.52), (int)(Screen.height/2.90)), CardBack, ImageStyle)){
			setSelectedCard(CardBackTab);
		}

		//Shows the selected card tabs.
		if (GUI.Button(new Rect((int)(Screen.width/1.313),(int)(Screen.height/10.24), (int)(Screen.width/4.54), (int)(Screen.height/11.42857)), SelectedCard[0], ImageStyle)){
			SelectedCard[0] = EmptyTab;
			collapseCardTab();
		} if (GUI.Button(new Rect((int)(Screen.width/1.313), (int)(Screen.height/5.40084), (int)(Screen.width/4.54), (int)(Screen.height/11.42857)), SelectedCard[1], ImageStyle)){
			SelectedCard[1] = EmptyTab;
			collapseCardTab();
		} if (GUI.Button(new Rect((int)(Screen.width/1.313), (int)(Screen.height/3.66762), (int)(Screen.width/4.54), (int)(Screen.height/11.42857)), SelectedCard[2], ImageStyle)){
			SelectedCard[2] = EmptyTab;
			collapseCardTab();
		} if (GUI.Button(new Rect((int)(Screen.width/1.313), (int)(Screen.height/2.77657), (int)(Screen.width/4.54), (int)(Screen.height/11.42857)), SelectedCard[3], ImageStyle)){
			SelectedCard[3] = EmptyTab;
			collapseCardTab();
		} if (GUI.Button(new Rect((int)(Screen.width/1.313), (int)(Screen.height/2.233856), (int)(Screen.width/4.54), (int)(Screen.height/11.42857)), SelectedCard[4], ImageStyle)){
			SelectedCard[4] = EmptyTab;
			collapseCardTab();
		} if (GUI.Button(new Rect((int)(Screen.width/1.313), (int)(Screen.height/1.87317), (int)(Screen.width/4.54), (int)(Screen.height/11.42857)), SelectedCard[5], ImageStyle)){
			SelectedCard[5] = EmptyTab;
			collapseCardTab();
		} if (GUI.Button(new Rect((int)(Screen.width/1.313), (int)(Screen.height/1.60938), (int)(Screen.width/4.54), (int)(Screen.height/11.42857)), SelectedCard[6], ImageStyle)){
			SelectedCard[6] = EmptyTab;
			collapseCardTab();
		} if (GUI.Button(new Rect((int)(Screen.width/1.313), (int)(Screen.height/1.41072), (int)(Screen.width/4.54), (int)(Screen.height/11.42857)), SelectedCard[7], ImageStyle)){
			SelectedCard[7] = EmptyTab;
			collapseCardTab();
		} if (GUI.Button(new Rect((int)(Screen.width/1.313), (int)(Screen.height/1.25572), (int)(Screen.width/4.54), (int)(Screen.height/11.42857)), SelectedCard[8], ImageStyle)){
			SelectedCard[8] = EmptyTab;
			collapseCardTab();
		} if (GUI.Button(new Rect((int)(Screen.width/1.313), (int)(Screen.height/1.13140), (int)(Screen.width/4.54), (int)(Screen.height/11.42857)), SelectedCard[9], ImageStyle)){
			SelectedCard[9] = EmptyTab;
			collapseCardTab();
		} 

		EndPage();
	}


	//Shows the Temporary Options Menu with no settings available.
	void ShowOptionsTemp(){
		GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), Options_Menu_PHolder);
		BeginPage (0, 0);
		EndPage();
	}

	//The Options Menu accessed from the main menu.
	void OptionsToolbar(){
		//get from main menu pause
	}

	//The Volume Settings Menu accessed from the Options Toolbar.
	void VolumeControl(){
		//get from main menu pause
	}
	    
	//The Video Settings Menu accessed from the Options Toolbar.
	void VideoSettings(){
	}
	//The Game Settings Menu accessed from the Options Toolbar.
	void GameSettings(){
	}
	
	//Pauses the game.
	void PauseGame() {
	    savedTimeScale = Time.timeScale;
		
	    Time.timeScale = 0;

		//Pauses in-game audio.
		//CHANGE THIS IF USING MENU MUSIC.
	    AudioListener.pause = true;
		
		//Sets the page to the main menu page.
	    currentPage = Page.Main;
	}

	//Shows the back button icon and makes it access the Main Menu.
	void ShowBackButton() {
		TextStyle.alignment = TextAnchor.MiddleLeft;
		TextStyle.padding.top = 0;
		TextStyle.padding.bottom = 0;
		if (GUI.Button(new Rect((int)(Screen.width/46.5), (int)(Screen.height/1.088), (int)(Screen.width/20.48), (int)(Screen.height/15.36)), Back_icon, TextStyle)) {
			currentPage = Page.Main;
		}
	}

	//Used for FPS - not quite sure why.
	void ScrollFPS() {
		for (int x = 1; x < fpsarray.Length; ++x) {
			fpsarray[x-1]=fpsarray[x];
		}
		if (fps < 1000) {
			fpsarray[fpsarray.Length - 1]=fps;
		}
	}

	//Used for FPS - not quite sure why.
	void FPSUpdate() {
		float delta = Time.smoothDeltaTime;
		if (!IsGamePaused() && delta !=0.0) {
			fps = 1 / delta;
		}
	}

	//Used to check if the game is paused.
	bool IsGamePaused() {
		return (Time.timeScale == 0);
	}

	//Halts the audio if the game is paused. This may need to be changed.
	void OnApplicationPause(bool pause) {
		if (IsGamePaused()) {
			AudioListener.pause = true;
		}
	}

}
